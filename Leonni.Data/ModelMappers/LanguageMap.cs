using System.Collections.Generic;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class LanguageMap
    {
        public static Language Map(LanguageModel objModel)
        {
            return new Language
            {
                Id = objModel.Id,
                LanguageName = objModel.LanguageName.Trim(),
                LanguageCode = objModel.LanguageCode,
                LanguageDirection = objModel.LanguageDirection.Trim()
            };

        }

        public static LanguageModel Map(Language objModel)
        {
            return new LanguageModel
            {
                Id = objModel.Id,
                LanguageName = objModel.LanguageName.Trim(),
                LanguageCode = objModel.LanguageCode,
                LanguageDirection = objModel.LanguageDirection.Trim()
            };
        }

        public static List<LanguageModel> Map(List<Language> lstModel)
        {
            var ret = new List<LanguageModel>();
            int i = 0, lstModel_count = lstModel.Count;
            
            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(LanguageMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
