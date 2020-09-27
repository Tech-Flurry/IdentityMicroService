using Domain.Models.OTP;

namespace InternalServices.Infrastructure.Abstractions
{
    public interface IOTPService
    {
        string GenerateOtp(string email, string phoneNumber, MethodInvokeModel confirmationMethod);
        string ValidateOTP(string otp, string appSecret);
    }
}
