﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Leonni.Models.ViewModels;
using System.ComponentModel;

namespace Leonni.Models.Binder
{
    public class TranslationImportModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {

            if (propertyDescriptor.Name == "Content")
            {
                if (controllerContext.RequestContext.HttpContext.Request.Files.Count > 0)
                {
                    var uploadedFile = controllerContext.RequestContext.HttpContext.Request.Files[0];
                    var translationImportModel = bindingContext.Model as TranslationImportModel;
                    //pageContentModel.FileType = uploadedFile.ContentType;
                    System.IO.StreamReader reader = new System.IO.StreamReader(uploadedFile.InputStream);
                    translationImportModel.Content = reader.ReadToEnd();

                    //base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
                    return;
                }
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }

    public class EmailImportModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {

            if (propertyDescriptor.Name == "Content")
            {
                if (controllerContext.RequestContext.HttpContext.Request.Files.Count > 0)
                {
                    var uploadedFile = controllerContext.RequestContext.HttpContext.Request.Files[0];
                    var translationImportModel = bindingContext.Model as EmailImportModel;

                    System.IO.StreamReader reader = new System.IO.StreamReader(uploadedFile.InputStream);
                    translationImportModel.Content = reader.ReadToEnd();

                    //base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
                    return;
                }
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}
