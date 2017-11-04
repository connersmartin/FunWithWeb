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
         * Auth/Search page done kind of, need to figure out auth/cookies
         * brings results in done
         * 
         * play files - not that hard, see https://developer.spotify.com/technologies/widgets/spotify-play-button/
         * search from DataTest info
         */
        

        //index, will auth or go to landing page
        public ActionResult Index()
        {
            if (Spotify._spotify == null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("Landing");
            }
        }

        //Shows detail of an artist's tracks

        //TODO figure out how to make for tracks for a specific album, then full info for a specific song maybe
        //think about partial views or doing some logic within the view to show specific detail
        //Also need to figure out how to play different 'types'
        public ActionResult Detail(string id)
        {    
            return View(Spotify.TrackDetail(id));
        }

        //checks to see if already authed, if so redirect to landing page
        public async Task<ActionResult> Auth(string id)
        {
            if (Spotify._spotify == null && id == null)
            {
                Spotify.SpotAuth();

                return RedirectToAction("Landing");
            }
            else if (Spotify._spotify == null && id != null)
            {
                Spotify.SpotAuth();
                return RedirectToAction("Search", new { id = id });
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        //Why did this give me an error? 

         /*
        public async Task<ActionResult> Auth(string id)
        {
            if (Spotify._spotify == null)
            {
                Spotify.SpotAuth();
            }
            return RedirectToAction("Search", new { id = id });
           
        }*/


        public ActionResult Search(string id)
        {
            if (Spotify._spotify != null)
            {
                return View(Spotify.SpotSearch(id));
            }

            else
            {
                return RedirectToAction("Auth", new { id = id });
            }
        }

        public ActionResult Landing()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Landing(string id, string artist)
        {
            //need to figure out how to send drop down menu value as well
            //OR send querytype so we can get a better user experience from the SQL db
            //next goal is to try a tempo search
            try
            {
                float tempo = (float)decimal.Parse(id);
                return RedirectToAction("Tempo", "API", new { id = tempo, artist = artist });
            }
            catch
            {
                //that new syntax messed me up, need to understand that better, maybe can't pass a value, but a parameter object of id?
                return RedirectToAction("Search", "API", new { id = id });
            }
        }

        public ActionResult Tempo(float id, string artist)
        {
            //this may get complex, but that's why I want to try it
            //I'll have to get all genres and search that tmepo
            //or get a couple of tracks
            return View("Detail",Spotify.TempoSearch(id, artist));
        }

        public ActionResult PlayerPane(string id)
        {
            return PartialView("_PlayerPane", id);
        }

        public ActionResult AlbumDetail(string id)
        {
            return View("Detail", Spotify.AlbumDetail(id));
        }
    }
}