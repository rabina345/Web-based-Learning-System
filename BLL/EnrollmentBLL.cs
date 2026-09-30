using System;
using System.Collections.Generic;
using SkillCore.Data_Access_Layer;
using SkillCore.Models;

namespace SkillCore.BLL
{
    public static class EnrollmentBLL
    {
        public static bool IsEnrolled(int userId, int courseId)
        {
            return EnrollmentDAL.IsEnrolled(userId, courseId);
        }

        public static void Enroll(int userId, int courseId)
        {
            if (EnrollmentDAL.IsEnrolled(userId, courseId))
                throw new ArgumentException("You're already enrolled in this course.");

            EnrollmentDAL.Enroll(userId, courseId);
        }

        public static List<Course> GetEnrolledCourses(int userId)
        {
            return EnrollmentDAL.GetEnrolledCourses(userId);
        }
    }
}