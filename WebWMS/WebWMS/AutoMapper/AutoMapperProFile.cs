﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Customers;
using WebWMS.Core.Domain.Menus;
using WebWMS.Core.DTO.Customers;
using WebWMS.Core.DTO.MenusDto;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Models;

namespace WebWMS.AutoMapper
{
    public class AutoMapperProFile:Profile
    {
        public AutoMapperProFile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<UserViewModel, CustomerDto>().ReverseMap();
            CreateMap <PagedList<CustomerDto>, PagedList<Customer>>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<MenuModel, MenuDto>().ReverseMap();
            CreateMap<PagedList<MenuDto>, PagedList<Menu>>().ReverseMap();
        }
    }
}
