using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmooviesPCL.Models
{
    public class Config
    {

        public Images Images { get; set; }
    }
    public class Images
    {
        public string base_url { get; set; }
        public string[] backdrop_sizes { get; set; }
        public string[] poster_sizes { get; set; }
    }
}



/*images
object
optional
base_url
string
optional
secure_base_url
string
optional
backdrop_sizes
array[string]
optional
logo_sizes
array[string]
optional
poster_sizes
array[string]
optional
profile_sizes
array[string]
optional
still_sizes
array[string]
optional
change_keys
array[string]*/
