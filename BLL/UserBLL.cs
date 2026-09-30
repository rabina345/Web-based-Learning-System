using SkillCore.Data_Access_Layer;
using SkillCore.Helpers;
using SkillCore.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SkillCore.BLL
{
    public static class UserBLL
    {
        // Registers a new Member account. Throws ArgumentException with a
        // user-friendly message on any validation failure — the .aspx page
        // catches this and shows it to the user.
        public static void Register(string fullName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.");

            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Please enter a valid email address.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters.");

            if (UserDAL.EmailExists(email))
                throw new ArgumentException("An account with this email already exists.");

            string salt = PasswordHelper.GenerateSalt();
            string hash = PasswordHelper.HashPassword(password, salt);

            User newUser = new User
            {
                FullName = fullName.Trim(),
                Email = email.Trim().ToLower(),
                PasswordHash = hash,
                PasswordSalt = salt,
                RoleID = 2,        // 2 = Member — self-registration can NEVER create an Admin
                IsActive = true
            };

            UserDAL.Insert(newUser);
        }

        // Returns the logged-in User on success, or null if email/password
        // don't match. Doesn't throw for wrong credentials — that's a normal
        // outcome, not an error.
        public static User Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            User user = UserDAL.GetByEmail(email.Trim().ToLower());
            if (user == null || !user.IsActive)
                return null;

            bool passwordOk = PasswordHelper.VerifyPassword(password, user.PasswordSalt, user.PasswordHash);
            return passwordOk ? user : null;
        }

        public static List<User> GetAll()
        {
            return UserDAL.GetAll();
        }

        public static User GetById(int userId)
        {
            return UserDAL.GetById(userId);
        }

        // currentAdminId is passed in so an admin can't lock themselves out by
        // demoting or deactivating their own account
        public static void UpdateRoleAndStatus(int userId, int roleId, bool isActive, int currentAdminId)
        {
            if (userId == currentAdminId && (roleId != 1 || !isActive))
                throw new ArgumentException("You can't remove your own admin access or deactivate your own account.");

            UserDAL.UpdateRoleAndStatus(userId, roleId, isActive);
        }

        public static void Delete(int userId, int currentAdminId)
        {
            if (userId == currentAdminId)
                throw new ArgumentException("You can't delete your own account while logged in as it.");

            UserDAL.Delete(userId);
        }
    }
}