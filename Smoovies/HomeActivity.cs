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

namespace Smoovies
{
    [Activity(Label = "Smoovies", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeActivity : Activity
    {
        private MovieAPI movieAPI;
        List<Movie> savedMovies;
        List<Movie> popularMovies;
        List < Movie > newMovies;
        List<Movie> goodMovies;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ActivityHome);

            movieAPI = new MovieAPI(new Http());

            ListView popularListView = this.FindViewById<ListView>(Resource.Id.listPopular);
            ListView newListView = this.FindViewById<ListView>(Resource.Id.listNowPlaying);
            ListView goodListView = this.FindViewById<ListView>(Resource.Id.listTopRated);         

           savedMovies = GetSavedMovies();
           popularMovies = movieAPI.GetPopular();
           newMovies = movieAPI.GetNowPlaying();
           goodMovies = movieAPI.GetTopRated();

            popularListView.Adapter = new MovieAdapter(this, popularMovies);
            newListView.Adapter = new MovieAdapter(this, newMovies);
            goodListView.Adapter = new MovieAdapter(this, goodMovies);

            popularListView.ItemClick += MovieList_Click;
            newListView.ItemClick += MovieList_Click;
            goodListView.ItemClick += MovieList_Click;
        }

        private void LaunchDetailsActivity(int id)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("id", id);
            StartActivity(intent);
        }

        private void MovieList_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            LaunchDetailsActivity((int)e.Parent.GetItemAtPosition(e.Position));
        }

        private List<Movie> GetSavedMovies()
        {
            return new List<Movie>();
        }
    }

    public class MovieAdapter : ArrayAdapter<Movie>
    {
        List<Movie> _movies;
        Context _context;
        public MovieAdapter(Context context, List<Movie> movies) : base(context, 0)
        {
            _context = context;
            _movies = movies;
        }
     
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.From(_context).Inflate(Resource.Layout.MovieTile, parent);
            }

            ImageView iv = convertView.FindViewById<ImageView>(Resource.Id.ivMovie);
            iv.SetImageURI((Android.Net.Uri)_movies[position].poster_path); //???

            return convertView;
        }        
    }
}

