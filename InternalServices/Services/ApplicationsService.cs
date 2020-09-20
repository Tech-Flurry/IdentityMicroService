using DataAcess.Abstractions;
using Domain.Models.Applications;
using InternalServices.Abstractions;
using InternalServices.Infrastructure.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace InternalServices.Services
{
    internal class ApplicationsService : IApplicationsService
    {
        private readonly IApplicationsRepository _repository;
        private readonly ITokenHandler _tokenHandler;
        private readonly ISessionTimeouts _timeouts;

        public ApplicationsService(IApplicationsRepository repository, ITokenHandler tokenHandler, ISessionTimeouts timeouts)
        {
            _repository = repository;
            _tokenHandler = tokenHandler;
            _timeouts = timeouts;
        }
        public string CreateNewApplication(CreateApplicationModel appInfo)
        {
            int appId = _repository.CreateApplication(appInfo);
            string key = "";
            if (appId > 0)
            {
                key = GenerateApplicationSecret(new ApplicationGenerateModel
                {
                    AppId = appId,
                    ApplicationName = appInfo.ApplicationName,
                    SessionExpire = DateTime.Now.AddDays(_timeouts.ApplicationTimeout)
                });
            }
            return key;
        }

        public string GenerateApplicationSecret(int appId)
        {
            ApplicationGenerateModel application = _repository.ReadApplicationToGenerateKey(appId);
            application.SessionExpire = DateTime.Now.AddMinutes(_timeouts.ApplicationTimeout);
            return GenerateApplicationSecret(application);
        }
        private string GenerateApplicationSecret(ApplicationGenerateModel application)
        {
            var encryptedKey = _tokenHandler.SerializeToken(application);
            return encryptedKey;
        }
        public List<ApplicationsListModel> GetApplicationsList()
        {
            List<ApplicationsListModel> list = _repository.GetApplicationsList();
            return list;
        }

        public bool UpdateApplicationInfo(UpdateApplicationModel appInfo)
        {
            bool result = _repository.UpdateApplicationInfo(appInfo);
            return result;
        }

        public bool ValidateApplicationSecret(string key)
        {
            var flag = false;
            try
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(key, out bool isValidToken);
                if (isValidToken)
                {
                    bool isExists = _repository.IsApplicationExists(application.AppId);
                    if (isExists)
                    {
                        if (application.SessionExpire > DateTime.Now)
                        {
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return flag;
        }
    }
}
