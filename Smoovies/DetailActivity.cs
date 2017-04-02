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

namespace Smoovies
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : Activity
    {
        ImageView ivBG, ivPoster, ivSimilar1, ivSimilar2, ivSimilar3;
        TextView tvTitle, tvDescr, tvReleaseDate, tvVotes;
        Button btnFav, btnPlay;

        Movie movie;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ActivityDetail);

            ivPoster = FindViewById<ImageView>(Resource.Id.ivPoster);
            ivSimilar1 = FindViewById<ImageView>(Resource.Id.ivSimilar1);
            ivSimilar2 = FindViewById<ImageView>(Resource.Id.ivSimilar2);
            ivSimilar3 = FindViewById<ImageView>(Resource.Id.ivSimilar3);
            ivBG = FindViewById<ImageView>(Resource.Id.ivBG);

            tvTitle = FindViewById<TextView>(Resource.Id.tvTitle);
            tvDescr = FindViewById<TextView>(Resource.Id.tvDescr);
            tvReleaseDate = FindViewById<TextView>(Resource.Id.tvReleaseDate);
            tvVotes = FindViewById<TextView>(Resource.Id.tvVotes);
            btnFav = FindViewById<Button>(Resource.Id.btnFav);
            btnPlay = FindViewById<Button>(Resource.Id.btnPlay);

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

        protected override void OnStart()
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



            btnFav.Click += BtnFav_Click;
            btnPlay.Click += BtnPlay_Click;


        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnFav_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}