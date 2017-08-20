using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FunWithWeb.Models
{
    public class Album
    {
        public string AlbumName { get; set; }
        public string AlbumLink { get; set; }
        public long AlbumNumber { get; set; }

        public Album(string albumName, string albumLink, long albumNumber)
        {
            AlbumName = albumName;
            AlbumLink = albumLink;
            AlbumNumber = albumNumber;
        }

        
    }
}