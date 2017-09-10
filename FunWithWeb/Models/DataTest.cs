using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace FunWithWeb.Models
{
    public class DataTest
    {
        public int ID { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string TrackName { get; set; }
        public string Drummer { get; set; }
        public int Year { get; set; }

        public DataTest(int id, string artist, string album, string trackName, string drummer, int year)
        {
            ID = id;
            Artist = artist;
            Album = album;
            TrackName = trackName;
            Drummer = drummer;
            Year = year;
        }

        public DataTest() { }

        
    }
}