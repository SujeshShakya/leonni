using System.IO;
using System.Web.Mvc;
using System.Xml;
using Leonni.Core;
using Leonni.Core.Controllers;
using Leonni.Interfaces;
using Leonni.Models;
using Leonni.Models.Binder;
using Leonni.Models.ModelMappers;
using Leonni.Models.ViewModels;
using Leonni.Web;
using Leonni.Core.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;


namespace Leonni.Controllers
{
    public class TranslationController : LeonniApplicationController
    {

        #region Private Member Variables

        private ITranslationRepository _repTranslation;
        private ILanguageRepository _repLanguage;
        

        #endregion Private Member Variables

        #region Constructor

        public TranslationController(ITranslationRepository TranslationRepository,ILanguageRepository LanguageRepository)
        {

            _repLanguage = LanguageRepository;
            _repTranslation = TranslationRepository;
        }

        #endregion

        #region Public Methods

        public ActionResult Index()
        {
            var viewModel = new TranslationIndexView();
            viewModel.Languages =  LanguageMap.Map(_repLanguage.GetList().ToList());
            TranslationMethods obj = new TranslationMethods(_repTranslation, _repLanguage);
            viewModel.TranslationClassifications = obj.Classifications;
                //Translation.Classifications;
            ViewBag.Contents="current";
            return View(viewModel);
        }

        public ActionResult Export(int languageId, string classification)
        {
            TranslationMethods obj = new TranslationMethods(_repTranslation, _repLanguage);
            var translationXML = obj.GetTranslationXMLForLanguageIdAndClassification(languageId, classification);
                
                //Translation.GetTranslationXMLForLanguageIdAndClassification(languageId, classification);
            if (translationXML != null)
            {
                Language language = _repLanguage.GetSingle(x=>x.Id==languageId);
                //System.IO.MemoryStream xmlStream = new System.IO.MemoryStream();
                //System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(xmlStream);
                //translationXML.WriteTo(writer);
                //xmlStream.Seek(0, System.IO.SeekOrigin.Begin);

                using (MemoryStream xmlMemoryStream = new MemoryStream())
                {
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.OmitXmlDeclaration = false;
                    xmlWriterSettings.Indent = true;

                    using (XmlWriter xw = XmlWriter.Create(xmlMemoryStream, xmlWriterSettings))
                    {
                        translationXML.WriteTo(xw);
                        xmlMemoryStream.Seek(0, SeekOrigin.Begin);
                    }

                    return File(xmlMemoryStream.ToArray(), "text/xml", string.Format("{0}_{1}_LeonniTranslations.xml", classification, language.LanguageCode));
                }
            }

            TempData[LeonniConstants.ErrorMessage] = "The tranlsation resource you were looking for was not found";
            return RedirectToAction("Index", "Translation");
            //throw new Exception("The tranlsation resource you were looking for was not found");
        }

        public ActionResult Import([ModelBinder(typeof(TranslationImportModelBinder))]TranslationImportModel model)
        {
            if (ModelState.IsValid)
            {
                TranslationMethods obj = new TranslationMethods(_repTranslation, _repLanguage);
                obj.Import(model.Content);
                TempData[LeonniConstants.SuccessMessage] = "File successfully imported";
            }
            else
            {
                TempData[LeonniConstants.ErrorMessage] = ModelState.ToHtmlString();
            }
            return RedirectToAction("Index", "Translation");
        }

        public ActionResult ExportEmail(int languageId, string emailType)
        {
            TranslationMethods obj = new TranslationMethods(_repTranslation, _repLanguage);
            var translationXML = obj.GetEmailXMLForLanguageIdAndType(languageId, emailType);
            if (translationXML != null)
            {
                Language language = _repLanguage.GetSingle(x=>x.Id==languageId);
                using (MemoryStream xmlMemoryStream = new MemoryStream())
                {
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.OmitXmlDeclaration = false;
                    xmlWriterSettings.Indent = true;

                    using (XmlWriter xw = XmlWriter.Create(xmlMemoryStream, xmlWriterSettings))
                    {
                        translationXML.WriteTo(xw);
                        xmlMemoryStream.Seek(0, SeekOrigin.Begin);
                    }

                    return File(xmlMemoryStream.ToArray(), "text/xml", string.Format("{0}_{1}_LeonniTranslations.xml", emailType, language.LanguageCode));
                }
            }

            TempData[LeonniConstants.ErrorMessage] = "The Email templated you were looking for was not found";
            return RedirectToAction("Index", "Translation");
        }

        public ActionResult ImportEmail([ModelBinder(typeof(EmailImportModelBinder))]EmailImportModel model)
        {
            if (ModelState.IsValid)
            {
                TranslationMethods obj = new TranslationMethods(_repTranslation, _repLanguage);
                obj.ImportEmail(model.EmailType, model.Content);
                TempData[LeonniConstants.SuccessMessage] = "File successfully imported";
            }
            else
            {
                TempData[LeonniConstants.ErrorMessage] = ModelState.ToHtmlString();
            }
            return RedirectToAction("Index", "Translation");
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion Public Methods


     
    }
}
