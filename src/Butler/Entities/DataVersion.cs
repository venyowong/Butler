using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Entities
{
    public interface IDataVersion
    {
        string Type { get; }

        DateTime UpdateTime { get; set; }
    }
}
