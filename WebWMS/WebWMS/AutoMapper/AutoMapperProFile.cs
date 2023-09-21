using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Users;
using WebWMS.Core.Domain.Menus;
using WebWMS.Core.DTO.UserInfosDto;
using WebWMS.Core.DTO.MenusDto;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Models;
using WebWMS.Core.DTO.Companys;
using WebWMS.Core.Domain.Companys;

namespace WebWMS.AutoMapper
{
    public class AutoMapperProFile:Profile
    {
        public AutoMapperProFile()
        {
            CreateMap<UserInfo, UserInfoDto>().ReverseMap();
            CreateMap<UserViewModel, UserInfoDto>().ReverseMap();
            CreateMap <PagedList<UserInfoDto>, PagedList<UserInfo>>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<MenuModel, MenuDto>().ReverseMap();
            CreateMap<PagedList<MenuDto>, PagedList<Menu>>().ReverseMap();
            CreateMap<CompanyViewModel, CompanyDto>().ReverseMap();
            CreateMap<PagedList<CompanyDto>, PagedList<Company>>().ReverseMap();
        }
    }
}
