using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Enums
{
    public enum NotificationOnjectEnum
    {
        //status notification object
        Inactive = 0,
        Active = 1,        
        Delete = 2,

        //Status isappoint

        [Description("Now")]
        Now = 0,

        [Description("Schedule")]
        Schedule = 1


    }
    
}
