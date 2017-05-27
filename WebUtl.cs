using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Net;
using Newtonsoft.Json;

public static class WebUtl
{
    public static dynamic CallWebApi(string url)
    {
        WebClient client = new WebClient();
        client.Headers["Accept"] = "application/json";

        try
        {
            string datastr = client.DownloadString(new Uri(url));

            return JsonConvert.DeserializeObject(datastr);
        }
        catch
        {
            return null;
        }
    }

}
