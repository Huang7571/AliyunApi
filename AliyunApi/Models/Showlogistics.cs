using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunApi.Models
{
    public class Showlogistics
    {
        /// <summary>
        /// 规定成功的状态码，默认：0
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 规定状态信息的字段名称，默认：msg  
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 规定数据总数的字段名称，默认：count   数据总条数
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 规定数据列表的字段名称，默认：data  要返回的数据
        /// </summary>
        public List<logistics> data { get; set; }
    }
}