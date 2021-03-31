using Butler.Entities;
using Butler.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Butler.Daos
{
    public static class AssetDao
    {
        private static SQLiteConnection _db;

        static AssetDao()
        {
            DbHelper.EnsureDbFolderExists();
            _db = new SQLiteConnection("dbs/asset.db", false);
            _db.CreateTable<FundPosition>();
            _db.CreateTable<AssetSnapshot>();
        }

        public static int AddFundPosition(FundPosition fundPosition) => _db.Insert(fundPosition);

        public static FundPosition GetFundPosition(string fundCode) => _db.Find<FundPosition>(fundCode);

        public static int UpdateFundPosition(FundPosition fundPosition) => _db.Update(fundPosition);

        public static List<FundPosition> GetAllPositions() => _db.Query<FundPosition>("SELECT * FROM FundPosition");

        public static int Clear() => _db.DeleteAll<FundPosition>();

        public static int DeleteFundPosition(string fundCode) => _db.Execute($"DELETE FROM FundPosition WHERE FundCode='{fundCode}'");

        public static AssetSnapshot GetAssetSnapshot(DateTime date) => _db.Query<AssetSnapshot>($"SELECT * FROM AssetSnapshot WHERE Date='{DbHelper.FormatDateTime(date)}'")?.FirstOrDefault();

        public static bool UpsertAssetSnapshot(AssetSnapshot snapshot)
        {
            if (snapshot == null)
            {
                return false;
            }

            if (GetAssetSnapshot(snapshot.Date) == null)
            {
                return _db.Insert(snapshot) > 0;
            }
            else
            {
                return _db.Update(snapshot) > 0;
            }
        }
    }
}
