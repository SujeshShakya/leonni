using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Leonni.Models.ViewModels
{
    public class FrontModel : BaseViewModel
    {
        public List<VideoLinkModel> VideoListModel { get; set; }
        public VideoLinkModel VideoLinkModel { get; set; }
        public FrontEntityFileModel FrontEntityFileModel { get; set; }
        public HttpPostedFileBase BannerPic { get; set; }
        public List<FrontEntityFileModel> FrontEntityFileListModel { get; set; }
        public string FileLink { get; set; }
        public List<SectionModel> SectionListModel { get; set; }
        public SectionModel SectionModel { get; set; }
        public long sectionId { get; set; }
    }
}
