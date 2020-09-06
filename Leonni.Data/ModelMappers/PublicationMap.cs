using System.Collections.Generic;
using Leonni.Models.ViewModels;
using System;

namespace Leonni.Models.ModelMappers
{
    public static class PublicationMap
    {
        public static Publication Map(PublicationModel objModel)
        {
            return new Publication
            {
                Id = objModel.Id,
                GroupId = objModel.GroupId,
                ProjectId = objModel.ProjectId,
                Title = objModel.Title,
                CategoryId = objModel.CategoryId,
                DisciplineId = objModel.DisciplineId,
                Resume = objModel.Resume,
                Description = objModel.Description,
                DeletedBy = objModel.DeletedBy,
                DisabledBy = objModel.DisabledBy,
                DeletedDate = objModel.DeletedDate,
                CreatedDate = objModel.CreatedDate,
                ModifiedDate = objModel.ModifiedDate,
                CreatedBy = objModel.CreatedBy,
                Status = objModel.Status
            };

        }

        public static PublicationModel Map(Publication objModel)
        {
            return new PublicationModel
            {
                Id = objModel.Id,
                GroupId = objModel.GroupId,
                ProjectId = objModel.ProjectId,
                Title = objModel.Title,
                CategoryId = objModel.CategoryId,
                DisciplineId = objModel.DisciplineId,
                Resume = objModel.Resume,
                Description = objModel.Description,
                DeletedBy = objModel.DeletedBy,
                DisabledBy = objModel.DisabledBy,
                DeletedDate = objModel.DeletedDate,
                CreatedDate = objModel.CreatedDate,
                ModifiedDate = objModel.ModifiedDate,
                Category = CategoryMap.Map(objModel.Category),
                Discipline = DisciplineMap.Map(objModel.Discipline),
                CreatedBy = objModel.CreatedBy,
                Status = objModel.Status
               // UserProfile = UserProfileMap.Map(objModel.UserProfile)

            };
        }

        public static List<PublicationModel> Map(List<Publication> lstModel)
        {
            var ret = new List<PublicationModel>();
            int i = 0, lstModel_count = lstModel.Count;

            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(PublicationMap.Map(lstModel[i]));
            }

            return ret;
        }

        public static List<Publication> Map(List<PublicationModel> lstModel)
        {
            var ret = new List<Publication>();
            int i = 0, lstModel_count = lstModel.Count;

            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(PublicationMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
