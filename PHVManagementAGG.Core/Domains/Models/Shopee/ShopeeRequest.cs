using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Domains.Models
{
    public class ShopeeRequest
    {
        [Required]
        public string merchant_restaurant_id { get; set; }        
    }
}
