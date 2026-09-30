using System;
using System.Collections.Generic;
using SkillCore.Data_Access_Layer;
using SkillCore.Models;

namespace SkillCore.BLL
{
    public static class PortfolioBLL
    {
        public static List<Portfolio> GetByUserId(int userId)
        {
            return PortfolioDAL.GetByUserId(userId);
        }

        public static Portfolio GetById(int portfolioId)
        {
            return PortfolioDAL.GetById(portfolioId);
        }

        public static void Save(Portfolio p)
        {
            if (string.IsNullOrWhiteSpace(p.Title))
                throw new ArgumentException("Title is required.");
            if (string.IsNullOrWhiteSpace(p.FilePath))
                throw new ArgumentException("Please choose a file to upload.");

            if (p.PortfolioID == 0)
                PortfolioDAL.Insert(p);
            else
                PortfolioDAL.Update(p);
        }

        public static void Delete(int portfolioId, int currentUserId, bool isAdmin)
        {
            Portfolio p = PortfolioDAL.GetById(portfolioId);
            if (p == null)
                throw new ArgumentException("Portfolio item not found.");

            if (p.UserID != currentUserId && !isAdmin)
                throw new ArgumentException("You can't delete a portfolio item that isn't yours.");

            PortfolioDAL.Delete(portfolioId);
        }

        public static List<Portfolio> GetAll(out Dictionary<int, string> uploaderNames)
        {
            return PortfolioDAL.GetAllWithUserName(out uploaderNames);
        }
    }
}