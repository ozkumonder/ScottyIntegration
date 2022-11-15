using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ScottyIntegration.WebApi.Core.Helper
{
    public static class CryptExtensions
    {
        private static readonly byte[] RgbKey;

        private static readonly byte[] RgbIv;

        static CryptExtensions()
        {
            CryptExtensions.RgbKey = Encoding.ASCII.GetBytes("03595252");
            CryptExtensions.RgbIv = Encoding.ASCII.GetBytes("78787878");
        }

        public static string DecryptIt(this string toDecrypt)
        {
            string end;
            string str;
            if (!string.IsNullOrWhiteSpace(toDecrypt))
            {
                try
                {
                    byte[] numArray = Convert.FromBase64String(toDecrypt);
                    MemoryStream memoryStream = new MemoryStream(numArray.Length);
                    DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(CryptExtensions.RgbKey, CryptExtensions.RgbIv), CryptoStreamMode.Read);
                    memoryStream.Write(numArray, 0, numArray.Length);
                    memoryStream.Position = (long)0;
                    end = (new StreamReader(cryptoStream)).ReadToEnd();
                    cryptoStream.Close();
                }
                catch (CryptographicException cryptographicException)
                {
                    throw cryptographicException;
                }
                str = end;
            }
            else
            {
                str = string.Empty;
            }
            return str;
        }

        public static string EncryptIt(this string toEnrypt)
        {
            if (string.IsNullOrWhiteSpace(toEnrypt))
            {
                toEnrypt = string.Empty;
            }
            byte[] bytes = Encoding.ASCII.GetBytes(toEnrypt);
            MemoryStream memoryStream = new MemoryStream(1024);
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(CryptExtensions.RgbKey, CryptExtensions.RgbIv), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] numArray = new byte[(int)memoryStream.Position];
            memoryStream.Position = (long)0;
            memoryStream.Read(numArray, 0, numArray.Length);
            cryptoStream.Close();
            return Convert.ToBase64String(numArray);
        }
    }

}
