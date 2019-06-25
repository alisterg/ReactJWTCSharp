using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Jose;
using ExampleApp.Exceptions;
using ExampleApp.Models;
using Newtonsoft.Json;

namespace ExampleApp.Services
{
    public class AuthService : IAuthService
    {
        private byte[] _secret;
        public AuthService(string secret)
        {
            _secret = Encoding.UTF8.GetBytes(secret);
        }
        
        /// <summary>
        /// Generates a user token for use with our API
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GenerateUserToken(string userName)
        {
            // TODO add expiry
            var payload = new Dictionary<string, object>()
            {
                {"sub", userName}
            };

            return JWT.Encode(payload, _secret, JwsAlgorithm.HS256);
        }

        /// <summary>
        /// Gets the username stored in a JWT token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string UserFromToken(string token)
        {
            string json = JWT.Decode(token, _secret);

            var decoded = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            string username = (string)decoded["sub"];

            if (username == null)
            {
                throw new InvalidUserException();
            }

            return username;
        }

        /// <summary>
        /// Generates a base64 encoded representation of a sha256 hashed password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GenerateHash(string password)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        /// <summary>
        /// Compares a plaintext password with a hashed one
        /// </summary>
        /// <param name="plainTextPass">The plaintext password (probably from a login request)</param>
        /// <param name="hashToCompare">The hashed password (probably from the DB)</param>
        /// <returns></returns>
        public bool CompareHash(string plainTextPass, string hashToCompare)
        {
            return GenerateHash(plainTextPass) == hashToCompare;
        }
    }
}