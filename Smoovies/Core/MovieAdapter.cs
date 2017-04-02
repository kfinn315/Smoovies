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
using SmooviesPCL.Models;
using Android.Util;
using SmooviesPCL.BusinessLogic;
using Square.Picasso;
using Android.Support.V7.Widget;

namespace Smoovies.Core
{
    public class MovieAdapter : RecyclerView.Adapter
    {
        List<Movie> _movies;
        Context _context;
        int _itemWidth;

        public event EventHandler<int> ItemClick;

        public override int ItemCount
        {
            get
            {
                return _movies.Count;
            }
        }

        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }

        public MovieAdapter(Context context, List<Movie> movies, int itemWidth) : base()
        {
            _context = context;
            _movies = movies;
            _itemWidth = itemWidth;
        }


        public override long GetItemId(int position)
        {
            return position;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            try
            {
                Log.Debug("GetView", "Position=" + position);
                MovieViewHolder vh = (MovieViewHolder)holder;

                Movie movie = _movies[position];

                Log.Debug("GetView", movie.ToString());
                string strURI = MovieAPI.GetImageURL(movie.poster_path, 0);
                Android.Net.Uri posteruri = Android.Net.Uri.Parse(strURI);
                Picasso.With(_context).Load(posteruri).Into(vh.ivPoster, new IVCallback(position));
                
            }
            catch (Exception e)
            {
                Log.Debug("GetView", "Exception " + e.Message);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            View itemView = LayoutInflater.From(_context).Inflate(Resource.Layout.MovieTile, parent, false);

            ImageView ivMovie = itemView.FindViewById<ImageView>(Resource.Id.ivMovie);
            ViewGroup.LayoutParams llParams = ivMovie.LayoutParameters;
            llParams.Width = _itemWidth;
            ivMovie.LayoutParameters = llParams;
            MovieViewHolder vh = new MovieViewHolder(itemView, OnClick);
            return vh;
        }
    }

    public class MovieViewHolder : RecyclerView.ViewHolder
    {
        public ImageView ivPoster { get; private set; }

        public MovieViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            ivPoster = itemView.FindViewById<ImageView>(Resource.Id.ivMovie);
            itemView.Click += (sender, e) => listener(base.Position);
        }
    }

    public class IVCallback : Java.Lang.Object, ICallback
    {
        int _position = -1;
        public IVCallback(int position)
        {
            _position = position;
        }
        public void OnError()
        {
            Log.Info("IVCallback", "OnError " + _position);
        }

        public void OnSuccess()
        {
            Log.Info("IVCallback", "OnSuccess " + _position);
        }
    }


}