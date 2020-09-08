using System;

namespace Domain.Models.Data
{
    public abstract class DbCreationEntry
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
