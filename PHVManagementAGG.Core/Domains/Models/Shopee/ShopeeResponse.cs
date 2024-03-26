using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Domains.Models.Shopee
{
    public class ShopeeResponse
    {
    }

    public class ShopeeMenuResponse
    {
        public string MenuId { get; set; }
        public string VersionID { get; set; }
        public string MenuJsonString { get; set; }
    }
}
