using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dgm.Common.Models
{
    public static class ClientBaseUrlsHelper
    {
        public static void Configure(ClientBaseUrls x)
        {
            x.AuthServer = Environment.GetEnvironmentVariable("URL_AUTHSERVER");
            x.ResourceAPI = Environment.GetEnvironmentVariable("URL_RESOURCEAPI");
            x.ClientApp = Environment.GetEnvironmentVariable("URL_CLIENTAPP");
            x.Flutter = Environment.GetEnvironmentVariable("URL_FLUTTER");
        }
    }
}
