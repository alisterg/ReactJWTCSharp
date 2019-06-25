namespace ExampleApp.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Generates a user token for use with our API
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string GenerateUserToken(string userName);

        /// <summary>
        /// Gets the username stored in a token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string UserFromToken(string token);

        /// <summary>
        /// Generates a hash from a string
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string GenerateHash(string password);
        /// <summary>
        /// Compares a plaintext string with a hash
        /// </summary>
        /// <param name="plainTextPass"></param>
        /// <param name="hashToCompare"></param>
        /// <returns></returns>
        bool CompareHash(string plainTextPass, string hashToCompare);
    }
}