using Domain.Models.Applications;
using System.Collections.Generic;

namespace InternalServices.Abstractions
{
    /// <summary>
    /// Includes all services related to Applications
    /// </summary>
    public interface IApplicationsService
    {
        /// <summary>
        /// Returns a list of applications registered in the service
        /// </summary>
        /// <returns></returns>
        List<ApplicationsListModel> GetApplicationsList();
        /// <summary>
        /// Validates the application secret for an application
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ValidateApplicationSecret(string key);
        /// <summary>
        /// Generates a new Secrete for an application
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        string GenerateApplicationSecret(int appId);
        /// <summary>
        /// Create new application in service
        /// </summary>
        /// <param name="appInfo"></param>
        /// <returns></returns>
        string CreateNewApplication(CreateApplicationModel appInfo);
        /// <summary>
        /// Updates an application's info
        /// </summary>
        /// <param name="appInfo"></param>
        /// <returns></returns>
        bool UpdateApplicationInfo(UpdateApplicationModel appInfo);
    }
}
