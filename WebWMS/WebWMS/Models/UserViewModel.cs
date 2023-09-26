using System.ComponentModel.DataAnnotations;
using WebWMS.Core.DTO.RolesDto;
using WebWMS.Core.DTO.UserInfosDto;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Models
{
    public class UserViewModel
    {
        public IPagedList<UserInfoDto>? PagedList { get; set; }

        public List<RoleDto> RoleList { get; set; }

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
