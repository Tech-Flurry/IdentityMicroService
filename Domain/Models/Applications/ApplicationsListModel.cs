using System;

namespace Domain.Models.Applications
{
    public class ApplicationsListModel
    {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
