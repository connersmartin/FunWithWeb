using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FunWithWeb.Controllers
{
    public class MusicController : Controller
    {
        // GET: Music
        public ActionResult Music()
        {
            ViewBag.Message = "I would like to try and embed a music player here.";
            ViewBag.AlbumName = "Remembering Youth by caffeinedrummer";
            ViewBag.AlbumLink = "http://caffeinedrummer.bandcamp.com/album/remembering-youth";
            ViewBag.AlbumNumber = 3634506203;

            return View();
        }
    }
}