using System.ComponentModel.DataAnnotations;
using WebWMS.Core.DTO.Customers;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Models
{
    public class UserViewModel
    {
        public IPagedList<CustomerDto>? PagedList { get; set; }

        public int Id { get; set; }
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string MoblePhone { get; set; }
        public string? Address { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsRemove { get; set; }

    }
}
