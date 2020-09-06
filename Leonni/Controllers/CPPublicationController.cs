using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leonni.Models.ViewModels;
using Leonni.Models.ModelMappers;
using Leonni.Core;

namespace Leonni.Controllers
{
    public partial class ControlPanelController
    {
        //
        // GET: /CPPublication/

        public ActionResult Publication()
        {

            ViewBag.CurrentMainTab = "Home";
            ViewBag.CurrentSubTab = "ControlPanel";
            ViewBag.CurrentSuperSubTab = "Publication";

            List<ProjectModel> lstProjects = new List<ProjectModel>();

            lstProjects.Add(new ProjectModel
            {
                Id = 0,
                Title = "Associate Project"
            });


            lstProjects.AddRange(ProjectMap.Map(_repoProject.GetList().ToList()));

            List<GroupModel> lstGroups = new List<GroupModel>();

            lstGroups.Add(new GroupModel
            {
                Id = 0,
                Title = "Associate Group"
            });


            lstGroups.AddRange(GroupMap.Map(_repoGroup.GetList().ToList()));

            PublicationModel objPublication = new PublicationModel();

            objPublication.Projects = new SelectList(lstProjects, "Id", "Title");
            objPublication.Groups = new SelectList(lstGroups, "Id", "Title");
            objPublication.IsNew = true;
            return View(objPublication);
        }

        [HttpPost]
        public ActionResult Publication(PublicationModel objPublication)
        {
            try
            {
                ViewBag.DisciplineId = new SelectList(_repoDiscipline.GetList().ToList(), "Id", "DisciplineName", "");
                if (ModelState.IsValid)
                {
                    if (objPublication.IsNew == true)
                    {
                        objPublication.CreatedBy = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId).Id;
                        objPublication.CreatedDate = DateTime.Now;
                        _repoPublication.Add(PublicationMap.Map(objPublication));
                        objPublication.IsNew = false;
                    }
                    else
                    {


                    }
                    _repoPublication.Save();
                }
                else
                {
                    TempData[LeonniConstants.ErrorMessage] = " Update Unsuccessful";
                    return View(objPublication);
                }
                TempData[LeonniConstants.SuccessMessage] = "Profile Updated Successfully";
                return RedirectToAction("Publication");
            }
            catch (Exception e)
            {
                TempData[LeonniConstants.ErrorMessage] = e.ToString();
                return View(objPublication);
            }
        }

        public ActionResult PublicationSearch()
        {
            return PartialView("CommonSearch");
        }


        //public PartialViewResult publicationList(string name, string countryName, int? provinceId, int? disciplineId, string sex, int? categoryId, int? sortOrder, int? pageNumber = null, int? itemsPerPage = null)
        //{
        //    int listCount = 0;
        //    int offset = 0;
        //    offset = (((int)pageNumber - 1) * (int)itemsPerPage);
        //    List<GroupModel> lstGroup = new List<GroupModel>();
        //    List<GroupModel> lstNewGroupModel = new List<GroupModel>();
        //    //if (groupName == default(string))
        //    //{

        //    //}
        //    if (CurrentRole != "Administrator")
        //    {
        //        createdBy = 9;
        //        //var obj = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId);

        //        //if(obj!=null)
        //        //{
        //        //    createdBy = 9;
        //        //}

        //        if (!string.IsNullOrEmpty(groupName))
        //        {
        //            lstGroup = GroupMap.Map(_repoGroup.GetList(x => x.Title == groupName && x.CreatedBy == createdBy && x.IsDeleted == false).OrderBy(x => x.Title).Skip(offset).Take((int)itemsPerPage).ToList());
        //            listCount = _repoGroup.GetList(x => x.Title == groupName && x.CreatedBy == createdBy && x.IsDeleted != true).ToList().Count;
        //        }
        //        else
        //        {
        //            lstGroup = GroupMap.Map(_repoGroup.GetList(x => x.CreatedBy == createdBy && x.IsDeleted == false).OrderBy(x => x.Title).Skip(offset).Take((int)itemsPerPage).ToList());
        //            listCount = _repoGroup.GetList(x => x.CreatedBy == createdBy && x.IsDeleted != true).ToList().Count;
        //        }
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(groupName))
        //        {
        //            lstGroup = GroupMap.Map(_repoGroup.GetList(x => x.Title == groupName && x.IsDeleted == false).OrderBy(x => x.Title).Skip(offset).Take((int)itemsPerPage).ToList());
        //            listCount = _repoGroup.GetList(x => x.Title == groupName && x.IsDeleted != true).ToList().Count;
        //        }
        //        else
        //        {
        //            lstGroup = GroupMap.Map(_repoGroup.GetList(x => x.IsDeleted == false).OrderBy(x => x.Title).Skip(offset).Take((int)itemsPerPage).ToList());
        //            listCount = _repoGroup.GetList(x => x.IsDeleted != true).ToList().Count;
        //        }
        //    }

        //    ViewBag.PagedListofUsers = lstGroup ?? new List<GroupModel>();
        //    ViewBag.TotalUserCount = listCount;

        //    foreach (var item in lstGroup.ToList())
        //    {
        //        var objEntityFile = _repoEntityFile.GetSingle(x => x.EntityId == item.Id && x.Section.SectionName == "groups");
        //        if (objEntityFile != null)
        //        {
        //            item.FileId = objEntityFile.FileId;
        //        }
        //        lstNewGroupModel.Add(item);
        //    }

        //    return PartialView("_GroupList", lstNewGroupModel);
        //}

    }
}
