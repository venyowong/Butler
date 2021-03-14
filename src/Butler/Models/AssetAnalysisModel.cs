using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Butler.Models
{
    public class AssetAnalysisModel : IDescription
    {
        public decimal TotalAsset { get; set; }

        public decimal TotalCost { get; set; }

        public decimal TotalProfit
        {
            get => this.TotalAsset - this.TotalCost;
        }

        public decimal TotalProfitRate
        {
            get => this.TotalProfit / this.TotalCost;
        }

        public decimal StockAsset { get; set; }

        public decimal StockRatio
        {
            get => this.StockAsset / this.TotalAsset;
        }

        public decimal BondAsset { get; set; }

        public decimal BondRatio
        {
            get => this.BondAsset / this.TotalAsset;
        }

        public decimal CashAsset { get; set; }

        public decimal CashRatio
        {
            get => this.CashAsset / this.TotalAsset;
        }

        public List<FundAnalysisModel> Funds { get; set; }

        public string GetDescription()
        {
            if (this.TotalAsset <= 0)
            {
                return "账户未持有资产";
            }

            var description = $@"账户投入成本为 {this.TotalCost.ToString("F2")} 元，目前盈利 {this.TotalProfit.ToString("F2")}元，实现 {this.TotalProfitRate.ToString("P")} 收益率，总资产为 {this.TotalAsset.ToString("F2")}元，其中
股票资产有 {this.StockAsset.ToString("F2")}元，占 {this.StockRatio.ToString("P")}
债券资产有 {this.BondAsset.ToString("F2")}元，占 {this.BondRatio.ToString("P")}
现金资产有 {this.CashAsset.ToString("F2")}元，占 {this.CashRatio.ToString("P")}";
            if (this.Funds?.Any() ?? false)
            {
                var first = this.Funds.First();
                description += $"\n目前持仓中最赚钱的基金是 {first.FundName}，收益率达到 {first.PositionProfitRate.ToString("P")}，基金收益明细如下：";
                this.Funds.ForEach(x =>
                {
                    description += $"\n\n{x.GetDescription()}";
                });
            }
            description += $"\n\n以上分析结果可能与真实结果存在差异，仅作参考使用，若用户设有定投计划，可将相应资产删除，重新导入数据";
            return description;
        }
    }
}
