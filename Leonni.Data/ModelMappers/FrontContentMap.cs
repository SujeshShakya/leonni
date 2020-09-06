using System.Collections.Generic;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class FrontContentMap
    {
        public static FrontContent Map(FrontContentModel objModel)
        {
            return new FrontContent
            {
                Id = objModel.Id,
                SectionId = objModel.SectionId,
                ContentId = objModel.ContentId,
                CreateDate = objModel.CreateDate
            };

        }

        public static FrontContentModel Map(FrontContent objModel)
        {
            return new FrontContentModel
            {
                Id = objModel.Id,
                SectionId = objModel.SectionId,
                ContentId = objModel.ContentId,
                CreateDate = objModel.CreateDate
            };
        }

        public static List<FrontContentModel> Map(List<FrontContent> lstModel)
        {
            var ret = new List<FrontContentModel>();
            int i = 0, lstModel_count = lstModel.Count;
            
            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(FrontContentMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
