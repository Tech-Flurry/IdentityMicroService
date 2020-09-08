using Domain.Models.Data;

namespace Domain.Models.Applications
{
    public class UpdateApplicationModel : DbUpdateEntry
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        public ApplicationConfigurationModel Configuration { get; set; }
    }
}
