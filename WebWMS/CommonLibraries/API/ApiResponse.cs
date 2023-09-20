using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraries.API
{
    public class ApiResponse
    {
        public string? Message { get; set; }

        public bool Status { get; set; }

        public object? Resulst { get; set; }
    }

    public class ApiResponse<T>
    {
        public string? Message { get; set; }

        public bool Status { get; set; }

        public T? Resulst { get; set; }
    }
}
