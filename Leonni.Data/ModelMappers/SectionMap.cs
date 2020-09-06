using System.Collections.Generic;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class SectionMap
    {
        public static Section Map(SectionModel objModel)
        {
            return new Section
            {
                Id = objModel.Id,
                LanguageId = objModel.LanguageId,
                SectionName = objModel.SectionName
            };

        }

        public static SectionModel Map(Section objModel)
        {
            return new SectionModel
            {
                Id = objModel.Id,
                LanguageId = objModel.LanguageId,
                SectionName = objModel.SectionName
            };
        }

        public static List<SectionModel> Map(List<Section> lstModel)
        {
            var ret = new List<SectionModel>();
            int i = 0, lstModel_count = lstModel.Count;
            
            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(SectionMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
