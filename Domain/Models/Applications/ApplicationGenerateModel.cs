using System;

namespace Domain.Models.Applications
{
    public class ApplicationGenerateModel
    {
        public int AppId { get; set; }
        public string ApplicationName { get; set; }
        public DateTime? SessionExpire { get; set; }
    }
}
