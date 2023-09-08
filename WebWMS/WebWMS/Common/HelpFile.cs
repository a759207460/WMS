using FileHelpers;
using WebWMS.Core.Domain.Customers;
using WebWMS.Models;

namespace WebWMS.Common
{
    public class HelpFile
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T[] ReadFile<T>(string path) where T : class
        {
            var read= new FileHelperEngine<T>();
            var dic=read.ReadFile(path);
            return dic;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void WriteFile<T>(string path,IEnumerable<T> list) where T : class
        {
            var wirte = new FileHelperEngine<T>();
            wirte.WriteFile(path, list);
        }
    }
}
