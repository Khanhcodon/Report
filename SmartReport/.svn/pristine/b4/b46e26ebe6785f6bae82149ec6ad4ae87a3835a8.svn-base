using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;

namespace Bkav.eGovCloud.Helper
{
    public static class LangHelper
    {
        public static string GetAdminLangCode()
        {
            return GetLangCode();
        }

        public static string GetUserLangCode()
        {
            return GetLangCode(true);
        }

        public static Language GetUserLang()
        {
            var userService = DependencyResolver.Current.GetService<UserBll>();
            var user = userService.CurrentUser;
            var userSettingModel = new UserSettingModel();
            if (!string.IsNullOrWhiteSpace(user.UserSetting))
            {
                userSettingModel = Json2.ParseAs<UserSettingModel>(user.UserSetting);
            }
            return userSettingModel.Language;
        }

        public static string GetLangCode(bool isUserLang = false)
        {
            if (isUserLang)
            {
                try
                {
                    return GetLanguageCode(GetUserLang().ToString());
                }
                catch (Exception)
                {
                    //var _logService = DependencyResolver.Current.GetService<LogBll>();
                    //_logService.Error("Not found User Language, use System Language");
                }
            }
            var _languageSettings = DependencyResolver.Current.GetService<LanguageSettings>();
            return GetLanguageCode(_languageSettings.Language.ToString());
        }

        private static string GetLanguageCode(string lang)
        {
            switch (lang.ToLower())
            {
                case "cambodian":
                    return "km-KH";
                case "chinese":
                    return "zh-CH";
                case "english":
                    return "en-US";
                case "lao":
                    return "lo-LA";
                case "thai":
                    return "th-TH";
                case "vietnamese":
                    return "vi-VN";
                default:
                    return System.Configuration.ConfigurationManager.AppSettings["DefaultCulture"];
            }
        }

    }
}