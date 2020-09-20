namespace InternalServices.Infrastructure.Abstractions
{
    internal interface ITokenHandler
    {
        /// <summary>
        /// Serialzes an object to token
        /// </summary>
        /// <param name="obj">object to be serailized</param>
        /// <returns></returns>
        string SerializeToken<T>(T obj);
        /// <summary>
        /// Reverts a token to the acutual object
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        T DeserializeToken<T>(string token, out bool isValidToken);
    }
}
