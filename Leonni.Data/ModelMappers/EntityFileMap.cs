using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public class EntityFileMap
    {
        public static EntityFile Map(EntityFileModel objModel)
        {
            return new EntityFile
            {
                Id = objModel.Id,
                SectionId = objModel.SectionId,
                EntityId = objModel.EntityId,
                FileId = objModel.FileId,
            };

        }

        public static EntityFileModel Map(EntityFile objModel)
        {
            return new EntityFileModel
            {
                Id = objModel.Id,
                SectionId = objModel.SectionId,
                EntityId = objModel.EntityId,
                FileId = objModel.FileId,
                Section = SectionMap.Map(objModel.Section)
            };

        }
        public static List<EntityFileModel> Map(List<EntityFile> lstModel)
        {
            var ret = new List<EntityFileModel>();
            int i = 0, lstModel_count = lstModel.Count;

            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(EntityFileMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
