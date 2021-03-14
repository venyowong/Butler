using Butler.Daos;
using Butler.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Helpers
{
    public static class DataHelper
    {
        /// <summary>
        /// 数据是否最新
        /// </summary>
        /// <param name="type">数据类型 fundnav</param>
        /// <param name="key">键，基金代码等</param>
        public static bool IsDataReady(IDataVersion version)
        {
            switch (version?.Type)
            {
                case "fundnav":
                case "fundassetconfig":
                case "fundscale":
                case "fundinfo":
                case "fundstockposition":
                case "indexquotation":
                    return version?.UpdateTime >= DateTime.Now.Date;
                case "indexconstituent":
                    return version?.UpdateTime >= DateTime.Now.AddMonths(-1).Date;
                default:
                    return false;
            }
        }
    }
}
