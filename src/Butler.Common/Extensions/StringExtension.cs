using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Common.Extensions
{
    public static class StringExtension
    {
        public static T ToObj<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                Log.Error(e, "json 反序列化失败");
                return default;
            }
        }

        public static string ToJson(this object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                Log.Error(e, "json 序列化失败");
                return string.Empty;
            }
        }
    }
}
