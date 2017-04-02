using Android.App;
using Android.Widget;
using Android.OS;
using SmooviesPCL.Models;
using System.Collections.Generic;
using SmooviesPCL.BusinessLogic;
using Smoovies.Core;
using Android.Database;
using Android.Views;
using Java.Lang;
using System;
using Android.Content;
using Android.Util;
using Newtonsoft.Json;

namespace Smoovies
{
    [Activity(Label = "HomeActivity", Icon = "@drawable/icon", Name = "com.Smoovies.HomeActivity", MainLauncher =true)]
    public class HomeActivity : Activity
    {
        private MovieAPI movieAPI;
        List<Movie> savedMovies;
        List<Movie> popularMovies;
        List<Movie> newMovies;
        List<Movie> goodMovies;
        ListView newListView;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ActivityHome);
            TextView tv = this.FindViewById<TextView>(Resource.Id.tv);
            tv.Text = "connected";

             newListView = this.FindViewById<ListView>(Resource.Id.listNowPlaying);
             newListView.ItemClick += NewListView_ItemClick;

            movieAPI = new MovieAPI();
            await movieAPI.GetConfig();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            
            newMovies = await movieAPI.GetNowPlaying();

            Log.Debug("Home", "Got newMovies: " + newMovies.Count);
            newListView.Adapter = new MovieAdapter(this, newMovies );

        }

        private void NewListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("movie", JsonConvert.SerializeObject(newMovies[e.Position]));
            StartActivity(intent);
        }

        private void LaunchDetailsActivity(object movie)
        {
           
        }

        //private void MovieList_Click(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    LaunchDetailsActivity((int)e.Parent.GetItemAtPosition(e.Position));
        //}

        private List<Movie> GetSavedMovies()
        {
            return new List<Movie>();
        }
    }

    
}

