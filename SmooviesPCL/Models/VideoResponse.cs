using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmooviesPCL.Models
{
    public class Video
    {
        public string id { get; set; }
        public string iso_639_1 { get; set; }
        public string iso_3166_1 { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string site { get; set; }
        public int size { get; set; }
        public string type { get; set; }

        public override string ToString()
        {
            return "id=" + id + "\nname=" + name + "\nsize=" + size + "\nsize=" + size + "\ntype=" + type;
        }
    }

    public class VideoResponse
    {
        public string id { get; set; }
        public List<Video> results { get; set; }
        public override string ToString()
        {
            return "id=" + id + "\n" + ((results != null) ? "results count:" + results.Count : "results: NULL");
        }
    }
}
