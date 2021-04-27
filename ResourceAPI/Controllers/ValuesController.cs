using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ResourceAPI.Controllers
{

    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ///GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new JsonResult(User.Claims.Select(c => new { c.Subject }));
        }
    }
}
