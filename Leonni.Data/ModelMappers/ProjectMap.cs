using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class ProjectMap
    {
        public static Project Map(ProjectModel objModel)
        {
            return new Project
            {
                Id = objModel.Id,

                Title = objModel.Title,
                DisciplineId = objModel.DisciplineId,
                //ProjectPic = objModel.ProjectPic
                CreatedDate = objModel.CreatedDate,
                CreatedBy = objModel.CreatedBy,
                DeletedBy = objModel.DeletedBy,
                DeletedDate = objModel.DeletedDate,
                IsDeleted = objModel.IsDeleted,
                
            };

        }

        public static ProjectModel Map(Project objModel)
        {
            return new ProjectModel
            {
                Id = objModel.Id,
                Title = objModel.Title,
                DisciplineId = objModel.DisciplineId,
                CreatedDate = objModel.CreatedDate,
                CreatedBy = objModel.CreatedBy,
                DeletedBy = objModel.DeletedBy,
                DeletedDate = objModel.DeletedDate,
                IsDeleted = objModel.IsDeleted,
                Category = CategoryMap.Map(objModel.Category),
                Discipline = DisciplineMap.Map(objModel.Discipline),
            };

        }

        public static List<Project> Map(List<ProjectModel> lstModel)
        {
            var ret = new List<Project>();
            int i = 0, lstModel_count = lstModel.Count;

            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(ProjectMap.Map(lstModel[i]));
            }

            return ret;
        }

        public static List<ProjectModel> Map(List<Project> lstModel)
        {
            List<ProjectModel> mapped = new List<ProjectModel>();

            foreach (Project listItem in lstModel)
            {
                mapped.Add(ProjectMap.Map(listItem));
            }

            return mapped;
        }
    }
}
