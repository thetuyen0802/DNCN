using BCrypt.Net;
using Infrastructure.Services.PasswordHasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.PasswordHasher
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly BcryptOption _bcryptOption;
        public PasswordHasher(BcryptOption bcryptOption)
        {
            _bcryptOption = bcryptOption ?? throw new ArgumentNullException(nameof(bcryptOption));
        }
        //hashpassword bằng Bcrypt
        public string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty");
            try
            {
                return BCrypt.Net.BCrypt.HashPassword(
                password,
                BCrypt.Net.BCrypt.GenerateSalt(_bcryptOption.WorkFactor) 
                );
            }
            catch (SaltParseException ex)
            {

                throw new CryptographicException("Salt generation failed", ex);
            }
            
        }
        public bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
