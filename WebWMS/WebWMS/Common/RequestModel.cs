namespace WebWMS.Common
{
    public class RequestModel
    {
        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = 10;

        public string? Where { get; set; }
    }

    public class RequestModel<T>
    {
        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = 10;

        public T? Where { get; set; }
    }
}
