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
            AddAlbum("Remembering Youth by caffeinedrummer", "http://caffeinedrummer.bandcamp.com/album/remembering-youth", 3634506203);
            AddAlbum("Marvels by James Rabbit", "http://jamesrabbit.bandcamp.com/album/marvels", 3998331254);
            AddAlbum("Fiji by Modern American Theatre", "http://modernamericantheatre.bandcamp.com/album/fiji", 76860617);

            return View(albumCollection);
        }

        public List<Album> albumCollection = new List<Album>();

        public void AddAlbum(string albumName, string albumTitle, long albumNumber)
        {
            Album albumToAdd = new Album(albumName, albumTitle, albumNumber);
            albumCollection.Add(albumToAdd);
        }
    }
}