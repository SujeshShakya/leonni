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
using Leonni.Models;

namespace Leonni.Controllers
{
    public class CommentsController : LeonniApplicationController
    {
        ICommentRepository _repoComments;
        ICommentThreadRepository _repoThread;
        IUserProfileRepository _repoUserProfile;
        IEntityFileRepository _repoEntityFile;
        ILikeRepository _repoLike;

        public CommentsController(ILikeRepository likeRepository, IEntityFileRepository entityFileRepository, ICommentThreadRepository commentThreadRepository, ICommentRepository commentRepository, IUserProfileRepository userProfileRepository)
        {
            _repoThread = commentThreadRepository;
            _repoComments = commentRepository;
            _repoUserProfile = userProfileRepository;
            _repoEntityFile = entityFileRepository;
            _repoLike = likeRepository;
        }

        public JsonResult Get(string commentUrl = "",long entityId = 0, long sectionId = 0)
        {
            if (Request.IsAjaxRequest())
            {
                
                CommentThread thread = null;
                if (!string.IsNullOrEmpty(commentUrl))
                {
                    thread = _repoThread.GetSingle(x => x.SectionId == sectionId && x.EntityID == entityId);
                }

                long fileId = 0;
                if (thread != null)
                {
                    var lstcomments = _repoComments.GetList(x => x.ThreadID == thread.ID).OrderByDescending(x => x.Date).ToList();
                    var comments = new List<CommentModel>();

                    foreach (Comment comment in lstcomments)
                    {
                        bool showLike = false;
                        int likeCount = 0;
                        var objEntityFile = _repoEntityFile.GetSingle(x => x.EntityId == comment.ProfileId && x.SectionId == 1);
                        if (objEntityFile != null)
                        {
                            fileId = objEntityFile.FileId ?? 0;
                        }
                        else
                        {
                            fileId = 0;
                        }


                        long currentProfileId = 0;                       
                        if (_repoUserProfile != null)
                        {
                            if (CurrentUser != null)
                            {
                                currentProfileId = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId).Id;
                            }
                        }

                        var objLike = _repoLike.GetSingle(x => x.ProfileId == currentProfileId && x.EntityName == "comment" && x.EntityId == comment.ID);
                        if (objLike == null)
                        {
                            showLike = true;
                        }
                        //else
                        //{
                            likeCount = _repoLike.GetList(x => x.EntityName == "comment" && x.EntityId == comment.ID).Count();
                        //}
                        comments.Add(new CommentModel
                        {
                            ID = comment.ID,
                            Body = comment.Body,
                            Date = String.Format("{0:d}", comment.Date),
                            ThreadID = comment.ThreadID,
                            UserID = comment.UserID,
                            UserName = comment.UserName,
                            Email = comment.Email,
                            ProfileId = comment.ProfileId,
                            FileId = fileId,
                            ShowLike = showLike,
                            LikeCount = likeCount,
                            CurrentUserRole = CurrentRole
                        });
                    }                 
               
                
                             
                

               // var allfiles = new Dictionary<string, List<object>>();
                //////long fileId = 0;
                //////if (thread != null)
                //////{
                //////    var lstcomments = _repoComments.GetList(x => x.ThreadID == thread.ID).OrderBy(x => x.ID).ToList();
                //////    var comments = new List<CommentModel>();
                //////    lstcomments.ForEach(x =>
                //////    {
                //////        var objEntityFile = _repoEntityFile.GetSingle(y => y.EntityId == x.ProfileId && y.SectionId == 1);
                //////        if (objEntityFile != null)
                //////        {
                //////            fileId = objEntityFile.FileId ?? 0;
                //////        }

                //////        comments.Add(new CommentModel
                //////        {
                //////            ID = x.ID,
                //////            Body = x.Body,
                //////            Date = String.Format("{0:F}", x.Date),
                //////            ThreadID = x.ThreadID,
                //////            UserID = x.UserID,
                //////            UserName = x.UserName,
                //////            Email = x.Email,
                //////            ProfileId = x.ProfileId,
                //////            FileId = fileId
                //////        });

                //////        //allfiles.Add(x.ID.ToString(), GetFilesForComment(x.ID));
                //////    });


                    return Json(new { comments = comments, commentcount = comments.Count }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { commentcount = 0 }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public JsonResult GetTopRatedComments(string commentUrl = "", long entityId = 0, long sectionId = 0)
        {
            if (Request.IsAjaxRequest())
            {

                CommentThread thread = null;
                if (!string.IsNullOrEmpty(commentUrl))
                {
                    thread = _repoThread.GetSingle(x => x.SectionId == sectionId && x.EntityID == entityId);
                }

                long fileId = 0;
                if (thread != null)
                {
                    
                    var lstcomments = _repoComments.GetList(x => x.ThreadID == thread.ID).OrderBy(x => x.ID).ToList();
                    var comments = new List<CommentModel>();

                    foreach (Comment comment in lstcomments)
                    {
                        bool showLike = false;
                        int likeCount = 0;
                        var objEntityFile = _repoEntityFile.GetSingle(x => x.EntityId == comment.ProfileId && x.SectionId == 1);
                        if (objEntityFile != null)
                        {
                            fileId = objEntityFile.FileId ?? 0;
                        }
                        else
                        {
                            fileId = 0;
                        }


                        long currentProfileId = 0;
                        if (_repoUserProfile != null)
                        {
                            if (CurrentUser != null)
                            {
                                currentProfileId = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId).Id;
                            }
                        }

                        var objLike = _repoLike.GetSingle(x => x.ProfileId == currentProfileId && x.EntityName == "comment" && x.EntityId == comment.ID);
                        if (objLike == null)
                        {
                            showLike = true;
                        }
                        //else
                        //{
                        likeCount = _repoLike.GetList(x => x.EntityName == "comment" && x.EntityId == comment.ID).Count();
                        //}
                        comments.Add(new CommentModel
                        {
                            ID = comment.ID,
                            Body = comment.Body,
                            Date = String.Format("{0:d}", comment.Date),
                            ThreadID = comment.ThreadID,
                            UserID = comment.UserID,
                            UserName = comment.UserName,
                            Email = comment.Email,
                            ProfileId = comment.ProfileId,
                            FileId = fileId,
                            ShowLike = showLike,
                            LikeCount = likeCount,
                            CurrentUserRole = CurrentRole
                        });
                    }

                    var objSortedComments = new List<CommentModel>();
                    foreach (CommentModel comment in comments)
                    {
                        objSortedComments.Add(new CommentModel
                        {
                            ID = comment.ID,
                            Body = comment.Body,
                            Date = String.Format("{0:d}", comment.Date),
                            ThreadID = comment.ThreadID,
                            UserID = comment.UserID,
                            UserName = comment.UserName,
                            Email = comment.Email,
                            ProfileId = comment.ProfileId,
                            FileId = comment.FileId,
                            ShowLike = comment.ShowLike,
                            LikeCount = comment.LikeCount,
                            CurrentUserRole = comment.CurrentUserRole
                        });
                    }

                    var lstSortedComments = objSortedComments.OrderByDescending(x => x.LikeCount).ToList();

                    return Json(new { comments = lstSortedComments, commentcount = comments.Count }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { commentcount = 0 }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        //private List<object> GetFilesForComment(long commentID)
        //{
        //    var tempFiles = new List<object>();
        //    var commentfiles = _repoEntityFile.GetAll(y => y.EntityIDInt == commentID && y.EntityName == "comment").ToList();

        //    commentfiles.ForEach(z =>
        //    {
        //        var file = _repoFiles.GetSingle(f => f.ID == z.FileID);
        //        tempFiles.Add(new
        //        {
        //            fileid = z.FileID,
        //            filename = file.Name,
        //            contenttype = file.ContentType
        //        });
        //    });

        //    return tempFiles;
        //}

        [HttpPost]
        public JsonResult Set(string commentBody, string commentUrl = "", int sectionId = 0, long entityID = 0)
        {
            if (Request.IsAjaxRequest())
            {
                CommentThread thread = new CommentThread();
                var threadID = Guid.NewGuid();

                if (!string.IsNullOrEmpty(commentUrl))
                {
                    //Only the url is known
                    //thread = _repoThread.GetSingle(x => x.URL == commentUrl);
                    thread = _repoThread.GetSingle(x => x.SectionId == sectionId && x.EntityID == entityID);
                    if (thread == null)
                    {
                        _repoThread.Add(new CommentThread
                        {
                            ID = threadID,
                            URL = commentUrl,
                            SectionId = sectionId,
                            EntityID = entityID
                        });
                        _repoThread.Save();
                    }
                    else threadID = thread.ID;
                }

                //bool showLike = false;
                
                var userName = "Guest";
                var email = "admin@leonni.com";
                long profileId = 0;
                var userId = new Guid("f22511dc-37d7-456d-9da7-2b9dbbe316e0");
                if (CurrentUser != null)
                {
                    var objProfile = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId);
                    if (objProfile != null)
                    {
                        profileId = objProfile.Id;
                        userName = objProfile.FirstName;
                    }
                    userId = CurrentUser.UserId;
                }


                var comment = new Comment
                {
                    ThreadID = threadID,
                    Body = commentBody,
                    Date = DateTime.Now,
                    UserName = userName, //base.objCurrentUser.UserName,
                    UserID = userId,//new Guid("f22511dc-37d7-456d-9da7-2b9dbbe316e0"), //(Guid)base.objCurrentUser.ProviderUserKey,
                    Email = email, //base.objCurrentUser.Email
                    ProfileId = profileId
                };

                _repoComments.Add(comment);
                _repoComments.Save();

                long fileId = 0;
                var objEntityFile = _repoEntityFile.GetSingle(x => x.EntityId == comment.ProfileId && x.SectionId == 1);
                if (objEntityFile != null)
                {
                    fileId = objEntityFile.FileId ?? 0;
                }
                int likeCount = 0;
                var objLike = _repoLike.GetSingle(x => x.ProfileId == profileId && x.EntityName == "comment" && x.EntityId == comment.ID);
                //if (objLike == null)
                //{
                //    showLike = true;
                //}
                if (objLike != null)
                {
                    likeCount = _repoLike.GetList(x => x.EntityName == "comment" && x.EntityId == comment.ID).Count();
                }

                var commentModelJSON = new CommentModel
                {
                    ID = comment.ID,
                    Body = comment.Body,
                    Date = String.Format("{0:d}", comment.Date),
                    ThreadID = comment.ThreadID,
                    Email = comment.Email,
                    UserID = comment.UserID,
                    UserName = comment.UserName,
                    ProfileId = comment.ProfileId,
                    FileId = fileId,
                    LikeCount = likeCount
                };

               // var files = this.GetFilesForComment(comment.ID);
                
                return Json(new { comment = commentModelJSON, success = true });
            }
            return null;
        }

        public ActionResult Delete(long id)
        {
            bool success = false;
            try
            {
                _repoComments.Delete(_repoComments.GetSingle(x => x.ID == id));
                _repoComments.Save();
                //delete comment files

                //var e = _repoEntityFile.GetAll(x => x.EntityIDInt == id && x.EntityName == "comment");
                //e.ToList().ForEach(x =>
                //{
                //    _repoEntityFile.Delete(x);

                //    var file = _repoFiles.GetSingle(f => f.ID == x.FileID);
                //    _repoFiles.Delete(file);

                //});

                success = true;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                success = false;
            }
            return Json(new { success = success });
        }

        public ActionResult Like(long id)
        {
            bool success = false;
            try
            {
                long currentProfileId = 0;
                var objLike = new Like();
                LikeModel objLikeModel = new LikeModel();
                if (_repoUserProfile != null)
                {
                    if (CurrentUser != null)
                    {
                        currentProfileId = _repoUserProfile.GetSingle(x => x.UserId == CurrentUser.UserId).Id;
                    }
                }
                objLike = _repoLike.GetSingle(x => x.ProfileId == currentProfileId && x.EntityName == "comment" && x.EntityId  == id );
                if (objLike == null)
                {
                    objLikeModel.EntityName = "comment";
                    objLikeModel.EntityId = id;
                    objLikeModel.ProfileId = currentProfileId;
                    objLike = LikeMap.Map(objLikeModel);
                    _repoLike.Add(objLike);
                    _repoLike.Save();
                }
                success = true;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                success = false;
            }
            return Json(new { success = success });
        }
    }
}

