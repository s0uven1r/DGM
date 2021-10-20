using Dgm.Common.Models.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using static IdentityServer4.IdentityServerConstants;

namespace AuthServer.Controllers
{
    [Route("Authorization/[controller]")]
    [Authorize(LocalApi.PolicyName)]
    public class SettingsController : ControllerBase
    {
        private IWebHostEnvironment Environment;

        public SettingsController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        [HttpPost]
        public IActionResult LogoUpload([FromForm] LogoUploadModel model)
        {
            try
            {
                var logo = model.Logo;
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads/Logo");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = Path.GetFileName(logo.FileName);
                using (FileStream stream = new(Path.Combine(path, fileName), FileMode.Create))
                {
                    logo.CopyTo(stream);
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
