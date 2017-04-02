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
using Newtonsoft.Json;
using SmooviesPCL.Models;
using Square.Picasso;
using Smoovies.Core;
using Android.Util;
using SmooviesPCL.BusinessLogic;
using Android.Support.V7.Widget;

namespace Smoovies
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : Activity
    {
        ImageView ivBG, ivPoster;
        TextView tvTitle, tvDescr, tvReleaseDate, tvVotes;
        Button btnFav, btnPlay;
        RatingBar ratingScore;
        RecyclerView listSimilar;

        Movie movie;
        bool isFav;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ActivityDetail);

            ivPoster = FindViewById<ImageView>(Resource.Id.ivPoster);
            listSimilar = FindViewById<RecyclerView>(Resource.Id.listSimilar);

            ivBG = FindViewById<ImageView>(Resource.Id.ivBG);

            tvTitle = FindViewById<TextView>(Resource.Id.tvTitle);
            tvDescr = FindViewById<TextView>(Resource.Id.tvDescr);
            tvReleaseDate = FindViewById<TextView>(Resource.Id.tvReleaseDate);
            tvVotes = FindViewById<TextView>(Resource.Id.tvVotes);
            btnFav = FindViewById<Button>(Resource.Id.btnFav);
            btnPlay = FindViewById<Button>(Resource.Id.btnPlay);
            ratingScore = FindViewById<RatingBar>(Resource.Id.ratingScore);

            string jsonMovie = Intent.GetStringExtra("movie");
            try
            {
                movie = JsonConvert.DeserializeObject<Movie>(jsonMovie);

            }
            catch (Exception ex)
            {
                Log.Debug("DetailActivity", "OnCreate exception " + ex.Message);
            }

        }

        protected async override void OnStart()
        {
            base.OnStart();

            string url = SmooviesPCL.BusinessLogic.MovieAPI.GetImageURL(movie.poster_path, 0);
            Picasso.With(this).Load(url).Into(ivPoster, new IVCallback(movie.id));

            string urlbg = SmooviesPCL.BusinessLogic.MovieAPI.GetBGImageURL(movie.backdrop_path, 0);
            Picasso.With(this).Load(urlbg).Into(ivBG, new IVCallback(-1));

            tvTitle.Text = movie.title;
            tvDescr.Text = movie.overview;
            tvReleaseDate.Text = "Release Date: "+movie.release_date;
            tvVotes.Text = "(from "+movie.vote_count.ToString()+" votes)";

            ratingScore.Rating = movie.vote_average;

            btnFav.Click += BtnFav_Click;
            btnPlay.Click += BtnPlay_Click;

            MovieAPI api = new MovieAPI();
            List<Movie> movies = await api.GetSimilar(movie.id);
            int itemWidth = 100;

            MovieAdapter similarAdapter = new MovieAdapter(this, movies, itemWidth);
            listSimilar.SetLayoutManager(new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false));
            listSimilar.SetAdapter(similarAdapter);
        }

        private async void BtnPlay_Click(object sender, EventArgs e)
        {
            MovieAPI api = new MovieAPI();
            List<Video> vids =  await api.GetVideos(movie.id);

            if (vids == null || vids.Count == 0)
            {
                Toast.MakeText(this, "No videos", ToastLength.Long).Show();
            }
            else
            {
                //launch video player
            }
        }

        private void BtnFav_Click(object sender, EventArgs e)
        {
                ToggleFavorite(movie, !isFav);
        }

        private void ToggleFavorite(Movie movie, bool favorite)
        {
            if (favorite)
            {
                //add to fav
            }
            else
            {
                //remove from fav
            }
        }
    }
}