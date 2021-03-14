using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Helpers
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// 获取当前时间节点的最新年报/半年报
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLastAnnualReportDate()
        {
            var now = DateTime.Now;
            if (now.Month < 4)
            {
                return new DateTime(now.Year - 1, 6, 30);
            }
            else if (now.Month < 9)
            {
                return new DateTime(now.Year - 1, 12, 31);
            }
            else
            {
                return new DateTime(now.Year, 6, 30);
            }
        }
    }
}
