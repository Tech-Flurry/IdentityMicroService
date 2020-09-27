namespace InternalServices.Infrastructure.Abstractions
{
    internal interface IOTPConfiguration
    {
        int GetOTPExpireSpan();
        int GetOTPLength();
    }
}
