using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FunWithWeb.Models
{
    public class DataTest
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string TrackName { get; set; }
        public string Drummer { get; set; }
        public int Year { get; set; }

        public DataTest(string artist, string album, string trackName, string drummer, int year)
        {
            Artist = artist;
            Album = album;
            TrackName = trackName;
            Drummer = drummer;
            Year = year;
        }


    }
}