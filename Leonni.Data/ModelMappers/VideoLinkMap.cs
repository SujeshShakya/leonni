using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leonni.Models.ViewModels;

namespace Leonni.Models.ModelMappers
{
    public class VideoLinkMap
    {
        public static VideoLink Map(VideoLinkModel objModel)
        {
            return new VideoLink
            {
                Id = objModel.Id,
                VideoLinkUrl = objModel.VideoLinkUrl,
                SectionId = objModel.SectionId,
                CreatedDate = objModel.CreatedDate
            };

        }

        public static VideoLinkModel Map(VideoLink objModel)
        {
            return new VideoLinkModel
            {
                Id = objModel.Id,
                VideoLinkUrl = objModel.VideoLinkUrl,
                SectionId = objModel.SectionId,
                CreatedDate = objModel.CreatedDate
            };
        }

        public static List<VideoLinkModel> Map(List<VideoLink> lstModel)
        {
            List<VideoLinkModel> mapped = new List<VideoLinkModel>();

            foreach (VideoLink listItem in lstModel)
            {
                mapped.Add(VideoLinkMap.Map(listItem));
            }

            return mapped;
        }
    }
}
