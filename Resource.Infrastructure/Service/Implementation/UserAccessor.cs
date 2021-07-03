using Microsoft.AspNetCore.Http;
using Resource.Application.Service.Abstract;
using System;

namespace Resource.Infrastructure.Service.Implementation
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetCurrentUserId() => _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;
    }
}
