using InternalServices.Infrastructure.Abstractions;

namespace InternalServices.Infrastructure
{
    internal class SaltValue : ISaltValue
    {
        private readonly string _salt;
        public SaltValue(string salt)
        {
            _salt = salt;
        }
        public string GetSaltValue()
        {
            return _salt;
        }
    }
}
