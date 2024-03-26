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
    public class GrabController : BaseController
    {
        private readonly IGrabService grabServices;
        public GrabController(IGrabService grabServices)
        {
            this.grabServices = grabServices;
        }

        [Route("menu-by-shop-code")]
        [HttpGet]
        public async Task<IActionResult> GetMenuGrabByMenuId(string menuId = "")
        {
            var result = await grabServices.GetDataMenuGrabJsonStringAsync(menuId);
            if (result.IsNullOrEmpty())
            {
                return DataNotFound();
            }
            return Success(result);
        }
    }
}
