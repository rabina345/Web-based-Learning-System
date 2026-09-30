using System;

namespace SkillCore.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Convenience — not a real column, just makes checks readable in code
        public bool IsAdmin { get { return RoleID == 1; } }
    }
}