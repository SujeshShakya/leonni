using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Leonni.Models.ViewModels;
using Leonni.Core.Controllers;
using Leonni.Interfaces;
using Leonni.Models.ModelMappers;
using MoreLinq;



namespace Leonni.Controllers
{
    //[Authorize(Roles = "Moderate")]
    public class FrontController : LeonniApplicationController
    {

        #region Private Member Variables

        private ICategoryRepository _repCategory;
        private IYearRepository _repYear;
        private IMonthRepository _repMonth;
        private ICountryRepository _repCountry;
        private IProvinceRepository _repProvince;
        private IDisciplineRepository _repDiscipline;
        private IEntityFileRepository _repEntityFile;
        private IFrontEntityFileRepository _repFrontEntityFile;
        private IFileRepository _repFile;
        private IVideoLinkRepository _repVideoLink;
        private IFrontContentRepository _repFrontContent;
        private IUserProfileRepository _repUserProfile;
        private IGroupRepository _repGroup;

        #endregion Private Member Variables

        #region Constructor


        public FrontController(ICategoryRepository categoryRepository, IYearRepository yearRepository, IMonthRepository monthRepository, IProvinceRepository provinceRepository, ICountryRepository countryRepository, IDisciplineRepository disciplineRepositiory, IFrontEntityFileRepository frontEntityFileRepositiory, IEntityFileRepository entityFileRepositiory, IFileRepository fileRepositiory, IVideoLinkRepository videoLinkRepository, IFrontContentRepository frontContentRepository, IUserProfileRepository userProfileRepository, IGroupRepository groupRepository)
        {
            _repCategory = categoryRepository;
            _repMonth = monthRepository;
            _repYear = yearRepository;
            _repCountry = countryRepository;
            _repProvince = provinceRepository;
            _repDiscipline = disciplineRepositiory;
            _repEntityFile = entityFileRepositiory;
            _repFrontEntityFile = frontEntityFileRepositiory;
            _repFile = fileRepositiory;
            _repVideoLink = videoLinkRepository;
            _repFrontContent = frontContentRepository;
            _repUserProfile = userProfileRepository;
            _repGroup = groupRepository;
            _repEntityFile = entityFileRepositiory;
        }
        #endregion

        #region Public Action Methods

        public ActionResult Index()
        {
            ViewBag.CurrentMainTab = "Front";
            HomeIndexModel homeIndexModel = new HomeIndexModel();

            string VideoLinkUrl = "";
            if (_repVideoLink.GetSingle(x => x.SectionId == 0) != null)
            {
                VideoLinkUrl = _repVideoLink.GetSingle(x => x.SectionId == 0).VideoLinkUrl;
            }

            var lstBannerImage = _repFrontEntityFile.GetList(x => x.SectionId == 0).OrderByDescending(x => x.Id).ToList();
            if (_repEntityFile.GetSingle(x => x.Section.SectionName == "Advertisement") != null)
            {
                homeIndexModel.AdvertisingBanner = _repEntityFile.GetList(x => x.Section.SectionName == "Advertisement").Select(x=>x.FileId).Max();
            }
            homeIndexModel.VideoUrl = VideoLinkUrl;
            homeIndexModel.BannerList = lstBannerImage;
            return View(homeIndexModel);
        }

        [AllowAnonymous]
        public ActionResult Message()
        {
            ViewBag.Message = TempData["messagetype"];
            return View();
        }

        [HttpPost]
        public ActionResult AjaxGetProvinces(string country)
        {
            ProvinceModel[] arrProvinces = ProvinceMap.Map(_repProvince.GetList(x => x.Country == country).DistinctBy(x => x.NAME_1).ToList()).ToArray();

            if (arrProvinces != null)
            {
                return Json(new { status = "success", provinceList = arrProvinces });
            }
            else
            {
                return Json(new { status = "error" });
            }

        }


        [HttpPost]
        public ActionResult AjaxGetDisciplines(int id)
        {
            DisciplineModel[] arrDisciplines = DisciplineMap.Map(_repDiscipline.GetList(x => x.CategoryId == id && x.LanguageId == CurrentLanguage.Id).ToList()).ToArray();

            if (arrDisciplines != null)
            {
                return Json(new { status = "success", disciplineList = arrDisciplines });
            }
            else
            {
                return Json(new { status = "error" });
            }

        }

        public ActionResult FrontSearch()
        {
            //List<UserProfileModel> lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().ToList());
            // ProfileSearchModel objProfileSearch = new ProfileSearchModel();
            return PartialView("FrontSearch");
        }


