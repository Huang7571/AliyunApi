using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunApi.Models
{
    /// <summary>
    /// 商品规划表
    /// </summary>
    public class ShopSize
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Ssid { get; set; }
        /// <summary>
        /// 商品颜色
        /// </summary>
        public string Sscolor { get; set; }
        /// <summary>
        /// 商品尺码
        /// </summary>
        public string Sssize { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Sssnum { get; set; }
        /// <summary>
        /// 库存量
        /// </summary>
        public int Ssstock { get; set; }
    }
}