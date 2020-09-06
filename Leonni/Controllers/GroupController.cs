using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leonni.Core.Controllers;
using Leonni.Models.ViewModels;
using Leonni.Interfaces;
using Leonni.Models.ModelMappers;
using System.IO;
using Leonni.Core;

namespace Leonni.Controllers
{
    public class GroupController : LeonniApplicationController
    {

        #region Private Member Variables

        private ICountryRepository _repoCountry;
        private IProvinceRepository _repoProvince;
        private IDisciplineRepository _repoDiscipline;
        private IUserProfileRepository _repoUserProfile;
        private IVideoLinkRepository _repoVideoLink;
        private IFrontEntityFileRepository _repoFrontEntityFile;
        private IEntityFileRepository _repoEntityFile;
        private IGroupRepository _repoGroup;
        private IProjectRepository _repoProject;
        private IPublicationRepository _repoPublication;
        private IFavouriteRepository _repoFavourite;

        #endregion Private Member Variables

        #region Constructor


        public GroupController(IEntityFileRepository entityFileRepository, IFrontEntityFileRepository frontEntityFileRepository, IVideoLinkRepository videoLinkRepository, IUserProfileRepository userProfileRepository, IProvinceRepository provinceRepository, ICountryRepository countryRepository, IDisciplineRepository disciplineRepositiory, IFileRepository repFile, IGroupRepository groupRepository, IProjectRepository projectRepository, IPublicationRepository publicationRepository, IFavouriteRepository favouriteRepository)
        {
            _repoCountry = countryRepository;
            _repoProvince = provinceRepository;
            _repoDiscipline = disciplineRepositiory;
            _repoUserProfile = userProfileRepository;
            _repoVideoLink = videoLinkRepository;
            _repoFrontEntityFile = frontEntityFileRepository;
            _repoEntityFile = entityFileRepository;
            _repoGroup = groupRepository;
            _repoProject = projectRepository;
            _repoPublication = publicationRepository;
            _repoFavourite = favouriteRepository;

        }

        #endregion

        //
        // GET: /Group/

        public ActionResult Index()
        {
            ViewBag.CurrentMainTab = "Front";
            ViewBag.CurrentSubTab = "Groups";

            PopulateViewBags();

            //long profileId = 0;
            //if (CurrentUser != null)
            //{
            //    var objProfile = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId);
            //    GroupModel objGroup = new GroupModel();
            //    if (objProfile != null)
            //    {
            //        profileId = objProfile.Id;
            //    }
            //}
            //ViewData["ProfileId"] = profileId;
            //ViewData["Url"] = Request.RawUrl;
            //ViewData["EntityId"] = 1;
            //ViewData["SectionId"] = 2;

            GroupModel objGroups = new GroupModel();
            string VideoLinkUrl = "";
            if (_repoVideoLink.GetSingle(x => x.SectionId == 1) != null)
            {
                VideoLinkUrl = _repoVideoLink.GetSingle(x => x.SectionId == 2).VideoLinkUrl;
            }
            var lstBannerImage = _repoFrontEntityFile.GetList(x => x.SectionId == 2).OrderByDescending(x => x.Id).ToList();
            //if (_repFrontEntityFile.GetList(x => x.SectionId == 1).Count() > 0)
            //{
            //    lstBannerImage = _repFrontEntityFile.GetList(x => x.SectionId == 1).OrderByDescending(x => x.Id).ToList();
            //}
            if (_repoEntityFile.GetSingle(x => x.SectionId == 25) != null)
            {
                objGroups.AdvertisingBanner = _repoEntityFile.GetSingle(x => x.SectionId == 25).FileId;
            }
            objGroups.VideoUrl = VideoLinkUrl;
            objGroups.BannerList = lstBannerImage;
            return View(objGroups);

            //return View(new GroupIndexModel());

        }

        private void PopulateViewBags()
        {
            //Countries and Provinces
            List<CountryModel> lstCountries = new List<CountryModel>();

            lstCountries.Add(new CountryModel
            {
                Id = 1111,
                Name = "Country"
            }
                );


            lstCountries.AddRange(CountryMap.Map(_repoCountry.GetList().ToList()));

            ViewBag.Countries = new SelectList(lstCountries, "Name", "Name");

            List<ProvinceModel> lstProvinces = new List<ProvinceModel>();

            lstProvinces.Add(new ProvinceModel
            {
                Id = 0,
                Name = "State"
            });

            ViewBag.Provinces = new SelectList(lstProvinces, "Id", "Name");

            List<YearModel> lstYears = new List<YearModel>();


            List<DisciplineModel> lstDisciplines = new List<DisciplineModel>();

            lstDisciplines.Add(new DisciplineModel
            {
                Id = 1111,
                DisciplineName = "Discipline",
                LanguageId = CurrentLanguage.Id
            });

            lstDisciplines.AddRange(DisciplineMap.Map(_repoDiscipline.GetList(x => x.LanguageId == CurrentLanguage.Id).ToList()));

            ViewBag.Disciplines = new SelectList(lstDisciplines, "Id", "DisciplineName");

            List<SortByModel> lstSort = new List<SortByModel>();
            lstSort.Add(new SortByModel
            {
                Id = 0,
                OrderBy = "Sort By"
            }

                );

            lstSort.Add(new SortByModel
            {
                Id = 1,
                OrderBy = "Ascending"
            });

            lstSort.Add(new SortByModel
            {
                Id = 2,
                OrderBy = "Descending"
            });

            ViewBag.SortBy = new SelectList(lstSort, "Id", "OrderBy");

        }

