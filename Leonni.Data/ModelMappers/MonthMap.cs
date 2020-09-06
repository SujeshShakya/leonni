using System.Collections.Generic;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class MonthMap
    {
        public static Month Map(MonthModel objModel)
        {
            return new Month
            {
                Id = objModel.Id,
                LanguageId = objModel.LanguageId,
                MonthName = objModel.Month
            };

        }

        public static MonthModel Map(Month objModel)
        {
            return new MonthModel
            {
                Id = objModel.Id,
                LanguageId = objModel.LanguageId,
                Month = objModel.MonthName
            };
        }

        public static List<MonthModel> Map(List<Month> lstModel)
        {
            var ret = new List<MonthModel>();
            int i = 0, lstModel_count = lstModel.Count;
            
            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(MonthMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
