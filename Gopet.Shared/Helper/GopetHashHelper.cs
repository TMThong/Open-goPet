

using System.Security.Cryptography;
using System.Text;

namespace Gopet.Shared.Helper
{
    public class GopetHashHelper
    {
        public static string ComputeSha256Hash(string Text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes("goPet" + String.Join(string.Empty, Text.Reverse()) + "________"));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifySha256Hash(string Hash, string Text)
        {
            string hashOfInput = ComputeSha256Hash(Text);
            return hashOfInput == Hash;
        }
    }
}