        public PartialViewResult FrontList(string name, string countryName, int? provinceId, int? disciplineId, string sex, int? categoryId, int? sortOrder, int? pageNumber = null, int? itemsPerPage = null)
        {
            int profileListCount = 0;
            int groupListCount = 0;
            int offset = 0;
            offset = (((int)pageNumber - 1) * (int)itemsPerPage);
            List<FrontContentModel> lstFrontContent = new List<FrontContentModel>();
            FrontContentModel objFrontContent = new FrontContentModel();
            objFrontContent.UserProfiles = new List<UserProfileModel>();
            objFrontContent.Groups = new List<GroupModel>();
            lstFrontContent = FrontContentMap.Map(_repFrontContent.GetList(x => true).ToList());
            List<long> lstUserProfileId = new List<long>();
            List<long> lstGroupId = new List<long>();
            if (lstFrontContent.Count != 0)
            {
                ////For listing Front Profiles Contents
                lstUserProfileId = lstFrontContent.Where(x => x.SectionId == 1).Select(x => x.ContentId).ToList();

                foreach (long item in lstUserProfileId)
                {
                    var objuser = _repUserProfile.GetSingle(x => x.Id == item);
                    if (objuser != null)
                    {
                        UserProfileModel objUserProfile = UserProfileMap.Map(objuser);
                        objFrontContent.UserProfiles.Add(objUserProfile);
                        profileListCount ++;
                        var objEntityFile = _repEntityFile.GetSingle(x => x.EntityId == item && x.Section.SectionName == "Users");
                        if (objEntityFile != null)
                        {
                            objUserProfile.FileId = objEntityFile.FileId;
                        }
                    }
                }

                lstGroupId = lstFrontContent.Where(x => x.SectionId == 2).Select(x => x.ContentId).ToList();
                groupListCount = lstGroupId.Count;
                foreach (long item in lstGroupId)
                {
                    GroupModel objGroups = GroupMap.Map(_repGroup.GetSingle(x => x.Id == item));
                    if (objGroups != null)
                    {
                        objFrontContent.Groups.Add(objGroups);

                        var objEntityFile = _repEntityFile.GetSingle(x => x.EntityId == item && x.Section.SectionName == "Groups");
                        if (objEntityFile != null)
                        {
                            objGroups.FileId = objEntityFile.FileId;

                        }
                    }
                }

            }
            ViewBag.PagedListofUserProfiles = objFrontContent.UserProfiles ?? new List<UserProfileModel>();
            ViewBag.PagedListofGroups = objFrontContent.Groups ?? new List<GroupModel>();

            ViewBag.TotalCount = profileListCount + groupListCount;

            return PartialView("_FrontContentList");
        }

        #endregion


        #region Private Methods

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


            lstCountries.AddRange(CountryMap.Map(_repCountry.GetList().ToList()));

            ViewBag.Countries = new SelectList(lstCountries, "Name", "Name");

            List<ProvinceModel> lstProvinces = new List<ProvinceModel>();

            lstProvinces.Add(new ProvinceModel
            {
                Id = 0,
                Name = "State"
            });

            ViewBag.Provinces = new SelectList(lstProvinces, "Id", "Name");


            ///Categories
            ///
            List<CategoryModel> lstCategories = new List<CategoryModel>();

            lstCategories.Add(new CategoryModel
            {
                Id = 1111,
                CategoryName = "Categories",
                LanguageId = CurrentLanguage.Id
            });

            List<YearModel> lstYears = new List<YearModel>();

            lstCategories.AddRange(CategoryMap.Map(_repCategory.GetList(x => x.LanguageId == CurrentLanguage.Id).ToList()));

            ViewBag.Categories = new SelectList(lstCategories, "Id", "CategoryName");

            ///Years

            lstYears.Add(new YearModel
            {
                Id = 0,
                Year = "Years",
                LanguageId = CurrentLanguage.Id
            });

            lstYears.AddRange(YearMap.Map(_repYear.GetList(x => x.LanguageId == CurrentLanguage.Id).ToList()));

            ViewBag.Years = new SelectList(lstYears, "Id", "Year");

            List<MonthModel> lstMonths = new List<MonthModel>();

            lstMonths.Add(new MonthModel
            {
                Id = 1111,
                Month = "Months",
                LanguageId = CurrentLanguage.Id
            });

            lstMonths.AddRange(MonthMap.Map(_repMonth.GetList(x => x.LanguageId == CurrentLanguage.Id).ToList()));

            ViewBag.Months = new SelectList(lstMonths, "Id", "Month");

            List<DisciplineModel> lstDisciplines = new List<DisciplineModel>();

            lstDisciplines.Add(new DisciplineModel
            {
                Id = 1111,
                DisciplineName = "Discipline",
                LanguageId = CurrentLanguage.Id
            });

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

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View();
            }
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #region Status Codes

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion

        #endregion


    }
}
