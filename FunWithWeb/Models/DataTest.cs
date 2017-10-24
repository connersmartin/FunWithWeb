using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace FunWithWeb.Models
{
    public class DataTest
    {

        public int ID { get; set; }

        [Required(ErrorMessage ="There must be an Artist")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "There must be an Album")]
        public string Album { get; set; }

        [Required(ErrorMessage = "There must be a Track Name")]
        public string TrackName { get; set; }

        [Required(ErrorMessage = "There must be a Drummer")]
        public string Drummer { get; set; }

        [Required(ErrorMessage = "There must be a year")]
        [Range(1700, 2100, ErrorMessage ="Year must be valid" )]
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