
using System.Text;
using XSystem.Security.Cryptography;

namespace API_School_own_prj.ServicesManager
{
    public static class PasswordHasher
    {
        public static string? HashPassword(string? password)
        {
            ArgumentNullException.ThrowIfNullOrEmpty("Please insert a valid password");

            var hasher = new SHA1Managed();
            var buffer = Encoding.UTF8.GetBytes(password!);
            var hash = hasher.ComputeHash(buffer);
            return BitConverter.ToString(hash).Replace("-", string.Empty);

        }

        public static Boolean VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null || providedPassword == null) return false;

            var hashedProvidePwd = HashPassword(providedPassword);

            if (hashedProvidePwd == null) return false;

            return hashedProvidePwd.Equals(hashedPassword, StringComparison.InvariantCultureIgnoreCase);

        }
    }
}
