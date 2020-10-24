using DataAcess.Abstractions;
using Domain.Infrastucture;
using Domain.Models.Account;
using Domain.Models.Applications;
using Domain.Models.OTP;
using Domain.Models.Users;
using InternalServices.Abstractions;
using InternalServices.Infrastructure.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace InternalServices.Services
{
    internal class AccountService : IAccountService
    {
        private readonly IUsersRepository _repository;
        private readonly IApplicationsService _applicationsService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IHasher _hasher;
        private readonly IOTPService _otpService;

        public AccountService(IUsersRepository repository,
                              IApplicationsService applicationsService,
                              ITokenHandler tokenHandler,
                              IHasher hasher,
                              IOTPService otpService)
        {
            _repository = repository;
            _applicationsService = applicationsService;
            _tokenHandler = tokenHandler;
            _hasher = hasher;
            _otpService = otpService;
        }
        public AuthenticationResult Authenticate(string userKey, List<string> roles, string appKey)
        {
            var isValid = _applicationsService.ValidateApplicationSecret(appKey);
            if (isValid)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(appKey, out _);
                var user = _tokenHandler.DeserializeToken<UserToken>(userKey, out bool isUserTokenValid);
                if (isUserTokenValid)
                {
                    if (roles != null || roles.Count > 0)
                    {
                        var isInRoles = false;
                        foreach (var role in roles)
                        {
                            isInRoles = user.Roles.Any(x => x == role);
                            break;
                        }
                        if (isInRoles)
                        {
                            return AuthenticationResult.Authenticated;
                        }
                    }
                    else
                    {
                        return AuthenticationResult.Authenticated;
                    }
                }
                return AuthenticationResult.NotAuthorized;
            }
            else
            {
                return AuthenticationResult.SessionTimedOut;
            }
        }

        public string ChangePassword(ChangePasswordModel info, string appKey)
        {
            var message = "Change password process failed";
            var isValid = _applicationsService.ValidateApplicationSecret(appKey);
            if (isValid)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(appKey, out _);
                var isUserExists = _repository.IsExists(info.Username, application.AppId);
                if (isUserExists)
                {
                    MethodInvokeModel confirmationMethod = new MethodInvokeModel
                    {
                        MethodName = "ChangePassword",
                        Params = new List<MethodParams>
                    {
                        new MethodParams
                        {
                            IsInJson=true,
                            Name="model",
                            Type=typeof(ChangePasswordModel).FullName,
                            Value=info.ToJson()
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
                    UserInfoModel userInfo = _repository.GetUserByName(info.Username, application.AppId);
                    message = _otpService.GenerateOtp(userInfo.Email, userInfo.MobileNumber, confirmationMethod);
                }
                else
                {
                    //No OTP will be sent if user don't exists, this is just for the security purpose
                    message = "OTP has been sent to your registered number";
                }
            }
            return message;
        }

        public string Login(LoginModel loginInfo, string appKey)
        {
            var token = "";
            var isValid = _applicationsService.ValidateApplicationSecret(appKey);
            if (isValid)
            {
                var application = _tokenHandler.DeserializeToken<ApplicationGenerateModel>(appKey, out _);
                loginInfo.Password = _hasher.GetHash(loginInfo.Password);
                UserToken userToken = _repository.Login(loginInfo, application.AppId);
                token = _tokenHandler.SerializeToken(userToken);
            }
            return token;
        }
    }
}
