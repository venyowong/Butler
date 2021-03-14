using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Entities
{
    public class FundPosition
    {
        [PrimaryKey]
        public string FundCode { get; set; }

        public decimal AvgCost { get; set; }

        public decimal Share { get; set; }
    }
}
