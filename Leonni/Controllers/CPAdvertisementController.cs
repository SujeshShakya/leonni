using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
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
        // GET: /CPAdvertising/

        public ActionResult Advertisement()
        {
            ViewBag.CurrentMainTab = "Home";
            ViewBag.CurrentSubTab = "ControlPanel";
            ViewBag.CurrentSuperSubTab = "Advertising";

            List<SectionModel> lstSections = new List<SectionModel>();

            lstSections.Add(new SectionModel
            {
                Id = 1111,
                SectionName = "Section",
                LanguageId = CurrentLanguage.Id
            });

            lstSections.AddRange(SectionMap.Map(_repoSection.GetList(x => x.LanguageId == CurrentLanguage.Id).ToList()));

            ViewBag.Sections = new SelectList(lstSections, "Id", "SectionName");

            List<EngineeredToModel> lstEngineered = new List<EngineeredToModel>();

            lstEngineered.Add(
            new EngineeredToModel
            {
                Id = 0,
                EngineeredTo = "Engineered To"
            }
                );

            lstEngineered.Add(
          new EngineeredToModel
          {
              Id = 1,
              EngineeredTo = "Visitors Only"
          }
              );

            lstEngineered.Add(
          new EngineeredToModel
          {
              Id = 2,
              EngineeredTo = "Leonni Users Only"
          }
              );

            lstEngineered.Add(
        new EngineeredToModel
        {
            Id = 3,
            EngineeredTo = "Visitors and Users"
        }
            );

            ViewBag.Engineered = new SelectList(lstEngineered, "EngineeredTo", "EngineeredTo");

            AdvertisementModel objAdvertisement = new AdvertisementModel();
            return View(objAdvertisement);
        }

        [HttpPost]
        public ActionResult Upload(string selectedFileName)
        {
            //try
            //{
            //    //long sectionId = long.Parse(fc["sectionId"]);
            //    //if (ModelState.IsValid)
            //    //{
            //        //if (objFrontModel.BannerPic != null && objFrontModel.BannerPic.InputStream != null)
            //        //{
            //            var file = new Models.File();

            //            file.ContentType = "image / jpeg";
            //            file.FileName = selectedFileName;

            //            var memoryStream = new MemoryStream();
            //            //objFrontModel.BannerPic.InputStream.CopyTo(memoryStream);
            //            var fileContent  = Request.Files[selectedFileName];
            //            var fileStream = fileContent.InputStream;
            //            file.Content = ReadFile("C:\\Users\\binesh\\Desktop\\salir.jpg"); //content;//ReadFile(content);
            //           // file.Link = objFrontModel.FileLink;

            //            _repoFile.Add(file);
            //            _repoFile.Save();

            //            EntityFileModel efModel = new EntityFileModel();
            //            var entityFile = EntityFileMap.Map(efModel);
            //            entityFile.EntityName = "advertisement";
            //            entityFile.EntityId = 0;
            //            entityFile.FileId = file.Id;
            //            _repoEntityFile.Add(entityFile);
            //            _repoEntityFile.Save();
            //        //}
            //   // }
            //            JsonResult res;
            //            string redirectUrl = "/JobDetail/Activity/";
            //            res = new JsonResult
            //            {
            //                Data = new { status = "redirectToURL", message = redirectUrl }
            //            };

            //            return res;
            //    //return Json(new { status = "success" });
            //    //return View("Front", objFrontModel);
            //}
            //catch (Exception e)
            //{
            //    return Json(new { status = "error" });
            //}
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
                //imageStream.CopyTo(memoryStream);

                //var fileContent = Request.Files[selectedFileName];
                //var fileStream = fileContent.InputStream;
                file.Content = ToByteArray(imageStream);//ReadFile("C:\\Users\\binesh\\Desktop\\salir.jpg"); //content;//ReadFile(content);
                // file.Link = objFrontModel.FileLink;
                //file.ContentType = 
                _repoFile.Add(file);
                _repoFile.Save();

                EntityFileModel efModel = new EntityFileModel();
                var entityFile = EntityFileMap.Map(efModel);
             //   entityFile.EntityName = "advertisement";
                entityFile.SectionId = _repoSection.GetSingle(x=>x.SectionName=="Advertisement").Id;
                entityFile.EntityId = 0;
                entityFile.FileId = file.Id;
                
                newId = file.Id;
                _repoEntityFile.Add(entityFile);
                _repoEntityFile.Save();
                
            }
            catch { success = false; }
            return Json(new { success = success, id = newId });
        }

        public static byte[] ToByteArray(Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length; )
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }

        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

    }
}
