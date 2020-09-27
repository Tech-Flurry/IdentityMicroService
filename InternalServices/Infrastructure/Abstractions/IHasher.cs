namespace InternalServices.Infrastructure.Abstractions
{
    internal interface IHasher
    {
        string GetHash(string s);
        bool VerifyHash(string s, string hash);
    }
}
