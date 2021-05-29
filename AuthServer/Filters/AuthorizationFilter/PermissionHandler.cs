using Auth.Infrastructure.Constants;
using Dgm.Common.Authorization.Claim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace AuthServer.Filters.AuthorizationFilter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ApiAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] permissions = null;
        public ApiAuthorizeAttribute(params string[] permissions)
        {
            this.permissions = permissions;
        }
      
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userClaims = context.HttpContext.User.Claims
                .Where(x => x.Type == ClaimType.Permission)
                .Select(a => a.Value).ToList();
            if (this.permissions.Length == 0)
            {
                return;
            }
            if (context.HttpContext.User.IsInRole(SystemRoles.SuperAdmin))
            {
                return;
            }
            if (userClaims.Count > 0)
            {
                bool isAuth = userClaims.Any(x => this.permissions.Contains(x));
                if (isAuth) return;
            }

            context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            return;
        }
    }
}
