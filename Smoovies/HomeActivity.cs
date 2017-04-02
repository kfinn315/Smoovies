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
using Android.Support.V7.Widget;

namespace Smoovies
{
    [Activity(Label = "HomeActivity", Icon = "@drawable/icon", Name = "com.Smoovies.HomeActivity", MainLauncher = true)]
    public class HomeActivity : Activity
    {
        private MovieAPI movieAPI;

        List<Movie> nowMovies;
        List<Movie> topMovies;
        List<Movie> popMovies;

        RecyclerView nowListView;
        RecyclerView topListView;
        RecyclerView popListView;

        MovieAdapter nowAdapter;
        MovieAdapter topAdapter;
        MovieAdapter popAdapter;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ActivityHome);

            movieAPI = new MovieAPI();

            //TODO save in SharedPrefs
            await movieAPI.GetConfig();

            topListView = this.FindViewById<RecyclerView>(Resource.Id.listTopRated);
            popListView = this.FindViewById<RecyclerView>(Resource.Id.listPopular);
            nowListView = this.FindViewById<RecyclerView>(Resource.Id.listNowPlaying);

            topMovies = await movieAPI.GetTopRated();
            popMovies = await movieAPI.GetPopular();
            nowMovies = await movieAPI.GetNowPlaying();

            int tileWidth = 1000;
            topAdapter = new MovieAdapter(this, topMovies, tileWidth);
            popAdapter = new MovieAdapter(this, popMovies, tileWidth);
            nowAdapter = new MovieAdapter(this, nowMovies, tileWidth);

            topListView.SetAdapter(topAdapter);
            popListView.SetAdapter(popAdapter);
            nowListView.SetAdapter(nowAdapter);

            topAdapter.ItemClick += TopAdapter_ItemClick;
            popAdapter.ItemClick += PopAdapter_ItemClick;
            nowAdapter.ItemClick += NowAdapter_ItemClick;


            topListView.SetLayoutManager(new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false));
            popListView.SetLayoutManager(new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false));
            nowListView.SetLayoutManager(new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false));
            
            Log.Debug("Home", "Got newMovies: " + nowMovies.Count);
        }

        private void TopAdapter_ItemClick(object sender, int e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("movie", JsonConvert.SerializeObject(topMovies[e]));
            StartActivity(intent);
        }
        private void PopAdapter_ItemClick(object sender, int e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("movie", JsonConvert.SerializeObject(popMovies[e]));
            StartActivity(intent);
        }
        private void NowAdapter_ItemClick(object sender, int e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("movie", JsonConvert.SerializeObject(nowMovies[e]));
            StartActivity(intent);
        }
      
        private List<Movie> GetSavedMovies()
        {
            return new List<Movie>();
        }
    }


}

