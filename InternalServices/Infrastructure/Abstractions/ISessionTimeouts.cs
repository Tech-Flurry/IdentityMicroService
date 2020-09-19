namespace InternalServices.Infrastructure.Abstractions
{
    internal interface ISessionTimeouts
    {
        int ApplicationTimeout { get; }
        int UserTimeout { get; }
    }
}
