using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.VendorsDto
{
    public class VendorInfoDto:BaseDto
    {
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string VendorCity { get; set; }
        public string VendorAddress { get; set; }
        public string VendorPrincipal { get; set; }
        public string VendorContact { get; set; }
    }
}
