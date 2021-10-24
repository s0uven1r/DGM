using Dgm.Common.Error;
using Dgm.Common.Error.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(y => y.ErrorMessage)).ToArray();


                ErrorResponse errorResponse = new();
                foreach (var erorr in errorsInModelState)
                {
                    foreach (var subError in erorr.Value)
                    {
                        var errorModel = new ErrorModel
                        {
                            FieldName = erorr.Key,
                            Message = subError
                        };

                        errorResponse.Errors.Add(errorModel);
                    }
                }
                throw new AppException("One or more validation failures have occurred.",errorResponse);
                //context.Result = new BadRequestObjectResult(errorResponse);
                //return;
            }
            await next();
        }
    }
}
