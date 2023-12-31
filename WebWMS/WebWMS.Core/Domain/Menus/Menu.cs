﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Roles;

namespace WebWMS.Core.Domain.Menus
{
    public class Menu:BaseModel
    {
        #region Property
         
        public ICollection<MenuRole> Roles { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
         
        /// <summary>
        /// 菜单标题
        /// </summary>
        public string Title { get; set; }
         
        /// <summary>
        /// 导航控制器名称
        /// </summary>
        public string? NavigateController { get; set; }
         
        /// <summary>
        /// 导航方法名称
        /// </summary>
        public string? NavigateActioin { get; set; }

        /// <summary>
        /// 父类菜单ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 父类菜单名称
        /// </summary>
        public string ParentName { get; set; }
         
        /// <summary>
        /// 导航Url地址
        /// </summary>
        public string? Url { get; set; }
        
        /// <summary>
        /// 图标
        /// </summary>
        public string? Tag { get; set; }

        /// <summary>
        /// 是否有子菜单
        /// </summary>
        public bool HasChildren { get; set; }

        /// <summary>
        /// 子标签样式
        /// </summary>
        public string? Style { get; set; }

        /// <summary>
        /// 父标签样式
        /// </summary>
        public string? HeadStyle { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Sort { get; set; }

        public string? Creator { get; set;}

        #endregion
    }
}
