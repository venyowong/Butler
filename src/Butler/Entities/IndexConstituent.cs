using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Entities
{
    public class IndexConstituent : IDataVersion
    {
        public string IndexCode { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public decimal Weight { get; set; }

        public DateTime Begin { get; set; }

        public string Type => "indexconstituent";

        public DateTime UpdateTime { get; set; }
    }
}
