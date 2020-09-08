using System;

namespace Domain.Models.Data
{
    public abstract class DbUpdateEntry
    {
        public string ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}
