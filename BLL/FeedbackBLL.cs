using System;
using System.Collections.Generic;
using SkillCore.Data_Access_Layer;
using SkillCore.Models;

namespace SkillCore.BLL
{
    public static class FeedbackBLL
    {
        public static List<Feedback> GetAll()
        {
            return FeedbackDAL.GetAll();
        }

        // Used by the public Contact page
        public static void Submit(string name, string email, string subject, string message, int? userId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message is required.");

            Feedback f = new Feedback
            {
                UserID = userId,
                Name = name.Trim(),
                Email = email.Trim(),
                Subject = string.IsNullOrWhiteSpace(subject) ? "(No subject)" : subject.Trim(),
                Message = message.Trim()
            };
            FeedbackDAL.Insert(f);
        }

        public static void MarkAsRead(int feedbackId)
        {
            FeedbackDAL.MarkAsRead(feedbackId);
        }

        public static void Delete(int feedbackId)
        {
            FeedbackDAL.Delete(feedbackId);
        }
    }
}