using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Enums
{
    public enum CRUDEnumeration
    {
        [Description("NONE")]
        None	=0,     //No definition

        [Description("INSERT")]
        Create	=1,     //Permission to create a new data record

        [Description("READ")]
        Read	=2,     //Permission to reads all data records

        [Description("UPDATE")]
        Update	=3,     //Permission to edit all data records

        [Description("DELETE")]
        Delete	=4,     //Permission to delete all data records
       
    }
}
