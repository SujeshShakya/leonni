using System.Collections.Generic;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class YearMap
    {
        public static Year Map(YearModel objModel)
        {
            return new Year
            {
                Id = objModel.Id,
                LanguageId = objModel.LanguageId,
                YearName = objModel.Year
            };

        }

        public static YearModel Map(Year objModel)
        {
            return new YearModel
            {
                Id = objModel.Id,
                LanguageId = objModel.LanguageId,
                Year = objModel.YearName
            };
        }

        public static List<YearModel> Map(List<Year> lstModel)
        {
            var ret = new List<YearModel>();
            int i = 0, lstModel_count = lstModel.Count;
            
            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(YearMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
