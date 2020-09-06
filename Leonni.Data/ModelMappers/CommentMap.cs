using System.Collections.Generic;
using Leonni.Models.ViewModels;
using System;
using System.Linq;
using System.Text;

namespace Leonni.Models.ModelMappers
{
    public static class CommentMap
    {
        public static Comment Map(CommentModel objModel)
        {
            return new Comment
            {
                ID = objModel.ID,
                Date = Convert.ToDateTime(objModel.Date),
                Body = objModel.Body,
                ThreadID = objModel.ThreadID,
                UserID = objModel.UserID,
                Email = objModel.Email,
                ProfileId = objModel.ProfileId
                //CommentThread = CommentThreadMap.Map(objModel.CommentThread)
            };

        }

        public static CommentModel Map(Comment objModel)
        {
            return new CommentModel
            {
                ID = objModel.ID,
                Date = Convert.ToString(objModel.Date),
                Body = objModel.Body,
                ThreadID = objModel.ThreadID,
                UserID = objModel.UserID,
                Email = objModel.Email,
                ProfileId = objModel.ProfileId
                //CommentThread = CommentThreadMap.Map(objModel.CommentThread)
            };
        }

        public static List<CommentModel> Map(List<Comment> lstModel)
        {
            var ret = new List<CommentModel>();
            int i = 0, lstModel_count = lstModel.Count;

            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(CommentMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
