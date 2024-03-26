using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Extensions
{
    public static class StringExtensions
    {
        //public static string GetMethodContextName()
        //{
        //    var name = new StackTrace().GetFrame(1).GetMethod().GetMethodContextName();
        //}

        public static string GetMethodContextName(this MethodBase method)
        {
            if (method.DeclaringType.GetInterfaces().Any(i => i == typeof(IAsyncStateMachine)))
            {
                var generatedType = method.DeclaringType;
                var originalType = generatedType.DeclaringType;
                var foundMethod = originalType.GetMethods()
                    .Single(m => m.GetCustomAttribute<AsyncStateMachineAttribute>()?.StateMachineType == generatedType);
                return foundMethod.DeclaringType.Name + "." + foundMethod.Name;
            }
            else
            {
                return method.DeclaringType.Name + "." + method.Name;
            }
        }

        public static string GetConnectionStringWithDecrypt(this IConfiguration configuration, string connectId)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == null || (env != null && env.Equals("Production")))
            {
                return CryptographyService.CryptoHelper.Decrypt(configuration.GetConnectionString(connectId));
            }
            return configuration.GetConnectionString(connectId);
        }

    }
}
