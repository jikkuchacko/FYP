using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FYP.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FYP.Controllers
{
    public class AccountController : Controller
    {
        private Login curUser()
        {
            Login cUser = HttpContext.Session.GetObject<Login>("phuser");
            return cUser;
        }
        [HttpGet]
        public IActionResult Authenticate()
        {
            // Login.cshtml is a shared form, therefore the Layout used should be changed programmatically
            ViewData["layout"] = "_Layout";

            return View("Login");
        }
        [HttpPost]
        public IActionResult Authenticate(Login login)
        {
            if (curUser() == null)
            {
                string sql = @"SELECT * FROM Al_lecturer WHERE Name = '{0}' AND Password = HASHBYTES('SHA1', '{1}')";
                var result = DBUtl.GetList(sql, login.UserId, login.Password);
                if (result.Count > 0)
                {
                    dynamic user = result[0];
                    login.Name = user.Name;
                    login.Password = null;
                    login.Id = user.Id;
                    HttpContext.Session.SetObject("Al_lecturer", login);
                    return RedirectToAction("Index");
                }
                ViewData["layout"] = "_Layout";
                ViewData["msg"] = "Login failed";
                return View("Login");
            }
            else
                return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
