using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Entities
{
    public class FileVersion
    {
        public DateTime LastWriteTime { get; set; }

        public DateTime LastAccessTime { get; set; }

        [PrimaryKey]
        public string FullName { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
