using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PHVManagementAGG.Core.Settings;
using System.Threading.Tasks;

namespace PHVManagementAGG.WebApp.Middleware
{
    public class HeaderVerificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ShopeeSettings shopeeSettings;

        public HeaderVerificationMiddleware(RequestDelegate next, IOptions<ShopeeSettings> shopeeSettings)
        {
            _next = next;
            this.shopeeSettings = shopeeSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {            
            // Kiểm tra xem header có tồn tại và có giá trị mong muốn không
            if (!context.Request.Headers.ContainsKey("Authorization") || context.Request.Headers["Authorization"] != shopeeSettings.Token )
            {
                // Nếu header không hợp lệ, trả về lỗi hoặc thực hiện xử lý khác tùy thuộc vào yêu cầu của bạn.
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized - Invalid PZH token.");
                return;
            }
            
            await _next(context);
        }
    }

    public static class HeaderVerificationMiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderVerificationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderVerificationMiddleware>();
        }
    }
}
