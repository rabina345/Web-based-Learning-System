using System.Collections.Generic;
using SkillCore.Data_Access_Layer;
using SkillCore.Models;

namespace SkillCore.BLL
{
    public static class QuizBLL
    {
        public static List<Quiz> GetByCourseId(int courseId)
        {
            return QuizDAL.GetByCourseId(courseId);
        }

        public static Quiz GetById(int quizId)
        {
            return QuizDAL.GetById(quizId);
        }

        public static List<Question> GetQuestions(int quizId)
        {
            return QuestionDAL.GetByQuizId(quizId);
        }

        public static List<QuestionOption> GetOptions(int questionId)
        {
            return QuestionDAL.GetOptionsByQuestionId(questionId);
        }

        // Takes the student's chosen optionId per questionId, scores it
        // server-side (never trust a score sent from the browser), and
        // records the attempt. Returns (score, totalMarks, isPassed).
        public static (int score, int totalMarks, bool isPassed) SubmitAttempt(
            int userId, int quizId, Dictionary<int, int> answers)
        {
            Quiz quiz = QuizDAL.GetById(quizId);
            List<Question> questions = QuestionDAL.GetByQuizId(quizId);

            int score = 0;
            int totalMarks = 0;

            foreach (Question q in questions)
            {
                totalMarks += q.Marks;

                if (answers.ContainsKey(q.QuestionID))
                {
                    int chosenOptionId = answers[q.QuestionID];
                    if (QuestionDAL.IsOptionCorrect(chosenOptionId))
                    {
                        score += q.Marks;
                    }
                }
            }

            bool isPassed = totalMarks > 0 && (score * 100 / totalMarks) >= quiz.PassMark;

            QuizAttemptDAL.Insert(userId, quizId, score, totalMarks, isPassed);

            return (score, totalMarks, isPassed);
        }
    }
}