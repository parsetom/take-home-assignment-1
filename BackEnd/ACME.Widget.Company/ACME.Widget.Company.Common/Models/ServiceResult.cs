using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Widget.Company.Common.Models
{
    public class ServiceResult<T> : IServiceResult
    {
        public T Result { get; set; }
        public object ObjectResult => Result;
        public ErrorCodes ErrorCode { get; set; }
    }

    public interface IServiceResult
    {
        object ObjectResult { get; }
        ErrorCodes ErrorCode { get; set; }
    }
}
