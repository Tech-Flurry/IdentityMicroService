using InternalServices.Infrastructure.Abstractions;

namespace InternalServices.Infrastructure
{
    internal class SessionTimeouts : ISessionTimeouts
    {
        private readonly int _applicationTimeout;
        private readonly int _userTimeout;

        public SessionTimeouts(int applicationTimeout, int userTimeout)
        {
            _applicationTimeout = applicationTimeout;
            _userTimeout = userTimeout;
        }

        public int ApplicationTimeout => _applicationTimeout;

        public int UserTimeout => _userTimeout;
    }
}
