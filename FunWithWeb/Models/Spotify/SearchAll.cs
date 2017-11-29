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
    public class SearchAll
    {
        //implement ienumerables of each search type to we can enumerate over them in the search view
        public SearchAll() {

        }
        public string tempo { get; set; }
        public string length { get; set; }
        public string searchType { get; set; }
        public string query { get; set; }
        public List<FullArtist> ArtistSearch { get; set; }
        public List<FullAlbum> AlbumSearch { get; set; }
        public List<FullTrack> TrackSearch { get; set; }

    }
}