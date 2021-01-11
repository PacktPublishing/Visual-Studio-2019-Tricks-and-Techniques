using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;

namespace CGHClientServer1.WebApi.Controllers
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

            var connectionString = ConfigurationManager.ConnectionStrings["CountryStateCityDbConn"];
            ViewBag.DBConn = connectionString;

            return View();
        }
    }
}