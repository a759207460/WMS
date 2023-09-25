namespace WebWMS.Models
{
    public class VendorViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string VendorCode { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// 公司所在城市
        /// </summary>
        public string VendorCity { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string VendorAddress { get; set; }

        /// <summary>
        ///公司负责人
        /// </summary>
        public string VendorPrincipal { get; set; }

        public string VendorContact { get; set; }
    }
}
