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


        public ActionResult Detail(string id)
        {
            SeveralTracks tracks = Spotify._spotify.GetArtistsTopTracks(id, "US");
           
            List<FullTrack> listTracks = tracks.Tracks;

            return View(listTracks);
        }


        public async Task<ActionResult> Auth()
        {
            if (Spotify._spotify == null)
            {
                Spotify.SpotAuth();
                //is this the right place to put cookies?
                //Also need to figure out how to tie that to actually being authed
                HttpCookie myCookie = new HttpCookie("authed");
                myCookie["Font"] = "Arial";
                myCookie["Color"] = "Blue";
                myCookie.Expires = DateTime.Now.AddSeconds(60d);
                Response.Cookies.Add(myCookie);

                return RedirectToAction("Landing");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult Search(string id)
        {
            List<FullArtist> fA = Spotify.SpotSearch(id);

            return View(fA);
        }

        public ActionResult Landing()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Landing(string id)
        {
            //that new syntax messed me up, need to understand that better, maybe can't pass a value, but a parameter object of id?
            return RedirectToAction("Search", "API", new { id = id });
        }

        public ActionResult Play(string id, string playType)
        {
            //Goal is to click on a link in the search page and play the track/artist/album

            switch (playType)
            {
                case "track":
                    break;
                case "artist":
                    break;
                case "album":
                    break;
                default:
                    break;
            }
            return View();
        }
    }
}