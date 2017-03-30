using SmooviesPCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmooviesPCL.BusinessLogic
{
    public class MovieAPI
    {
        IHttp _http;
        const string apiKey = "ab41356b33d100ec61e6c098ecc92140";
        const string baseURL = "http://api.themoviedb.org/3/movie/";
        public MovieAPI(IHttp http)
        {
            _http = http;
        }

        public List<Movie> GetNowPlaying()
        {
            string action = "now_playing";
            string list = _http.GET(baseURL+action+"?api_key="+apiKey+"&sort_by=popularity.des");

            return new List<Movie>();
        }

        public List<Movie> GetTopRated()
        {
            string action = "top_rated";
            string list = _http.GET(baseURL+action+"?api_key="+apiKey+"&sort_by=popularity.des");

            return new List<Movie>();
        }

        public List<Movie> GetPopular()
        {
            string action = "popular";
            string list = _http.GET(baseURL + action + "?api_key="+apiKey+"&sort_by=popularity.des");

            return new List<Movie>();
        }

        public List<Movie> GetSimilar(int id)
        {
            string action = "/similar";
            string list = _http.GET(baseURL + id.ToString() + action+"?api_key="+apiKey+"&sort_by=popularity.des");

            return new List<Movie>();
        }

        public List<Video> GetVideo(int id)
        {
            string action = "/videos";
            string result = _http.GET(baseURL + id.ToString() + action + "?api_key="+apiKey+"&sort_by=popularity.des");

            return new VideoResult().results;
        }
    }
}
