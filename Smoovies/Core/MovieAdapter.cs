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

namespace Smoovies.Core
{
    public class MovieAdapter : BaseAdapter<Movie>
    {
        List<Movie> _movies;
        Context _context;

        public override int Count
        {
            get
            {
                return _movies.Count;
            }
        }

        public override Movie this[int position]
        {
            get
            {
                return _movies[position];
            }
        }

        public MovieAdapter(Context context, List<Movie> movies) : base()
        {
            _context = context;
            _movies = movies;

        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            try
            {

                Log.Debug("GetView", "Position=" + position);
                if (convertView == null)
                {
                    convertView = LayoutInflater.From(_context).Inflate(Resource.Layout.MovieTile, null);
                }

                ImageView iv = convertView.FindViewById<ImageView>(Resource.Id.ivMovie);

                Movie movie = _movies[position];

                Log.Debug("GetView", movie.ToString());
                string strURI = MovieAPI.GetImageURL(movie.poster_path, 0);
                Android.Net.Uri posteruri = Android.Net.Uri.Parse(strURI);
                Picasso.With(_context).Load(posteruri).Into(iv, new IVCallback(position));

                TextView tv = convertView.FindViewById<TextView>(Resource.Id.tvTitle1);
                tv.Text = movie.title;

                return convertView;
            }
            catch (Exception e)
            {
                Log.Debug("GetView", "Exception " + e.Message);
            }

            return new View(_context);
        }

        public override long GetItemId(int position)
        {
            return position;
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
            Log.Info("IVCallback", "OnError "+_position);
        }

        public void OnSuccess()
        {
            Log.Info("IVCallback", "OnSuccess "+_position);
        }
    }


}