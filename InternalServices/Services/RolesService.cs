using DataAcess.Abstractions;
using Domain.Models.Applications;
using Domain.Models.Roles;
using InternalServices.Abstractions;
using InternalServices.Infrastructure.Abstractions;
using System.Collections.Generic;

namespace InternalServices.Services
{
    internal class RolesService : IRolesService
    {
        private readonly IRolesRepository _repository;
        private readonly ITokenHandler _tokenHandler;
        private readonly IApplicationsService _applicationsService;

        public RolesService(IRolesRepository repository, ITokenHandler tokenHandler, IApplicationsService applicationsService)
        {
            _repository = repository;
            _tokenHandler = tokenHandler;
            _applicationsService = applicationsService;
        }
        public bool AddNewApplicationRole(CreateRoleModel roleInfo)
        {
            bool result = false;
            var isValidated = _applicationsService.ValidateApplicationSecret(roleInfo.Key);
            if (isValidated)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(roleInfo.Key, out _); //out parameter discarded
                bool isAlreadyExists = _repository.IsExists(roleInfo.RoleName, application.AppId);
                if (!isAlreadyExists)
                {
                    AddRoleModel model = new AddRoleModel
                    {
                        AppId = application.AppId,
                        CreatedBy = roleInfo.CreatedBy,
                        CreatedDate = roleInfo.CreatedDate,
                        RoleName = roleInfo.RoleName
                    };
                    result = _repository.AddNewApplicationRole(model);
                }
            }
            return result;
        }

        public bool DisableRole(string role, string appSecret)
        {
            var result = false;
            var isValidated = _applicationsService.ValidateApplicationSecret(appSecret);
            if (isValidated)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(appSecret, out _);
                result = _repository.DisableRole(role, application.AppId);
            }
            return result;
        }

        public bool EnableRole(string role, string appSecret)
        {
            var result = false;
            var isValidated = _applicationsService.ValidateApplicationSecret(appSecret);
            if (isValidated)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(appSecret, out _);
                result = _repository.EnableRole(role, application.AppId);
            }
            return result;
        }

        public List<RolesListModel> GetApplicationRoles(string appSecret)
        {
            List<RolesListModel> list = new List<RolesListModel>();
            var isValidated = _applicationsService.ValidateApplicationSecret(appSecret);
            if (isValidated)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(appSecret, out _); //out parameter discarded
                list = _repository.GetApplicationRoles(application.AppId);
            }
            return list;
        }

        public bool RemoveRole(string role, string appSecret)
        {
            var result = false;
            var isValidated = _applicationsService.ValidateApplicationSecret(appSecret);
            if (isValidated)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(appSecret, out _); //out parameter discarded
                bool isRoleAssigned = _repository.IsRoleAssigned(role, application.AppId);
                if (!isRoleAssigned)
                {
                    result = _repository.DeleteRole(role, application.AppId);
                }
            }
            return result;
        }
    }
}
