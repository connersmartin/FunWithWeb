using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FunWithWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult API()
        {
            ViewBag.Message = "I'm going to try and play with APIs here.";

            return View();
        }


    }
}