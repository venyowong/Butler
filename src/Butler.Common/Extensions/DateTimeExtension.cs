using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Butler.Common.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime TryParseDateTime(this string str, string format = null)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                DateTime.TryParse(str, out DateTime result);
                return result;
            }
            else
            {
                DateTime.TryParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);
                return result;
            }
        }
    }
}
