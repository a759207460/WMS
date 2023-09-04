namespace WebWMS.Models
{
    public class MenuViewModel
    {
        public List<MenuModel> FirstMenu { get; set; }

        public List<MenuModel> ChlidrenMenu { get; set; }

    }

    public class MenuModel
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string NavigateController { get; set; }

        public string NavigateActioin { get; set; } = "Index";

        public string ParentName { get; set; }

        public string? Url { get; set; }

        public string Tag { get; set; }

        public bool HasChildren { get; set; }

        public string Style { get; set; }

        public string HeadStyle { get; set; }
    }
}
