using Butler.Common.Extensions;
using Butler.Entities;
using Butler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Helpers
{
    public static class MathHelper
    {
        public static List<IncreaseRate> GetIncreaseRates(List<IndexQuotation> quotations)
        {
            if (quotations.IsNullOrEmpty())
            {
                return new List<IncreaseRate>();
            }

            var result = new List<IncreaseRate>();
            for (int i = 1; i < quotations.Count; i++)
            {
                result.Add(new IncreaseRate
                {
                    Date = quotations[i].Date,
                    Rate = quotations[i].Close / quotations[i - 1].Close - 1
                });
            }
            return result;
        }
    }
}
