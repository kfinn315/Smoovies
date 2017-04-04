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

namespace Smoovies.Core
{
    public class MovieLists
    {
        public static List<Movie> NowMovies { get; internal set; }
        public static List<Movie> PopMovies { get; internal set; }
        public static List<Movie> TopMovies { get; internal set; }
        public static List<Movie> FavMovies { get; internal set; }
    }
}