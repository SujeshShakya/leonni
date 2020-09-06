using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public static class UserProfileMap
    {
        public static UserProfile Map(UserProfileModel objModel)
        {
            return new UserProfile
            {
                Id = objModel.Id,
                UserId = objModel.UserId,
                Address = objModel.Address,
                BirthDate = objModel.BirthDate,
                CategoryId = objModel.CategoryId,
                Company = objModel.Company,
                CountryName = objModel.CountryName,
                Description = objModel.Description,
                DisciplineId = objModel.DisciplineId,
                FirstName = objModel.FirstName,
                Locality = objModel.Locality,
                Occupation = objModel.Occupation,
                Phone = objModel.Phone,
                Sex = objModel.Sex,
                StateId = objModel.StateId,
                Summary = objModel.Summary,
                SurName = objModel.SurName,
                WebLink = objModel.WebLink,
                ZipCode = objModel.ZipCode,
                IsLeonniTeam = objModel.IsLeonniTeam,
                CreatedDate = objModel.CreatedDate,
                ModifiedDate = objModel.ModifiedDate,
                StatusId = objModel.StatusId
            };

        }

        public static UserProfileModel Map(UserProfile objModel)
        {
            return new UserProfileModel
            {
                Id = objModel.Id,
                UserId = objModel.UserId,
                Address = objModel.Address,
                BirthDate = objModel.BirthDate,
                CategoryId = objModel.CategoryId,
                Company = objModel.Company,
                CountryName = objModel.CountryName,
                Description = objModel.Description,
                DisciplineId = objModel.DisciplineId,
                FirstName = objModel.FirstName,
                Locality = objModel.Locality,
                Occupation = objModel.Occupation,
                Phone = objModel.Phone,
                Sex = objModel.Sex,
                StateId = objModel.StateId,
                Summary = objModel.Summary,
                SurName = objModel.SurName,
                WebLink = objModel.WebLink,
                ZipCode = objModel.ZipCode,
                Category = CategoryMap.Map(objModel.Category),
                Discipline = DisciplineMap.Map(objModel.Discipline),
                Date = objModel.BirthDate.ToShortDateString(),
                IsLeonniTeam = objModel.IsLeonniTeam,
                CreatedDate = objModel.CreatedDate,
                ModifiedDate = objModel.ModifiedDate,
                StatusId = objModel.StatusId,
              //  Province = ProvinceMap.Map(objModel.Province)
            };

        }
        public static List<UserProfileModel> Map(List<UserProfile> lstModel)
        {
            var ret = new List<UserProfileModel>();
            int i = 0, lstModel_count = lstModel.Count;

            for (i = 0; i < lstModel_count; i++)
            {
                ret.Add(UserProfileMap.Map(lstModel[i]));
            }

            return ret;
        }
    }
}
