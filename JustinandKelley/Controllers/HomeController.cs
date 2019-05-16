using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustinandKelley.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Authenticate()
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie("kelleyiscrazy", false);
            return RedirectToAction("Index", "Home");
        }
    }
}
