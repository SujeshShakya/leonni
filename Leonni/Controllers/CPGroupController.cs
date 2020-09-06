using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Leonni.Core.Controllers;
using Leonni.Models.ViewModels;
using Leonni.Models.ModelMappers;
using System.IO;
using Leonni.Core;
using MoreLinq;
using Leonni.Models;
using Leonni.Models.Binder;

namespace Leonni.Controllers
{
    public partial class ControlPanelController : LeonniApplicationController
    {
        //
        // GET: /CPGroup/

        public ActionResult Groups()
        {
            ViewBag.DisciplineId = new SelectList(_repoDiscipline.GetList().ToList(), "Id", "DisciplineName", "Discipline");

            ViewBag.CurrentMainTab = "Home";
            ViewBag.CurrentSubTab = "ControlPanel";
            ViewBag.CurrentSuperSubTab = "Groups";

            GroupModel objGroup = new GroupModel();
            objGroup.IsNew = true;
            return View("Groups", objGroup);
        }

        [HttpPost]
        public ActionResult Groups(GroupModel objGroup)
        {
            ViewBag.DisciplineId = new SelectList(_repoDiscipline.GetList().ToList(), "Id", "DisciplineName", "");
            if (objGroup.DisciplineId == 0)
            {
                TempData[LeonniConstants.ErrorMessage] = " Please Select Any Discipline";
                return View(objGroup);
            }
            else if (objGroup.CountryName == "Country")
            {
                TempData[LeonniConstants.ErrorMessage] = " Please Select Any Country";
                return View(objGroup);
            }
            if (objGroup.ProvinceId == 0)
            {
                TempData[LeonniConstants.ErrorMessage] = " Please Select Any State";
                return View(objGroup);
            }
            else
            {
                try
                {
                    Group newGroup = null;

                    if (ModelState.IsValid)
                    {
                        if (objGroup != null)
                        {
                            if (objGroup.IsNew == true)
                            {
                                objGroup.CreatedDate = DateTime.Now;
                                if (_repoUserProfile != null)
                                {
                                    objGroup.CreatedBy = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId).Id;
                                }
                                objGroup.IsDeleted = false;
                                objGroup.Status = (int)Status.Active;
                                objGroup.UserProfile = UserProfileMap.Map(_repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId));
                                newGroup = GroupMap.Map(objGroup);
                                _repoGroup.Add(newGroup);
                                objGroup.IsNew = false;
                            }
                            else
                            {
                                if (objGroup.GroupPic != null)
                                {
                                    var entityFileModel = _repoEntityFile.GetSingle(x => x.SectionId == 2 && x.EntityId == objGroup.Id);
                                    if (entityFileModel != null)
                                    {
                                        var fileModel = _repoFile.GetSingle(x => x.Id == entityFileModel.FileId);
                                        if (fileModel != null)
                                        {
                                            _repoFile.Delete(fileModel);
                                            _repoFile.Save();
                                        }

                                        _repoEntityFile.Delete(entityFileModel);
                                        _repoEntityFile.Save();
                                    }
                                }
                                objGroup.IsDeleted = false;
                                objGroup.Status = (int)Status.Active;
                                _repoGroup.Update(GroupMap.Map(objGroup));
                            }

                            _repoGroup.Save();


                            if (objGroup.GroupPic != null && objGroup.GroupPic.InputStream != null)
                            {
                                var file = new Models.File();

                                file.ContentType = objGroup.GroupPic.ContentType;
                                file.FileName = objGroup.GroupPic.FileName;

                                var memoryStream = new MemoryStream();
                                objGroup.GroupPic.InputStream.CopyTo(memoryStream);

                                file.Content = memoryStream.ToArray();
                                //file.UserId = group.UserId;

                                if (file.Content == null)
                                {
                                    TempData[LeonniConstants.ErrorMessage] = "Please upload Photo";
                                    return View(objGroup); // RedirectToAction("Create");
                                }

                                _repoFile.Add(file);
                                _repoFile.Save();

                                EntityFileModel efModel = new EntityFileModel();
                                var entityFile = EntityFileMap.Map(efModel);
                                entityFile.SectionId = _repoSection.GetSingle(x => x.SectionName == "Groups").Id;

                                if (objGroup.Id != 0)
                                {
                                    entityFile.EntityId = objGroup.Id;
                                }
                                else
                                {
                                    entityFile.EntityId = newGroup.Id;
                                }
                                entityFile.FileId = file.Id;
                                _repoEntityFile.Add(entityFile);
                                _repoEntityFile.Save();
                            }
                        }
                        else
                        {
                            TempData[LeonniConstants.ErrorMessage] = " Update Unsuccessful";
                            return View(objGroup);
                        }
                        TempData[LeonniConstants.SuccessMessage] = "Group Updated Successfully";
                        return RedirectToAction("Groups");
                    }
                    return View(objGroup);
                    //TempData[LeonniConstants.SuccessMessage] = " Update Successful";
                    //return RedirectToAction("Groups", new { model = objGroup });
                }
                catch (Exception e)
                {
                    TempData[LeonniConstants.ErrorMessage] = e.ToString();
                    return View(objGroup);
                }
            }
        }

        public ActionResult ViewGroup(long? id)
        {
            var objProfile = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId);
            GroupModel objGroup = new GroupModel();
            if (objProfile != null)
            {
                long profileId = objProfile.Id;
                //objGroup = GroupMap.Map(_repoGroup.GetSingle(x => x.CreatedBy == profileId && x.Id == id));
                objGroup = GroupMap.Map(_repoGroup.GetSingle(x => x.Id == id));
                objGroup.UserProfile = new UserProfileModel();
                objGroup.UserProfile = UserProfileMap.Map(_repoUserProfile.GetSingle(x => x.Id == objGroup.CreatedBy));

            }

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

            return View(objGroup);
        }

        [HttpPost]
        public ActionResult DeleteGroup(long id)
        {
            try
            {
                var objGroup = _repoGroup.GetSingle(x => x.Id == id);
                if (objGroup != null)
                {
                    _repoGroup.Delete(objGroup);
                    _repoGroup.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            JsonResult res;
            string redirectUrl = "";
            res = new JsonResult
            {
                Data = new { status = "redirectToURL", message = redirectUrl }
            };

            return res;
        }

        [HttpPost]
        public ActionResult AjaxGetProvinces(string country)
        {
            ProvinceModel[] arrProvinces = ProvinceMap.Map(_repoProvince.GetList(x => x.Country == country).DistinctBy(x => x.NAME_1).ToList()).ToArray();

            if (arrProvinces != null)
            {
                return Json(new { status = "success", provinceList = arrProvinces });
            }
            else
            {
                return Json(new { status = "error" });
            }

        }

        public ActionResult GroupSearch()
        {

            //List<UserProfileModel> lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().ToList());
            GroupModel objGroupSearch = new GroupModel();
            return PartialView("GroupSearch", objGroupSearch);

        }
        //public PartialViewResult groupList(string groupName=default(string), long? createdBy, int? pageNumber = null, int? itemsPerPage = null)
        public PartialViewResult groupList(string groupName, long? createdBy, int? pageNumber = null, int? itemsPerPage = null)
        {
            int listCount = 0;
            int offset = 0;
            offset = (((int)pageNumber - 1) * (int)itemsPerPage);
            List<GroupModel> lstGroup = new List<GroupModel>();
            List<GroupModel> lstNewGroupModel = new List<GroupModel>();
            //if (groupName == default(string))
            //{

            //}
            if (CurrentRole != "Administrator")
            {
                createdBy = 9;
                //var obj = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId);

                //if(obj!=null)
                //{
                //    createdBy = 9;
                //}

                if (!string.IsNullOrEmpty(groupName))
                {
                    lstGroup = GroupMap.Map(_repoGroup.GetList(x => x.Title == groupName && x.CreatedBy == createdBy && x.Status == (int)Status.Active).OrderBy(x => x.Title).Skip(offset).Take((int)itemsPerPage).ToList());
                    listCount = _repoGroup.GetList(x => x.Title == groupName && x.CreatedBy == createdBy && x.Status == (int)Status.Active).ToList().Count;
                }
                else
                {
                    lstGroup = GroupMap.Map(_repoGroup.GetList(x => x.CreatedBy == createdBy && x.Status == (int)Status.Active).OrderBy(x => x.Title).Skip(offset).Take((int)itemsPerPage).ToList());
                    listCount = _repoGroup.GetList(x => x.CreatedBy == createdBy && x.Status == (int)Status.Active).ToList().Count;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(groupName))
                {
                    lstGroup = GroupMap.Map(_repoGroup.GetList(x => x.Title == groupName && x.Status == (int)Status.Active).OrderBy(x => x.Title).Skip(offset).Take((int)itemsPerPage).ToList());
                    listCount = _repoGroup.GetList(x => x.Title == groupName && x.Status == (int)Status.Active).ToList().Count;
                }
                else
                {
                    lstGroup = GroupMap.Map(_repoGroup.GetList(x => x.Status == (int?)Status.Active).OrderBy(x => x.Title).Skip(offset).Take((int)itemsPerPage).ToList());
                    listCount = _repoGroup.GetList(x => x.Status == (int)Status.Active).ToList().Count;
                }
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

            return PartialView("_GroupList", lstNewGroupModel);
        }

        public ActionResult AjaxGetGroupById(int Id)
        {
            GroupModel objGroup = GroupMap.Map(_repoGroup.GetSingle(x => x.Id == Id));
            List<ProvinceModel> lstProvince = new List<ProvinceModel>();
            lstProvince = ProvinceMap.Map(_repoProvince.GetList(x => x.Country == objGroup.CountryName).ToList());
            return Json(new { status = "success", group = objGroup, lstProvince = lstProvince.ToArray() });
        }

        public ActionResult RemoveGroup(int id)
        {

            var objGroup = _repoGroup.GetSingle(x => x.Id == id);
            //   objGroup.IsDeleted = true;
            objGroup.Status = (int)Status.Deleted;
            _repoGroup.Update(objGroup);
            _repoGroup.Save();

            return Json(new { status = "success" });
        }



    }
}