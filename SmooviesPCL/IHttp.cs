using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmooviesPCL
{
    public interface IHttp
    {
        string GET(string url);

        string POST(string url, object data);
    }
}
