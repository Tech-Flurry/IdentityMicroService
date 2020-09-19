using Domain.Models.Applications;
using System.Collections.Generic;

namespace DataAcess.Abstractions
{
    public interface IApplicationsRepository
    {
        int CreateApplication(CreateApplicationModel appInfo);
        ApplicationGenerateModel ReadApplicationToGenerateKey(int appId);
        List<ApplicationsListModel> GetApplicationsList();
        bool UpdateApplicationInfo(UpdateApplicationModel appInfo);
        bool IsApplicationExists(int appId);
    }
}
