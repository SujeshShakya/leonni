using System.Collections.Generic;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class DisciplineMap
    {
        public static Discipline Map(DisciplineModel objModel)
        {
            return new Discipline
            {
                Id = objModel.Id,
                LanguageId = objModel.LanguageId,
                DisciplineName = objModel.DisciplineName
            };

        }

        public static DisciplineModel Map(Discipline objModel)
        {
            return new DisciplineModel
            {
                Id = objModel.Id,
                LanguageId = objModel.LanguageId,
                DisciplineName = objModel.DisciplineName
            };
        }

        public static List<DisciplineModel> Map(List<Discipline> lstModel)
        {
            var ret = new List<DisciplineModel>();
            int i = 0, lstModel_count = lstModel.Count;
            
            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(DisciplineMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
