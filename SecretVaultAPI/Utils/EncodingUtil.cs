using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace SecretVaultAPI.Utils
{
    public class EncodingUtil
    {
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    public string EncryptString(string key, string plainText)
    {
      byte[] iv = new byte[16];
      byte[] array;

      using (Aes aes = Aes.Create())
      {
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = iv;

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
          {
            using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
            {
              streamWriter.Write(plainText);
            }

            array = memoryStream.ToArray();
          }
        }
      }

      return Convert.ToBase64String(array);
    }

    public string DecryptString(string key, string cipherText)
    {
      byte[] iv = new byte[16];
      byte[] buffer = Convert.FromBase64String(cipherText);

      using (Aes aes = Aes.Create())
      {
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = iv;
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using (MemoryStream memoryStream = new MemoryStream(buffer))
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
          {
            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
            {
              return streamReader.ReadToEnd();
            }
          }
        }
      }
    }
  }
}
