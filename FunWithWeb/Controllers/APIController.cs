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

        //Shows track list of an album

        public ActionResult AlbumDetail(string id)
        {
            return View("Detail", Spotify.AlbumDetail(id));
        }


        public ActionResult Search(string id, string search)
        {
            if (Spotify._spotify != null)
            {
                return View(Spotify.SpotSearch(id, search));
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

        //Partial view of the spotify player
        //need to alter to load different players depending on view
        //album, track, playlist
        public ActionResult PlayerPane(string id)
        {
            return PartialView("_PlayerPane", Spotify._spotify.GetTrack(id, "US"));
        }

    }
}