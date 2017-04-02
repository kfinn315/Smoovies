using SmooviesPCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Android.Util;

namespace SmooviesPCL.BusinessLogic
{
    public class MovieAPI
    {
        static Models.Config APIConfig;
        HttpClient client;

        const string apiKey = "ab41356b33d100ec61e6c098ecc92140";
        public const string baseURL = "http://api.themoviedb.org/3/";
        public MovieAPI()
        {
            client = new HttpClient();            
        }

        public async Task GetConfig() {
            string configURL = "https://api.themoviedb.org/3/configuration?api_key="+apiKey;
            HttpResponseMessage responsemsg = await client.GetAsync(configURL);
            string content = await responsemsg.Content.ReadAsStringAsync();

            APIConfig = JsonConvert.DeserializeObject<Models.Config>(content);
        }
        public static string GetImageURL(string id, int size)
        {            
            return APIConfig.Images.base_url+APIConfig.Images.poster_sizes[size]+id+"?api_key=" + apiKey;
        }
        public static string GetBGImageURL(string id, int size)
        {
            return APIConfig.Images.base_url + APIConfig.Images.backdrop_sizes[size] + id + "?api_key=" + apiKey;
        }
        public async Task<List<Movie>> GetNowPlaying()
        {
            string action = "now_playing";
            List<Movie> movies = new List<Movie>();
            try
            {
                string url = baseURL+"movie" +"/"+ action + "?api_key=" + apiKey + "&sort_by=popularity.des";
                Log.Debug("GetNowPlaying", "url = "+url);

                HttpResponseMessage responseMsg = await client.GetAsync(url);
                string content = await responseMsg.Content.ReadAsStringAsync();
                Log.Debug("GetNowPlaying", "GetNowPlaying received = " + content);

                Response response = JsonConvert.DeserializeObject<Response>(content);
                Log.Debug("GetNowPlaying", "Response " + response.ToString());

                movies = response.results;
            }
            catch (Exception ex) {
                Log.Debug("GetNowPlaying", "Exception "+ex.Message);
            }

            Log.Debug("GetNowPlaying", "Returning count="+movies.Count);
            return movies;
        }

        //public List<Movie> GetTopRated()
        //{
        //    string action = "top_rated";
        //    string list = _http.GET(baseURL+action+"?api_key="+apiKey+"&sort_by=popularity.des");

        //    return new List<Movie>();
        //}

        //public List<Movie> GetPopular()
        //{
        //    string action = "popular";
        //    string list = _http.GET(baseURL + action + "?api_key="+apiKey+"&sort_by=popularity.des");

        //    return new List<Movie>();
        //}

        //public List<Movie> GetSimilar(int id)
        //{
        //    string action = "/similar";
        //    string list = _http.GET(baseURL + id.ToString() + action+"?api_key="+apiKey+"&sort_by=popularity.des");

        //    return new List<Movie>();
        //}

        //public List<Video> GetVideo(int id)
        //{
        //    string action = "/videos";
        //    string result = _http.GET(baseURL + id.ToString() + action + "?api_key="+apiKey+"&sort_by=popularity.des");

        //    return new VideoResult().results;
        //}
    }
}
