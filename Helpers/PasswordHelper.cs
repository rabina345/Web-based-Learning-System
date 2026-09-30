using System;
using System.Security.Cryptography;

namespace SkillCore.Helpers
{
    // Never store plain-text passwords. This hashes with a random salt so
    // two users with the same password don't end up with the same hash.
    public static class PasswordHelper
    {
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return BitConverter.ToString(saltBytes).Replace("-", "");
        }

        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] combined = System.Text.Encoding.UTF8.GetBytes(password + salt);
                byte[] hashBytes = sha256.ComputeHash(combined);
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        public static bool VerifyPassword(string password, string salt, string storedHash)
        {
            string computedHash = HashPassword(password, salt);
            return computedHash.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}