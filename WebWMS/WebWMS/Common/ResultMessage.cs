namespace WebWMS.Common
{
    public class ResultMessage
    {
        public string? Message { get; set; }

        public int Status { get; set; }

        public Object? Source { get; set; }
    }

    public class ResultMessage<T>
    {
        public string? Message { get; set; }

        public int Status { get; set; }

        public T? Source { get; set; }
    }
}
