using PHVManagementAGG.Core.Domains.Models;
using PHVManagementAGG.Core.Constants;
using PHVManagementAGG.Core.Enums;
using System;
using System.Threading.Tasks;
using PHVManagementAGG.Core.Domains.Shared;

namespace PHVManagementAGG.Core.Services.Interfaces
{
    public interface ICommonService
    {        

        Task<SystemConfig> GetSystemConfig(string projectType);

        void WriteLogExceptionToFile(Exception ex, string msg);

        void WriteLogHandleToFile(string controllerName, string functionName, dynamic dataLog);

        Task WriteLogToDBAsync(DBLogRequest request);

        Task WriteLogToDBAsync(string logType = PHVNConstant.LogDBType.Handler, string keySearch = null, string parameters = null, string errorMessage = null);
                
    }
}
