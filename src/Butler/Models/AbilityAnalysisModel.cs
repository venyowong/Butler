using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Models
{
    public class AbilityAnalysisModel : IDescription
    {
        public string FundCode { get; set; }

        public string FundName { get; set; }

        public string Type { get; set; }

        public string TypeName { get; set; }

        public double Factor { get; set; }

        public string GetDescription() => $"{FundName}({FundCode}) {TypeName}的能力分析，得分为 {Factor}";
    }
}
