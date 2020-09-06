using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public class FrontEntityFileMap
    {
        public static FrontEntityFile Map(FrontEntityFileModel objModel)
        {
            return new FrontEntityFile
            {
                Id = objModel.Id,
                SectionId = objModel.SectionId,
                EntityId = objModel.EntityId,
                FileId = objModel.FileId
            };

        }

        public static FrontEntityFileModel Map(FrontEntityFile objModel)
        {
            return new FrontEntityFileModel
            {
                Id = objModel.Id,
                SectionId = objModel.SectionId,
                EntityId = objModel.EntityId,
                FileId = objModel.FileId
            };

        }

        public static List<FrontEntityFileModel> Map(List<FrontEntityFile> lstModel)
        {
            var ret = new List<FrontEntityFileModel>();
            int i = 0, lstModel_count = lstModel.Count;

            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(FrontEntityFileMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
