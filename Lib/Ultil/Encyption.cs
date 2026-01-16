using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Ultil
{
    public class Encyption
    {
        public Encyption()
        {

        }
        //private const string passPhrase = "1567!!$(%athngLang674ikt77565887t8230)(()**&^(&)(&)7hghgfhg8776876hjgfhgfghfhgfd765gjhfcd";  // can be any string
        //private const string saltValue = "98434ThangCoG8Wyt&^^&%*&^()JHHLKH)(&(*&^*(&^(*&^(*&_)(*;olkjmsdwkFTganLan";  // can be any string
        //private const string hashAlgorithm = "SHA256";  // can be "MD5"
        //private const int passwordIterations = 2;  // can be any number
        //private const string initVector = "859432940576215$"; // must be 16 bytes
        //private const int keySize = 256;
        private const string passPhrase = "1567!!$(%athngLang";  // can be any string
        private const string saltValue = "98434ThangCoG8WytwkFTganLan";  // can be any string
        private const string hashAlgorithm = "SHA1";  // can be "MD5"
        private const int passwordIterations = 2;  // can be any number
        private const string initVector = "859432940576215$"; // must be 16 bytes
        private const int keySize = 256;
        public static string Encrypt(string plainText,string sall="")
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue+ sall);

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,encryptor,CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);            
            return cipherText;
        }
        public static string Decrypt(string cipherText, string sall = "")
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue + sall);

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            // Return decrypted string.
            return plainText;
        }


    }

}
