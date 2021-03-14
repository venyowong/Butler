using Butler.Daos;
using Butler.Entities;
using Butler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Services
{
    public static class DataService
    {
        public static async Task<List<FundNav>> GetFundNavs(string fundCode)
        {
            var navs = DataDao.GetFundNavs(fundCode);
            if (DataHelper.IsDataReady(navs?.LastOrDefault()))
            {
                return navs;
            }
            else
            {
                navs = await EmService.GetFundNavs(fundCode);
                DataDao.UpdateFundNavs(navs);
                return navs;
            }
        }

        public static List<FundAssetConfig> GetFundAssetConfigs(string fundCode)
        {
            var configs = DataDao.GetFundAssetConfigs(fundCode);
            if (DataHelper.IsDataReady(configs?.LastOrDefault()))
            {
                return configs;
            }
            else
            {
                configs = EmService.GetFundAssetConfigs(fundCode);
                DataDao.UpdateFundAssetConfigs(configs);
                return configs;
            }
        }

        public static async Task<List<FundScale>> GetFundScales(string fundCode)
        {
            var scales = DataDao.GetFundScales(fundCode);
            if (DataHelper.IsDataReady(scales?.LastOrDefault()))
            {
                return scales;
            }
            else
            {
                scales = await EmService.GetFundScales(fundCode);
                DataDao.UpdateFundScales(scales);
                return scales;
            }
        }

        public static FundInfo GetFundInfo(string fundCode)
        {
            var info = DataDao.GetFundInfo(fundCode);
            if (DataHelper.IsDataReady(info))
            {
                return info;
            }
            else
            {
                var noData = info == null;
                info = EmService.GetFundInfo(fundCode);
                if (noData)
                {
                    DataDao.InsertFundInfo(info);
                }
                else
                {
                    DataDao.UpdateFundInfo(info);
                }
                return info;
            }
        }

        public static async Task<List<FundStockPosition>> GetFundStockPositions(string fundCode, DateTime date)
        {
            var positions = DataDao.GetFundStockPositions(fundCode, date);
            if (DataHelper.IsDataReady(positions?.FirstOrDefault()))
            {
                return positions;
            }
            else
            {
                positions = await EmService.GetFundStockPositions(fundCode, date);
                DataDao.UpdateFundStockPositions(positions);
                return positions;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="source">数据来源 0 数据库 1 接口</param>
        /// <returns></returns>
        public static async Task<List<IndexQuotation>> GetIndexQuotations(string indexCode, DateTime begin, DateTime end, int source = 1)
        {
            var quotations = DataDao.GetIndexQuotations(indexCode, begin, end);
            if (DataHelper.IsDataReady(quotations?.OrderBy(x => x.UpdateTime).FirstOrDefault()))
            {
                return quotations;
            }
            else if (source == 1)
            {
                quotations = await SwService.GetIndexQuotations(begin, end, indexCode);
                DataDao.UpdateIndexQuotations(quotations);
                return quotations;
            }
            else
            {
                return new List<IndexQuotation>();
            }
        }

        public static async Task<List<IndexConstituent>> GetIndexConstituents(string indexCode)
        {
            var constituents = DataDao.GetIndexConstituents(indexCode);
            if (DataHelper.IsDataReady(constituents?.FirstOrDefault()))
            {
                return constituents;
            }
            else
            {
                constituents = await SwService.GetIndexConstituents(indexCode);
                DataDao.UpdateIndexConstituents(constituents);
                return constituents;
            }
        }
    }
}
