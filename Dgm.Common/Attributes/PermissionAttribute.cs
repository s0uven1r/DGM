using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dgm.Common.Attributes
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        private string actionName = string.Empty;
        public PermissionAttribute(string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(actionName, claimValue) };
        }
        public class ClaimRequirementFilter : IAsyncActionFilter
        {
            private readonly Claim _claim;

            public ClaimRequirementFilter(Claim claim)
            {
                _claim = claim;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                Claim permissionClaim = context.HttpContext.User.FindFirst("permission");
                if (permissionClaim != null && !string.IsNullOrEmpty(permissionClaim.Value))
                {
                    try
                    {
                        var userPermissions = JsonConvert.DeserializeObject<List<string>>(permissionClaim.Value);
                        if (userPermissions.Contains(_claim.Value))
                        {
                            await next();
                            return;
                        }
                    }
                    catch
                    { }
                }

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
    public static class CheckPermissionExtension
    {
        public static bool CheckPermission(this ClaimsPrincipal user, string claimPermission)
        {
            Claim permissionClaim = user.FindFirst("permission");
            if (permissionClaim != null && !string.IsNullOrEmpty(permissionClaim.Value))
            {
                try
                {
                    var userPermissions = JsonConvert.DeserializeObject<List<string>>(permissionClaim.Value);
                    if (userPermissions.Contains(claimPermission))
                    {
                        return true;
                    }
                }
                catch
                { }
            }
            return false;
        }
    }
}
