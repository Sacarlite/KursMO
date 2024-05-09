using Domain.PasswordService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordService
{
    public class PasswordHasher : IPasswordHasher
    {
        public string? GetHashPassword(string password)
        {
            return ComputeHmacsha1(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes("key")).ToString();
        }
        public byte[] ComputeHmacsha1(byte[] data, byte[] key)
        {
            using (var hmac = new HMACSHA1(key))
            {
                return hmac.ComputeHash(data);
            }
        }
    }
  
}
