using System.Collections.Generic;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class CategoryMap
    {
        public static Category Map(CategoryModel objModel)
        {
            return new Category
            {
                Id = objModel.Id,
                LanguageId = objModel.LanguageId,
                CategoryName = objModel.CategoryName,
            };

        }

        public static CategoryModel Map(Category objModel)
        {
            return new CategoryModel
            {
                Id = objModel.Id,
                LanguageId = objModel.LanguageId,
                CategoryName = objModel.CategoryName
            };
        }

        public static List<CategoryModel> Map(List<Category> lstModel)
        {
            var ret = new List<CategoryModel>();
            int i = 0, lstModel_count = lstModel.Count;
            
            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(CategoryMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
