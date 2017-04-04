using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using SmooviesPCL.BusinessLogic;
using Smoovies.Core;
using SmooviesPCL;

namespace Smoovies
{
    [Activity(Label = "Smoovies", Icon = "@drawable/icon", Name = "com.Smoovies.SplashActivity", MainLauncher = true)]
    public class SplashActivity : Activity
    {
        ProgressBar progBar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ActivitySplash);

            progBar = FindViewById<ProgressBar>(Resource.Id.progBar);

        }

        protected async override void OnResume()
        {
            base.OnResume();

            Task getDataTask = GetData();

            await GetData();

            Intent intent = new Intent(this, typeof(HomeActivity));
            StartActivity(intent);
        }

        private void incrementProgBar(int prog)
        {
            RunOnUiThread(() => {
                progBar.Progress += prog;
            });
        }


        private async Task GetData()
        {
            MovieAPI api = new MovieAPI();

            MovieLists.TopMovies = await api.GetTopRated();
            incrementProgBar(25);
            MovieLists.PopMovies = await api.GetPopular();
            incrementProgBar(25);
            MovieLists.NowMovies = await api.GetNowPlaying();
            incrementProgBar(25);

            await api.GetConfig();
            incrementProgBar(25);

            FavoriteDatasource datasource = new FavoriteDatasource(Constants.dbFavPath);

            MovieLists.FavMovies = await datasource.GetFavorites();
        }
    }
}