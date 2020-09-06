using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leonni.Core;
using Leonni.Core.Controllers;
using Leonni.Core.Helpers;
using Leonni.Models;
using Leonni.Models.ModelMappers;
using Leonni.Models.ViewModels;
using Leonni.Interfaces;
using System.Linq.Dynamic;


namespace Leonni.Areas.SysAdmin.Controllers
{
    public class LanguageController : LeonniApplicationController
    {
        #region Private Variables

        private Language objLanguage = null;

        ILanguageRepository _repLanguage;

        #endregion Private Variables

        #region Public Constructor

        public LanguageController(ILanguageRepository LanguageRepository)
        {
            objLanguage = new Language();

            _repLanguage = LanguageRepository;

        }

        #endregion Public Constructor

        #region Public Methods

        public ActionResult Index()
        {
            //List<LanguageModel> lstLanguages = LanguageMap.Map(_repLanguage.GetList(x => true));
            ViewBag.Contents = "current";
            return View(new BaseViewModel());
        }

        public ActionResult Create()
        {
            List<SelectListItem> lstitems = LanguageDirectionList();
            ViewData["LanguageDirection"] = lstitems;
            ViewBag.Contents = "current";
            return View(new LanguageModel());
        }

        [HttpPost]
        public ActionResult Create(LanguageModel languageModel)
        {
            try
            {
                if (!IsAvailable(languageModel.LanguageName))
                {
                    string strFileName = string.Empty;
                    string strFileExtension = string.Empty;

                    foreach (string inputTagName in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[inputTagName];
                        if (file.ContentLength == 0)
                        {
                            TempData[LeonniConstants.ErrorMessage] = "Please upload country flag";
                            return RedirectToAction("Create");
                        }

                        strFileExtension = Path.GetExtension(file.FileName);
                        strFileName = languageModel.LanguageCode + strFileExtension;

                        if (strFileExtension.Equals(".png"))
                        {
                            string strTarget = Path.Combine(HttpContext.Server.MapPath(AppSettings.UploadFlagImage), strFileName);
                            file.SaveAs(strTarget);
                        }
                        else
                        {
                            TempData[LeonniConstants.ErrorMessage] = "\"" + strFileExtension + "\"" + " extension file is not supported, Please select '.png' extension file.";
                            return RedirectToAction("Create");
                        }
                    }

                    languageModel.LanguageName = CaseChange.TitleCase(languageModel.LanguageName);
                    objLanguage = LanguageMap.Map(languageModel);

                    _repLanguage.Add(objLanguage);
                    _repLanguage.Save();
                    TempData[LeonniConstants.SuccessMessage] = "Language is successfully created";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData[LeonniConstants.ErrorMessage] = "\"" + languageModel.LanguageName + "\"" + " already exists,Please select another Language Name";
                    return RedirectToAction("Create");
                }

            }
            catch
            {
                TempData[LeonniConstants.ErrorMessage] = "Some errors on Language Insertion";
                return RedirectToAction("Create");
            }
        }

