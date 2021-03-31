using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Entities
{
    public class AssetSnapshot
    {
        [PrimaryKey]
        public DateTime Date { get; set; }

        public decimal TotalAsset { get; set; }

        public decimal TotalCost { get; set; }

        public decimal StockAsset { get; set; }

        public decimal BondAsset { get; set; }

        public decimal CashAsset { get; set; }
    }
}
