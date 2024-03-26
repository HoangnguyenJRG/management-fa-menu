using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PHVManagementAGG.Core.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Domains.BaseResponse
{
    [DebuggerStepThrough]
    public class BaseResponseModel
    {
        public ErrorCodes Code { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }

    public class BaseResponseModel<T> : BaseResponseModel
    {
        public T Data { get; set; }
    }

    public class BaseResponse
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
    public class BaseMessageResponse : BaseResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
