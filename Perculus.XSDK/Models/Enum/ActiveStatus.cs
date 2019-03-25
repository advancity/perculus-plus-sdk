using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models.Enum
{
    public enum ActiveStatus : int
    {
        Deleted = -1,
        Passive = 0,
        Active = 1,
        Locked = 2,
        Expired = 3
    }
}
