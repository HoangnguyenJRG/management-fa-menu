using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Domains.Shared
{
    public class FileLogConfig
    {
        public string Path { get; set; }

        public bool ExceptionEnable { get; set; }

        public bool HandleEnable { get; set; }
    }
}
