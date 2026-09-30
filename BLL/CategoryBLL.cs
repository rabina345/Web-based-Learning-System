using System.Collections.Generic;
using SkillCore.Data_Access_Layer;
using SkillCore.Models;

namespace SkillCore.BLL
{
    public static class CategoryBLL
    {
        public static List<Category> GetAll()
        {
            return CategoryDAL.GetAll();
        }

        public static Category GetById(int categoryId)
        {
            return CategoryDAL.GetById(categoryId);
        }

        public static void Save(Category c)
        {
            if (string.IsNullOrWhiteSpace(c.CategoryName))
                throw new System.ArgumentException("Category name is required.");

            if (c.CategoryID == 0)
                CategoryDAL.Insert(c);
            else
                CategoryDAL.Update(c);
        }

        public static void Delete(int categoryId)
        {
            CategoryDAL.Delete(categoryId);
        }
    }
}