        public ActionResult GroupContents(int id)
        {

            //List<UserProfileModel> lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().ToList());
            GroupModel objGroupSearch = new GroupModel();
            objGroupSearch.Id = id;
            return PartialView("GroupSearch", objGroupSearch);

        }

        public ActionResult GroupSearch()
        {
            //List<UserProfileModel> lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().ToList());
            GroupModel objGroupSearch = new GroupModel();
            return PartialView("GroupSearch", objGroupSearch);

        }

        public ActionResult ViewGroup(long? id)
        {
            GroupModel objGroup = new GroupModel();
            objGroup = GroupMap.Map(_repoGroup.GetSingle(x => x.Id == id));
            ViewBag.CurrentMainTab = "Front";
            ViewBag.CurrentSubTab = "Group";
            ViewBag.CurrentSuperSubTab = "Name";
            ViewBag.DisciplineName = _repoDiscipline.GetSingle(x => x.Id == objGroup.DisciplineId).DisciplineName;
            ViewBag.ProvinceName = _repoProvince.GetSingle(x => x.Id == objGroup.ProvinceId).NAME_1;
            var objEntityFile = _repoEntityFile.GetSingle(x => x.EntityId == objGroup.Id && x.Section.SectionName == "groups");

            if (objEntityFile != null)
            {
                ViewBag.FileId = objEntityFile.FileId.GetValueOrDefault();
            }

            //for comment 
            long profileId = 0;
            if (CurrentUser != null)
            {
                var objProfile = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId);
                if (objProfile != null)
                {
                    profileId = objProfile.Id;
                }
            }
            ViewData["ProfileId"] = profileId;
            ViewData["Url"] = Request.RawUrl;
            ViewData["EntityId"] = id;
            ViewData["SectionId"] = 2;

            var objFavourite = _repoFavourite.GetSingle(x => x.SectionId == 2 && x.EntityId == id);
            if (objFavourite != null)
            {
                objGroup.isFavourite = true;
            }
            else
            {
                objGroup.isFavourite = false;
            }


            //--------------
            return View(objGroup);
        }

        public enum Status
        {
            Deleted = 0,
            Active = 1,
            Disabled = 2
        }

        public PartialViewResult groupList(string groupName, long? createdBy, int? pageNumber = null, int? itemsPerPage = null)
        {
            int listCount = 0;
            int offset = 0;
            offset = (((int)pageNumber - 1) * (int)itemsPerPage);
            List<GroupModel> lstGroup = new List<GroupModel>();
            List<GroupModel> lstNewGroupModel = new List<GroupModel>();


            if (!string.IsNullOrEmpty(groupName))
            {
                lstGroup = GroupMap.Map(_repoGroup.GetList(x => x.Title == groupName && x.Status == (int)Status.Active).OrderBy(x => x.CreatedDate).Skip(offset).Take((int)itemsPerPage).ToList());
                listCount = _repoGroup.GetList(x => x.Title == groupName && x.Status == (int)Status.Active).ToList().Count;
            }
            else
            {
                lstGroup = GroupMap.Map(_repoGroup.GetList(x => x.Status == (int)Status.Active).OrderBy(x => x.CreatedDate).Skip(offset).Take((int)itemsPerPage).ToList());
                listCount = _repoGroup.GetList(x => x.Status == (int)Status.Active).ToList().Count;
            }

            ViewBag.PagedListofUsers = lstGroup ?? new List<GroupModel>();
            ViewBag.TotalUserCount = listCount;

            foreach (var item in lstGroup.ToList())
            {
                var objEntityFile = _repoEntityFile.GetSingle(x => x.EntityId == item.Id && x.Section.SectionName == "groups");
                if (objEntityFile != null)
                {
                    item.FileId = objEntityFile.FileId;
                }
                lstNewGroupModel.Add(item);
            }

            return PartialView("_visitorsGroupList", lstNewGroupModel);
        }

        public PartialViewResult groupContentList(int id, int? pageNumber = null, int? itemsPerPage = null)
        {
            int listCount = 0;
            int offset = 0;
            offset = (((int)pageNumber - 1) * (int)itemsPerPage);

            GroupContentModel objGroupContent = new GroupContentModel();

            /* Contents 
             * Projects
             * Publications
             * Activities
             * Galleries
             * */

            //Projects


            objGroupContent.Publications = PublicationMap.Map(_repoPublication.GetList(x => x.GroupId == id).OrderBy(x => x.CreatedDate).ToList());

            ViewBag.PagedListofPublications = objGroupContent.Publications ?? new List<PublicationModel>();
            //ViewBag.TotalUserCount = listCount;          
            return PartialView("_GroupContents", objGroupContent);
        }

        public ActionResult AddRemovetoFavourites(int id, int sectionid)
        {

            var objFavourite = _repoFavourite.GetSingle(x => x.SectionId == sectionid && x.EntityId == id);
            if (objFavourite == null)
            {
                FavouriteModel objFavouriteModel = new FavouriteModel();

                objFavouriteModel.EntityId = id;
                objFavouriteModel.SectionId = sectionid;
                objFavouriteModel.UserProfileId = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId).Id;
                _repoFavourite.Add(FavouriteMap.Map(objFavouriteModel));
                _repoFavourite.Save();
                return Json(new { status = "success", message = "added" });
            }
            else
            {
                _repoFavourite.Delete(objFavourite);
                _repoFavourite.Save();
                return Json(new { status = "success", message = "removed" });
            }
        }


    }
}
