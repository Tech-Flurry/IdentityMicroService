using System.Threading.Tasks;

namespace InternalServices.Infrastructure.Abstractions
{
    internal interface IEmailHandler
    {
        Task SendOTPMail(string otp, string email);
    }
}
