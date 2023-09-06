using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO
{
    public class BaseDto
    {
        public int Id { get; set; }

        public string CreateTime { get; set; }

        public string? UpdateTime { get; set; }
    }
}
