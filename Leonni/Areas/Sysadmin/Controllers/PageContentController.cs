using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Leonni.Core;
using Leonni.Core.Controllers;
using Leonni.Core.Helpers;
using Leonni.Models;
using Leonni.Models.Binder;
using Leonni.Models.ModelMappers;
using Leonni.Models.ViewModels;
using Leonni.Interfaces;
using System.Linq.Dynamic;


namespace Leonni.Areas.SysAdmin.Controllers
{
    public class PageContentController : LeonniApplicationController
    {

        #region Private Member Variables

        private IPageContentRepository _repPageContent;
        private ILanguageRepository _repLanguage;

        #endregion Private Member Variables

        #region Constructor

        public PageContentController(IPageContentRepository PageContentRepository,ILanguageRepository LanguageRepository)
        {

            _repPageContent = PageContentRepository;
            _repLanguage = LanguageRepository;
        }

        #endregion

        #region Public Methods

        [HttpGet]
        public ActionResult Index()
        {
            List<PageContent> obj = _repPageContent.GetList().ToList();
            List<PageContentModel> objPageContent = PageContentMap.Map(obj);
            if (objPageContent.Count == 0)
            {
                TempData[LeonniConstants.ErrorMessage] = "Record Not Found !";
            }

            ViewBag.Contents = "current";
            return View(objPageContent);
        }

        [HttpGet]
        public ActionResult Create()
        {
            LoadLanguageDropdown();
            PageContentModel objPageContentModel = new PageContentModel();
            ViewBag.Contents = "current";
            return View(objPageContentModel);
        }

        [HttpPost]
        public ActionResult Create([ModelBinder(typeof(PageContentModelBinder))]PageContentModel pageContentModel)
        {
            if (ModelState.IsValid)
            {
                if (!IsAvailable(pageContentModel.PageUrl))
                {
                    pageContentModel.PageUrl = CaseChange.TitleCase(pageContentModel.PageUrl);
                    if (!pageContentModel.PageUrl.StartsWith("/"))
                    {
                        pageContentModel.PageUrl = "/" + pageContentModel.PageUrl;
                    }
                    pageContentModel.PageName = CaseChange.TitleCase(pageContentModel.PageName);
                    PageContent objPageContent = PageContentMap.Map(pageContentModel);
                    _repPageContent.Add(objPageContent);
                    _repPageContent.Save();
                    TempData[LeonniConstants.SuccessMessage] = "Page Content added successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData[LeonniConstants.ErrorMessage] = "\"" + pageContentModel.PageUrl + "\"" + " already exists, Please select another PageUrl name";
                    return RedirectToAction("Create");
                }
            }
            else
            {
                LoadLanguageDropdown();
                return View(pageContentModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PageContentModel objPageContentModel = PageContentMap.Map(_repPageContent.GetSingle(x=>x.Id==id));
            LoadLanguageDropdown();
            ViewBag.Contents = "current";
            return View(objPageContentModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, [ModelBinder(typeof(PageContentModelBinder))]PageContentEditModel pageContentModel)
        {
            if (ModelState.IsValid)
            {
                PageContent objPageContent = _repPageContent.GetSingle(x=>x.Id==id);
                if (objPageContent != null)
                {
                    pageContentModel.PageUrl = CaseChange.TitleCase(pageContentModel.PageUrl);
                    pageContentModel.PageName = CaseChange.TitleCase(pageContentModel.PageName);
                    if (!pageContentModel.PageUrl.StartsWith("/"))
                    {
                        pageContentModel.PageUrl = "/" + pageContentModel.PageUrl;
                    }

                    PageContentMap.ApplyChanges(pageContentModel, objPageContent);

                    if (!string.IsNullOrWhiteSpace(objPageContent.Content))
                    {
                        _repPageContent.Update(objPageContent);
                        _repPageContent.Save();
                    }
                    else
                    {
                        //objPageContent.UpdateWithoutContent();
                    }
                }
                TempData[LeonniConstants.SuccessMessage] = "Page Content edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                LoadLanguageDropdown();
                return View(pageContentModel);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                PageContent objPageContent = _repPageContent.GetSingle(x => x.Id == id);
                _repPageContent.Delete(objPageContent);
                _repPageContent.Save();
                TempData[LeonniConstants.SuccessMessage] = "Page Content Deleted Successfully";
            }
            catch
            {
                TempData[LeonniConstants.ErrorMessage] = "Cannot be deleted, PageContent is being Used";
            }
            return RedirectToAction("Index");

        }

        public class FlexPageContent
        {
            public int PageId { get; set; }
            public int LanguageId { get; set; }
            public string LanguageName { get; set; }
            public string PageUrl { get; set; }
            public string PageName { get; set; }
            public string Content { get; set; }
            public string HyperLinkText { get; set; }
            public string SortKey { get; set; }
            public string FileType { get; set; }

            public LanguageModel Language { get; set; }
            public string Action { get; set; }
        }

        private class RowsCols
        {
            public int id = 0;
            public FlexPageContent cell;
        }


        [HttpPost]
        public ActionResult AjaxGetPageContents(string sortname, string sortorder, int page, string qtype, string query, int rp)
        {
            PageContentModel[] totalRows = null;


            var model = _repPageContent.GetList(x => true).ToList();
            List<PageContentModel> lstPageContent = new List<PageContentModel>(model.Select(x => PageContentMap.Map(x)));
            totalRows = lstPageContent.ToArray();


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
                string strAction = "<a href='/SysAdmin/PageContent/Edit/" + totalRows[i].PageId + "'>Edit</a>|" +
                                   "<a href='/SysAdmin/PageContent/Delete/" + totalRows[i].PageId + "' data-ajax-method='POST' data-ajax-confirm='Are you sure you want to delete this Item ?' data-ajax-complete='RemoveRow(" + totalRows[i].PageId + ")' data-ajax='true'>Delete</a>";

                rows.Add(new RowsCols
                {

                    id = totalRows[i].PageId,
                    cell = new FlexPageContent
                    {
                        PageId = totalRows[i].PageId,
                        FileType = totalRows[i].FileType,
                        Content = totalRows[i].Content,
                        HyperLinkText = totalRows[i].HyperLinkText,
                        Language = totalRows[i].Language,
                        LanguageId = totalRows[i].LanguageId,
                        LanguageName = totalRows[i].Language.LanguageName,
                        PageName = totalRows[i].PageName,
                        PageUrl = totalRows[i].PageUrl,
                        Action = strAction,
                    }
                });
            }

            return Json(data);
        }

        #endregion Public Methods

        #region Private Methods

        private void LoadLanguageDropdown()
        {
            List<LanguageModel> lstLanguage = LanguageMap.Map(_repLanguage.GetList(x => true).ToList());
            ViewBag.Languages = new SelectList(lstLanguage, "Id", "LanguageName");
        }

        private bool IsAvailable(string pageUrl)
        {
            PageContent objPageContent = _repPageContent.GetSingle(x=>x.PageUrl==pageUrl);
            if (objPageContent != null)
                return true;
            else
                return false;
        }

        #endregion Private Methods
    }
}
