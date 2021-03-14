using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Butler.Models
{
    public class MarketAnalysisModel : IDescription
    {
        public List<StyleInflection> StyleInflections { get; set; }

        public string GetDescription()
        {
            if (!this.StyleInflections.Any())
            {
                return "当前市场无明显异常点";
            }

            var description = string.Empty;
            foreach (var s in this.StyleInflections)
            {
                if (string.IsNullOrWhiteSpace(description))
                {
                    description = s.GetDescription();
                }
                else
                {
                    description += $"\n{s.GetDescription()}";
                }
            }
            return description;
        }
    }
}
