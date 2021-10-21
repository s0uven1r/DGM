using System;

namespace AuthServer.Entities
{
    public class AppSettingImageRecord
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsLogo { get; set; }
    }
}
