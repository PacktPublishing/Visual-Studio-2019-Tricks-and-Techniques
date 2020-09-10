using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace $safeprojectname$.Controllers
{
    public class HomeController : Controller
    {
        public static string GetAssemblyFileVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersion.FileVersion;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "CitySelector API";
            ViewBag.Version = GetAssemblyFileVersion();

            var connectionString = ConfigurationManager.ConnectionStrings["myDBConnectionString"];
            ViewBag.DBConn = connectionString;

            return View();
        }
    }
}