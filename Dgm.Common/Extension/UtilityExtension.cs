using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;

namespace Dgm.Common.Extension
{
    public static class UtilityExtension
    {
        public static string AppendTimeStamp(this string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                "_",
                DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                Path.GetExtension(fileName)
                );
        }

        public static string GetContentType(this string fileName)
        {
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return contentType ?? "application/octet-stream";
        }
    }
}
