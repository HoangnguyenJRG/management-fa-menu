using Dapper;
using PHVManagementAGG.Core.DBAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using PHVManagementAGG.Core.Domains.Shared;
using PHVManagementAGG.Core.Domains.BaseResponse;
using Microsoft.Extensions.Configuration;
using PHVManagementAGG.Core.Extensions;
using PHVManagementAGG.Core.Constants;
using PHVManagementAGG.Core.Domains.Models;
using PHVManagementAGG.Core.Services.Interfaces;

namespace PHVManagementAGG.Core.Services.Implementations
{
    public class CommonService : ICommonService
    {
        private readonly ISqlDataAccess db;
        private readonly FileLogConfig fileLogConfig;
        private readonly DBLogConfig dbLogConfig;
        private IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CommonService(ISqlDataAccess db, 
                             IOptions<FileLogConfig> fileLogConfig, 
                             IOptions<DBLogConfig> dbLogConfig,
                             IHttpContextAccessor httpContextAccessor,
                             IConfiguration configuration )
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.fileLogConfig = fileLogConfig.Value;
            this.dbLogConfig = dbLogConfig.Value;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<SystemConfig> GetSystemConfig(string projectType)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ProjectType", projectType);

                var apiResponse = await db.QueryFirstOrDefaultAsync<SystemConfig, DynamicParameters>(
                                                command: "usp_GetSystemConfig",
                                                parameters: parameters,
                                                connectionString: configuration.GetConnectionStringWithDecrypt("MiddlewareDigitalLog"),
                                                commandType : CommandType.StoredProcedure);

                return apiResponse;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void WriteLogExceptionToFile(Exception ex, string msg)
        {          
                if (fileLogConfig.ExceptionEnable)
                {
                    var projectId = httpContextAccessor.HttpContext.Request.Headers["Project_ID"].ToString() ?? string.Empty;
                    var line = Environment.NewLine;
                    var now = DateTime.Now;
                    var errorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                    var errormsg = ex.GetType().Name.ToString();
                    var extype = ex.GetType().ToString();
                    var errorLocation = ex.Message.ToString();
                    var dir = $"{fileLogConfig.Path}\\1_LogErrors\\{now.Year}\\{now.Month.ToString("00")}";

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    var filePath = Path.Combine(dir, $"{now.ToString("yyyy-MM-dd")}-ErrorLog.json");
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        var error = $"Log Written Date: {now.ToString("dd-MM-yyyy HH:mm:ss")} {projectId} {line}" +
                                    $"Error Line No : {errorlineNo} {line} - {errormsg} {line}" +
                                    $"Exception Type: {extype} {line}" +
                                    $"Error Location: {errorLocation} {line}" +
                                    $"Error FullMessage: {msg}";

                        sw.WriteLine(error);
                        sw.WriteLine(line);
                        sw.Flush();
                        sw.Close();
                    }
                }            
        }

        public void WriteLogHandleToFile(string controllerName, string functionName, dynamic dataLog)
        {            
                if (fileLogConfig.HandleEnable)
                {
                    var now = DateTime.Now;
                    var dir = $"{fileLogConfig.Path}\\2_HandlesLog\\{controllerName}\\{functionName}\\{now.Year}\\{now.Month.ToString("00")}";

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    var filePath = Path.Combine(dir, $"{functionName}_error_{now.ToString("yyyy-MM-dd")}.json");
                    using var file = File.AppendText(filePath);
                    var data = JsonConvert.SerializeObject(dataLog);
                    file.WriteLine(data);
                    file.Close();
                }            
        }

        public async Task WriteLogToDBAsync(string logType = PHVNConstant.LogDBType.Handler, string keySearch = null, string parameters = null, string errorMessage = null)
        {
           
                var context = httpContextAccessor.HttpContext;
                var request = context.Request;

                var projectId = request.Headers["Project_ID"].ToString() ?? string.Empty;
                var version = request.Headers["Version_App"].ToString() ?? string.Empty;

                var queryPath = request.Path.ToString();
                var headers = JsonConvert.SerializeObject(request.Headers);
                var url = request.Path.ToString();
                var queryString = request.QueryString.ToString();

                var paramsRequest = string.Empty;
                paramsRequest = request.Method.ToLower() == "post" ? parameters : queryString;

                await WriteLogToDBAsync(new DBLogRequest
                {
                    LogType = logType,
                    ProjectType = projectId,
                    Version = version,
                    Method = request.Method,
                    Path = url,
                    Action = url,
                    Protocol = request.Protocol.ToString(),
                    Host = request.Host.ToString(),
                    Headers = headers,
                    Parameters = paramsRequest,
                    Content = errorMessage,
                    KeySearch = keySearch,
                });            
      
        }

        public async Task WriteLogToDBAsync(DBLogRequest request)
        {
            
                if (dbLogConfig.IsEnable)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@LogType", request.LogType);
                    parameters.Add("@ProjectType", request.ProjectType);
                    parameters.Add("@Version", request.Version);
                    parameters.Add("@Method", request.Method);
                    parameters.Add("@Action", request.Action);
                    parameters.Add("@Path", request.Path);
                    parameters.Add("@Protocol", request.Protocol);
                    parameters.Add("@Host", request.Host);
                    parameters.Add("@Headers", request.Headers);
                    parameters.Add("@KeySearch", request.KeySearch);
                    parameters.Add("@Parameters", request.Parameters);
                    parameters.Add("@Content", request.Content);

                    await db.QueryFirstOrDefaultAsync<BaseResponse, dynamic>(
                                                        command: "usp_WriteSystemLogs",
                                                        parameters: parameters,
                                                        connectionString: configuration.GetConnectionStringWithDecrypt("MiddlewareDigitalLog"),
                                                        commandType: System.Data.CommandType.StoredProcedure);
                }
         
        }


    }
}
