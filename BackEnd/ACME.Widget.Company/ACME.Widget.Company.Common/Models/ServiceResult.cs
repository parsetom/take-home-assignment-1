using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Widget.Company.Common.Models
{
    public class ServiceResult<T>
    {
        public T Result { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
