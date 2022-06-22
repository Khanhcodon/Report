using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;

namespace Bkav.eGovCloud.Helper
{
    public class UserSetting
    {
        private readonly UserBll _userService;
        private readonly LanguageSettings _languageSettings;
        private readonly AdminGeneralSettings _generalSettings;

        public UserSetting()
        {
            _userService = DependencyResolver.Current.GetService<UserBll>();
            _languageSettings = DependencyResolver.Current.GetService<LanguageSettings>();
            _generalSettings = DependencyResolver.Current.GetService<AdminGeneralSettings>();
        }

        /// <summary>
        /// Lấy thiết lập cấu hình chung của người dùng hiện tại
        /// </summary>
        /// <param name="editable">Query readonly for update setting</param>
        /// <returns>Trả về đối tượng cấu hình của người dùng trong database</returns>
        public UserSettingModel GetUserCurrentSetting(bool editable = false)
        {
            string userSetting;
            userSetting = editable ? _userService.CurrentEditableUser.UserSetting : _userService.CurrentUser.UserSetting;
            return GetUserSetting(userSetting);
        }

        /// <summary>
        /// Lấy thiết lập của người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserSettingModel GetUserSetting(User user)
        {
            if (user == null)
            {
                return null;
            }

            return GetUserSetting(user.UserSetting);
        }

        /// <summary>
        /// Lấy thông tin cấu hình theo chuỗi json
        /// </summary>
        /// <param name="cfgSettingJson"></param>
        /// <returns></returns>
        public UserSettingModel GetUserSetting(string cfgSettingJson)
        {
            var userSettingModel = new UserSettingModel();
            if (!string.IsNullOrWhiteSpace(cfgSettingJson))
            {
                try
                {
                    userSettingModel = Json2.ParseAs<UserSettingModel>(cfgSettingJson);
                }
                catch { }
            }

            return userSettingModel;
        }

        public NotifyInfo GetNotifyInfo(int userId)
        {
            var user = _userService.GetFromCache(userId);
            if (user == null)
            {
                return null;
            }

            return user.NotifyInfoModel;
        }
        
        public NotifyInfoModel GetNotifyInfo(string notifyInfoJson)
        {
            var result = new NotifyInfoModel();

            if (string.IsNullOrEmpty(notifyInfoJson))
            {
                return result;
            }

            try
            {
                result = Json2.ParseAs<NotifyInfoModel>(notifyInfoJson);
            }
            catch { }

            return result;
        }

        public string GetAvaterPath()
        {
            return _generalSettings.Avatar;
        }

        public string GetUserAvatar(string userName)
        {
            var avartar = string.Empty;
            try
            {
                avartar = string.Format(GetAvaterPath(), userName) + "?date=" + DateTime.Now.ToString("ddmmyyyyhhmmss");

            }
            catch { }

            return avartar;
        }

        public string Avatar { get; set; }
    }
}