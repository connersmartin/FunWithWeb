using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FunWithWeb.Models;
using MySql.Data.MySqlClient;
using FunWithWeb.Models.DataAccess;

namespace FunWithWeb.Controllers
{
    public class DataTestController : Controller
    {

        DataAccess d = new DataAccess();

        // GET: DataTest all
        public ActionResult Index()
        {
            return View(d.ViewInfo(0, "all"));
        }

        //View single
        public ActionResult Detail(int id)
        {
                      
            return View(d.ViewInfo(id, "one").FirstOrDefault());
        }

        //Create a new entry
        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(DataTest newDataTest)
        {
            if (ModelState.IsValid)
            {
                d.AddInfo(newDataTest);

                return RedirectToAction("Index");
            }
            else
            {
                return View(newDataTest);
            }
        }

        //Edit existing
        public ActionResult Edit(int id)
        {

            return View(d.ViewInfo(id, "one").FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(DataTest newDataTest)
        {
            if (ModelState.IsValid)
            {
                d.EditInfo(newDataTest,  "edit");

                return RedirectToAction("Index");
            }
            else
            {
                return View(newDataTest);
            }
        }

        public ActionResult Delete(DataTest delDataTest)
        {
            d.EditInfo(delDataTest, "delete");

            return RedirectToAction("Index");
        }

       

      

    }
}