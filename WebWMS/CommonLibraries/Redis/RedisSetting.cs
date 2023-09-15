using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraries.Redis
{
    public class RedisSetting
    {
        public string Ip { get; set; }

        public int Port { get; set; }

        public int DataBase { get; set; }

        public string ?PassWord { get; set; }

        public string? User { get; set; }
    }
}
