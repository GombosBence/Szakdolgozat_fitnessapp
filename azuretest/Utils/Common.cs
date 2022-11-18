using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace azuretest.Utils
{
    public class Common
    {
        //random salt string
        [Obsolete]
        public static byte[] GetRandomSalt(int length)
        {
            var random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[length];
            random.GetNonZeroBytes(salt);
            return salt;
        }

        //password with salt
        [Obsolete]
        public static byte[] SaltHashPassword(byte[] password, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainTextWithSaltBytes  = new byte[password.Length + salt.Length];
            for (int i = 0; i < password.Length; i++)
                plainTextWithSaltBytes[i] = password[i];
            for (int j = 0; j < salt.Length; j++)
                plainTextWithSaltBytes[password.Length + j] = salt[j];
            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

    }
}
