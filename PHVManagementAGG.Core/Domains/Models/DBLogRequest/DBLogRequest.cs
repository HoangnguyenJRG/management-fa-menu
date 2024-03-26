using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Domains.Models
{
    public class DBLogRequest
    {
        public string LogType { get; set; }

        public string ProjectType { get; set; }

        public string Version { get; set; }

        public string Method { get; set; }

        public string Action { get; set; }

        public string Path { get; set; }

        public string Protocol { get; set; }

        public string Host { get; set; }

        public string Headers { get; set; }

        public string Parameters { get; set; }

        public string KeySearch { get; set; }

        public string Content { get; set; }
    }
}
