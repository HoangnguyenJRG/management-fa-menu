using PHVManagementAGG.Core.Enums;
using PHVManagementAGG.Core.Exceptions;
using PHVManagementAGG.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using PHVManagementAGG.Core.Domains.BaseResponse;

namespace PHVManagementAGG.WebApp.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    PHVN_SYSTEM.PHVN_SYS lPHVN_SYS = new PHVN_SYSTEM.PHVN_SYS();
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";

                    var controllerName = !string.IsNullOrEmpty(context.Request.Path.ToString()) ? context.Request.Path.ToString() : string.Empty;
                    var controllerMethod = !string.IsNullOrEmpty(context.Request.Method.ToString()) ? context.Request.Method.ToString() : string.Empty;

                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var ex = error.Error;
                        var code = ErrorCodes.InternalServerError;
                        //var msg = ex.Message + Environment.NewLine + ex.InnerException?.Message + Environment.NewLine + ex.StackTrace;
                        var msg = ex.Message;

                        if (ex.GetType() == typeof(BusinessException))
                        {
                            lPHVN_SYS.WriteLog("BusinessException", "", msg);
                            code = ((BusinessException)ex).StatusCode;
                            msg = ex.Message;
                        }
                        else if (ex.GetType() == typeof(ArgumentException))
                        {
                            //var s = new StackTrace(ex).GetFrame(0).GetMethod().Name;                            
                            MethodBase a = ex.TargetSite;

                            lPHVN_SYS.WriteLog("ArgumentException", "", $"Controller : {controllerName} - Method : {controllerMethod} - FunctionName : {a.GetMethodContextName()} - Exception error - {ex.Message}");
                            code = ErrorCodes.BadRequest;
                        }
                        else
                        {
                            MethodBase a = ex.TargetSite;

                            lPHVN_SYS.WriteLog("SystemException", "", $"Controller : {controllerName} - Method : {controllerMethod} - FunctionName : {a.GetMethodContextName()} - Exception error - {ex.Message}");
                            code = ErrorCodes.BadRequest;
                        }

                        await context.Response.WriteAsync(new BaseResponseModel
                        {
                            Code = code,
                            Message = msg
                        }.ToString());
                    }
                });
            });

            return app;
        }
    }
}
