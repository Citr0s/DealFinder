using System;
using System.Security.Cryptography;
using System.Text;

namespace DealFinder.Core.Security
{
    public interface IAesEncryptor
    {
        string Encrypt(string data);
        string Decrypt(string data);
    }

    public class AesEncryptor : IAesEncryptor
    {
        private readonly string _key;

        public AesEncryptor(string key)
        {
            _key = key;
        }

        public string Encrypt(string data)
        {
            var cipher = Aes.Create();
            cipher.Mode = CipherMode.CBC;
            cipher.Padding = PaddingMode.PKCS7;
            cipher.KeySize = 0x80;
            cipher.BlockSize = 0x80;
            var passBytes = Encoding.UTF8.GetBytes(_key);
            var encryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var len = passBytes.Length;

            if (len > encryptionkeyBytes.Length)
                len = encryptionkeyBytes.Length;

            Array.Copy(passBytes, encryptionkeyBytes, len);

            cipher.Key = encryptionkeyBytes;
            cipher.IV = encryptionkeyBytes;

            var objtransform = cipher.CreateEncryptor();
            var textDataByte = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
        }

        public string Decrypt(string data)
        {
            var cipher = Aes.Create();
            cipher.Mode = CipherMode.CBC;
            cipher.Padding = PaddingMode.PKCS7;
            cipher.KeySize = 0x80;
            cipher.BlockSize = 0x80;

            var encryptedTextByte = Convert.FromBase64String(data);
            var passBytes = Encoding.UTF8.GetBytes(_key);
            var encryptionkeyBytes = new byte[0x10];
            var len = passBytes.Length;

            if (len > encryptionkeyBytes.Length)
                len = encryptionkeyBytes.Length;

            Array.Copy(passBytes, encryptionkeyBytes, len);
            cipher.Key = encryptionkeyBytes;
            cipher.IV = encryptionkeyBytes;
            var textByte = cipher.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
            return Encoding.UTF8.GetString(textByte);
        }
    }
}
