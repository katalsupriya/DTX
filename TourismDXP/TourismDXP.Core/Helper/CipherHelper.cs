using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TourismDXP.Core.Helper
{
    public class CipherHelper
    {
        private const string AesIV256 = @"!QAZ2WSX#EDC4RFV";
        private const string AesKey256 = @"5TGB&YHN7UJM(IK<5TGB&YHN7UJM(IK<";

        public static string EncryptPassword(string text)
        {
            // AesCryptoServiceProvider
            var aes = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                IV = Encoding.UTF8.GetBytes(AesIV256),
                Key = Encoding.UTF8.GetBytes(AesKey256),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            // Convert string to byte array
            byte[] src = Encoding.Unicode.GetBytes(text);

            // encryption
            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                // Convert byte array to Base64 strings
                return Convert.ToBase64String(dest);
            }
        }


        public static string DecryptPassword(string text)
        {
            // AesCryptoServiceProvider
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                IV = Encoding.UTF8.GetBytes(AesIV256),
                Key = Encoding.UTF8.GetBytes(AesKey256),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            // Convert Base64 strings to byte array
            byte[] src = System.Convert.FromBase64String(text);

            // decryption
            using (ICryptoTransform decrypt = aes.CreateDecryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return Encoding.Unicode.GetString(dest);
            }
        }


        /// <summary>
        /// Genrate Random Token String
        /// </summary>
        /// <returns></returns>
        public static string GenerateCode()
        {
            var validChars = Enumerable.Range('A', 26)
                .Concat(Enumerable.Range('a', 26))
                .Concat(Enumerable.Range('0', 10))
                .Select(i => (char)i)
                .ToArray();
            var randomByte = new byte[64 + 1]; // Max Length + Length
            using (var rnd = new RNGCryptoServiceProvider())
            {
                rnd.GetBytes(randomByte);
                var secretLength = 32 + (int)(32 * (randomByte[0] / (double)byte.MaxValue));
                return new string(
                    randomByte
                        .Skip(1)
                        .Take(secretLength)
                        .Select(b => (int)((validChars.Length - 1) * (b / (double)byte.MaxValue)))
                        .Select(i => validChars[i])
                        .ToArray()
                ) + Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            }
        }
    }
}
