using AuthServer.Entities;
using AuthServer.Persistence;
using Dgm.Common.Extension;
using Dgm.Common.Models.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AuthServer.Controllers
{
    [Route("Authorization/[controller]")]
    [Authorize(LocalApi.PolicyName)]
    public class SettingsController : ControllerBase
    {
        private IWebHostEnvironment Environment;
        private readonly AppIdentityDbContext _appIdentityDbContext;

        public SettingsController(IWebHostEnvironment _environment, AppIdentityDbContext appIdentityDbContext)
        {
            Environment = _environment;
            _appIdentityDbContext = appIdentityDbContext;
        }

        [HttpGet]
        [Route("GetLogo")]
        public IActionResult GetLogo()
        {
            var logoImageName = _appIdentityDbContext.AppSettingImageRecord.Where(x => x.IsLogo).FirstOrDefault()?.Name ?? $"defaultLogo.jpg";
            string imageDirectory = Path.Combine("Uploads", "Logo");
            string directoryFullpath = Path.Combine(this.Environment.WebRootPath, imageDirectory);
            var image = System.IO.File.OpenRead(Path.Combine(directoryFullpath, logoImageName));
            return File(image, logoImageName.GetContentType());
        }

        [HttpPost]
        [Route("LogoUpload")]
        public async Task<IActionResult> LogoUpload([FromForm] LogoUploadModel model)
        {
            var transction = await _appIdentityDbContext.Database.BeginTransactionAsync();
            try
            {
                var logo = model.Logo;
                string imageDirectory = Path.Combine("Uploads", "Logo");
                string directoryFullpath = Path.Combine(this.Environment.WebRootPath, imageDirectory);
                if (!Directory.Exists(directoryFullpath))
                {
                    Directory.CreateDirectory(directoryFullpath);
                }
                string fileName = logo.FileName.AppendTimeStamp();
                using (FileStream stream = new(Path.Combine(directoryFullpath, fileName), FileMode.Create))
                {
                    logo.CopyTo(stream);
                }

                var requestedBy = User.FindFirst("UserId").Value.ToString();
                var oldLogo = await _appIdentityDbContext.AppSettingImageRecord.Where(x => x.IsLogo).FirstOrDefaultAsync();
                if (oldLogo != null)
                {
                    oldLogo.CreatedBy = requestedBy;
                    oldLogo.CreatedDate = DateTime.UtcNow;
                    oldLogo.Name = fileName;
                    _appIdentityDbContext.AppSettingImageRecord.Update(oldLogo);
                }
                else
                {
                    AppSettingImageRecord newLogo = new()
                    {
                        IsActive = true,
                        IsLogo = true,
                        CreatedBy = requestedBy,
                        CreatedDate = DateTime.UtcNow,
                        Name = fileName
                    };
                    await _appIdentityDbContext.AppSettingImageRecord.AddAsync(newLogo);
                }

                await _appIdentityDbContext.SaveChangesAsync();
                await transction.CommitAsync();
                return Ok();
            }
            catch
            {
                await transction.RollbackAsync();
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("DescriptiveImageUpload")]
        public async Task<IActionResult> DescriptiveImageUpload([FromForm] DescriptiveImageUploadModel model)
        {
            var transction = await _appIdentityDbContext.Database.BeginTransactionAsync();
            try
            {
                string imageDirectory = Path.Combine("Uploads", "DescriptiveImage");
                string directoryFullpath = Path.Combine(this.Environment.WebRootPath, imageDirectory);
                if (!Directory.Exists(directoryFullpath))
                {
                    Directory.CreateDirectory(directoryFullpath);
                }

                var requestedBy = User.FindFirst("UserId").Value.ToString();
                DateTime now = DateTime.UtcNow;
                List<AppSettingImageRecord> appSettingImages = new();
                foreach (var image in model.Images)
                {
                    string fileName = image.FileName.AppendTimeStamp();
                    using (FileStream stream = new(Path.Combine(directoryFullpath, fileName), FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                    AppSettingImageRecord newImages = new()
                    {
                        IsActive = true,
                        IsLogo = false,
                        CreatedBy = requestedBy,
                        CreatedDate = now,
                        Name = fileName
                    };
                    appSettingImages.Add(newImages);
                }

                await _appIdentityDbContext.AppSettingImageRecord.AddRangeAsync(appSettingImages);
                await _appIdentityDbContext.SaveChangesAsync();
                await transction.CommitAsync();
                return Ok();
            }
            catch
            {
                await transction.RollbackAsync();
                return BadRequest();
            }
        }
    }
}
