using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraries.API
{
    public class BaseRequest
    {
        public Method Method { get; set; }

        public string Route { get; set; }

        public string ContentType { get; set; } = "application/json";

        public object? Body { get; set; }
    }
}
