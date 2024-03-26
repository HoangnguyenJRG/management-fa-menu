using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PHVManagementAGG.Core.Services.Interfaces;
using PHVManagementAGG.Core.Domains.Models;
using PHVManagementAGG.Core.Domains.BaseResponse;
using PHVManagementAGG.Core.Enums;
using PHVManagementAGG.Core.Extensions;

namespace PHVManagementAGG.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopeeController : BaseController
    {
        private readonly IShopeeService shopeeServices;
        public ShopeeController(IShopeeService shopeeServices)
        {
            this.shopeeServices = shopeeServices;
        }

        [Route("menu-by-shop-code")]
        [HttpPost]
        public async Task<IActionResult> GetMenuShopeeByShopCode([FromBody] [Required] ShopeeRequest merchant_restaurant_id)
        {
            var result = await shopeeServices.GetMenuShopeeByShopCodeAsync(merchant_restaurant_id.merchant_restaurant_id);
            if (result.IsNullOrEmpty())
            {               
                return DataNotFound();
            }
            return Success(result);
        }

        [Route("pzh-menu-by-shop-code")]
        [HttpGet]
        public async Task<IActionResult> GetMenuPzhByShopCode(string category, string menuId, string orderChannel)
        {
            var result = await shopeeServices.GetMenuPzhByShopCodeAsync(category, menuId, orderChannel);
            return Success(result);
        }

        [Route("shopee-menu-by-shop-code")]
        [HttpGet]
        public async Task<IActionResult> GetDataMenuShopeeByShopCode(string menuId)
        {
            var result = await shopeeServices.GetDataMenuShopeeByShopCodeAsync( menuId);
            return Success(result);
        }

    }
}
