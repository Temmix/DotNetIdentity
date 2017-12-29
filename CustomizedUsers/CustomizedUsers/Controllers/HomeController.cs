using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomizedUsers.Extensions;

namespace CustomizedUsers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           // var user = manager.FindById(User.Identity.Name);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}