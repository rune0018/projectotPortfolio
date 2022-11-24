using System;
using System.Security.Cryptography;

namespace ProjectorAPI.models
{
    public class Encrypt
    {
        public static string sha512(string toEncrypt)
        {
            
            string Finalresult = "";
            char[] chars = toEncrypt.ToCharArray();
            byte[] bytes = new byte[chars.Length + 10];//salt
            for (int i = 0; i < chars.Length; i++)
            {
                bytes[i] = (byte)chars[i];
            }
            SHA512 sham = new SHA512Managed();
            var encryptedBytes = sham.ComputeHash(bytes);
            var encryptedAsChars = new char[encryptedBytes.Length];
            for (var i = 0; i < encryptedBytes.Length; i++)
            {
                encryptedAsChars[i] = (char)encryptedBytes[i];
            }
            foreach (char c in encryptedAsChars)
            {
                Finalresult += c;
            }
            return Finalresult;
        }
        public static string sha256(string toEncrypt)
        {
            string Finalresult = "";
            char[] chars = toEncrypt.ToCharArray();
            byte[] bytes = new byte[chars.Length + 10];//salt
            for (int i = 0; i < chars.Length; i++)
            {
                bytes[i] = (byte)chars[i];
            }
            SHA256 sham = new SHA256Managed();
            var encryptedBytes = sham.ComputeHash(bytes);
            var encryptedAsChars = new char[encryptedBytes.Length];
            for (var i = 0; i < encryptedBytes.Length; i++)
            {
                encryptedAsChars[i] = (char)encryptedBytes[i];
            }
            foreach (char c in encryptedAsChars)
            {
                Finalresult += c;
            }
            return Finalresult;
        }
    }
}
