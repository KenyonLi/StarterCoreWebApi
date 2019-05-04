//*******************************
// Create By Ahoo Wang
// Date 2019-04-23 23:30
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Starter.WebApi.Exceptions
{
    public class APIException : Exception
    {
        public APIException()
        {
            ErrorCode = "0001";
        }
        public String ErrorCode { get; set; }

        public APIException(string errorCode) : base() { ErrorCode = errorCode; }

        public APIException(string errorCode, string message) : base(message) { ErrorCode = errorCode; }

        public APIException(string errorCode, SerializationInfo info, StreamingContext context) : base(info, context) { ErrorCode = errorCode; }

        public APIException(string errorCode, string message, System.Exception innerException) : base(message, innerException) { ErrorCode = errorCode; }
    }
}





