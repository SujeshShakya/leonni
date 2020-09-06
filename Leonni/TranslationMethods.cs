using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leonni.Interfaces;
using System.Xml.Linq;
using System.Text;
using Leonni.Models;
using System.Xml;
using System.IO;

namespace Leonni.Web
{
    public  class TranslationMethods
    {
        #region Private Member Variables

        public  ITranslationRepository _repTranslation;
        public  ILanguageRepository _repLanguage;

        #endregion Private Member Variables

        #region Constructor

        public  TranslationMethods(ITranslationRepository TranslationRepository, ILanguageRepository LanguageRepository)
        {

            _repLanguage = LanguageRepository;
            _repTranslation = TranslationRepository;
        }

        #endregion

        #region Methods

        public  XElement GetTranslationXMLForLanguageIdAndClassification(int languageId, string classification)
        {
            var translations = _repTranslation.GetList(x => x.LanguageId == languageId && x.Classification == classification).ToList();
            //FindAllBy.LanguageId.And.Classification(languageId, classification);
            Language language = _repLanguage.GetSingle(x => x.Id == languageId);
            //Language.FindSingleBy.Id(languageId) ?? Language.Default;
            XNamespace xn = XNamespace.Xml;
            if (translations.Count > 0)
            {
                XElement xtranslations = new XElement("Translations", new XAttribute(xn + "lang", language.LanguageCode));
                foreach (Translation translation in translations)
                {
                    xtranslations.Add(
                        new XElement("Translation",
                            new XAttribute("key", translation.Key),
                            new XAttribute("classification", translation.Classification),
                            new XElement("page",
                                   new XAttribute("location", translation.PageUrl)),
                            new XElement("value", translation.Value),
                            new XElement("groupname", translation.GroupName)
                        ));
                }
                return xtranslations;
            }

            return null;
        }

        public  XElement GetEmailXMLForLanguageIdAndType(int languageId, string emailType)
        {
            Translation translation = _repTranslation.GetSingle(x => x.LanguageId == languageId && x.Classification == "Email" && x.Key == emailType);
            //FindSingleBy.LanguageId.And.Classification.And.Key(languageId, "Email", emailType);

            if (translation != null)
            {
                Language language = _repLanguage.GetSingle(x => x.Id == languageId) ?? Default;
                XNamespace xn = XNamespace.Xml;
                XElement xtranslation = XElement.Parse(translation.Value);
                return xtranslation;
            }

            return null;
        }

        private  List<string> classifications = new List<string> { "UI", "JS" };

        public  List<string> Classifications
        {
            get
            {
                return classifications;
            }
        }

        public  void Import(string XmlContent)
        {
            XDocument docTranslation = XDocument.Load(XmlReader.Create(new StringReader(XmlContent)));
            XNamespace xn = XNamespace.Xml;
            XElement xTranslations = docTranslation.Element("Translations");

            //**getting language name
            string LanguageName = (xTranslations.Attribute(xn + "lang") ?? new XAttribute("xml:lang", "en-us")).Value;

            
            Language language = _repLanguage.GetSingle(x => x.LanguageCode == LanguageName);
            //Language.FindSingleBy.LanguageCode(LanguageName);
            if (language == null)
            {
                language = Default;
            }

            IEnumerable<XElement> KeysList = docTranslation.Elements("Translations").Elements("Translation");

            List<Translation> Translated = (from tempKeys in KeysList
                                            select new Translation
                                            {
                                                Key = tempKeys.Attribute("key").Value,
                                                LanguageId = language.Id,
                                                Classification = tempKeys.Attribute("classification").Value,
                                                Value = tempKeys.Element("value").Value.ToString(),
                                                PageUrl = tempKeys.Element("page").Attribute("location").Value,
                                                GroupName = tempKeys.Element("groupname").Value.ToString()
                                            }).ToList();
            ///*** saving from xml file to database
            foreach (var trans in Translated)
            {
                try
                {
                    Translation tempTrans = _repTranslation.GetSingle(x => x.Key == trans.Key && x.LanguageId == trans.LanguageId);
                    if (tempTrans != null)
                    {
                        tempTrans.Classification = trans.Classification;
                        tempTrans.Value = trans.Value;
                        tempTrans.PageUrl = trans.PageUrl;
                        tempTrans.GroupName = trans.GroupName;

                        _repTranslation.Update(tempTrans);
                        _repTranslation.Save();
                        //tempTrans.Update();
                    }
                    else
                    {
                        _repTranslation.Add(trans);
                        _repTranslation.Save();
                        //trans.Insert();
                    }
                    //end if
                }
                catch
                {
                }

            }
            //end foreach

        }

        public  void ImportEmail(string emailType, string XmlContent)
        {
            XDocument docTranslation = XDocument.Load(XmlReader.Create(new StringReader(XmlContent)));
            XNamespace xn = XNamespace.Xml;
            XElement xTranslations = docTranslation.Element("email");

            //**getting language name
            string LanguageName = (xTranslations.Attribute(xn + "lang") ?? new XAttribute("xml:lang", "en-us")).Value;

            Language language = _repLanguage.GetSingle(x => x.LanguageName == LanguageName);
            //Language.FindSingleBy.LanguageCode(LanguageName);
            if (language == null)
            {
                language = Default;
            }

            var email = new Translation
            {
                Key = emailType,
                LanguageId = language.Id,
                Classification = "Email",
                Value = XmlContent,
                PageUrl = "~/",
            };
            Translation tempTrans = _repTranslation.GetSingle(x => x.Key == email.Key && x.LanguageId == email.LanguageId);

            //Translation.FindSingleBy.Key.And.LanguageId(email.Key, email.LanguageId);
            if (tempTrans != null)
            {
                tempTrans.Classification = email.Classification;
                tempTrans.Value = email.Value;
                tempTrans.PageUrl = email.PageUrl;

                _repTranslation.Update(tempTrans);
                _repTranslation.Save();
            }
            else
            {
                _repTranslation.Add(email);
                _repTranslation.Save();
            }

        }
        //end function

        public  System.Net.Mail.MailMessage GetEmailMessageTemplate(string emailType, Language language)
        {

            XElement emailXML = GetEmailXMLForLanguageIdAndType(language.Id, emailType);
            if (emailXML != null)
            {
                StringBuilder emailBuilder = new StringBuilder();
                var greetings = emailXML.Elements("greeting");
                foreach (var greeting in greetings)
                {
                    emailBuilder.AppendLine(string.Format("<p>{0}</p>", greeting.Value));
                }

                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.Subject = (emailXML.Element("subject") ?? new XElement("subject")).Value;
                msg.IsBodyHtml = true;
                msg.BodyEncoding = UTF8Encoding.UTF8;
                msg.Body = emailBuilder.ToString();
                return msg;

            }
            return null;
        }

        public static Language Default
        {
            get { return new Language { LanguageCode = "en-US", Id = 1 }; }
        }
        #endregion

    }
}