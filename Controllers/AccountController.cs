using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FYP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Web.Mvc;
using ClosedXML;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Data.OleDb;
using System.Net;
using System.Web.Helpers;

namespace FYP.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext _dbContext;

        public AccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private Login curUser()
        {
            Login cUser = HttpContext.Session.GetObject<Login>("associate_lecturer");
            return cUser;
        }
        [HttpGet]
        public IActionResult Authenticate()
        {

            ViewData["layout"] = "_Layout";

            return View("_Login");
        }
        [HttpPost]
        public IActionResult Authenticate(Login login)
        {
            if (curUser() == null)
            {
                string sql = @"SELECT * FROM associate_lecturer WHERE al_email = '{0}' AND al_password = HASHBYTES('SHA1', '{1}')";
                var result = DBUtl.GetList(sql, login.UserId, login.al_password);
                if (result.Count > 0)
                {
                    dynamic user = result[0];
                    login.al_name = user.al_name;
                    login.al_password = null;
                    login.al_id = user.al_id;
                    login.type = user.type;
                    HttpContext.Session.SetObject("associate_lecturer", login);
                    return View("home");
                }
                ViewData["layout"] = "_Layout";
                ViewData["msg"] = "Login failed";
                return View("Index");
            }

            else
                return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult home()
        {
            return View("home");
        }
        //[HttpGet]
        //public IActionResult InvTimeslot()
        //{
        //    return View("InvTimeSlot");
        //}
        //[HttpPost]
        //public ActionResult InvTimeslot(HttpPostedFileBase upload)
        //{
        //    bool isImported = false;
        //    string sResponseText = string.Empty;
        //    string sPath = "";
        //    try
        //    {
        //        if (upload != null && upload.ContentLength > 0)
        //        {

        //            string filePath = string.Empty;
        //            string path = Server.MapPath("~/Uploads/");
        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }

        //            filePath = path + Path.GetFileName(upload.FileName);
        //            string extension = Path.GetExtension(upload.FileName);
        //            upload.SaveAs(filePath);

        //            string conString = string.Empty;
        //            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
        //            DataTable dt = new DataTable();
        //            conString = string.Format(conString, filePath);

        //            using (OleDbConnection connExcel = new OleDbConnection(conString))
        //            {
        //                using (OleDbCommand cmdExcel = new OleDbCommand())
        //                {
        //                    using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
        //                    {
        //                        cmdExcel.Connection = connExcel;

        //                        //Get the name of First Sheet.
        //                        connExcel.Open();
        //                        DataTable dtExcelSchema;
        //                        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        //                        connExcel.Close();

        //                        //Read Data from First Sheet.
        //                        connExcel.Open();
        //                        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
        //                        odaExcel.SelectCommand = cmdExcel;
        //                        odaExcel.Fill(dt);
        //                        foreach (var item in dt.Rows)
        //                        {
        //                            var x = item;
        //                        }
        //                        connExcel.Close();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;

        //    }
        //    return View();
        //}

        //SEND TIMESLOT EMAIL>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ActionResult SendTimeslot()
        {
            return View("SendTimeslot");
        }
        [HttpPost]
        public ActionResult SendTimeslot(MailModel obj)
        {

            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  
                WebMail.SmtpServer = "smtp.gmail.com";
                //gmail port to send emails  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = "fypfyp87@gmail.com";
                WebMail.Password = "Fyp_1234";

                //Sender email address.  
                WebMail.From = "fypfyp87@gmail.com";
                // WebMail.

                //Send email  
                WebMail.Send(to: obj.ToEmail, subject: obj.EmailSubject, body: obj.EMailBody, cc: obj.EmailCC, isBodyHtml: true);
                ViewBag.Status = "Email Sent Successfully.";
            }
            catch (Exception)
            {
                ViewBag.Status = "Problem while sending email, Please check details.";

            }
            return View();
        }

        // ALLOCATION>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public IActionResult ViewStatus()
        {
            var allSlot = DBUtl.GetList("select * from timeslot");
            return View(allSlot);
        }
        private int search(dynamic lecturers, dynamic searchSlot)
        {
            for (int i = 0; i < lecturers.Count; i++)
            {
                if (lecturers[i].timeslot_id == searchSlot)
                {
                    return i;

                }
            }
            return -1;
        }
        [HttpPost]
        public IActionResult allot()
        {
            string venueSql = @"SELECT t.timeslot_id, E.class_id
                            FROM Timeslot t, exam_venue E WHERE
                            E.Timeslot_timeslot_id = t.timeslot_id
                            and E.associate_lecturer_al_id is null;";
            string lecturerSql = @"SELECT ls.Timeslot_timeslot_id,
                            ls.associate_lecturer_al_id,ls.request_time, t.timeslot_id
                            FROM lect_slot ls, Timeslot t WHERE 
                            ls.Timeslot_timeslot_id = t.timeslot_id;";
            var lecturers = DBUtl.GetList(lecturerSql);
            var venues = DBUtl.GetList(venueSql);
            if (venues.Count > 0)
            {
                foreach (var i in venues)
                {
                    int pos = search(lecturers, i.timeslot_id);

                    string updateSql = @"update exam_venue set associate_lecturer_al_id = {0}
                                    where class_id = {1}";
                    int success = DBUtl.ExecSQL(updateSql, lecturers[pos].associate_lecturer_al_id, i.class_id);
                    lecturers.RemoveAt(pos);
                    //TempData["msg"] = DBUtl.DB_Message;
                }
                //TempData["msg"] = "Auto allocation success";
            }
            else
            {
                //TempData["error"] = "No venues to auto allocate";
            }
            return RedirectToAction("AllocatedStatus");
        }
        public IActionResult AllocatedStatus()
        {
            return View("AllocatedStatus");
        }
        //SEND REMINDER EMAIL>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ActionResult SendReminder()
        {
            return View("SendTimeslot");
        }
        [HttpPost]
        public ActionResult SendReminder(MailModel obj)
        {

            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  
                WebMail.SmtpServer = "smtp.gmail.com";
                //gmail port to send emails  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = "fypfyp87@gmail.com";
                WebMail.Password = "Fyp_1234";

                //Sender email address.  
                WebMail.From = "fypfyp87@gmail.com";
                // WebMail.

                //Send email  
                WebMail.Send(to: obj.ToEmail, subject: obj.EmailSubject, body: obj.EMailBody, cc: obj.EmailCC, isBodyHtml: true);
                ViewBag.Status = "Email Sent Successfully.";
            }
            catch (Exception)
            {
                ViewBag.Status = "Problem while sending email, Please check details.";

            }
            return View();
        }
        //SEND OFFER OF ENGAGEMENT EMAIL>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public IActionResult SendEmail()
        {
            return View("SendEmail");
        }
        [HttpPost]
        public ActionResult SendEmail(MailModel obj)
        {

            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  
                WebMail.SmtpServer = "smtp.gmail.com";
                //gmail port to send emails  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = "fypfyp87@gmail.com";
                WebMail.Password = "Fyp_1234";

                //Sender email address.  
                WebMail.From = "fypfyp87@gmail.com";

                //Send email  
                WebMail.Send(to: obj.ToEmail, subject: obj.EmailSubject, body: obj.EMailBody, cc: obj.EmailCC, isBodyHtml: true);
                ViewBag.Status = "Email Sent Successfully.";
            }
            catch (Exception)
            {
                ViewBag.Status = "Problem while sending email, Please check details.";

            }
            return View();
        }
        //GENERATE EXCEL>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //protected void GenerateExcel(object sender, EventArgs e)
        //{
        //    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SELECT * FROM customers"))
        //        {
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                cmd.Connection = con;
        //                sda.SelectCommand = cmd;
        //                using (DataTable dt = new DataTable())
        //                {
        //                    sda.Fill(dt);

        //                    //Build the CSV file data as a Comma separated string.
        //                    string csv = string.Empty;

        //                    foreach (DataColumn column in dt.Columns)
        //                    {
        //                        //Add the Header row for CSV file.
        //                        csv += column.ColumnName + ',';
        //                    }

        //                    //Add new line.
        //                    csv += "\r\n";

        //                    foreach (DataRow row in dt.Rows)
        //                    {
        //                        foreach (DataColumn column in dt.Columns)
        //                        {
        //                            //Add the Data rows.
        //                            csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
        //                        }

        //                        //Add new line.
        //                        csv += "\r\n";
        //                    }


        //                   // Download the CSV file.


        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.AppendHeader("content-disposition", "attachment;filename=SqlExport.csv");
        //                    Response.Charset = "";
        //                    Response.ContentType = "application/text";
        //                    Response.Output.Write(csv);
        //                    Response.Flush();
        //                    Response.End();

        //                }
        //            }
        //        }
        //    }
        //}


        //-----------------------------------------------AL_LECTURERS-------------------------------------------------
        public IActionResult IndicateAvailability()
        {
            return View("IndicateAvailability");
        }
        [HttpPost]
        public IActionResult Submit(LectSlot objSubmit)
        {
            string timeslotsql = @"SELECT timeslot_id FROM Timeslot WHERE examDate = date AND examDate = preferred_time";
            DBUtl.ExecSQL(timeslotsql);
            string sql = @"INSERT timeslot_id FROM Timeslot AS Timeslot_timeslot_id INTO lect_slot";
            DBUtl.ExecSQL(sql);
            TempData["Msg"] = "Timeslot recorded successfully.";
            TempData["request_time"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
            return View("IndicateAvailability");
        }

        public IActionResult ViewAllocation()
        {
            ViewData["Timeslot"] = DBUtl.GetList(@"SELECT * FROM Timeslot");
            ViewData["ExamVenue"] = DBUtl.GetList(@"SELECT class_name FROM exam_venue WHERE associate_lecturer_al_id = al_id");
            return View();
        }

        public IActionResult Accept(Timeslot objAccept)
        {
            ViewBag.Msg = "Confirmed timeslot!";
            return View();
        }

        public IActionResult MaintainDetails()
        {
            if (curUser() == null)
                return this.Authenticate();
            else
            {
                List<AssociateLecturer> model = DBUtl.GetList<AssociateLecturer>("SELECT * FROM associate_lecturer"); //WHERE type = {0}", curUser().al_id);
                return View(model);
            }
        }
    }
}


