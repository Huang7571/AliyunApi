using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunApi.Models
{
    /// <summary>
    /// 用于返回数据
    /// </summary>
    public class ReturnModel
    {
		/// <summary>
		/// 大黄
		/// </summary>
        public List<Shop> list { get; set; }
        public int TotalCount { get; set; }
    }
}