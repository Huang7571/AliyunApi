using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunApi.Models
{
	public class PageModel
	{
		/// <summary>
		/// 规定成功的状态码
		/// </summary>
		public int code { get; set; }
		/// <summary>
		/// 规定状态信息的字段名称
		/// </summary>
		public string msg { get; set; }
		/// <summary>
		/// 规定数据总数的字段名称
		/// </summary>
		public int count { get; set; }
		/// <summary>
		/// 规定数据列表的字段名称 要返回的数据
		/// </summary>
		public List<Merchant> data { get; set; }
	}
}