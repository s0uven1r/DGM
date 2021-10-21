using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Dgm.Common.Models.Settings
{
    public class DescriptiveImageUploadModel
    {
        public List<IFormFile> Images { get; set; }
    }
}
