using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Entities
{
    public class FundAssetConfig : IDataVersion
    {
        public string FundCode { get; set; }

        public DateTime Date { get; set; }

        public decimal StockRatio { get; set; }

        public decimal BondRatio { get; set; }

        public decimal CashRatio { get; set; }

        public decimal NetAsset { get; set; }

        public string Type => "fundassetconfig";

        public DateTime UpdateTime { get; set; }
    }
}
