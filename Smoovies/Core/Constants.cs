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
using System.IO;

namespace Smoovies.Core
{
    public class Constants
    {
        public static string dbFavPath {
            get {
                return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "favorites.db");
            }
            private set { }
        }
    }
}