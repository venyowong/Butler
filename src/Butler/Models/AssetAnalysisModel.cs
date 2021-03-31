using Butler.Entities;
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
            get => TotalAsset - TotalCost;
        }

        public decimal TotalProfitRate
        {
            get => TotalProfit / TotalCost;
        }

        public decimal StockAsset { get; set; }

        public decimal StockRatio
        {
            get => StockAsset / TotalAsset;
        }

        public decimal BondAsset { get; set; }

        public decimal BondRatio
        {
            get => BondAsset / TotalAsset;
        }

        public decimal CashAsset { get; set; }

        public decimal CashRatio
        {
            get => CashAsset / TotalAsset;
        }

        public List<FundAnalysisModel> Funds { get; set; }

        public AssetSnapshot LastSnapshot { get; set; }

        public string GetDescription()
        {
            if (TotalAsset <= 0)
            {
                return "账户未持有资产";
            }

            var description = $"账户投入成本为 {TotalCost.ToString("F2")} 元";
            if (LastSnapshot?.TotalCost <= TotalCost)
            {
                description += $"\n昨日收益：{TotalProfit - (LastSnapshot.TotalAsset - LastSnapshot.TotalCost)}";
            }
            description += '\n' + $@"目前盈利 {TotalProfit.ToString("F2")}元，实现 {TotalProfitRate.ToString("P")} 收益率，总资产为 {TotalAsset.ToString("F2")}元，其中
股票资产有 {StockAsset.ToString("F2")}元，占 {StockRatio.ToString("P")}
债券资产有 {BondAsset.ToString("F2")}元，占 {BondRatio.ToString("P")}
现金资产有 {CashAsset.ToString("F2")}元，占 {CashRatio.ToString("P")}";
            if (Funds?.Any() ?? false)
            {
                var first = Funds.First();
                description += $"\n目前持仓中最赚钱的基金是 {first.FundName}，收益率达到 {first.PositionProfitRate.ToString("P")}，基金收益明细如下：";
                Funds.ForEach(x =>
                {
                    description += $"\n\n{x.GetDescription()}";
                });
            }
            description += $"\n\n以上分析结果可能与真实结果存在差异，仅作参考使用，若用户设有定投计划，可将相应资产删除，重新导入数据";
            return description;
        }
    }
}
