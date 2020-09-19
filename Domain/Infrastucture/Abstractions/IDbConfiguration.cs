namespace Domain.Infrastucture.Abstractions
{
    public interface IDbConfiguration
    {
        string ConnectionString { get; }
        int ConnectionTimeout { get; }
    }
}
