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
        [Route("LogoUpload")]
        public IActionResult LogoUpload([FromForm] LogoUploadModel model)
        {
            try
            {
                var logo = model.Logo;
                string imageDirectory = Path.Combine("Uploads", "Logo");
                string path = Path.Combine(this.Environment.WebRootPath, imageDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fullFilePath = Path.Combine(path, logo.FileName);
                using (FileStream stream = new(fullFilePath, FileMode.Create))
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
