using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FunWithWeb.Models;

namespace FunWithWeb.Controllers
{
    public class MusicController : Controller
    {
       
        // GET: Music
        public ActionResult Music()
        {
            ViewBag.Message = "I would like to try and embed a music player here.";

            AddAlbum("Remembering Youth by caffeinedrummer", "http://caffeinedrummer.bandcamp.com/album/remembering-youth", 3634506203);
            AddAlbum("Marvels by James Rabbit", "http://jamesrabbit.bandcamp.com/album/marvels", 3998331254);
            AddAlbum("Totally Fake Data", "http://reddit.com", 1234567890);

            ViewBag.Albums = albumCollection;

            return View();
        }

        public List<Album> albumCollection = new List<Album>();

        public void AddAlbum(string albumName, string albumTitle, long albumNumber)
        {
            Album albumToAdd = new Album(albumName, albumTitle, albumNumber);
            albumCollection.Add(albumToAdd);
        }
    }
}