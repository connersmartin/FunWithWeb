using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
using SpotifyAPI.Web.Enums; //Enums
using SpotifyAPI.Web.Models; //Models for the JSON-responses

namespace FunWithWeb.Models.Spotify
{

    public class Spotify
    {
        public Spotify() { }

        public Spotify(string artist, string trackName)
        {
            Artist = artist;
            TrackName = trackName;
        }

        public string Artist { get; set; }
        public string TrackName { get; set; }

        private static SpotifyWebAPI _spotify;

        public string Track()
        {
            _spotify = new SpotifyWebAPI()
            {
                UseAuth = false, //This will disable Authentication.
            };
                FullTrack track = _spotify.GetTrack("3Hvu1pq89D4R0lyPBoujSv");
            if (track.Name != null)
            {
                return track.Name; //Yeay! We just printed a tracks name.
            }
            else
            {
                return track.Error.Message;
            }
        }


    }
}