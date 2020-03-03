using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunApi.Models
{
    /// <summary>
    /// 商品表
    /// </summary>
    public class Shop
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Sid { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public int Stype { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Sname { get; set; }
        /// <summary>
        /// 商品标题
        /// </summary>
        public string Stitle { get; set; }
        /// <summary>
        /// 商品照片
        /// </summary>
        public string Simg { get; set; }
        /// <summary>
        /// 商品规格
        /// </summary>
        public bool Ssize { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Sprice { get; set; }
        /// <summary>
        /// 商品积分
        /// </summary>
        public decimal Sscore { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Sdescribe { get; set; }
        /// <summary>
        /// 售后信息
        /// </summary>
        public bool Safter { get; set; }
        /// <summary>
        /// 单选按钮  三种类型（立即上架，定时上架，加入库存）
        /// </summary>
        public string Stime { get; set; }
        /// <summary>
        /// 上架，下架
        /// </summary>
        public bool Sstate { get; set; }
        /// <summary>
        /// 审核状态	通过，未通过
        /// </summary>
        public bool Saudit { get; set; }
        /// <summary>
        /// 外键（商品大类表）
        /// </summary>
        public int Btid { get; set; }
        /// <summary>
        /// 外键（商品小类表）
        /// </summary>
        public int Stid { get; set; }
        /// <summary>
        /// 外键（商品规格表）
        /// </summary>
        public int Ssid { get; set; }
        /// <summary>
        /// 商家ID
        /// </summary>
        public int MerchantId { get; set; }

    }
}