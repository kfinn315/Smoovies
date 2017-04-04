using Android.OS;
using Android.Util;
using SmooviesPCL.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmooviesPCL
{
    public class FavoriteDatasource
    {

        private string _dbPath;

        public FavoriteDatasource(string dbPath)
        {
            _dbPath = dbPath;


        }
        private async Task<bool> CreateTable()
        {
            var conn = new SQLiteAsyncConnection(_dbPath);

            try
            {
                CreateTablesResult rs = await conn.CreateTableAsync<Movie>();
                Log.Debug("CreateTable", "returned "+ (rs.Results.ToString()));

                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Debug("CreateTable", "Exception " + ex.Message);
                return false;
            }
        }

        public async Task<bool> IsFavorite(string movieid)
        {
            try
            {
                await CreateTable();
                var db = new SQLite.SQLiteAsyncConnection(_dbPath);
                List<Movie> results = await db.QueryAsync<Movie>("Select _id FROM Movie WHERE _id = " + movieid);
                Log.Debug("IsFavorite", "movieid '"+movieid+"' returned "+results.Count+"results");

                return results.Count > 0;
            }
            catch (SQLiteException ex)
            {
                Log.Debug("IsFavorite", "Exception " + ex.Message);
                return false;
            }
        }

        public async Task<List<Movie>> GetFavorites()
        {
            try
            {
                await CreateTable();
                var db = new SQLite.SQLiteAsyncConnection(_dbPath);
                //                    var db = new SQLiteAsyncConnection(_dbPath);
                List<Movie> results = await db.QueryAsync<Movie>("Select * FROM Movie");

                string logResult = "";
                foreach (Movie result in results)
                {
                    logResult += result.id+"-"+result.title + ",";
                }
                Log.Debug("GetFavorites", "Returned " + logResult);

                return results;
            }
            catch (SQLiteException ex)
            {
                Log.Debug("Get Favorites", "Exception " + ex.Message);
                return new List<Movie>();
            }
        }

        public async Task<string> InsertFavorite(Movie movie)
        {
            try
            {
                var db = new SQLiteAsyncConnection(_dbPath);
                int retval = await db.InsertAsync(movie);
                Log.Debug("Insert Favorites", "returned "+retval);
                return retval.ToString();
            }
            catch (SQLiteException ex)
            {
                Log.Debug("Insert Favorites", "Exception " + ex.Message);
                return ex.Message;
            }
        }

        public async Task<string> RemoveFavorite(Movie movie)
        {
            try
            {
                await CreateTable();
                var db = new SQLiteAsyncConnection(_dbPath);
                int retval = await db.DeleteAsync(movie);
                Log.Debug("Remove Favorites", "movie "+movie.title+" returned " + retval);
                return retval.ToString();
            }
            catch (SQLiteException ex)
            {
                Log.Debug("Remove Favorites", "Exception " + ex.Message);
                return ex.Message;
            }
        }

    }
}
