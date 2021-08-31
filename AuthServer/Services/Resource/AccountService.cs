using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuthServer.Services.Resource
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(HttpClient httpClient, IConfiguration config,
            IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.config = config;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> GetAccountNumber(string type, string alias)
        {
            var resourceBaseUrl = this.config.GetSection("ModuleUrl:Resource").Value;
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var request = new HttpRequestMessage(HttpMethod.Get,
               $"{resourceBaseUrl}api/AccountHead/GetAccountNumber?type={type}&alias={alias}");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token.ToString().Split(' ').LastOrDefault());
            }
            var response = await this.httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                throw new Exception(msg);
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
