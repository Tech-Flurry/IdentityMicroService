using InternalServices.Infrastructure.Abstractions;

namespace InternalServices.Infrastructure
{
    internal class OTPConfiguration : IOTPConfiguration
    {
        private readonly int _otpExpirySpan;
        private readonly int _otpLength;

        public OTPConfiguration(int otpExpirySpan, int otpLength)
        {
            _otpExpirySpan = otpExpirySpan;
            _otpLength = otpLength;
        }
        public int GetOTPExpireSpan()
        {
            return _otpExpirySpan;
        }

        public int GetOTPLength()
        {
            return _otpLength;
        }
    }
}
