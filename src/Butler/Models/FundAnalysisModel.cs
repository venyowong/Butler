using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Butler.Models
{
    public class FundAnalysisModel : IDescription
    {
        public string FundCode { get; set; }

        public string FundName { get; set; }

        /// <summary>
        /// 持仓收益
        /// </summary>
        public decimal PositionProfit { get; set; }

        /// <summary>
        /// 持仓收益率
        /// </summary>
        public decimal PositionProfitRate { get; set; }

        /// <summary>
        /// 最新净值
        /// </summary>
        public decimal FundNav { get; set; }

        /// <summary>
        /// 净值日期
        /// </summary>
        public DateTime NavDate { get; set; }

        public decimal Share { get; set; }

        public decimal AvgCost { get; set; }

        public decimal MarketValue { get; set; }

        public List<AbilityAnalysisModel> Abilities { get; set; }

        public string GetDescription()
        {
            var description =  $"{FundName}({FundCode}) 以 {AvgCost.ToString("F4")} 的价格持有 {Share.ToString("F2")} 份，" +
                $"根据 {NavDate.ToString("yyyy-MM-dd")} 的净值({FundNav.ToString("F4")}) 计算，" +
                $"市值达 {MarketValue.ToString("F2")}，盈利 {PositionProfit.ToString("F2")}，" +
                $"实现 {PositionProfitRate.ToString("P")} 收益率";
            if (Abilities?.Any() ?? false)
            {
                Abilities.ForEach(a => description += $"\n{a.GetDescription()}");
            }
            return description;
        }
    }
}
