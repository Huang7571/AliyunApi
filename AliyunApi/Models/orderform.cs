using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunApi.Models
{
    /// <summary>
    /// 商品订单表
    /// </summary>
    public class orderform
    {
        /// <summary>
        /// 自增主键ID
        /// </summary>
        public int OF_Id { get; set; }
        /// <summary>
        /// 商品id（外键）
        /// </summary>
        public int Shop_Sid { get; set; }
        /// <summary>
        /// 商品的处理时间
        /// </summary>
        public DateTime OF_datetime { get; set; }
        /// <summary>
        /// 订单个数
        /// </summary>
        public int OF_Count { get; set; }
        /// <summary>
        /// 订单总额
        /// </summary>
        public decimal OF_Money { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public int OF_status { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string OF_consignee { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string OF_phonenumber { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string OF_site { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string OF_Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime OF_establishdatetime { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime OF_paymentdatetime { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string transactions { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string OF_paymode { get; set; }
        /// <summary>
        /// 支付流水
        /// </summary>
        public string OF_paywater { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal OF_freight { get; set; }
        /// <summary>
        /// 外键，关联物流（外键）
        /// </summary>
        public int LG_Id { get; set; }
        /// <summary>
        /// 商家表(外键)
        /// </summary>
        public int MerchantId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Sname { get; set; }
        /// <summary>
        /// 商品标题
        /// </summary>
        public string Stitle { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string Simg { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Sprice { get; set; }
        /// <summary>
        /// 商品颜色
        /// </summary>
        public string Scolor { get; set; }
        /// <summary>
        /// 商品尺码
        /// </summary>
        public string Ssnum { get; set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Sdescribe { get; set; }
    }
}