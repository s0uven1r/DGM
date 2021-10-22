using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dgm.Common.Error
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var err = error as AppException;
                        var result = JsonSerializer.Serialize(new { message = err?.Message, error = err?.ErrorResponse });
                        await response.WriteAsync(result);
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var resultUnhand = JsonSerializer.Serialize(new { message = error?.Message});
                        await response.WriteAsync(resultUnhand);
                        break;
                }

               
            }
        }
    }

}
