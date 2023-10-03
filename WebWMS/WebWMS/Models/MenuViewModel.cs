namespace WebWMS.Models
{
    public class MenuViewModel
    {
        public List<MenuModel> FirstMenu { get; set; }

        public List<MenuModel> ChlidrenMenu { get; set; }

    }

    public class MenuModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string? NavigateController { get; set; }

        public string? NavigateActioin { get; set; }

        public string ParentName { get; set; } = "Root";

        public string? Url { get; set; }

        public string? Tag { get; set; }

        public bool HasChildren { get; set; }

        public string Style { get; set; } = "nav nav-list collapse";

        public string HeadStyle { get; set; } = "nav-header collapsed";

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int ?Sort { get; set; }
    }
}
