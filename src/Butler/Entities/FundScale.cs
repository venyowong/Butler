using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Entities
{
    public class FundScale : IDataVersion
    {
        public DateTime Date { get; set; }

        /// <summary>
        /// 期间申购份额
        /// </summary>
        public decimal Purchase { get; set; }

        /// <summary>
        /// 期间赎回份额
        /// </summary>
        public decimal Redeem { get; set; }

        /// <summary>
        /// 期末总份额
        /// </summary>
        public decimal Share { get; set; }

        /// <summary>
        /// 期末净资产
        /// </summary>
        public decimal NetNav { get; set; }

        /// <summary>
        /// 净资产变动率
        /// </summary>
        public decimal ChangeRate { get; set; }

        public string FundCode { get; set; }

        public string Type => "fundscale";

        public DateTime UpdateTime { get; set; }
    }
}
