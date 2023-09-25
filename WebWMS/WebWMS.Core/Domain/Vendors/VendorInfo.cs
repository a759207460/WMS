using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Vendors
{
    public class VendorInfo:BaseModel
    {
        public string VendorCode { get; set; }
        public string VendorName { get; set;}
        public string VendorCity { get; set;}
        public string VendorAddress { get; set;}
        public string VendorPrincipal { get; set;}
        public string VendorContact { get; set;}
    }
}
