using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FunWithWeb.Models.Spotify;

namespace FunWithWeb.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        public ActionResult Index()
        {
            Spotify s = new Spotify();

            s.Artist = "Test";
            s.TrackName = s.Track();

            return View(s);
        }


    }
}