using DataAcess.Abstractions;
using Domain.Infrastucture;
using Domain.Infrastucture.Common;
using Domain.Models.OTP;
using Domain.Models.Users;
using InternalServices.Abstractions;
using InternalServices.Infrastructure.Abstractions;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace InternalServices.Infrastructure
{
    internal class OTPService : IOTPService
    {
        private readonly IOTPRepository _repository;
        private readonly IOTPConfiguration _config;
        private readonly IEmailHandler _emailService;
        private readonly IApplicationsService _applicationsService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUsersRepository _usersRepository;
        private readonly IHasher _hasher;
        private const string invalidOTPMessage = "The requested OTP failed to validate";
        public OTPService(IOTPRepository repository,
                          IOTPConfiguration config,
                          IEmailHandler emailService,
                          IApplicationsService applicationsService,
                          ITokenHandler tokenHandler,
                          IUsersRepository usersRepository,
                          IHasher hasher)
        {
            _repository = repository;
            _config = config;
            _emailService = emailService;
            _applicationsService = applicationsService;
            _tokenHandler = tokenHandler;
            _usersRepository = usersRepository;
            _hasher = hasher;
            Task.Run(() =>
            {
                _repository.DeleteAllExpiredOTPs();
            });
        }
        public string GenerateOtp(string email, string phoneNumber, MethodInvokeModel confirmationMethod)
        {
            var message = "Otp generation unsuccessful.";
            string newOtp;
            string hashedOtp;
            do
            {
                newOtp = GetOTP();
                hashedOtp = _hasher.GetHash(newOtp);
            } while (_repository.IsExists(hashedOtp));
            bool result = _repository.AddOTP(hashedOtp, confirmationMethod, _config.GetOTPExpireSpan());
            Functions.SetTimeout(() =>
            {
                //Deletes the otp after expire time
                _repository.DeleteOTP(hashedOtp);
            }, _config.GetOTPExpireSpan());
            if (result)
            {
                //sending otp to email
                _emailService.SendOTPMail(newOtp, email);
                //sending otp to phone (not implemented yet)

                message = $"You OTP has been sent to {email}.";
            }
            return message;
        }
        public string ValidateOTP(string otp, string appSecret)
        {
            var message = invalidOTPMessage;
            var isValid = _applicationsService.ValidateApplicationSecret(appSecret);
            if (isValid)
            {
                var hashedOtp = _hasher.GetHash(otp);
                if (_repository.IsExists(hashedOtp))
                {
                    MethodInvokeModel confimationCallback = _repository.GetConfirmationInvokeMethod(hashedOtp);
                    if (confimationCallback != null)
                    {
                        message = CallConfirmationMethod(confimationCallback);
                    }
                }
            }
            return message;
        }
        public string CreateNewUserConfirmation(CreateUserModel model, int appId)
        {
            var message = invalidOTPMessage;
            bool result = _usersRepository.CreateUser(model, appId);
            if (result)
            {
                message = "User has been created successfully";
            }
            return message;
        }
        public string UpdatePhoneNumberConfirmation(UserPhoneNumberParam model, int appId)
        {
            var message = invalidOTPMessage;
            bool result = _usersRepository.UpdatePhoneNumber(model, appId);
            if (result)
            {
                message = "Phone number has been updated successfully";
            }
            return message;
        }
        public string UpdateUserConfirmation(UpdateUserModel model, int appId)
        {
            var message = invalidOTPMessage;
            bool result = _usersRepository.UpdateUser(model, appId);
            if (result)
            {
                message = "User has been updated successfully";
            }
            return message;
        }
        private string CallConfirmationMethod(MethodInvokeModel methodInvoke)
        {
            //the code below will call the method dynamically
            Type type = GetType();
            var methodConvention = "{0}Confirmation";
            var methodName = string.Format(methodConvention, methodInvoke.MethodName);
            var method = type.GetMethod(methodName);
            var parameters = method.GetParameters();
            object[] methodParameters;
            if (parameters.Length <= methodInvoke.Params.Count)
            {
                methodParameters = new object[parameters.Length];
                foreach (var param in parameters)
                {
                    var requestedParam = methodInvoke.Params.First(x => x.Name == param.Name);
                    if (requestedParam != null)
                    {
                        if (requestedParam.IsInJson)
                        {
                            var convertionType = Assembly.GetAssembly(typeof(CreateUserModel))
                                .GetType(requestedParam.Type);
                            var obj = ((string)requestedParam.Value).ToObject(convertionType);
                            methodParameters[param.Position] = Convert.ChangeType(obj, convertionType);
                        }
                        else
                        {
                            var convertionType = Type.GetType(requestedParam.Type);
                            methodParameters[param.Position] = Convert.ChangeType(requestedParam.Value, convertionType);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Not all parameters provided.");
            }
            var messageResult = (string)(method.Invoke(this, methodParameters));
            return messageResult;
        }
        private string GetOTP()
        {
            string pickups = "0123456789";
            var otp = string.Empty;
            for (int i = 0; i < _config.GetOTPLength(); i++)
            {
                Random rand = new Random(new Random().Next());
                var pickIndex = rand.Next(0, pickups.Length - 1);
                otp += pickups[pickIndex];
            }
            return otp;
        }
    }
}
