using FileHelpers;

namespace WebWMS.Models
{
    [DelimitedRecord(",")]
    public class FileAccountModel
    {
        public string Account { get; set; }

        public string Password { get; set; }


        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd hh:mm:ss")]
        public DateTime CreatTime { get; set; }
    }
}
