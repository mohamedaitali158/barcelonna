using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Ashfall.Security
{
    /// <summary>
    /// Encrypts/decrypts save payloads and appends SHA256 checksum.
    /// Corruption or tampering throws InvalidDataException.
    /// </summary>
    public static class SaveEncryptionUtility
    {
        // Integration note: move to secure secret handling for production builds.
        private static readonly byte[] Key = SHA256.HashData(Encoding.UTF8.GetBytes("Ashfall_Save_Key_seed"));
        private static readonly byte[] Iv = MD5.HashData(Encoding.UTF8.GetBytes("Ashfall_Save_IV_seed"));

        public static string EncryptWithChecksum(string plainJson)
        {
            string checksum = ComputeSha256(plainJson);
            string payload = checksum + "::" + plainJson;

            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = Iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(payload);
            byte[] cipher = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(cipher);
        }

        public static string DecryptAndValidate(string cipherText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = Iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            byte[] bytes = Convert.FromBase64String(cipherText);
            byte[] plain = decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            string payload = Encoding.UTF8.GetString(plain);

            string[] parts = payload.Split(new[] { "::" }, 2, StringSplitOptions.None);
            if (parts.Length != 2) throw new InvalidDataException("Invalid encrypted save payload.");

            string actual = ComputeSha256(parts[1]);
            if (!string.Equals(parts[0], actual, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("Save checksum mismatch. Save may be modified.");

            return parts[1];
        }

        private static string ComputeSha256(string input)
        {
            using var sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
