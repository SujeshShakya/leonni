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
using System.Web.Security;
using System.Windows.Media;
using System.Drawing;


namespace Leonni.Controllers
{
    public partial class ControlPanelController : LeonniApplicationController
    {
        //
        // GET: /ControlPanel/1


        #region Private Member Variables

        private ICountryRepository _repoCountry;
        private IProvinceRepository _repoProvince;
        private IDisciplineRepository _repoDiscipline;
        private IGroupRepository _repoGroup;
        private IFileRepository _repoFile;
        private IProjectRepository _repoProject;
        private ICategoryRepository _repoCat;
        private IUserProfileRepository _repoUserProfile;
        private IEntityFileRepository _repoEntityFile;
        private ISectionRepository _repoSection;
        private IVideoLinkRepository _repoVideoLink;
        private IFrontEntityFileRepository _repoFrontEntityFile;
        private IFrontContentRepository _repoFrontContent;
        private IMessageRepository _repoMessage;
        private IPublicationRepository _repoPublication;

        #endregion Private Member Variables

        #region Constructor

        public ControlPanelController(IProvinceRepository provinceRepository, ICountryRepository countryRepository, IDisciplineRepository disciplineRepositiory, IGroupRepository repGroup, IFileRepository repFile, ICategoryRepository repoCat, IUserProfileRepository repoUserProfile, IEntityFileRepository repoEntityFile, ISectionRepository repoSection, IProjectRepository repoProject, IVideoLinkRepository repoVideoLink, IFrontEntityFileRepository repoFrontEntityFile, IFrontContentRepository repoFrontContent, IMessageRepository repoMessage, IPublicationRepository repoPublication)
        {
            _repoCountry = countryRepository;
            _repoProvince = provinceRepository;
            _repoDiscipline = disciplineRepositiory;
            _repoGroup = repGroup;
            _repoFile = repFile;
            _repoCat = repoCat;
            _repoUserProfile = repoUserProfile;
            _repoEntityFile = repoEntityFile;
            _repoSection = repoSection;
            _repoProject = repoProject;
            _repoVideoLink = repoVideoLink;
            _repoFrontEntityFile = repoFrontEntityFile;
            _repoFrontContent = repoFrontContent;
            _repoMessage = repoMessage;
            _repoPublication = repoPublication;
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            UserProfileModel objUserProfile = new UserProfileModel();

            //_repoFile.GetList(x=>x.
            if (CurrentRole != "Administrator")
            {
                var objProfile = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId);
                if (objProfile != null)
                {
                    ViewBag.DisciplineId = new SelectList(_repoDiscipline.GetList(x => x.CategoryId == objProfile.CategoryId).ToList(), "Id", "DisciplineName", objProfile.DisciplineId);
                    ViewBag.ProvinceId = new SelectList(_repoProvince.GetList(x => x.Country == objProfile.CountryName).ToList(), "Id", "Name_1", objProfile.StateId);

                    objUserProfile = UserProfileMap.Map(objProfile);
                    objUserProfile.IsNew = false;

                    var objEntityFile = _repoEntityFile.GetSingle(x => x.EntityId == objProfile.Id && x.SectionId == 1);
                    if (objEntityFile != null)
                    {
                        objUserProfile.FileId = objEntityFile.FileId.GetValueOrDefault();
                    }
                }
                else
                {
                    objUserProfile.IsNew = true;
                }
            }
            else
            {
                var objProfile = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId);
                if (objProfile == null) ////This condition is to check if user has a profile or not
                {
                    objUserProfile.IsNew = true;
                }
                List<UserProfileModel> lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList(x => x.StatusId == 0).ToList());
                ViewBag.TotalUserList = lstUserProfile.Count;
            }


            ViewBag.CurrentMainTab = "Home";
            ViewBag.CurrentSubTab = "ControlPanel";
            ViewBag.CurrentSuperSubTab = "Profile";

            ProfileSearchModel objProfileSearch = new ProfileSearchModel();

            objUserProfile.ProfileSearch = objProfileSearch;

            return View(objUserProfile);
        }

        [HttpPost]
        public ActionResult ProfilePhotoUpload(string selectedFileName)
        {
            long newId = 0;
            bool success = true;
            Stream imageStream = Request.InputStream; // The actual file stream
            try
            {
                //Save info in database
                var file = new Models.File();

                file.ContentType = "image/jpeg";// Request.Files[0].ContentType;
                file.FileName = selectedFileName;

                var memoryStream = new MemoryStream();

                file.Content = ToByteArray(imageStream);//ReadFile("C:\\Users\\binesh\\Desktop\\salir.jpg"); //content;//ReadFile(content);

                _repoFile.Add(file);
                _repoFile.Save();

                //EntityFileModel efModel = new EntityFileModel();
                //var entityFile = EntityFileMap.Map(efModel);

                //entityFile.SectionId = _repoSection.GetSingle(x => x.SectionName == "Users").Id;
                //entityFile.EntityId = 0;
                //entityFile.FileId = file.Id;

                newId = file.Id;
                //_repoEntityFile.Add(entityFile);
                //_repoEntityFile.Save();
            }
            catch { success = false; }
            return Json(new { success = success, id = newId });
            //return Json(new { success = true,id = 100});
        }

        public ActionResult ProfileSearch()
        {
            //List<UserProfileModel> lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().ToList());
            ProfileSearchModel objProfileSearch = new ProfileSearchModel();
            return PartialView("ProfileSearch", objProfileSearch);
        }

        [HttpPost]
        public ActionResult Profile(UserProfileModel objProfile)
        {
            if (objProfile.CategoryId == 0)
            {
                TempData[LeonniConstants.ErrorMessage] = " Please Select Any Category";
                return View(objProfile);
            }
            if (objProfile.DisciplineId == 0)
            {
                TempData[LeonniConstants.ErrorMessage] = " Please Select Any Discipline";
                return View(objProfile);
            }
            else if (objProfile.Sex == "Sex")
            {
                TempData[LeonniConstants.ErrorMessage] = " Please Select Any Sex";
                return View(objProfile);
            }
            else if (objProfile.CountryName == "Country")
            {
                TempData[LeonniConstants.ErrorMessage] = " Please Select Any Country";
                return View(objProfile);
            }
            if (objProfile.StateId == 0)
            {
                TempData[LeonniConstants.ErrorMessage] = " Please Select Any State";
                return View(objProfile);
            }
            else
            {
                try
                {
                    ViewBag.DisciplineId = new SelectList(_repoDiscipline.GetList().ToList(), "Id", "DisciplineName", "");
                    if (ModelState.IsValid)
                    {
                        if (objProfile.IsNew == true)
                        {
                            objProfile.IsLeonniTeam = false;
                            objProfile.UserId = CurrentUser.UserId;
                            objProfile.CreatedDate = DateTime.Now;
                            objProfile.StatusId = (int)Status.Active;  ////Active
                            _repoUserProfile.Add(UserProfileMap.Map(objProfile));
                            _repoUserProfile.Save();
                            objProfile.Id = _repoUserProfile.GetList(x => true).Select(x => x.Id).Max();
                        }
                        else
                        {
                            if (objProfile.FileId != null)//if (objProfile.ProfilePic != null)
                            {
                                var entityFileModel = _repoEntityFile.GetSingle(x => x.SectionId == 1 && x.EntityId == objProfile.Id);
                                if (entityFileModel != null)
                                {
                                    var fileModel = _repoFile.GetSingle(x => x.Id == entityFileModel.FileId);

                                    if (fileModel != null)
                                    {
                                        _repoFile.Delete(fileModel);
                                        _repoFile.Save();
                                    }
                                    if (entityFileModel != null)
                                    {
                                        _repoEntityFile.Delete(entityFileModel);
                                        _repoEntityFile.Save();
                                    }
                                }
                            }
                            objProfile.UserId = CurrentUser.UserId;
                            objProfile.ModifiedDate = DateTime.Now;
                            _repoUserProfile.Update(UserProfileMap.Map(objProfile));
                            _repoUserProfile.Save();

                        }

                        if (objProfile.FileId != null)
                        //if (objProfile.ProfilePic != null && objProfile.ProfilePic.InputStream != null)
                        {

                            ////////Delete the existing entity and file
                            ////EntityFileModel objEntityFile = EntityFileMap.Map(_repoEntityFile.GetSingle(x => x.SectionId == 1 && x.EntityId == objProfile.Id));

                            ////Leonni.Models.File objFile = _repoFile.GetSingle(x => x.Id == objEntityFile.FileId);
                            ////_repoFile.Delete(objFile);
                            ////_repoFile.Save();
                            ////var obj1 = EntityFileMap.Map(objEntityFile);
                            ////_repoEntityFile.Delete(EntityFileMap.Map(objEntityFile));
                            ////_repoEntityFile.Save();

                            /////Delete the existing file
                            /////

                            //var file = new Models.File();

                            //file.ContentType = objProfile.ProfilePic.ContentType;
                            //file.FileName = objProfile.ProfilePic.FileName;

                            //var memoryStream = new MemoryStream();
                            //objProfile.ProfilePic.InputStream.CopyTo(memoryStream);

                            //Image obj = Image.FromStream(objProfile.ProfilePic.InputStream, true, true);

                            //file.Content = memoryStream.ToArray();
                            //file.Height = obj.Height;
                            //file.Width = obj.Width;

                            //if (file.Content == null)
                            //{
                            //    TempData[LeonniConstants.ErrorMessage] = "Please upload Photo";
                            //    return View(objProfile); // RedirectToAction("Create");
                            //}

                            //_repoFile.Add(file);
                            //_repoFile.Save();

                            //EntityFileModel objEntityFile = new EntityFileModel();
                            //objEntityFile.SectionId = _repoSection.GetSingle(x => x.SectionName == "Users").Id;
                            //objEntityFile.EntityId = objProfile.Id;
                            //objEntityFile.FileId = file.Id;
                            //_repoEntityFile.Add(EntityFileMap.Map(objEntityFile));
                            //_repoEntityFile.Save();

                            EntityFileModel efModel = new EntityFileModel();
                            var entityFile = EntityFileMap.Map(efModel);

                            entityFile.SectionId = _repoSection.GetSingle(x => x.SectionName == "Users").Id;
                            entityFile.EntityId = objProfile.Id;
                            entityFile.FileId = objProfile.FileId;
                            
                            _repoEntityFile.Add(entityFile);
                            _repoEntityFile.Save();
                        }
                    }
                    else
                    {
                        TempData[LeonniConstants.ErrorMessage] = " Update Unsuccessful";
                        return View(objProfile);
                    }
                    TempData[LeonniConstants.SuccessMessage] = "Profile Updated Successfully";
                    return RedirectToAction("Profile");
                }
                catch (Exception e)
                {
                    TempData[LeonniConstants.ErrorMessage] = e.ToString();
                    return View(objProfile);
                }
            }
        }


        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = System.Web.Security.Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    TempData[LeonniConstants.SuccessMessage] = "The password has been changed";
                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    TempData[LeonniConstants.ErrorMessage] = "The current password is incorrect or the new password is invalid.";
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public enum Status
        {
            Deleted = 0,
            Active = 1,
            Disabled = 2
        }

        public ActionResult Configuration()
        {
            ViewBag.CurrentMainTab = "Home";
            ViewBag.CurrentSubTab = "ControlPanel";
            ViewBag.CurrentSuperSubTab = "Configuration";
            return View(new DefaultBaseViewModel());
        }

        public ActionResult Links()
        {
            ViewBag.CurrentMainTab = "Home";
            ViewBag.CurrentSubTab = "ControlPanel";
            ViewBag.CurrentSuperSubTab = "Links";
            return View(new DefaultBaseViewModel());
        }

        public ActionResult Stores()
        {
            ViewBag.CurrentMainTab = "Home";
            ViewBag.CurrentSubTab = "ControlPanel";
            ViewBag.CurrentSuperSubTab = "Stores";
            return View(new DefaultBaseViewModel());
        }

        public ActionResult Guide()
        {
            ViewBag.CurrentMainTab = "Home";
            ViewBag.CurrentSubTab = "ControlPanel";
            ViewBag.CurrentSuperSubTab = "Guide";
            return View(new DefaultBaseViewModel());
        }

        public ActionResult Features()
        {
            ViewBag.CurrentMainTab = "Home";
            ViewBag.CurrentSubTab = "ControlPanel";
            ViewBag.CurrentSuperSubTab = "Features";
            return View(new DefaultBaseViewModel());
        }

        public ActionResult NewsLetters()
        {
            ViewBag.CurrentMainTab = "Home";
            ViewBag.CurrentSubTab = "ControlPanel";
            ViewBag.CurrentSuperSubTab = "NewsLetters";
            return View(new DefaultBaseViewModel());
        }

        public ActionResult ViewProfile(long? id)
        {
            ViewBag.CurrentMainTab = "Front";
            ViewBag.CurrentSubTab = "Profile";
            ViewBag.CurrentSuperSubTab = "Name";
            var objUser = _repoUserProfile.GetSingle(x => x.Id == id);
            UserProfileModel objUserProfile = UserProfileMap.Map(objUser);

            var objEntityFile = _repoEntityFile.GetSingle(x => x.EntityId == objUserProfile.Id && x.Section.SectionName == "Users");
            if (objEntityFile != null)
            {
                objUserProfile.FileId = objEntityFile.FileId;
            }

            objUserProfile.Email = Membership.GetUser(objUser.UserId).Email;

            objUserProfile.Age = DateTime.Now.Year - objUserProfile.BirthDate.Year;
            return View(objUserProfile);
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
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
                lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList(x => x.StatusId == (int)Status.Active).OrderBy(x => x.FirstName).Skip(offset).Take((int)itemsPerPage).ToList());
                listCount = _repoUserProfile.GetList().ToList().Count;
            }
            else if (sortOrder == 2)
            {
                lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList(x => x.StatusId == (int)Status.Active).OrderByDescending(x => x.FirstName).Skip(offset).Take((int)itemsPerPage).ToList());
                listCount = _repoUserProfile.GetList().ToList().Count;
            }
            else
            {
                lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList(x => x.StatusId == (int)Status.Active).OrderBy(x => x.FirstName).Skip(offset).Take((int)itemsPerPage).ToList());
                listCount = _repoUserProfile.GetList(x => x.StatusId == 0).ToList().Count;
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

            return PartialView("_UserProfileList", lstNewUserProfileModel);
        }

        public ActionResult AjaxGetProfileById(int Id)
        {
            UserProfileModel objProfile = UserProfileMap.Map(_repoUserProfile.GetSingle(x => x.Id == Id));
            List<DisciplineModel> lstDiscipline = new List<DisciplineModel>();
            lstDiscipline = DisciplineMap.Map(_repoDiscipline.GetList(x => x.CategoryId == objProfile.CategoryId).ToList());
            List<ProvinceModel> lstProvince = new List<ProvinceModel>();
            lstProvince = ProvinceMap.Map(_repoProvince.GetList(x => x.Country == objProfile.CountryName).ToList());
            return Json(new { status = "success", profile = objProfile, lstDiscipline = lstDiscipline.ToArray(), lstProvince = lstProvince.ToArray() });
        }

        public PartialViewResult GetAdminOptions()
        {
            return PartialView("_AdminOptions");
        }

        [HttpPost]
        public ActionResult AddRemovePublic(int id, int sectionId)
        {

            var objFrontContent = _repoFrontContent.GetSingle(x => x.ContentId == id && x.SectionId == sectionId);

            if (objFrontContent == null)
            {
                FrontContentModel FrontContent = new FrontContentModel();
                FrontContent.SectionId = sectionId;
                FrontContent.ContentId = id;
                FrontContent.CreateDate = DateTime.Now;
                _repoFrontContent.Add(FrontContentMap.Map(FrontContent));
                _repoFrontContent.Save();
                return Json(new { status = "success", message = "added" });
            }
            else
            {
                _repoFrontContent.Delete(objFrontContent);
                _repoFrontContent.Save();
                return Json(new { status = "success", message = "deleted" });
            }
        }

        public ActionResult DeleteContent(int id, int sectionId)
        {
            var objUserProfile = _repoUserProfile.GetSingle(x => x.Id == id);
            objUserProfile.StatusId = 1; ///Deleted
            _repoUserProfile.Update(objUserProfile);
            _repoUserProfile.Save();

            return Json(new { status = "success" });
        }

        public ActionResult AddRemoveFromTeam(int id)
        {
            var userProfile = _repoUserProfile.GetSingle(x => x.Id == id);
            string message = string.Empty;
            if (userProfile.IsLeonniTeam == false)
            {
                userProfile.IsLeonniTeam = true;
                message = "added";
            }
            else
            {
                userProfile.IsLeonniTeam = false;
                message = "deleted";
            }
            userProfile.ModifiedDate = DateTime.Now;
            _repoUserProfile.Update(userProfile);
            _repoUserProfile.Save();

            return Json(new { status = "success", message = message });
        }

        public ActionResult DisableContent(int id, int sectionId)
        {
            if (sectionId == 1)
            {
                var objUserProfile = _repoUserProfile.GetSingle(x => x.Id == id);
                objUserProfile.StatusId = (int)Status.Disabled; ///Disable
                _repoUserProfile.Update(objUserProfile);
                _repoUserProfile.Save();
            }
            else if (sectionId == 2)
            {
                var objGroup = _repoGroup.GetSingle(x => x.Id == id);
                objGroup.Status = (int)Status.Disabled;
                _repoGroup.Update(objGroup);
                _repoGroup.Save();
            }
            else if (sectionId == 3)
            {
                var objPublication = _repoPublication.GetSingle(x => x.Id == id);
                objPublication.Status = (int)Status.Disabled;
                _repoPublication.Update(objPublication);
                _repoPublication.Save();
            }


            return Json(new { status = "success" });
        }

        public ActionResult ApproveUsers()
        {
            ApproveUserModel objApproveUser = new ApproveUserModel();
            objApproveUser.MembershipUsers = new List<MembershipUserModel>();

            MembershipUserCollection obj = Membership.GetAllUsers();

            foreach (MembershipUser item in obj)
            {
                MembershipUserModel objMembershipUser = new MembershipUserModel();
                objMembershipUser.Email = item.Email;
                objMembershipUser.isApproved = item.IsApproved;
                objMembershipUser.id = (Guid)item.ProviderUserKey;
                objApproveUser.MembershipUsers.Add(objMembershipUser);
            }
            return View(objApproveUser);
        }


        public ActionResult SendMessage(long id, FormCollection fc)
        {
            string strMessage = fc["Message"];

            try
            {
                Guid userid = _repoUserProfile.GetSingle(x => x.Id == id).UserId;
                EmailSender.SendMail(Membership.GetUser(userid).Email, "Leonni Private Message", strMessage);

                MessageModel objMessage = new MessageModel();
                objMessage.SentDate = DateTime.Now;
                objMessage.SentBy = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId).Id;
                objMessage.MessageContent = strMessage;
                objMessage.SentTo = id;
                _repoMessage.Add(MessageMap.Map(objMessage));
                _repoMessage.Save();
                return Json(new { status = "success", Message = "Message sent" });
            }
            catch (Exception e)
            {
                return Json(new { status = "error", Message = "Unable to send Message" });
            }
        }

        public ActionResult UserContentSearch()
        {
            //List<UserProfileModel> lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().ToList());
            //ProfileSearchModel objProfileSearch = new ProfileSearchModel();
            return PartialView("UserContentSearch");
        }

        public PartialViewResult SearchFilters()
        {
            ProfileSearchModel objProfileSearch = new ProfileSearchModel();
            return PartialView("_SearchFilterPartial", objProfileSearch);
        }

        public ActionResult RemoveUser(int id)
        {

            var objUser = _repoUserProfile.GetSingle(x => x.Id == id);
            objUser.StatusId = (int)Status.Deleted;
            _repoUserProfile.Update(objUser);
            _repoUserProfile.Save();

            return Json(new { status = "success" });
        }

        public ActionResult ApproveDisApprove(Guid id)
        {
            //var userid = _repoUserProfile.GetSingle(x => x.Id.Equals(id)).UserId;
            string message = string.Empty;
            MembershipUser objMembership = Membership.GetUser(id);
            bool isApproved = objMembership.IsApproved;
            if (isApproved)
            {
                objMembership.IsApproved = false;
                message = "DisApproved";
            }
            else
            {
                objMembership.IsApproved = true;
                message = "Approved";
            }
            Membership.UpdateUser(objMembership);
            return Json(new { status = "success" , message = message});
        }

        public PartialViewResult userContentList(string name, string countryName, int? provinceId, int? disciplineId, string sex, int? categoryId, int? sortOrder, int? pageNumber = null, int? itemsPerPage = null)
        {

            int publicationListCount = 0;
            int groupListCount = 0;
            int offset = 0;
            offset = (((int)pageNumber - 1) * (int)itemsPerPage);

            List<GroupModel> lstGroups = GroupMap.Map(_repoGroup.GetList(x => x.CreatedBy == 25).ToList());

            groupListCount = lstGroups.Count();

            List<PublicationModel> lstPublication = PublicationMap.Map(_repoPublication.GetList(x => x.CreatedBy == 25).ToList());

            publicationListCount = lstPublication.Count();

            foreach (var item in lstGroups.ToList())
            {
                var objEntityFile = _repoEntityFile.GetSingle(x => x.EntityId == item.Id && x.Section.SectionName == "Groups");
                if (objEntityFile != null)
                {
                    item.FileId = objEntityFile.FileId;
                }
                lstGroups.Add(item);
            }

            //foreach (var item in lstPublication.ToList())
            //{
            //    var objEntityFile = _repoEntityFile.GetSingle(x => x.EntityId == item.Id && x.Section.SectionName == "Groups");
            //    if (objEntityFile != null)
            //    {
            //        item.FileId = objEntityFile.FileId;
            //    }
            //    lstGroups.Add(item);
            //}
            ViewBag.TotalCount =  groupListCount + publicationListCount;

            ViewBag.PagedListofPublication = lstPublication ?? new List<PublicationModel>();
            ViewBag.PagedListofGroups = lstGroups ?? new List<GroupModel>();

            return PartialView("_UserContentList");
        }

    }
}
