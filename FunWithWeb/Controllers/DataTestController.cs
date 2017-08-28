using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FunWithWeb.Models;

namespace FunWithWeb.Controllers
{
    public class DataTestController : Controller
    {
        // GET: DataTest
        public ActionResult Index()
        {

            //This is just test data. I'm looking to figure out CRUD for this kind of information

            List<DataTest> TestData = new List<DataTest>();

            DataTest Testing = new DataTest("The Police", "Outlandos d\'Amour", "Roxanne", "Stewart Copland", 1978);

            TestData.Add(Testing);

            return View(TestData);
        }

    }
}