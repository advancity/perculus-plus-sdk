using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public enum ApiErrorCode : int
    {
        None = 0,

        GeneralError = 10,
        ServerError = 11,
        ValidationError = 12,
        MultipleErrors = 13,
        Unauthorized = 14,

        SessionNotFound = 30,
        SessionStatNotFound = 31,

        AttendeeNotFound = 40,
        AttendeeListLimitExceed = 41,

        UserNameConflict = 50,
        UserMailConflict = 51,
        UserNotFound = 52
    }
}
