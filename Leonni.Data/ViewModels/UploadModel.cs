using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Leonni.Models.ViewModels
{
   public class UploadModel : BaseViewModel
    {
           public List<VideoLinkModel> VideoListModel { get; set; }
           public VideoLinkModel VideoLinkModel { get; set; }

           public string FileLink { get; set; }
           public HttpPostedFileBase ProjectPic { get; set; }
    }
}
