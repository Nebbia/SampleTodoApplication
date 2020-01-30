using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Services
{
    public class PasswordHashService
    {
        private const int SALT_SIZE = 10;

        public (string, string) HashPassword(string rawPassword)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(rawPassword);
            var salt = CreateRandomSalt(SALT_SIZE);

            using (var sha256Hash = SHA256.Create())
            {
                var hashedBytes = sha256Hash.ComputeHash(passwordBytes);
                var hashString = Encoding.UTF8.GetString(hashedBytes);

                return (hashString, Encoding.UTF8.GetString(salt));
            }
        }

        byte[] CreateRandomSalt(int saltSize)
        {
            byte[] randomBytes = new byte[saltSize];
            RNGCryptoServiceProvider crytoProvider = new RNGCryptoServiceProvider();
            crytoProvider.GetBytes(randomBytes);

            return randomBytes;
        }
    }
}
