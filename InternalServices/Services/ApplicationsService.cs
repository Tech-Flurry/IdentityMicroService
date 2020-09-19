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
        private readonly ICryptography _cryptography;
        private readonly ISessionTimeouts _timeouts;

        public ApplicationsService(IApplicationsRepository repository, ICryptography cryptography, ISessionTimeouts timeouts)
        {
            _repository = repository;
            _cryptography = cryptography;
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
            var json = JsonConvert.SerializeObject(application);
            var encryptedKey = _cryptography.Encrypt(json);
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
                var json = _cryptography.Decrypt(key);
                ApplicationGenerateModel application = JsonConvert.DeserializeObject<ApplicationGenerateModel>(json);
                bool isExists = _repository.IsApplicationExists(application.AppId);
                if (isExists)
                {
                    if (application.SessionExpire > DateTime.Now)
                    {
                        flag = true;
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
