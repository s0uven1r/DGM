using Dgm.Common.Attributes;
using Dgm.Common.Authorization.Claim.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ResourceAPI.Controllers
{

    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ///GET api/values
        [Permission(IdentityClaimConstant.ViewIdentity)]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok("success!");
        }
    }
}
