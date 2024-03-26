using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Domains.Shared
{
    public class SystemConfig
    {
        public int Id { get; set; }

        public string ProjectType { get; set; }

        public string Code { get; set; }

        public string AccessKey { get; set; }

        public string Secret { get; set; }
    }
}
