using InternalServices.Infrastructure.Abstractions;
using Newtonsoft.Json;
using System;

namespace InternalServices.Infrastructure
{
    internal class TokenHandler : ITokenHandler
    {
        private readonly ICryptography _cryptography;
        private const string initial = "Sprak";

        public TokenHandler(ICryptography cryptography)
        {
            _cryptography = cryptography;
        }
        public T DeserializeToken<T>(string token, out bool isValidToken)
        {
            T obj = default;
            if (!token.StartsWith(initial))
            {
                isValidToken = false;
                return obj;
            }
            try
            {
                var actualToken = token.Replace(initial, string.Empty);
                var json = _cryptography.Decrypt(actualToken);
                obj = JsonConvert.DeserializeObject<T>(json);
                isValidToken = true;
            }
            catch (Exception ex)
            {
                isValidToken = false;
            }
            return obj;
        }

        public string SerializeToken<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var encryptedKey = _cryptography.Encrypt(json);
            return initial + encryptedKey;
        }
    }
}
