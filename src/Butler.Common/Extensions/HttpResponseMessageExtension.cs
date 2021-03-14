using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Common.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<T> ReadAs<T>(this HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result.ToObj<T>();
        }
    }
}
