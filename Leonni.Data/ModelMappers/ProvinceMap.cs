using System.Collections.Generic;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class ProvinceMap
    {
        public static Province Map(ProvinceModel objModel)
        {
            return new Province
            {
                Id = objModel.Id,
                NAME_1 = objModel.Name
            };

        }

        public static ProvinceModel Map(Province objModel)
        {
            return new ProvinceModel
            {
                Id = objModel.Id,
                Name = objModel.NAME_1
            };
        }

        public static List<ProvinceModel> Map(List<Province> lstModel)
        {
            var ret = new List<ProvinceModel>();
            int i = 0, lstModel_count = lstModel.Count;
            
            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(ProvinceMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
