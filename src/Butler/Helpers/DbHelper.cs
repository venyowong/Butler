using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Butler.Helpers
{
    public static class DbHelper
    {
        public static void EnsureDbFolderExists()
        {
            if (!Directory.Exists("dbs"))
            {
                Directory.CreateDirectory("dbs");
            }
        }

        public static string FormatDateTime(DateTime time) => time.ToString("yyyy-MM-ddTHH:mm:ss.fff");
    }
}
