using System.Collections.Generic;
using Leonni.Models.ViewModels;
using System;

namespace Leonni.Models.ModelMappers
{
    public static class FavouriteMap
    {
        public static Favourite Map(FavouriteModel objModel)
        {
            return new Favourite
            {
                Id = objModel.Id,
                EntityId = objModel.EntityId,
                SectionId = objModel.SectionId,
                UserProfileId = objModel.UserProfileId
            };

        }

        public static FavouriteModel Map(Favourite objModel)
        {
            return new FavouriteModel
            {
                Id = objModel.Id,
                EntityId = objModel.EntityId,
                SectionId = objModel.SectionId,
                UserProfileId = objModel.UserProfileId,
                Section = SectionMap.Map(objModel.Section)
            };
        }

        public static List<FavouriteModel> Map(List<Favourite> lstModel)
        {
            var ret = new List<FavouriteModel>();
            int i = 0, lstModel_count = lstModel.Count;

            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(FavouriteMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
