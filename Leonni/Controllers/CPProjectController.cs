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
    public partial class ControlPanelController
    {
        //
        // GET: /CPProject/

        public ActionResult Project()
        {
            ViewBag.CurrentMainTab = "Project";
            ViewBag.CurrentSubTab = "ControlPanel";
            ViewBag.CurrentSuperSubTab = "Profile";

            ProjectModel objProjectModel = new ProjectModel();

            //if (CurrentRole != "Administrator")
            //{
            //    objProjectModel.IsNew = true;
            //    //var objProject = _repoProject.GetSingle(x => x.CreatedBy == CurrentUser.UserId);
            //    //if (objProject != null)
            //    //{
            //    //    ViewBag.DisciplineId = new SelectList(_repoDiscipline.GetList(x => x.CategoryId == objProject.CategoryId).ToList(), "Id", "DisciplineName", objProject.DisciplineId);
            //    //    ViewBag.ProvinceId = new SelectList(_repoProvince.GetList(x => x.Country == objProject.CountryName).ToList(), "Id", "Name_1", objProject.StateId);
            //    //    //objUserProfile = UserProfileMap.Map(objProfile);
            //    //    // objUserProfile.IsNew = false;
            //    //}
            //    //else
            //    //{
            //    //    objProject.IsNew = true;
            //    //}
            //}
            //else
            //{
            //    objProjectModel.IsNew = true;
            //    //List<UserProfileModel> lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().ToList());
            //    //ViewBag.TotalUserList = lstUserProfile.Count;
            //}

            return View(objProjectModel);
        }

        [HttpPost]
        public ActionResult Project(ProjectModel objProject)
        {
            try
            {
                string option = Request["submit_link"];
                ViewBag.DisciplineId = new SelectList(_repoDiscipline.GetList().ToList(), "Id", "DisciplineName", "");
                if (ModelState.IsValid)
                {
                    if (Request["submit_link"] == "Create Project")
                    {
                        objProject.Id = 0;
                        objProject.CreatedBy = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId).Id;
                        objProject.CreatedDate = DateTime.Now;
                        var obj = ProjectMap.Map(objProject);
                        _repoProject.Add(ProjectMap.Map(objProject));
                    }
                    else
                    {
                        objProject.CreatedDate = DateTime.Now;   ////@Sujesh: Need to check for the javascript to C# conversion for datetime
                        objProject.ModifiedDate = DateTime.Now;
                        _repoProject.Update(ProjectMap.Map(objProject));
                    }
                    _repoProject.Save();
                }
                else
                {
                    TempData[LeonniConstants.ErrorMessage] = " Update Unsuccessful";
                    return View(objProject);
                }
                TempData[LeonniConstants.SuccessMessage] = "Profile Updated Successfully";
                return RedirectToAction("Project");
            }
            catch (Exception e)
            {
                TempData[LeonniConstants.ErrorMessage] = e.ToString();
                return View(objProject);
            }
        }

        public ActionResult ProjectUpload(UploadModel ProjectUploads)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(ProjectUploads.VideoLinkModel.VideoLinkUrl))
                    {
                        ProjectUploads.VideoLinkModel.CreatedDate = DateTime.Now;
                        ProjectUploads.VideoLinkModel.SectionId = 0;
                        var vidLink = VideoLinkMap.Map(ProjectUploads.VideoLinkModel);
                        _repoVideoLink.Add(vidLink);
                        _repoVideoLink.Save();
                    }
                    if (ProjectUploads.ProjectPic != null && ProjectUploads.ProjectPic.InputStream != null)
                    {
                        var file = new Models.File();

                        file.ContentType = ProjectUploads.ProjectPic.ContentType;
                        file.FileName = ProjectUploads.ProjectPic.FileName;

                        var memoryStream = new MemoryStream();
                        ProjectUploads.ProjectPic.InputStream.CopyTo(memoryStream);

                        file.Content = memoryStream.ToArray();
                        file.Link = ProjectUploads.FileLink;

                        _repoFile.Add(file);
                        _repoFile.Save();

                        //FrontEntityFileModel efModel = new FrontEntityFileModel();
                        //var entityFile = FrontEntityFileMap.Map(efModel);
                        //entityFile.SectionId = 0;
                        //entityFile.EntityId = 0;
                        //entityFile.FileId = file.Id;
                        //_repoFrontEntityFile.Add(entityFile);
                        //_repoFrontEntityFile.Save();
                    }
                }
                return View(ProjectUploads);
            }
            catch (Exception e)
            {
                return View(ProjectUploads);
            }
        }


        public ActionResult ProjectsUploads()
        {
            return PartialView("ProjectUploads", new UploadModel());
        }

        public ActionResult AddProject(ProjectModel project)
        {
            return View(project);
        }

        public ActionResult ProjectSearch()
        {
            //List<UserProfileModel> lstUserProfile = UserProfileMap.Map(_repoUserProfile.GetList().ToList());
            //GroupSearchModel objGroupSearch = new GroupSearchModel();
            return PartialView("ProfileSearch");
        }

        public PartialViewResult projectList(string projectName, long? createdBy, int? pageNumber = null, int? itemsPerPage = null)
        {
            int listCount = 0;
            int offset = 0;
            offset = (((int)pageNumber - 1) * (int)itemsPerPage);
            List<ProjectModel> lstProject = new List<ProjectModel>();

            createdBy = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId).Id;

            if (!string.IsNullOrEmpty(projectName))
            {
                lstProject = ProjectMap.Map(_repoProject.GetList(x => x.Title == projectName && x.CreatedBy == createdBy).OrderBy(x => x.Title).Skip(offset).Take((int)itemsPerPage).ToList());
                listCount = _repoGroup.GetList(x => x.Title == projectName && x.CreatedBy == createdBy).ToList().Count;
            }
            else
            {
                lstProject = ProjectMap.Map(_repoProject.GetList(x => x.CreatedBy == createdBy).OrderBy(x => x.Title).Skip(offset).Take((int)itemsPerPage).ToList());
                listCount = _repoGroup.GetList(x => x.CreatedBy == createdBy).ToList().Count;
            }

            ViewBag.PagedListofUsers = lstProject ?? new List<ProjectModel>();
            ViewBag.TotalUserCount = listCount;

            return PartialView("_ProjectList", lstProject);
        }

        public ActionResult AjaxGetProjectById(int Id)
        {
            ProjectModel objProject = ProjectMap.Map(_repoProject.GetSingle(x => x.Id == Id));
            List<ProvinceModel> lstProvince = new List<ProvinceModel>();
            lstProvince = ProvinceMap.Map(_repoProvince.GetList(x => x.Country == objProject.CountryName).ToList());

            List<DisciplineModel> lstDiscipline = new List<DisciplineModel>();
            lstDiscipline = DisciplineMap.Map(_repoDiscipline.GetList(x => x.CategoryId == objProject.CategoryId).ToList());

            return Json(new { status = "success", project = objProject, lstProvince = lstProvince.ToArray(), lstDiscipline = lstDiscipline.ToArray() });
        }

    }
}
