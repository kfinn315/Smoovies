using SmooviesPCL.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmooviesPCL.Models
{
    public class Response
    {
        public Response()
        {
            results = new List<Movie>();
        }
        public int page { get; set; }

        public List<Movie> results{ get; set; }

        public int total_pages { get; set; }

        public int total_results{ get; set; }


        public override string ToString()
        {
            return "total_results: " + total_results + "\ntotal_pages " + total_pages + ((results != null) ? "results count:" + results.Count : "results: NULL");
        }
    }
}
