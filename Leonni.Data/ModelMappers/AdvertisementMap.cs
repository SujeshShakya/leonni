using System.Collections.Generic;
using Leonni.Models.ViewModels;
using System;

namespace Leonni.Models.ModelMappers
{
    public static class AdvertisementMap
    {
        public static Advertisement Map(AdvertisementModel objModel)
        {
            return new Advertisement
            {
                Id = objModel.Id,
                CategoryId = objModel.CategoryId,
                DisciplineId = objModel.DisciplineId,
                CountryName = objModel.CountryName,
                UserProfileId = objModel.UserProfileId,
                StateId = objModel.ProvinceId,
                Title = objModel.Title,
                Link = objModel.Link,
                BeginningDay = Convert.ToDateTime(objModel.BeginningDay),
                EngineeredTo = objModel.EngineeredTo
            
            };

        }

        public static AdvertisementModel Map(Advertisement objModel)
        {
            return new AdvertisementModel
            {
                Id = objModel.Id,
                CategoryId = objModel.CategoryId,
                DisciplineId = objModel.DisciplineId,
                CountryName = objModel.CountryName,
                UserProfileId = objModel.UserProfileId,
                ProvinceId = objModel.StateId,
                Title = objModel.Title,
                Link = objModel.Link,
                BeginningDay = objModel.BeginningDay.ToShortDateString(),
                EngineeredTo = objModel.EngineeredTo
            };
        }

        public static List<AdvertisementModel> Map(List<Advertisement> lstModel)
        {
            var ret = new List<AdvertisementModel>();
            int i = 0, lstModel_count = lstModel.Count;
            
            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(AdvertisementMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
