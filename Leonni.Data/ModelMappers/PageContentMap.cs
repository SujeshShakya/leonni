using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leonni.Models.ViewModels;
using Leonni.Models;

namespace Leonni.Models.ModelMappers
{
    public class PageContentMap
    {
        public static PageContent Map(PageContentModel objModel)
        {
            PageContent objPageContent = new PageContent()
            {
                Id = objModel.PageId,
                LanguageId = objModel.LanguageId,
                PageUrl = objModel.PageUrl.Trim(),
                PageName = objModel.PageName.Trim(),
                Content = objModel.Content.Trim(),
                Language = LanguageMap.Map(objModel.Language)
            };

            return objPageContent;

        }

        public static PageContentModel Map(PageContent objModel)
        {
            PageContentModel objPageContentModel = new PageContentModel()
            {
                PageId = objModel.Id,
                //LanguageId = objModel.LanguageId,
                PageUrl = objModel.PageUrl.Trim(),
                PageName = objModel.PageName.Trim(),
                Content = objModel.Content.Trim(),
                Language = LanguageMap.Map(objModel.Language)
            };
            //objPageContentModel.Language = LanguageMap.Map(Language.FindSingleBy.Id(objModel.LanguageId));
            return objPageContentModel;
        }

        public static List<PageContentModel> Map(List<PageContent> objModel)
        {
            var ret = new List<PageContentModel>();
            int i = 0;
            int objModelCount = objModel.Count;
            for (i = 0; i < objModelCount; i++)
            {
                ret.Add(Map(objModel[i]));
            }

            return ret;
        }

        public static void ApplyChanges(PageContentEditModel pageContentModel, PageContent objPageContent)
        {
            objPageContent.LanguageId = pageContentModel.LanguageId;
            objPageContent.PageUrl = pageContentModel.PageUrl;
            objPageContent.PageName = pageContentModel.PageName;
            objPageContent.Content = pageContentModel.Content;
        }
    }
}
