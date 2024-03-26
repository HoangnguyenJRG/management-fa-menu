using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Extensions
{
    public static class DataValidatorExtensions
    {
        public static T GetNonNullValue<T>(T value)
        {
            return value != null ? value : default(T);
        }
    }
}
