using Butler.Entities;
using Butler.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Butler.Daos
{
    public static class DataDao
    {
        private static SQLiteConnection _db;

        static DataDao()
        {
            DbHelper.EnsureDbFolderExists();
            _db = new SQLiteConnection("dbs/data.db", false);
            _db.CreateTable<FundNav>();
            _db.CreateTable<FundAssetConfig>();
            _db.CreateTable<FundScale>();
            _db.CreateTable<FundInfo>();
            _db.CreateTable<FundStockPosition>();
            _db.CreateTable<IndexQuotation>();
            _db.CreateTable<IndexConstituent>();
            _db.CreateTable<FileVersion>();
            _db.CreateTable<FundStyleExposure>();
        }

        public static List<FundNav> GetFundNavs(string fundCode) => _db.Query<FundNav>(
            $"SELECT * FROM FundNav WHERE FundCode='{fundCode}' ORDER BY Date");

        public static bool UpdateFundNavs(List<FundNav> navs)
        {
            if (navs == null || !navs.Any())
            {
                return false;
            }

            var fundCode = navs.First().FundCode;
            _db.Execute($"DELETE FROM FundNav WHERE FundCode='{fundCode}'");
            navs.ForEach(x => x.UpdateTime = DateTime.Now);
            return _db.InsertAll(navs) > 0;
        }

        public static List<FundAssetConfig> GetFundAssetConfigs(string fundCode) => _db.Query<FundAssetConfig>(
            $"SELECT * FROM FundAssetConfig WHERE FundCode='{fundCode}' ORDER BY Date");

        public static bool UpdateFundAssetConfigs(List<FundAssetConfig> configs)
        {
            if (configs == null || !configs.Any())
            {
                return false;
            }

            var fundCode = configs.First().FundCode;
            _db.Execute($"DELETE FROM FundAssetConfig WHERE FundCode='{fundCode}'");
            configs.ForEach(x => x.UpdateTime = DateTime.Now);
            return _db.InsertAll(configs) > 0;
        }

        public static List<FundScale> GetFundScales(string fundCode) => _db.Query<FundScale>(
            $"SELECT * FROM FundScale WHERE FundCode='{fundCode}' ORDER BY Date");

        public static bool UpdateFundScales(List<FundScale> scales)
        {
            if (scales == null || !scales.Any())
            {
                return false;
            }

            var fundCode = scales.First().FundCode;
            _db.Execute($"DELETE FROM FundScale WHERE FundCode='{fundCode}'");
            scales.ForEach(x => x.UpdateTime = DateTime.Now);
            return _db.InsertAll(scales) > 0;
        }

        public static FundInfo GetFundInfo(string fundCode) => _db.Find<FundInfo>(fundCode);

        public static int InsertFundInfo(FundInfo info)
        {
            if (info == null)
            {
                return 0;
            }

            info.UpdateTime = DateTime.Now;
            return _db.Insert(info);
        }

        public static int UpdateFundInfo(FundInfo info)
        {
            if (info == null)
            {
                return 0;
            }

            info.UpdateTime = DateTime.Now;
            return _db.Update(info);
        }

        public static List<FundStockPosition> GetFundStockPositions(string fundCode, DateTime date) =>
            _db.Query<FundStockPosition>($@"SELECT * FROM FundStockPosition WHERE FundCode='{fundCode}' AND 
                ReportDate='{DbHelper.FormatDateTime(date)}' ORDER BY Ratio DESC");

        public static bool UpdateFundStockPositions(List<FundStockPosition> positions)
        {
            if (positions == null || !positions.Any())
            {
                return false;
            }

            var position = positions.First();
            _db.Execute($"DELETE FROM FundStockPosition WHERE FundCode='{position.FundCode}' AND ReportDate='{DbHelper.FormatDateTime(position.ReportDate)}'");
            positions.ForEach(x => x.UpdateTime = DateTime.Now);
            return _db.InsertAll(positions) > 0;
        }

        public static List<IndexQuotation> GetIndexQuotations(string indexCode, DateTime begin, DateTime end) =>
            _db.Query<IndexQuotation>($@"SELECT * FROM IndexQuotation WHERE IndexCode='{indexCode}' AND 
                Date>='{DbHelper.FormatDateTime(begin)}' AND Date<='{DbHelper.FormatDateTime(end)}' ORDER BY Date");

        public static bool UpdateIndexQuotations(List<IndexQuotation> indexQuotations)
        {
            if (indexQuotations == null || !indexQuotations.Any())
            {
                return false;
            }

            var indexCode = indexQuotations[0].IndexCode;
            var begin = indexQuotations.Min(x => x.Date);
            var end = indexQuotations.Max(x => x.Date);
            _db.Execute($@"DELETE FROM IndexQuotation WHERE IndexCode='{indexCode}' AND 
                Date>='{DbHelper.FormatDateTime(begin)}' AND Date<='{DbHelper.FormatDateTime(end)}'");
            indexQuotations.ForEach(x => x.UpdateTime = DateTime.Now);
            return _db.InsertAll(indexQuotations) > 0;
        }

        public static List<IndexConstituent> GetIndexConstituents(string indexCode) => 
            _db.Query<IndexConstituent>($"SELECT * FROM IndexConstituent WHERE IndexCode='{indexCode}'");

        public static bool UpdateIndexConstituents(List<IndexConstituent> indexConstituents)
        {
            if (indexConstituents == null || !indexConstituents.Any())
            {
                return false;
            }

            _db.Execute($"DELETE FROM IndexConstituent WHERE IndexCode='{indexConstituents[0].IndexCode}'");
            indexConstituents.ForEach(x => x.UpdateTime = DateTime.Now);
            return _db.InsertAll(indexConstituents) > 0;
        }

        public static FileVersion GetFileVersion(string path) => _db.Find<FileVersion>(path);

        public static int AddFileVersion(FileVersion version) => _db.Insert(version);

        public static int UpdateFileVersion(FileVersion version) => _db.Update(version);

        public static List<FundStyleExposure> GetFundStyleExposures(string fundCode, DateTime begin, DateTime end) => 
            _db.Query<FundStyleExposure>($@"SELECT * FROM FundStyleExposure WHERE FundCode='{fundCode}' AND 
                Date>='{DbHelper.FormatDateTime(begin)}' AND Date<='{DbHelper.FormatDateTime(end)}' ORDER BY Date");

        public static int InsertFundStyleExposures(List<FundStyleExposure> exposures) => _db.InsertAll(exposures);
    }
}
