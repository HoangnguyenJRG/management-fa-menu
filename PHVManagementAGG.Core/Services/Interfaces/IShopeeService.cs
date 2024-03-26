using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Services.Interfaces
{
    public interface IShopeeService
    {
        Task<JObject> GetMenuShopeeByShopCodeAsync(string storeId);
        Task<JObject> GetMenuPzhByShopCodeAsync(string category, string menuId, string orderChannel);
        Task<JObject> GetDataMenuShopeeByShopCodeAsync(string menuId);
    }
}
