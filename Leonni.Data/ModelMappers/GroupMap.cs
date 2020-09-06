using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class GroupMap
    {
        public static Group Map(GroupModel objModel)
        {
            if (objModel != null)
            {
                return new Group
                {
                    Id = objModel.Id,
                    IsVisible = objModel.IsVisible,
                    IsCloseIncome = objModel.IsCloseIncome,
                    AllowContents = objModel.AllowContents,
                    Title = objModel.Title,
                    DisciplineId = objModel.DisciplineId,
                    Summary = objModel.Summary,
                    Text = objModel.Text,
                    CountryName = objModel.CountryName,
                    ProvinceId = objModel.ProvinceId,
                    //GroupPic = objModel.GroupPic
                    CreatedDate = objModel.CreatedDate,
                    CreatedBy = objModel.CreatedBy,
                    DeletedDate = objModel.DeletedDate,
                    Status = objModel.Status,
                    //UserProfile = UserProfileMap.Map(objModel.UserProfile)

                };
            }
            return new Group();

        }

        public static GroupModel Map(Group objModel)
        {
            if (objModel != null)
            {
                return new GroupModel
                {
                    Id = objModel.Id,
                    IsVisible = objModel.IsVisible,
                    IsCloseIncome = objModel.IsCloseIncome,
                    AllowContents = objModel.AllowContents,
                    Title = objModel.Title,
                    DisciplineId = objModel.DisciplineId,
                    Summary = objModel.Summary,
                    Text = objModel.Text,
                    CountryName = objModel.CountryName,
                    ProvinceId = objModel.ProvinceId,
                    CreatedDate = objModel.CreatedDate,
                    CreatedBy = objModel.CreatedBy,
                    DeletedDate = objModel.DeletedDate,
                    DisciplineName = objModel.Discipline.DisciplineName,
                    Status = objModel.Status,
                    //UserProfile = UserProfileMap.Map(objModel.UserProfile)
                };
            }
            else
                return null;

        }

        //public static List<GroupModel> Map(List<Group> lstModel)
        //{
        //    var ret = new List<GroupModel>();
        //    int i = 0, lstModel_count = lstModel.Count;

        //    for (i = 0; i < lstModel_count; i++)
        //    {
        //        ret.Add(GroupMap.Map(lstModel[i]));
        //    }

        //    return ret;
        //}

        public static List<GroupModel> Map(List<Group> lstModel)
        {
            List<GroupModel> mapped = new List<GroupModel>();

            foreach (Group listItem in lstModel)
            {
                mapped.Add(GroupMap.Map(listItem));
            }

            return mapped;
        }
    }
}
