using System;
using System.Security.Cryptography;
using System.Text;

namespace myreadiness.API.Helpers
{
    public class clsEncryptAndDecryptSP
    {
        public static string hash = "MyHashCode";

        public static string EncryptData(string inputString)
        {
            string strEncrypt = "";
            byte[] data = UTF8Encoding.UTF8.GetBytes(inputString);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    strEncrypt = Convert.ToBase64String(results, 0, results.Length);
                }
            }

            byte[] data1 = UTF32Encoding.UTF32.GetBytes(strEncrypt);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF32Encoding.UTF32.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data1, 0, data1.Length);
                    strEncrypt = Convert.ToBase64String(results, 0, results.Length);
                }
            }
            return strEncrypt;
        }

        public static string DecryptData(string inputString)
        {
            string strDecrypt = "";
            byte[] data1 = Convert.FromBase64String(inputString);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF32Encoding.UTF32.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data1, 0, data1.Length);
                    strDecrypt = UTF32Encoding.UTF32.GetString(results);
                }
            }

            byte[] data = Convert.FromBase64String(strDecrypt);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    strDecrypt = UTF8Encoding.UTF8.GetString(results);
                }
            }
            return strDecrypt;
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}