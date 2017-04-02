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

            Log.Debug("MovieAPI","GETNOWPLAYING()");
            List<Movie> movies = await GetMovies(action);

            Log.Debug("GetNowPlaying", "Returning count="+movies.Count);
            return movies;
        }
        private async Task<List<Movie>> GetMovies(string action, int id = -1)
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                string url = baseURL + "movie" + "/"+ (id!=-1?(id+"/"):"") + action + "?api_key=" + apiKey + "&sort_by=popularity.des";
                Log.Debug("MovieAPI", "url = " + url);

                HttpResponseMessage responseMsg = await client.GetAsync(url);
                string content = await responseMsg.Content.ReadAsStringAsync();

                Response response = JsonConvert.DeserializeObject<Response>(content);

                Log.Debug("MovieAPI", "Response " + response.ToString());

                movies = response.results;
            }
            catch (Exception ex)
            {
                Log.Debug("MovieAPI", "Exception " + ex.Message);
            }

            return movies;
        }
      

        public async Task<List<Movie>> GetTopRated()
        {
            string action = "top_rated";

            Log.Debug("MovieAPI", "GETTOPRATED()");
            List<Movie> movies = await GetMovies(action);

            Log.Debug("GetNowPlaying", "Returning count=" + movies.Count);
            return movies;
        }

        public async Task<List<Movie>> GetPopular()
        {
            string action = "popular";

            Log.Debug("MovieAPI", "GETPOPULAR()");
            List<Movie> movies = await GetMovies(action);

            Log.Debug("GetNowPlaying", "Returning count=" + movies.Count);
            return movies;
        }

        public async Task<List<Movie>> GetSimilar(int id)
        {
            string action = "similar";

            Log.Debug("MovieAPI", "GET SIMILAR()");
            List<Movie> movies = await GetMovies(action, id);

            Log.Debug("GetNowPlaying", "Returning count=" + movies.Count);
            return movies;
        }

        public async Task<List<Video>> GetVideos(int id)
        {
            string action = "/videos";

            List<Video> movies = new List<Video>();
            try
            {
                string url = baseURL + "movie/"+id.ToString() + action + "?api_key=" + apiKey + "&sort_by=popularity.des";
                Log.Debug("MovieAPI", "url = " + url);

                HttpResponseMessage responseMsg = await client.GetAsync(url);
                string content = await responseMsg.Content.ReadAsStringAsync();

                VideoResponse response = JsonConvert.DeserializeObject<VideoResponse>(content);

                Log.Debug("MovieAPI", "Response " + response.ToString());

                movies = response.results;
            }
            catch (Exception ex)
            {
                Log.Debug("MovieAPI", "Exception " + ex.Message);
            }

            return movies;
        }
    }
}
