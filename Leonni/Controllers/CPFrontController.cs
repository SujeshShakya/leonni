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
        // GET: /CPFront/

        public ActionResult Front()
        {
            ViewBag.CurrentMainTab = "Front";
            ViewBag.CurrentSubTab = "Profile";
            ViewBag.CurrentSuperSubTab = "Name";
            VideoLinkModel objVideoLink = new VideoLinkModel();
            FrontModel objFront = new FrontModel();
            objFront.VideoLinkModel = objVideoLink;

            List<SectionModel> objSection = new List<SectionModel>();
            objSection = SectionMap.Map(_repoSection.GetList(x => x.LanguageId == 1).ToList());
            objFront.SectionListModel = objSection;
            return View("Front", objFront);
        }

        [HttpPost]
        public ActionResult Front(FrontModel objFrontModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(objFrontModel.VideoLinkModel.VideoLinkUrl))
                    {
                        objFrontModel.VideoLinkModel.CreatedDate = DateTime.Now;
                        objFrontModel.VideoLinkModel.SectionId = 0;
                        var vidLink = VideoLinkMap.Map(objFrontModel.VideoLinkModel);
                        _repoVideoLink.Add(vidLink);
                        _repoVideoLink.Save();
                    }
                    if (objFrontModel.BannerPic != null && objFrontModel.BannerPic.InputStream != null)
                    {
                        var file = new Models.File();

                        file.ContentType = objFrontModel.BannerPic.ContentType;
                        file.FileName = objFrontModel.BannerPic.FileName;

                        var memoryStream = new MemoryStream();
                        objFrontModel.BannerPic.InputStream.CopyTo(memoryStream);

                        file.Content = memoryStream.ToArray();
                        file.Link = objFrontModel.FileLink;

                        _repoFile.Add(file);
                        _repoFile.Save();

                        FrontEntityFileModel efModel = new FrontEntityFileModel();
                        var entityFile = FrontEntityFileMap.Map(efModel);
                        entityFile.SectionId = 0;
                        entityFile.EntityId = 0;
                        entityFile.FileId = file.Id;
                        _repoFrontEntityFile.Add(entityFile);
                        _repoFrontEntityFile.Save();
                    }
                }
                return View(objFrontModel);
            }
            catch (Exception e)
            {
                return View(objFrontModel);
            }
        }

        [HttpPost]
        public ActionResult FrontSection(FrontModel objFrontModel, FormCollection fc)
        {
            try
            {
                long sectionId = long.Parse(fc["sectionId"]);
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(objFrontModel.VideoLinkModel.VideoLinkUrl))
                    {
                        objFrontModel.VideoLinkModel.CreatedDate = DateTime.Now;
                        objFrontModel.VideoLinkModel.SectionId = sectionId;
                        var vidLink = VideoLinkMap.Map(objFrontModel.VideoLinkModel);
                        _repoVideoLink.Add(vidLink);
                        _repoVideoLink.Save();
                    }
                    if (objFrontModel.BannerPic != null && objFrontModel.BannerPic.InputStream != null)
                    {
                        var file = new Models.File();

                        file.ContentType = objFrontModel.BannerPic.ContentType;
                        file.FileName = objFrontModel.BannerPic.FileName;

                        var memoryStream = new MemoryStream();
                        objFrontModel.BannerPic.InputStream.CopyTo(memoryStream);

                        file.Content = memoryStream.ToArray();
                        file.Link = objFrontModel.FileLink;

                        _repoFile.Add(file);
                        _repoFile.Save();

                        FrontEntityFileModel efModel = new FrontEntityFileModel();
                        var entityFile = FrontEntityFileMap.Map(efModel);
                        entityFile.SectionId = sectionId;
                        entityFile.EntityId = 0;
                        entityFile.FileId = file.Id;
                        _repoFrontEntityFile.Add(entityFile);
                        _repoFrontEntityFile.Save();
                    }
                }
                return View("Front", objFrontModel);
            }
            catch (Exception e)
            {
                return View("Front", objFrontModel);
            }
        }


        public PartialViewResult FrontSearch()
        {
            //long sectionId = 0;
            //if (HttpContext.Items["one"] != null)
            //{
            long sectionId = long.Parse(HttpContext.Items["one"].ToString());
            //}
            FrontModel objFront = new FrontModel();
            List<VideoLinkModel> lstVideo = new List<VideoLinkModel>();

            lstVideo = VideoLinkMap.Map(_repoVideoLink.GetList(x => x.SectionId == sectionId).OrderBy(x => x.CreatedDate).ToList());

            FrontModel fModel = new FrontModel();
            fModel.VideoListModel = lstVideo;

            List<FrontEntityFileModel> lstFrontEntityFile = new List<FrontEntityFileModel>();
            lstFrontEntityFile = FrontEntityFileMap.Map(_repoFrontEntityFile.GetList(x => x.SectionId == sectionId).OrderBy(x => x.Id).ToList());
            fModel.FrontEntityFileListModel = lstFrontEntityFile;
            return PartialView("_FrontList", fModel);
            //return View(fModel);
        }

        //public PartialViewResult FrontList()
        //{
        //    List<VideoLinkModel> lstVideo = new List<VideoLinkModel>();   

        //    lstVideo = VideoLinkMap.Map(_repoVideoLink.GetList().OrderBy(x => x.CreatedDate).ToList());
        //    FrontModel fModel = new FrontModel();
        //    fModel.VideoListModel = lstVideo;
        //    return PartialView("_FrontList", fModel);
        //}

    }
}
