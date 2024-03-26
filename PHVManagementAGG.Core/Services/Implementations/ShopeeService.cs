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
    public class ShopeeService : IShopeeService
    {
        private readonly ISqlDataAccess db;
        private IConfiguration configuration;
        private readonly appSettings appSetting;
        private readonly ShopeeSettings shopeeSettings;

        public ShopeeService(ISqlDataAccess db,
                                  IConfiguration configuration,
                                  IOptions<appSettings> appSetting,
                                  IOptions<ShopeeSettings> shopeeSettings
                                  )
        {
            this.db = db;
            this.configuration = configuration;
            this.appSetting = appSetting.Value;
            this.shopeeSettings = shopeeSettings.Value;
        }
        public async Task<JObject> GetMenuShopeeByShopCodeAsync(string storeId)
        {
            var merchantID = shopeeSettings.MerchantId;
            var partnerMerchantID = shopeeSettings.PartnerMerchantID;
            var p = new DynamicParameters();
            p.Add("@storeId", storeId);
            p.Add("@platform", "SHOPEEFOOD");

            var result = (await db.QueryAsync<ShopeeMenuResponse, DynamicParameters>(command: "usp_MenuTool_GetMenuStringByStoreforAGG",
                                                     parameters: p,
                                                     connectionString: configuration.GetConnectionStringWithDecrypt("CMS_PRD"),
                                                     commandType: System.Data.CommandType.StoredProcedure)).ToList();
            if (!result.Any())
            {
                return new JObject();
                //throw new BusinessException(ErrorCodes.DataNotFound, new object[] { "GetMenuShopeeByShopCode" });
            }

            //ShopeeFoodResponse menuByShopCode = new ShopeeFoodResponse();
            //menuByShopCode.MerchantID = shopeeFoodMenuSetting.MerchantId;
            //menuByShopCode.PartnerMerchantID = shopeeFoodMenuSetting.PartnerMerchantID;
            //menuByShopCode.Currency.Code = shopeeFoodMenuSetting.Code;
            //menuByShopCode.Currency.Symbol = shopeeFoodMenuSetting.Symbol;
            //menuByShopCode.Currency.Exponent = shopeeFoodMenuSetting.Exponent;

            JToken menuOject = JToken.Parse(result.FirstOrDefault().MenuJsonString);
            menuOject.RemovePropertiesByRegex("zip.*");
            menuOject.UpdatePropertyValue("merchantID", merchantID);
            menuOject.UpdatePropertyValue("partnerMerchantID", partnerMerchantID);

            //var json = menuOject.ToString(Formatting.None);
            JObject jObjectResult = menuOject as JObject;

            return jObjectResult;
        }

        public async Task<JObject> GetMenuPzhByShopCodeAsync(string category, string menuId, string orderChannel)
        {
            var merchantID = shopeeSettings.MerchantId;
            var partnerMerchantID = shopeeSettings.PartnerMerchantID;
            var p = new DynamicParameters();
            p.Add("@MENU_VERSION_ID", "1");
            p.Add("@MenuID", menuId);
            p.Add("@OrderChannel", orderChannel);
            p.Add("@category", category);
            p.Add("@enviroment", 0);
            p.Add("@R_Status", null, dbType: DbType.Boolean, direction: ParameterDirection.Output, 50);

            var result = (await db.QueryAsync<ShopeeMenuResponse, DynamicParameters>(command: "usp_MENU_BUILDER_BY_PLATFORM_JSONSTRING",
                                                     parameters: p,
                                                     connectionString: configuration.GetConnectionStringWithDecrypt("MenuTool"),
                                                     commandType: System.Data.CommandType.StoredProcedure)).ToList();
            if (!result.Any())
            {
                throw new BusinessException(ErrorCodes.DataNotFound, new object[] { "GetMenuShopeeByShopCode" });
            }

            //ShopeeFoodResponse menuByShopCode = new ShopeeFoodResponse();
            //menuByShopCode.MerchantID = shopeeFoodMenuSetting.MerchantId;
            //menuByShopCode.PartnerMerchantID = shopeeFoodMenuSetting.PartnerMerchantID;
            //menuByShopCode.Currency.Code = shopeeFoodMenuSetting.Code;
            //menuByShopCode.Currency.Symbol = shopeeFoodMenuSetting.Symbol;
            //menuByShopCode.Currency.Exponent = shopeeFoodMenuSetting.Exponent;

            JToken menuOject = JToken.Parse(result.FirstOrDefault().MenuJsonString);
            //menuOject.RemovePropertiesByRegex("zip.*");
            //menuOject.UpdatePropertyValue("merchantID", merchantID);
            //menuOject.UpdatePropertyValue("partnerMerchantID", partnerMerchantID);

            //var json = menuOject.ToString(Formatting.None);
            //JToken menuOject = JToken.Parse(result.FirstOrDefault().MenuJsonString);
            JObject jObjectResult = menuOject as JObject;

            return jObjectResult;
        }
        public async Task<JObject> GetDataMenuShopeeByShopCodeAsync(string menuId)
        {
            var merchantID = shopeeSettings.MerchantId;
            var partnerMerchantID = shopeeSettings.PartnerMerchantID;
            var p = new DynamicParameters();
            p.Add("@menuVersionId", "1");
            p.Add("@menuId", menuId);
            p.Add("@serviceCharge", 0);
            p.Add("@platform", "SHOPEEFOOD");
            p.Add("@enviroment", 0);
            p.Add("@rStatus", null, dbType: DbType.Boolean, direction: ParameterDirection.Output, 50);

            var result = (await db.QueryAsync<ShopeeMenuResponse, DynamicParameters>(command: "usp_MenuTool_BuilderByPlatform_Jsonstring",
                                                     parameters: p,
                                                     connectionString: configuration.GetConnectionStringWithDecrypt("MenuToolPre"),
                                                     commandType: System.Data.CommandType.StoredProcedure)).ToList();
            if (!result.Any())
            {
                throw new BusinessException(ErrorCodes.DataNotFound, new object[] { "GetMenuShopeeByShopCode" });
            }

            //ShopeeFoodResponse menuByShopCode = new ShopeeFoodResponse();
            //menuByShopCode.MerchantID = shopeeFoodMenuSetting.MerchantId;
            //menuByShopCode.PartnerMerchantID = shopeeFoodMenuSetting.PartnerMerchantID;
            //menuByShopCode.Currency.Code = shopeeFoodMenuSetting.Code;
            //menuByShopCode.Currency.Symbol = shopeeFoodMenuSetting.Symbol;
            //menuByShopCode.Currency.Exponent = shopeeFoodMenuSetting.Exponent;

            JToken menuOject = JToken.Parse(result.FirstOrDefault().MenuJsonString);
            //menuOject.RemovePropertiesByRegex("zip.*");
            //menuOject.UpdatePropertyValue("merchantID", merchantID);
            //menuOject.UpdatePropertyValue("partnerMerchantID", partnerMerchantID);

            //var json = menuOject.ToString(Formatting.None);
            //JToken menuOject = JToken.Parse(result.FirstOrDefault().MenuJsonString);
            JObject jObjectResult = menuOject as JObject;

            return jObjectResult;
        }



    }
}
