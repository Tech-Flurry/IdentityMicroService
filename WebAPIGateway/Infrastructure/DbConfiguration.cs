using Domain.Infrastucture.Abstractions;

namespace WebAPIGateway.Infrastructure
{
    public class DbConfiguration : IDbConfiguration
    {
        private readonly string _conString;
        private readonly int _conTimeout;

        public DbConfiguration(string conString, int conTimeout)
        {
            _conString = conString;
            _conTimeout = conTimeout;
        }

        public string ConnectionString
        {
            get
            {
                return _conString;
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                return _conTimeout;
            }
        }
    }
}
