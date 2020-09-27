using Domain.Models.OTP;

namespace DataAcess.Abstractions
{
    public interface IOTPRepository
    {
        bool IsExists(string otp);
        bool AddOTP(string otp, MethodInvokeModel confirmationMethod, int expirySpan);
        MethodInvokeModel GetConfirmationInvokeMethod(string otp);
        void DeleteOTP(string otp);
        void DeleteAllExpiredOTPs();
    }
}
