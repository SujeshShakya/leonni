using System.Collections.Generic;
using Leonni.Models.ViewModels;
using System;
using System.Linq;
using System.Text;

namespace Leonni.Models.ModelMappers
{
     public static class LikeMap
    {
        public static Like Map(LikeModel objModel)
        {
            return new Like
            {
                Id = objModel.Id,
                EntityId = objModel.EntityId,
                EntityName = objModel.EntityName,
                ProfileId = objModel.ProfileId
            };

        }

        public static LikeModel Map(Like objModel)
        {
            return new LikeModel
            {
                Id = objModel.Id,
                EntityId = objModel.EntityId,
                EntityName = objModel.EntityName,
                ProfileId = objModel.ProfileId
            };
        }

        public static List<LikeModel> Map(List<Like> lstModel)
        {
            var ret = new List<LikeModel>();
            int i = 0, lstModel_count = lstModel.Count;

            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(LikeMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
