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

        public Album()
        {
            AlbumName = "";
            AlbumLink = "";
            AlbumNumber = 0;
        }
        
    }
}