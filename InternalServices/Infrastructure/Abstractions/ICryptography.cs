namespace InternalServices.Infrastructure.Abstractions
{
    internal interface ICryptography
    {
        string Encrypt(string plainValue);
        string Decrypt(string cipherValue);
    }
}
