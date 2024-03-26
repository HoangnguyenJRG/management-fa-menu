using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Services.Interfaces
{
    public interface IGojekService
    {
        Task<JObject> GetDataMenuGojekJsonStringAsync(string menuId);
    }
}
