using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Entities
{
    /// <summary>
    /// 指数行情数据
    /// </summary>
    public class IndexQuotation : IDataVersion
    {
        public string IndexCode { get; set; }

        public string IndexName { get; set; }

        public DateTime Date { get; set; }

        public decimal Open { get; set; }

        public decimal Close { get; set; }

        public decimal Max { get; set; }

        public decimal Min { get; set; }

        /// <summary>
        /// 成交量
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// 成交额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 涨跌幅
        /// </summary>
        public decimal Markup { get; set; }

        /// <summary>
        /// 涨跌额
        /// </summary>
        public decimal? MarkupAmount { get; set; }

        public string Type => "indexquotation";

        public DateTime UpdateTime { get; set; }
    }
}
