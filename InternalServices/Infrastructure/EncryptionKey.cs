using InternalServices.Infrastructure.Abstractions;

namespace InternalServices.Infrastructure
{
    internal class EncryptionKey : IEncryptionKey
    {
        private readonly string encryptionKey;
        public EncryptionKey(string encryptionKey)
        {
            this.encryptionKey = encryptionKey;
        }
        public string GetEncryptionKey()
        {
            return encryptionKey;
        }
    }
}
