using Dgm.Common.Attributes;
using Dgm.Common.Authorization.Claim.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ResourceAPI.Controllers
{

    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {

        }

        ///GET api/values
        [Permission(IdentityClaimConstant.ViewIdentity)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("success!");
        }
    }
}
