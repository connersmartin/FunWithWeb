using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FunWithWeb.Models.Spotify;
using SpotifyAPI.Web.Models;
using System.Threading.Tasks;

namespace FunWithWeb.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        public async Task<ActionResult> Detail()
        {
            Spotify s = new Spotify();
           
            s.SpotAuth();


            s.Artist = s.Performer();
            s.TrackName = s.Track();

            return View(s);
        }

        public ActionResult Index()
        {
            return View();
        }


    }
}