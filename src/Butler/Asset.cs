using Butler.Daos;
using Butler.Models;
using Butler.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Butler
{
    public static class Asset
    {
        public static bool AddFundPosition(string fundCode, decimal avgCost, decimal share)
        {
            if (string.IsNullOrWhiteSpace(fundCode))
            {
                Log.Warning("Asset.AddFundPosition 指令 fundcode 不能为空");
                return false;
            }
            if (avgCost <= 0)
            {
                Log.Warning("Asset.AddFundPosition 指令 avgCost 必须为正数");
                return false;
            }

            var position = AssetDao.GetFundPosition(fundCode);
            if (position == null)
            {
                if (share <= 0)
                {
                    Log.Warning("Asset.AddFundPosition 指令 share 必须为正数");
                    return false;
                }

                if (AssetDao.AddFundPosition(new Entities.FundPosition
                {
                    FundCode = fundCode,
                    AvgCost = avgCost,
                    Share = share
                }) > 0)
                {
                    Log.Information($"添加持仓成功，fundcode: {fundCode}, avgCost: {avgCost}, share: {share}");
                    return true;
                }
                else
                {
                    Log.Warning($"添加持仓失败，fundcode: {fundCode}, avgCost: {avgCost}, share: {share}");
                    return false;
                }
            }
            else
            {
                if (share > 0)
                {
                    var amount = position.AvgCost * position.Share + avgCost * share;
                    position.Share += share;
                    position.AvgCost = amount / position.Share;
                }
                else
                {
                    if (position.Share + share < 0)
                    {
                        Log.Warning($"添加持仓失败，fundcode: {fundCode}, avgCost: {avgCost}, share: {share}, 减仓份额大于现有份额 {position.Share}");
                        return false;
                    }
                    var amount = position.AvgCost * position.Share + avgCost * share;
                    position.Share += share;
                    position.AvgCost = amount / position.Share;
                }
                if (AssetDao.UpdateFundPosition(position) > 0)
                {
                    Log.Information($"添加持仓成功，fundcode: {fundCode}, avgCost: {avgCost}, share: {share}, 现有份额: {position.Share}, 摊薄成本: {position.AvgCost}");
                    return true;
                }
                else
                {
                    Log.Warning($"添加持仓失败，fundcode: {fundCode}, avgCost: {avgCost}, share: {share}");
                    return false;
                }
            }
        }

        public static bool RemoveFundPosition(string fundCode) => AssetDao.DeleteFundPosition(fundCode) > 0;

        public static async Task<AssetAnalysisModel> Analyse()
        {
            decimal total = 0, stock = 0, bond = 0, cash = 0;
            var funds = new List<FundAnalysisModel>();
            var positions = AssetDao.GetAllPositions();
            foreach (var item in positions)
            {
                if (item.Share <= 0)
                {
                    continue;
                }

                var nav = (await DataService.GetFundNavs(item.FundCode))?.LastOrDefault();
                if (nav == null)
                {
                    Log.Warning($"{item.FundCode} 无法获取到净值数据，无法进行数据分析");
                    continue;
                }
                var config = DataService.GetFundAssetConfigs(item.FundCode)?.LastOrDefault();
                if (config == null)
                {
                    Log.Warning($"{item.FundCode} 无法获取到资产配置，无法进行数据分析");
                    continue;
                }
                Log.Information($"{item.FundCode} 最新的净值日期为 {nav.Date}");
                Log.Information($"{item.FundCode} 最新的报告期为 {config.Date}");

                var net = item.Share * nav.UnitNav;
                total += net;
                stock += net * config.StockRatio;
                bond += net * config.BondRatio;
                cash += net * config.CashRatio;

                var info = DataService.GetFundInfo(item.FundCode);
                funds.Add(new FundAnalysisModel
                {
                    FundCode = item.FundCode,
                    FundName = info?.FundName,
                    AvgCost = item.AvgCost,
                    FundNav = nav.UnitNav,
                    NavDate = nav.Date,
                    PositionProfit = item.Share * (nav.UnitNav - item.AvgCost),
                    MarketValue = item.Share * nav.UnitNav,
                    PositionProfitRate = nav.UnitNav / item.AvgCost - 1,
                    Share = item.Share,
                    Abilities = await Ability.AnalyseFund(item.FundCode)
                });
            }

            return new AssetAnalysisModel
            {
                TotalAsset = total,
                TotalCost = funds.Sum(x => x.Share * x.AvgCost),
                BondAsset = bond,
                CashAsset = cash,
                StockAsset = stock,
                Funds = funds.OrderByDescending(x => x.PositionProfitRate).ToList()
            };
        }

        public static void Clear()
        {
            if (AssetDao.Clear() > 0)
            {
                Log.Information("清理资产数据成功");
            }
            else
            {
                Log.Warning("清理资产数据失败");
            }
        }
    }
}
