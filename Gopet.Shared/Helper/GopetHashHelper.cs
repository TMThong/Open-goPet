

using System.Security.Cryptography;
using System.Text;

namespace Gopet.Shared.Helper
{
    public class GopetHashHelper
    {
        public static string ComputeHash(string Text)
        {
            return BCrypt.Net.BCrypt.HashPassword(Text, 12);
        }

        public static bool VerifyHash(string Hash, string Text)
        {
            return BCrypt.Net.BCrypt.Verify(Text, Hash);
        }
    }
}