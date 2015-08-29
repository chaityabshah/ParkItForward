using System;
using System.Security.Cryptography;
using System.Text;

namespace Planner
{
    public static class Crypt
    {
        // Todo increase the max to 16 (and increase the column size in user_credentials.password_salt)
        private const int MinimumSaltSize = 4;
        private const int MaximumSaltSize = 8;

        public static string GenerateSalt()
        {
            var random = new Random();
            var saltSize = random.Next(MinimumSaltSize, MaximumSaltSize);

            var saltBytes = new byte[saltSize];

            var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        ///     Generates hash given the plain text
        /// </summary>
        /// <param name="plainText">password in plain text</param>
        /// <param name="salt">a salt</param>
        /// <returns>base64 string</returns>
        public static string GenerateHash(string plainText, string salt)
        {
            var plainTextWithSaltBytes = Encoding.UTF8.GetBytes(plainText + salt);

            HashAlgorithm hash = new SHA512Managed();
            var hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            var hashValue = Convert.ToBase64String(hashBytes);

            return hashValue;
        }

        public static bool VerifyPassword(string plainText, string salt, string hashValue)
        {
            var expectedHashString = GenerateHash(plainText, salt);

            return (hashValue == expectedHashString);
        }
    }
}