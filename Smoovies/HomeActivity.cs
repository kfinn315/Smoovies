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
using SmooviesPCL;
using System.Threading.Tasks;

namespace Smoovies
{
    [Activity(Label = "Home", Icon = "@drawable/icon", Name = "com.Smoovies.HomeActivity")]
    public class HomeActivity : Activity
    {
        //List<Movie> nowMovies;
        //List<Movie> topMovies;
        //List<Movie> popMovies;
        //List<Movie> favMovies;

        RecyclerView nowListView;
        RecyclerView topListView;
        RecyclerView popListView;
        RecyclerView favListView;

        MovieAdapter nowAdapter;
        MovieAdapter topAdapter;
        MovieAdapter popAdapter;
        MovieAdapter favAdapter;

        FavoriteDatasource datasource;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ActivityHome);

            topListView = this.FindViewById<RecyclerView>(Resource.Id.listTopRated);
            popListView = this.FindViewById<RecyclerView>(Resource.Id.listPopular);
            nowListView = this.FindViewById<RecyclerView>(Resource.Id.listNowPlaying);
            favListView = this.FindViewById<RecyclerView>(Resource.Id.listFav);

            topAdapter = new MovieAdapter(this, MovieLists.TopMovies);
            popAdapter = new MovieAdapter(this, MovieLists.PopMovies);
            nowAdapter = new MovieAdapter(this, MovieLists.NowMovies);

            topListView.SetAdapter(topAdapter);
            popListView.SetAdapter(popAdapter);
            nowListView.SetAdapter(nowAdapter);

            topAdapter.ItemClick += TopAdapter_ItemClick;
            popAdapter.ItemClick += PopAdapter_ItemClick;
            nowAdapter.ItemClick += NowAdapter_ItemClick;


            topListView.SetLayoutManager(new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false));
            popListView.SetLayoutManager(new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false));
            nowListView.SetLayoutManager(new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false));

        }

        protected async override void OnStart()
        {
            base.OnStart();

            datasource = new FavoriteDatasource(Constants.dbFavPath);

            MovieLists.FavMovies = await datasource.GetFavorites();

            RunOnUiThread(() =>
            {
                favAdapter = new MovieAdapter(this, MovieLists.FavMovies);

                favListView.SetAdapter(favAdapter);
                favAdapter.ItemClick += FavAdapter_ItemClick;
                favListView.SetLayoutManager(new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false));
            });
        }

        private void FavAdapter_ItemClick(object sender, int e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("movie", JsonConvert.SerializeObject(MovieLists.FavMovies[e]));
            StartActivity(intent);
        }

        private void TopAdapter_ItemClick(object sender, int e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("movie", JsonConvert.SerializeObject(MovieLists.TopMovies[e]));
            StartActivity(intent);
        }
        private void PopAdapter_ItemClick(object sender, int e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("movie", JsonConvert.SerializeObject(MovieLists.PopMovies[e]));
            StartActivity(intent);
        }
        private void NowAdapter_ItemClick(object sender, int e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("movie", JsonConvert.SerializeObject(MovieLists.NowMovies[e]));
            StartActivity(intent);
        }
    }
}

