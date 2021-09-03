using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Widget.Company.Common
{
    public enum ErrorCodes
    {
        None = 0,
        SystemError = 1000,
        ActivityNotFound = 1001,
        LateRegistration = 1002,
        EventEnded = 1003,
        AlreadyInEvent = 1004
    }
}
