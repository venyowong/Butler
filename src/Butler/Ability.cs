using Butler.Models;
using Butler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler
{
    public static class Ability
    {
        public static async Task<List<AbilityAnalysisModel>> AnalyseFund(string fundCode, params string[] types)
        {
            var result = new List<AbilityAnalysisModel>();
            var info = DataService.GetFundInfo(fundCode);
            if (info == null)
            {
                return result;
            }

            var factor = await Strategy.GetAlphaBasedOnIndustry(fundCode);
            if (factor != null)
            {
                result.Add(new AbilityAnalysisModel
                {
                    FundCode = fundCode,
                    FundName = info.FundName,
                    Type = "BasedOnIndustry",
                    TypeName = "基于行业划分",
                    Factor = factor.Value
                });
            }

            if (types?.Contains("TimingAbility") ?? false)
            {
               factor = await Strategy.GetFundTimingAbility(fundCode);
                if (factor != null)
                {
                    result.Add(new AbilityAnalysisModel
                    {
                        FundCode = fundCode,
                        FundName = info.FundName,
                        Type = "TimingAbility",
                        TypeName = "择时",
                        Factor = factor.Value
                    });
                }
            }

            return result;
        }
    }
}
