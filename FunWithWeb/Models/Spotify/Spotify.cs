﻿using System;
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

        public static SearchAll SA = new SearchAll();
      

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

        public static SearchAll SpotSearch(string queryString, string searchType = null)
        {
            SA.ArtistSearch = _spotify.SearchItems(queryString, SearchType.Artist, 10, 0, "US").Artists.Items.ToList();

            SA.AlbumSearch = SimpleToFull(_spotify.SearchItems(queryString, SearchType.Album, 10, 0, "US").Albums.Items.ToList());

            SA.TrackSearch = _spotify.SearchItems(queryString, SearchType.Track, 10, 0, "US").Tracks.Items.ToList();

            SA.query = queryString;

            SA.searchType = searchType;

            return SA;
        }

        //returns the top tracks for an artist

        public static SearchAll TopTrackDetail(string id)
        {
            SA.TrackSearch = _spotify.GetArtistsTopTracks(id, "US").Tracks;

            return SA;
        }

        //returns specific tracks

        public static SearchAll TrackDetail(List<string> ids)
        {
            SA.TrackSearch = _spotify.GetSeveralTracks(ids, "US").Tracks.ToList();

            return SA;
        }

        //Get recommended tracks based of a tempo and query

        //Potentially make an 'advanced' search form, so instead of tempo search, this would be a general recommendation search

        public static SearchAll TempoSearch(float tempo, string artistSearch)
        {
            TuneableTrack trackParam = new TuneableTrack();
            TuneableTrack trackParamMin = new TuneableTrack();
            TuneableTrack trackParamMax = new TuneableTrack();

            trackParam.Tempo = tempo;
            trackParamMin.Tempo = .9f * tempo;
            trackParamMax.Tempo = 1.1f * tempo; 

            List<FullArtist> fullArtist = _spotify.SearchItems(artistSearch, SearchType.Artist, 1, 0, "US").Artists.Items.ToList();

            List<string> artistString = new List<string>();
            
            foreach (FullArtist fullArt in fullArtist)
            {
                artistString.Add(fullArt.Id);
            }

            List<SimpleTrack> recTracks = _spotify.GetRecommendations(artistString, null, null, trackParam, trackParamMin, trackParamMax, 25, "US").Tracks.ToList();

            List<SimpleTrack> playTracks = new List<SimpleTrack>();

            float playlistLength = 3600000;

            foreach (SimpleTrack trk in recTracks)
            {
                if (playlistLength > 0)
                {
                    playlistLength -= trk.DurationMs;
                    if (playlistLength < 0)
                    {
                        playlistLength += trk.DurationMs;
                        continue;
                    }
                    else
                    {
                        playTracks.Add(trk);
                    }
                }
            }

            SA.TrackSearch = SimpleToFull(playTracks);
            
            SA.query = "tempo";

            return SA;
           
        }

        public static SearchAll AlbumDetail (string id)
        {
            SA.AlbumSearch = new List<FullAlbum>();

            SA.AlbumSearch.Add(_spotify.GetAlbum(id, "US"));

            SA.TrackSearch = SimpleToFull(_spotify.GetAlbumTracks(id, 50, 0, "US").Items.ToList());

            SA.query = "album";

            return SA;
        }

        public static List<SimpleTrack> AlbumToTrack (FullAlbum album)
        {
            return album.Tracks.Items.ToList();
        }
        
        public static List<FullTrack> SimpleToFull (List<SimpleTrack> simple)
        {
            List<FullTrack> full = new List<FullTrack>();

            foreach (SimpleTrack s in simple)
            {
                full.Add(_spotify.GetTrack(s.Id));
            }

            return full;
        }

        public static List<FullAlbum> SimpleToFull(List<SimpleAlbum> simple)
        {
            List<FullAlbum> full = new List<FullAlbum>();

            foreach (SimpleAlbum s in simple)
            {
                full.Add(_spotify.GetAlbum(s.Id));
            }

            return full;
        }


    }
}