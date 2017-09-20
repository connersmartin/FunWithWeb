﻿using System;
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

        public ActionResult Index()
        {

            return View();
        }

        // GET: API
        // just figuring out how the API works 
        public ActionResult Detail()
        {
           //Still test data, but now I understand better how to incorporate 3rd party models into views
            
            FullTrack track = Spotify._spotify.GetTrack("0s1aSsYlLIEiy16LjFWbdp");

            return View(track);
        }


        public async Task<ActionResult> Auth()
        {

            Spotify.SpotAuth();


            //is this the right place to put cookies?
            HttpCookie myCookie = new HttpCookie("authed");
            myCookie["Font"] = "Arial";
            myCookie["Color"] = "Blue";
            myCookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(myCookie);

            return RedirectToAction("Index");
        }

        public ActionResult Search(string qStr)
        {
            //need to have submit buttn on search page go to this
            // then figure out how to render a view on a model that doesn't exist in the models folder
            List<FullArtist> fA  = Spotify.SpotSearch(qStr);

            return View(fA);
        }
    }
}