        public ActionResult Edit(int id)
        {
            objLanguage = _repLanguage.GetSingle(x => x.Id == id);
            LanguageModel objLanguageModel = LanguageMap.Map(objLanguage);
            List<SelectListItem> lstitems = LanguageDirectionList();
            ViewData["LanguageDirection"] = lstitems;
            lstitems.Where(x => x.Text == objLanguage.LanguageDirection).SingleOrDefault().Selected=true;
            ViewBag.Contents = "current";
            return View(objLanguageModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, LanguageModel languageModel)
        {
            try
            {
                languageModel.LanguageName =CaseChange.TitleCase(languageModel.LanguageName);
                objLanguage = LanguageMap.Map(languageModel);
                string strFileName = string.Empty;
                string strFileExtension = string.Empty;

                foreach (string inputTagName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[inputTagName];
                    if (file.ContentLength > 0)
                    {
                        strFileExtension = Path.GetExtension(file.FileName);
                        strFileName = languageModel.LanguageCode + strFileExtension;

                        if (strFileExtension.Equals(".png"))
                        {
                            string strTarget = Path.Combine(HttpContext.Server.MapPath(AppSettings.UploadFlagImage), strFileName);
                            file.SaveAs(strTarget);
                        }
                        else
                        {
                            TempData[LeonniConstants.ErrorMessage] = "\"" + strFileExtension + "\"" + " extension file is not supported, Please select '.png' extension file.";
                            return RedirectToAction("Create");
                        }
                    }
                }

                _repLanguage.Update(objLanguage);
                _repLanguage.Save();
                TempData[LeonniConstants.SuccessMessage] = "Language is successfully updated";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData[LeonniConstants.ErrorMessage] = "Some errors on Language Update";
                return RedirectToAction("Edit");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                objLanguage = _repLanguage.GetSingle(x => x.Id == id);
                _repLanguage.Delete(objLanguage);
                _repLanguage.Save();
                TempData[LeonniConstants.SuccessMessage] = "Language is successfully Deleted";
            }
            catch
            {
                TempData[LeonniConstants.ErrorMessage] = "Cannot be deleted, Language is being used";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AjaxGetLanguages(string sortname, string sortorder, int page, string qtype, string query, int rp)
        {
            LanguageModel[] totalRows = null;


            var model = _repLanguage.GetList().ToList();
            List<LanguageModel> lstlanguage = new List<LanguageModel>(model.Select(x => LanguageMap.Map(x)));
            totalRows = lstlanguage.ToArray();


            List<RowsCols> rows = new List<RowsCols>(totalRows.Length);

            var data = new
            {
                page = 1,
                total = totalRows.Length,
                rows = rows
            };

            if (!string.IsNullOrEmpty(sortname) && !string.IsNullOrEmpty(sortorder))
            {
                totalRows = totalRows.AsQueryable().OrderBy(sortname + " " + sortorder).ToArray();
            }

            for (int i = 0; i < totalRows.Length; i++)
            {
                string strAction = "<a href='/SysAdmin/Language/Edit/" + totalRows[i].Id + "'>Edit</a>|<a href='/SysAdmin/Language/Details/" + totalRows[i].Id + "'>Details</a>|" +
                                   "<a href='/SysAdmin/Language/Delete/" + totalRows[i].Id + "' data-ajax-method='POST' data-ajax-confirm='Are you sure you want to delete this Item ?' data-ajax-complete='RemoveRow(" + totalRows[i].Id + ")' data-ajax='true'>Delete</a>";

                rows.Add(new RowsCols
                {

                    id = totalRows[i].Id,
                    cell = new FlexLanguage
                    {
                        Id = totalRows[i].Id,
                        LanguageName = totalRows[i].LanguageName,
                        LanguageCode = totalRows[i].LanguageCode,
                        LanguageDirection = totalRows[i].LanguageDirection,
                        Action = strAction,
                    }
                });
            }

            return Json(data);
        }

        #endregion Public Methods

        #region Private Methods

        private bool IsAvailable(string language)
        {
            Language objLanguage = _repLanguage.GetSingle(x => x.LanguageName == language);
            if (objLanguage != null)
                return true;
            else
                return false;
        }

        private static List<SelectListItem> LanguageDirectionList()
        {
            List<SelectListItem> lstitems = new List<SelectListItem>();
            lstitems.Add(new SelectListItem
            {
                Text = "LTR",
                Value = "LTR"

            });
            lstitems.Add(new SelectListItem
            {
                Text = "RTL",
                Value = "RTL"

            });
            return lstitems;
        }

        public class FlexLanguage
        {
            public int Id { get; set; }
            public string LanguageName { get; set; }
            public string LanguageCode { get; set; }
            public string LanguageDirection { get; set; }
            public string Action { get; set; }
        }

        private class RowsCols
        {
            public int id = 0;
            public FlexLanguage cell;
        }

        #endregion Private Methods
    }
}
