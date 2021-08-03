using Microsoft.AspNetCore.Http;
using Resource.Application.Common.Interfaces;
using System;

namespace Resource.Infrastructure.Service
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string UserId => _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;
    }
}
