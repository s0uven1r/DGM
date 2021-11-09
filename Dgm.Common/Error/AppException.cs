using Dgm.Common.Error.Response;
using System;
using System.Globalization;

namespace Dgm.Common.Error
{
    public class AppException : Exception
    {
        public ErrorResponse ErrorResponse { get; }

        public AppException(string message) : base(message)
        {
            ErrorResponse = new();
        }

        public AppException(string message, ErrorResponse errorResponse)
           : this(message)
        {
            ErrorResponse = errorResponse;
        }
    }
}
