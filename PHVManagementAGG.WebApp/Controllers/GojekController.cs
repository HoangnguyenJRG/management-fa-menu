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
    public class GojekController : BaseController
    {
        private readonly IGojekService gojekServices;
        public GojekController(IGojekService gojekServices)
        {
            this.gojekServices = gojekServices;
        }

        [Route("menu-by-shop-code")]
        [HttpGet]
        public async Task<IActionResult> GetMenuShopeeByShopCode(string menuId = "")
        {
            var result = await gojekServices.GetDataMenuGojekJsonStringAsync(menuId);
            if (result.IsNullOrEmpty())
            {
                return DataNotFound();
            }
            return Success(result);
        }
    }
}
