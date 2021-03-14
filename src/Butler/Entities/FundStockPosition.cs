using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Entities
{
    public class FundStockPosition : IDataVersion
    {
        public string StockCode { get; set; }

        public string StockName { get; set; }

        public decimal Ratio { get; set; }

        /// <summary>
        /// 持股数
        /// </summary>
        public decimal Share { get; set; }

        public decimal MarketValue { get; set; }

        public DateTime ReportDate { get; set; }

        public string FundCode { get; set; }

        public string Type => "fundstockposition";

        public DateTime UpdateTime { get; set; }
    }
}
