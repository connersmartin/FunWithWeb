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
        /* Overall design goal would be:
         * Auth/Search page
         * brings results in
         * 
         * stretch goal play files
         * search from DataTest info
         */


        // GET: API
        // just figuring out how the API works 
        public async Task<ActionResult> Detail()
        {

            //need to figure out how to call method from index view
            Spotify s = new Spotify();
           
            s.SpotAuth();

            //figure out how to implement this better
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