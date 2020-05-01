
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Praticis.Framework.Tools.Helpers
{
    public static class CryptoHelper
    {
        /*
            TODO: Mordenize encrypt system. https://docs.microsoft.com/pt-br/dotnet/core/compatibility/cryptography
            It is old but used by Praticis System, to change need make more refactories in Praticis. 
        */
        private const string PASSWORD = "AAECAwQFBgcICQoLDA0ODw==";

        public static string Encrypt(string plaintext)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            byte[] plaintextByte = Encoding.Unicode.GetBytes(plaintext);
            byte[] saltByte = Encoding.ASCII.GetBytes(PASSWORD.Length.ToString());

            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(PASSWORD, saltByte);
            ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plaintextByte, 0, plaintextByte.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();
            encryptor.Dispose();

            return Convert.ToBase64String(cipherBytes);
        }

        public static string Decrypt(string ciphertext)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            byte[] ciphertextByte = Convert.FromBase64String(ciphertext);
            byte[] saltByte = Encoding.ASCII.GetBytes(PASSWORD.Length.ToString());

            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(PASSWORD, saltByte);
            ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream(ciphertextByte);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainText = new byte[ciphertextByte.Length];

            int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);

            memoryStream.Close();
            cryptoStream.Close();

            return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
        }
    }
}
