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
        public Spotify()
        {

        }

        public static SpotifyWebAPI _spotify;

        //I think searching and returning stuff will be my goal
        //https://developer.spotify.com/web-api/console/get-search-item/
        //I think that will be fun to try

      

        //Auth prob 1. a better way to implement, 2. a better place to put this, 3. need to figure out exactly how this works

        public static async void SpotAuth()
        {
            WebAPIFactory webApiFactory = new WebAPIFactory(
               "http://localhost",
               8888,
               "873024ff9d744bef8d16db221ca61ab1",
               Scope.UserReadPrivate,
               TimeSpan.FromSeconds(20)
               );

            try
            {
                //This will open the user's browser and returns once
                //the user is authorized.
                _spotify = await webApiFactory.GetWebApi();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (_spotify == null)
                return;
        }

        //need to figure out how to make a universal search method

        //for a given search parameter, we need Artist, Album, songs returned
        //for that we could have 3 separate methods and return each in a partial view... easy?

        public static SearchAll SpotSearch(string qS)
        {

            //return a new model: each property would be collection of potential search types
            //from there you would pass that to the search

            SearchAll SA = new SearchAll();

            SA.AlbumSearch = new List<FullAlbum>();

            SA.ArtistSearch = _spotify.SearchItems(qS, SearchType.Artist, 10, 0, "US").Artists.Items.ToList();

            List<SimpleAlbum> simpleAlbum = _spotify.SearchItems(qS, SearchType.Album, 10, 0, "US").Albums.Items.ToList();

            foreach (SimpleAlbum item in simpleAlbum)
            {
                SA.AlbumSearch.Add(_spotify.GetAlbum(item.Id, "US"));
            }

            SA.TrackSearch = _spotify.SearchItems(qS, SearchType.Track, 10, 0, "US").Tracks.Items.ToList();

            //try doing search all maybe easier

            SA.query = qS;

            return SA;
        }


    }
}