using PHVManagementAGG.Core.Enums;
using PHVManagementAGG.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using PHVManagementAGG.Core.Domains.BaseResponse;

namespace PHVManagementAGG.WebApp.Controllers
{
    public class BaseController : Controller
    {
        [DebuggerStepThrough]
        protected IActionResult Success()
        {
            return Ok(new BaseResponse
            {
                Code = ErrorCodes.Success.GetHashCode(),
                Message = ErrorCodes.Success.GetDescription()
            });
        }

        [DebuggerStepThrough]
        protected IActionResult Success<T>(T data)
        {
            return Ok(new BaseResponse<T>
            {
                Code = ErrorCodes.Success.GetHashCode(),
                Message = ErrorCodes.Success.GetDescription(),
                Data = data
            });
        }

        [DebuggerStepThrough]
        protected IActionResult Success<T>(string message)
        {
            return Ok(new BaseResponse<T>
            {
                Code = ErrorCodes.Success.GetHashCode(),
                Message = message,
            });
        }

        [DebuggerStepThrough]
        protected IActionResult Failure()
        {
            return Ok(new BaseResponse
            {
                Code = ErrorCodes.BadRequest.GetHashCode(),
                Message = ErrorCodes.BadRequest.GetDescription()
            });
        }

        [DebuggerStepThrough]
        protected IActionResult Failure(string message)
        {
            return Ok(new BaseResponse
            {
                Code = ErrorCodes.BadRequest.GetHashCode(),
                Message = message
            });
        }

        [DebuggerStepThrough]
        protected IActionResult Failure(ErrorCodes errorCode)
        {
            return Ok(new BaseResponse
            {
                Code = errorCode.GetHashCode(),
                Message = errorCode.GetDescription()
            });
        }

        [DebuggerStepThrough]
        protected IActionResult Failure(ErrorCodes errorCode, string message)
        {
            return Ok(new BaseResponse
            {
                Code = errorCode.GetHashCode(),
                Message = message
            });
        }

        [DebuggerStepThrough]
        protected IActionResult InvalidModel()
        {
            return base.Ok(new BaseResponse
            {
                Code = ErrorCodes.InvalidModel.GetHashCode(),
                Message = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)))
            });
        }

        [DebuggerStepThrough]
        protected IActionResult DataNotFound()
        {
            return base.Ok(new BaseResponse
            {
                Code = ErrorCodes.DataNotFound.GetHashCode(),
                Message = ErrorCodes.DataNotFound.GetDescription()
            });
        }

    }
}
