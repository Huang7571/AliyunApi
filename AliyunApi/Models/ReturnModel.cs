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
        public List<Shop> list { get; set; }
        public int TotalCount { get; set; }
    }
}