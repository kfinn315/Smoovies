namespace SmooviesPCL.Models
{
    public class Movie
    {
        public int id { get; set; }
        public int vote_count { get; set; }
        public float vote_average { get; set; }
        public string title { get; set; }
        public string release_date { get; set; }
        public float popularity { get; set; }
        public string overview { get; set; }
        public string backdrop_path { get; set; }
        public string poster_path { get; set; }
        public bool video { get; set; }

        public override string ToString()
        {
            return "title: " + title + "\nid: " + id + "\noverview: " + overview;
        }
    }
}
