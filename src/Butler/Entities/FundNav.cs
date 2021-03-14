using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Entities
{
    public class FundNav : IDataVersion
    {
        public string FundCode { get; set; }

        public decimal UnitNav { get; set; }

        public decimal AccUnitNav { get; set; }

        public DateTime Date { get; set; }

        public string Type => "fundnav";

        public DateTime UpdateTime { get; set; }
    }
}
