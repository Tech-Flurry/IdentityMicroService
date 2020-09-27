using HashLib;
using InternalServices.Infrastructure.Abstractions;
using System.Text;

namespace InternalServices.Infrastructure
{
    internal class Hasher : IHasher
    {
        readonly string _salt;
        public Hasher(ISaltValue salt)
        {
            _salt = salt.GetSaltValue();
        }
        public string GetHash(string s)
        {
            var str1 = s.Substring(0, s.Length / 2);
            var str2 = s.Substring(s.Length / 2);
            string str = str1 + _salt + str2;
            var result = GetHasher().ComputeString(str, Encoding.ASCII);
            var hash = result.ToString();
            return hash;
        }

        public bool VerifyHash(string s, string hash)
        {
            var computedHash = GetHash(s);
            return (computedHash.Equals(hash));
        }
        private IHash GetHasher()
        {
            IHash hash = HashFactory.Crypto.CreateHaval_4_128();
            return hash;
        }
    }
}
