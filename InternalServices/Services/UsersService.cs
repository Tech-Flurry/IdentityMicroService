using DataAcess.Abstractions;
using Domain.Infrastucture;
using Domain.Models.Applications;
using Domain.Models.OTP;
using Domain.Models.Users;
using InternalServices.Abstractions;
using InternalServices.Infrastructure.Abstractions;
using System.Collections.Generic;

namespace InternalServices.Services
{
    internal class UsersService : IUsersService
    {
        private readonly IOTPService _otpService;
        private readonly IUsersRepository _repository;
        private readonly IApplicationsService _applicationsService;
        private readonly ITokenHandler _tokenHandler;

        public UsersService(IOTPService otpService,
                            IUsersRepository repository,
                            IApplicationsService applicationsService,
                            ITokenHandler tokenHandler)
        {
            _otpService = otpService;
            _repository = repository;
            _applicationsService = applicationsService;
            _tokenHandler = tokenHandler;
        }
        public string CreateNewUser(CreateUserModel userInfo)
        {
            var message = "Create User process failed";
            var isValid = _applicationsService.ValidateApplicationSecret(userInfo.AppKey);
            if (isValid)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(userInfo.AppKey, out _);
                var isUsernameAvailable = !(_repository.IsExists(userInfo.Username, application.AppId));
                if (isUsernameAvailable)
                {
                    MethodInvokeModel confirmationMethod = new MethodInvokeModel
                    {
                        MethodName = "CreateNewUser",
                        Params = new List<MethodParams>
                    {
                        new MethodParams
                        {
                            IsInJson=true,
                            Name="model",
                            Type=typeof(CreateUserModel).FullName,
                            Value=userInfo.ToJson()
                        },
                        new MethodParams
                        {
                            IsInJson=false,
                            Name="appId",
                            Type=typeof(int).FullName,
                            Value=application.AppId
                        }
                    }
                    };
                    var phoneNumber = userInfo.MobileNumber;
                    var email = userInfo.Email;
                    message = _otpService.GenerateOtp(email, phoneNumber, confirmationMethod);
                }
                else
                {
                    message = "Username not available";
                }
            }
            return message;
        }

        public bool DisableUser(string username, string key)
        {
            bool result = false;
            if (_applicationsService.ValidateApplicationSecret(key))
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(key, out _);
                long userId = _repository.GetUserId(username, application.AppId);
                if (userId != default || userId != 0)
                {
                    result = _repository.ChangeDisableFlag(userId, true);
                }
            }
            return result;
        }

        public bool EnableUser(string username, string key)
        {
            bool result = false;
            if (_applicationsService.ValidateApplicationSecret(key))
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(key, out _);
                long userId = _repository.GetUserId(username, application.AppId);
                if (userId != default || userId != 0)
                {
                    result = _repository.ChangeDisableFlag(userId, false);
                }
            }
            return result;
        }

        public List<UserListingModel> GetRegisteredUsersList(string key)
        {
            List<UserListingModel> result = new List<UserListingModel>();
            if (_applicationsService.ValidateApplicationSecret(key))
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(key, out _);
                result = _repository.GetRegisteredUsers(application.AppId);
            }
            return result;
        }

        public string UpdatePhoneNumber(UpdatePhoneNumberModel usersPhoneNumberInfo, string key)
        {
            var message = "";
            var isValid = _applicationsService.ValidateApplicationSecret(key);
            if (isValid)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(key, out _);
                var user = _tokenHandler.DeserializeToken<UserGenerateModel>(usersPhoneNumberInfo.UserSecret, out _);
                UserPhoneNumberParam userPhone = new UserPhoneNumberParam
                {
                    PhoneNumber = usersPhoneNumberInfo.MobileNumber,
                    UserId = user.UserId
                };
                MethodInvokeModel confirmationMethod = new MethodInvokeModel
                {
                    MethodName = "UpdatePhoneNumber",
                    Params = new List<MethodParams>
                    {
                        new MethodParams
                        {
                            IsInJson=true,
                            Name="model",
                            Type=typeof(UserPhoneNumberParam).FullName,
                            Value=userPhone.ToJson()
                        },
                        new MethodParams
                        {
                            IsInJson=false,
                            Name="appId",
                            Type=typeof(int).FullName,
                            Value=application.AppId
                        }
                    }
                };
                var phoneNumber = usersPhoneNumberInfo.MobileNumber;
                var email = user.Email;
                message = _otpService.GenerateOtp(email, phoneNumber, confirmationMethod);
            }
            return message;
        }

        public string UpdateUser(UpdateUserModel userInfo, string appSecret)
        {
            var message = "Update User process failed";
            var isValid = _applicationsService.ValidateApplicationSecret(userInfo.Key);
            if (isValid)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(userInfo.Key, out _);
                var user = _tokenHandler.DeserializeToken<UserGenerateModel>(userInfo.Key, out _);
                userInfo.UserId = user.UserId;
                MethodInvokeModel confirmationMethod = new MethodInvokeModel
                {
                    MethodName = "UpdateUser",
                    Params = new List<MethodParams>
                    {
                        new MethodParams
                        {
                            IsInJson=true,
                            Name="model",
                            Type=typeof(UpdateUserModel).FullName,
                            Value=userInfo.ToJson()
                        },
                        new MethodParams
                        {
                            IsInJson=false,
                            Name="appId",
                            Type=typeof(int).FullName,
                            Value=application.AppId
                        }
                    }
                };
                var phoneNumber = user.MobileNumber;
                var email = user.Email;
                message = _otpService.GenerateOtp(email, phoneNumber, confirmationMethod);
            }
            return message;
        }
        public bool IsUsernameAvailable(string username, string appKey)
        {
            var result = false;
            var isValid = _applicationsService.ValidateApplicationSecret(appKey);
            if (isValid)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(appKey, out _);
                var isUsernameAvailable = !(_repository.IsExists(username, application.AppId));
                result = isUsernameAvailable;
            }
            return result;
        }
    }
}
