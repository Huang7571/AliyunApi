using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunApi.Models
{
	public class Merchant//商家表
	{
		/// <summary>
		/// 商家编号
		/// </summary>
		public int MerchantId		 { get; set; }
		/// <summary>
		/// 商家名称
		/// </summary>
		public string MerchantName	 { get; set; }
		/// <summary>
		/// 商家邮箱
		/// </summary>
		public string MerchantEmail	 { get; set; }
		/// <summary>
		/// 商家密码
		/// </summary>
		public string MerchantPwd		 { get; set; }
		/// <summary>
		/// 商家备注
		/// </summary>
		public string MerchantContent	 { get; set; }
		/// <summary>
		/// 权限Id（关联权限表）
		/// </summary>
		public int Aid		         { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public int MerchantState	 { get; set; }

	}
}