namespace WebWMS.Models
{
    public class CustomerViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户所在城市
        /// </summary>
        public string CustomerCity { get; set; }

        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustomerAddress { get; set; }

        /// <summary>
        ///客户负责人
        /// </summary>
        public string CustomerPrincipal { get; set; }

        public string CustomerContact { get; set; }
    }
}
