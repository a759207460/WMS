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
using WebWMS.Core.DTO.CompanysDto;
using WebWMS.Core.Domain.Vendors;
using WebWMS.Core.DTO.VendorsDto;
using WebWMS.Core.DTO.CustomersDto;
using WebWMS.Core.Domain.Customers;
using WebWMS.Core.DTO.RolesDto;
using WebWMS.Core.Domain.Roles;
using StackExchange.Redis;

namespace WebWMS.AutoMapper
{
    public class AutoMapperProFile : Profile
    {
        public AutoMapperProFile()
        {
            CreateMap<UserInfo, UserInfoDto>().ReverseMap();
            CreateMap<UserViewModel, UserInfoDto>().ReverseMap();
            CreateMap<PagedList<UserInfoDto>, PagedList<UserInfo>>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<MenuModel, MenuDto>().ReverseMap();
            CreateMap<PagedList<MenuDto>, PagedList<Menu>>().ReverseMap();
            CreateMap<CompanyDto, Company>().ReverseMap();
            CreateMap<CompanyViewModel, CompanyDto>().ReverseMap();
            CreateMap<PagedList<CompanyDto>, PagedList<Company>>().ReverseMap();
            CreateMap<VendorInfo, VendorInfoDto>().ReverseMap();
            CreateMap<VendorViewModel, VendorInfoDto>().ReverseMap();
            CreateMap<PagedList<VendorInfoDto>, PagedList<VendorInfo>>().ReverseMap();
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<CustomerViewModel, CustomerDto>().ReverseMap();
            CreateMap<PagedList<CustomerDto>, PagedList<Customer>>().ReverseMap();
            CreateMap<RoleDto, RoleInfo>().ReverseMap();
            CreateMap<RoleViewModel, RoleDto>().ReverseMap();
            CreateMap<PagedList<RoleDto>, PagedList<RoleInfo>>().ReverseMap();

            CreateMap<UserInfoDto, ImportUserInfoDto>().ForMember(dest => dest.账号, option => option.MapFrom(c => c.Account))
               .ForMember(dest => dest.名称, option => option.MapFrom(c => c.Name))
               .ForMember(dest => dest.邮箱, option => option.MapFrom(c => c.Email))
               .ForMember(dest => dest.地址, option => option.MapFrom(c => c.Address))
               .ForMember(dest => dest.手机, option => option.MapFrom(c => c.MoblePhone))
               .ReverseMap();
            CreateMap<CompanyDto, ImportCompanyDto>().ForMember(dest => dest.公司编号, option => option.MapFrom(c => c.CompanyCode))
                .ForMember(dest => dest.公司名称, option => option.MapFrom(c => c.CompanyName))
                .ForMember(dest => dest.所在城市, option => option.MapFrom(c => c.CompanyCity))
                .ForMember(dest => dest.详细地址, option => option.MapFrom(c => c.CompanyAddress))
                .ForMember(dest => dest.负责人, option => option.MapFrom(c => c.CompanyPrincipal))
                .ForMember(dest => dest.联系方式, option => option.MapFrom(c => c.CompanyContact))
                .ReverseMap();
            CreateMap<VendorInfoDto, ImportVendorDto>().ForMember(dest => dest.供应商编号, option => option.MapFrom(c => c.VendorCode))
                .ForMember(dest => dest.供应商名称, option => option.MapFrom(c => c.VendorName))
                .ForMember(dest => dest.所在城市, option => option.MapFrom(c => c.VendorCity))
                .ForMember(dest => dest.详细地址, option => option.MapFrom(c => c.VendorAddress))
                .ForMember(dest => dest.负责人, option => option.MapFrom(c => c.VendorPrincipal))
                .ForMember(dest => dest.联系方式, option => option.MapFrom(c => c.VendorContact))
                .ReverseMap();
            CreateMap<CustomerDto, ImportCustomerDto>().ForMember(dest => dest.客户编号, option => option.MapFrom(c => c.CustomerCode))
              .ForMember(dest => dest.客户名称, option => option.MapFrom(c => c.CustomerName))
              .ForMember(dest => dest.所在城市, option => option.MapFrom(c => c.CustomerCity))
              .ForMember(dest => dest.详细地址, option => option.MapFrom(c => c.CustomerAddress))
              .ForMember(dest => dest.负责人, option => option.MapFrom(c => c.CustomerPrincipal))
              .ForMember(dest => dest.联系方式, option => option.MapFrom(c => c.CustomerContact))
              .ReverseMap();

        }
    }
}
