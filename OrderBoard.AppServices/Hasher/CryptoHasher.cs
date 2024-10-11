using System.Security.Cryptography;
using System.Text;

namespace OrderBoard.AppServices.Hasher
{
    public class CryptoHasher
    {
        public static string GetBase64Hash(string stringToEncrypt)
        {
            var buffer = Encoding.UTF8.GetBytes(stringToEncrypt);
            var sha = SHA256.Create() as HashAlgorithm;
            var hash = sha.ComputeHash(buffer);

            return Convert.ToBase64String(hash);
        }
    }
}
