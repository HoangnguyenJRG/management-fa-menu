using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PHVManagementAGG.Core.DBAccess;
using PHVManagementAGG.Core.Domains.Models.Shopee;
using PHVManagementAGG.Core.Enums;
using PHVManagementAGG.Core.Exceptions;
using PHVManagementAGG.Core.Extensions;
using PHVManagementAGG.Core.Services.Interfaces;
using PHVManagementAGG.Core.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Services.Implementations
{
    public class GrabService : IGrabService
    {
        private readonly ISqlDataAccess db;
        private IConfiguration configuration;
        private readonly appSettings appSetting;

        public GrabService(ISqlDataAccess db,
                                  IConfiguration configuration,
                                  IOptions<appSettings> appSetting,
                                  IOptions<ShopeeSettings> shopeeSettings
                                  )
        {
            this.db = db;
            this.configuration = configuration;
            this.appSetting = appSetting.Value;
        }

        public async Task<JObject> GetDataMenuGrabJsonStringAsync(string menuId)
        {
            var p = new DynamicParameters();
            p.Add("@menuVersionId", "1");
            p.Add("@menuId", "GRAB_A_TEST");
            p.Add("@platform", "GRAB");
            p.Add("@enviroment", 0);
            p.Add("@rStatus", null, dbType: DbType.Boolean, direction: ParameterDirection.Output, 50);

            var result = (await db.QueryAsync<ShopeeMenuResponse, DynamicParameters>(command: "usp_MenuTool_BuilderByPlatformGrab_Jsonstring",
                                                     parameters: p,
                                                     connectionString: configuration.GetConnectionStringWithDecrypt("MenuToolPre"),
                                                     commandType: System.Data.CommandType.StoredProcedure)).ToList();
            if (!result.Any())
            {
                throw new BusinessException(ErrorCodes.DataNotFound, new object[] { "BuilderByPlatforGrab" });
            }


            JToken menuOject = JToken.Parse(result.FirstOrDefault().MenuJsonString);

            JObject jObjectResult = menuOject as JObject;

            return jObjectResult;
        }

    }
}
