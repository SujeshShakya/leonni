using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leonni.Core.Controllers;
using Leonni.Models.ViewModels;
using Leonni.Interfaces;
using Leonni.Models;
using Leonni.Models.ModelMappers;
using System.IO;
using System.Web.Security;

namespace Leonni.Controllers
{

    public class UserController : LeonniApplicationController
    {
        ICategoryRepository _repoCat;
        IDisciplineRepository _repoDiscipline;
        IUserProfileRepository _repoUserProfile;
        IFileRepository _repoFile;
        IEntityFileRepository _repoEntityFile;
        IVideoLinkRepository _repVideoLink;
        IFrontEntityFileRepository _repFrontEntityFile;

        public UserController(IVideoLinkRepository repoVideoLink, IFrontEntityFileRepository repoFrontEntityFile, ICategoryRepository repoCat, IDisciplineRepository repoDiscipline, IUserProfileRepository repoUserProfile, IFileRepository repoFile, IEntityFileRepository repoEntityFile)
        {
            _repoCat = repoCat;
            _repoDiscipline = repoDiscipline;
            _repoUserProfile = repoUserProfile;
            _repoFile = repoFile;
            _repoEntityFile = repoEntityFile;
            _repFrontEntityFile = repoFrontEntityFile;
            _repVideoLink = repoVideoLink;
        }

        public ActionResult Index()
        {
            ViewBag.CurrentMainTab = "User";
            UserIndexModel objUsers = new UserIndexModel();
            string VideoLinkUrl = "";
            if (_repVideoLink.GetSingle(x => x.SectionId == 1) != null)
            {                
                VideoLinkUrl = _repVideoLink.GetSingle(x => x.SectionId == 1).VideoLinkUrl;
            }
            var lstBannerImage = _repFrontEntityFile.GetList(x => x.SectionId == 1).OrderByDescending(x => x.Id).ToList();
            //if (_repFrontEntityFile.GetList(x => x.SectionId == 1).Count() > 0)
            //{
            //    lstBannerImage = _repFrontEntityFile.GetList(x => x.SectionId == 1).OrderByDescending(x => x.Id).ToList();
            //}
            if (_repoEntityFile.GetSingle(x => x.SectionId == 25) != null)
            {
                objUsers.AdvertisingBanner = _repoEntityFile.GetSingle(x => x.SectionId == 25).FileId;
            }
            objUsers.VideoUrl = VideoLinkUrl;
            objUsers.BannerList = lstBannerImage;
            return View(objUsers);
        }

        public ActionResult UserSearch()
        {
            //List<UserProfileModel> lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().ToList());
            ProfileSearchModel objProfileSearch = new ProfileSearchModel();
            return PartialView("ProfileSearch", objProfileSearch);
        }

        public PartialViewResult UserList(string name, string countryName, int? provinceId, int? disciplineId, string sex, int? categoryId, int? sortOrder, int? pageNumber = null, int? itemsPerPage = null)
        {
            int listCount = 0;
            int offset = 0;
            offset = (((int)pageNumber - 1) * (int)itemsPerPage);
            List<UserProfileModel> lstUserProfile = new List<UserProfileModel>();
            List<UserProfileModel> lstNewUserProfileModel = new List<UserProfileModel>();

            if (sortOrder == 1)
            {
                lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().OrderBy(x => x.FirstName).Skip(offset).Take((int)itemsPerPage).ToList());
                listCount = _repoUserProfile.GetList().ToList().Count;
            }
            else if (sortOrder == 2)
            {
                lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().OrderByDescending(x => x.FirstName).Skip(offset).Take((int)itemsPerPage).ToList());
                listCount = _repoUserProfile.GetList().ToList().Count;
            }
            else
            {
                lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().OrderBy(x => x.FirstName).Skip(offset).Take((int)itemsPerPage).ToList());
                listCount = _repoUserProfile.GetList().ToList().Count;
            }

            if (categoryId != null)
            {
                if (categoryId != 0)
                {
                    lstUserProfile = lstUserProfile.Where(x => x.CategoryId == categoryId).ToList();
                    listCount = _repoUserProfile.GetList(x => x.CategoryId == categoryId).ToList().Count;
                }
            }

            if (!string.IsNullOrEmpty(sex))
            {
                if (sex != "Sex")
                {
                    lstUserProfile = lstUserProfile.Where(x => x.Sex == sex).ToList();
                    listCount = _repoUserProfile.GetList(x => x.Sex == sex).ToList().Count;
                }
            }

            if (disciplineId != null)
            {
                if (disciplineId != 0)
                {
                    lstUserProfile = lstUserProfile.Where(x => x.DisciplineId == disciplineId).ToList();
                    listCount = _repoUserProfile.GetList(x => x.DisciplineId == disciplineId).ToList().Count;
                }
            }


            if (!string.IsNullOrEmpty(countryName))
            {
                if (countryName != "Country")
                {
                    lstUserProfile = lstUserProfile.Where(x => x.CountryName == countryName).ToList();
                    listCount = _repoUserProfile.GetList(x => x.CountryName == countryName).ToList().Count;
                }
            }


            if (provinceId != null)
            {
                if (provinceId != 0)
                {
                    lstUserProfile = lstUserProfile.Where(x => x.StateId == provinceId).ToList();
                    listCount = _repoUserProfile.GetList(x => x.StateId == provinceId).ToList().Count;
                }
            }

            if (!string.IsNullOrEmpty(name))
            {
                lstUserProfile = lstUserProfile.Where(x => x.FirstName == name).ToList();
                listCount = _repoUserProfile.GetList(x => x.FirstName == name).ToList().Count;
            }

            foreach (var item in lstUserProfile.ToList())
            {
                var objEntityFile = _repoEntityFile.GetSingle(x => x.EntityId == item.Id && x.Section.SectionName == "Users");
                if (objEntityFile != null)
                {
                    item.FileId = objEntityFile.FileId;
                }
                lstNewUserProfileModel.Add(item);
            }

            ViewBag.PagedListofUsers = lstNewUserProfileModel ?? new List<UserProfileModel>();
            ViewBag.TotalUserCount = listCount;

            return PartialView("_UsersList", lstNewUserProfileModel);
        }

        public PartialViewResult SearchFilters()
        {
            ProfileSearchModel objProfileSearch = new ProfileSearchModel();
            return PartialView("_SearchFilterPartial",objProfileSearch);
        }
    }
}
