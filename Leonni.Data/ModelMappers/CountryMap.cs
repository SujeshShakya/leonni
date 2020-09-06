using System.Collections.Generic;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class CountryMap
    {
        public static Country Map(CountryModel objModel)
        {
            return new Country
            {
                Id = objModel.Id,
                Name = objModel.Name
            };

        }

        public static CountryModel Map(Country objModel)
        {
            return new CountryModel
            {
                Id = objModel.Id,
                Name = objModel.Name
            };
        }

        public static List<CountryModel> Map(List<Country> lstModel)
        {
            var ret = new List<CountryModel>();
            int i = 0, lstModel_count = lstModel.Count;
            
            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(CountryMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
