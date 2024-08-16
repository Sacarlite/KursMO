using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordService
{
    public class RsaWithCspKey
    {
        const string FirstKey = "FirstKey";
        const string Salt = "TmpSalt";
        const string SecondKey = "MyContainer";
        public byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cipherbytes;
            var firstCspParams = new CspParameters { KeyContainerName = FirstKey };
            using (var rsa = new RSACryptoServiceProvider(2048, firstCspParams))
            {
                cipherbytes = rsa.Encrypt(dataToEncrypt, false);
            }
            var cipherString = Encoding.Unicode.GetString(cipherbytes);
            cipherString += Salt;
            var secondCspParams = new CspParameters { KeyContainerName = SecondKey };
            using (var rsa = new RSACryptoServiceProvider(2048, secondCspParams))
            {
                cipherbytes = rsa.Encrypt(Encoding.Unicode.GetBytes(cipherString), false);
            }
            return cipherbytes;
        }
        public byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plain;
            var firstCspParams = new CspParameters { KeyContainerName = SecondKey };
            using (var rsa = new RSACryptoServiceProvider(2048, firstCspParams))
            {
                plain = rsa.Decrypt(dataToDecrypt, false);
            }
            int index = Encoding.Unicode.GetString(plain).IndexOf(Salt);
            var str = Encoding.Unicode.GetString(plain).Remove(index, Salt.Length);
            var secondCspParams = new CspParameters { KeyContainerName = FirstKey };
            using (var rsa = new RSACryptoServiceProvider(2048, firstCspParams))
            {
                plain = rsa.Decrypt(Encoding.Unicode.GetBytes(str), false);
            }
            return plain;
        }
    }
}