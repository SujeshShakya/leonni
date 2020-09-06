using System.Collections.Generic;
using Leonni.Models.ViewModels;
using System;

namespace Leonni.Models.ModelMappers
{
    public static class CommentThreadMap
    {
        public static CommentThread Map(CommentThreadModel objModel)
        {
            return new CommentThread
            {
                ID = objModel.ID,
                URL = objModel.URL,
                SectionId = objModel.SectionId,
                EntityID = objModel.EntityID

            };
        }

        public static CommentThreadModel Map(CommentThread objModel)
        {
            return new CommentThreadModel
            {
                ID = objModel.ID,
                URL = objModel.URL,
                SectionId = objModel.SectionId,
                EntityID = objModel.EntityID
            };
        }

        public static List<CommentThreadModel> Map(List<CommentThread> lstModel)
        {
            var ret = new List<CommentThreadModel>();
            int i = 0, lstModel_count = lstModel.Count;

            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(CommentThreadMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
