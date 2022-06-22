using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Business.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SettingProvider - public - BLL
    /// Access Modifiers: 
    /// Create Date : 090812
    /// Author      : TrungVH
    /// Description : Provider giúp lưu lại hoặc xóa tất cả các loại cấu hình được kế thừa từ interface ISettings
    /// </summary>
    public class SettingProvider<TSettings> where TSettings : ISettings, new()
    {
        private readonly SettingBll _settingService;

        ///<summary>
        /// Khởi tạo class.
        ///</summary>
        ///<param name="settingService">BLL tương ứng với bảng Setting trong CSDL</param>
        public SettingProvider(SettingBll settingService)
        {
            _settingService = settingService;
            BuildSetting();
        }

        /// <summary>
        /// Lấy hoặc thiết lập loại cấu hình
        /// </summary>
        public TSettings Settings { get; protected set; }

        private void BuildSetting()
        {
            Settings = Activator.CreateInstance<TSettings>();
            var type = typeof(TSettings);
            // lấy ra những thuộc tính cho phép ghi
            var properties = type.GetProperties()
                        .Where(prop => prop.CanWrite && prop.CanRead)
                        .Select(
                            prop =>
                            new
                            {
                                prop,
                                setting = _settingService.GetSettingValueByKey<string>(type.Name + "." + prop.Name)
                            }
                        )
                        .Where(set => !(String.IsNullOrEmpty(set.setting))
                                      && ConvertHelper.GetTypeConverter(set.prop.PropertyType).CanConvertFrom(typeof(string))
                                      && ConvertHelper.GetTypeConverter(set.prop.PropertyType).IsValid(set.setting))
                        .Select(
                            set =>
                            new
                            {
                                set.prop,
                                value = ConvertHelper.GetTypeConverter(set.prop.PropertyType).ConvertFromInvariantString(set.setting)
                            }
                        );

            // gán giá trị cho các thuộc tính
            properties.ToList().ForEach(p => p.prop.SetValue(Settings, p.value, null));
        }

        /// <summary>
        /// Lưu các cấu hình liên quan đến loại cấu hình được truyền vào
        /// </summary>
        /// <param name="settings">Loại cấu hình</param>
        public void SaveSettings(TSettings settings)
        {
            var type = typeof(TSettings);
            var properties = type.GetProperties()
                            .Where(prop => prop.CanWrite && prop.CanRead)
                            .Where(prop => ConvertHelper.GetTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string)));

            foreach (var prop in properties)
            {
                var key = type.Name + "." + prop.Name;
                dynamic value = prop.GetValue(settings, null);
                _settingService.SetSetting(key, value ?? "", false);
            }
            _settingService.SaveChanges();
            _settingService.ClearCache();
            Settings = settings;
        }

        /// <summary>
        /// Xóa các cấu hình liên quan đến loại cấu hình được truyền vào
        /// </summary>
        public void DeleteSettings()
        {
            var type = typeof(TSettings);
            var properties = type.GetProperties();

            var settingList = new List<Setting>();
            foreach (var prop in properties)
            {
                var key = type.Name + "." + prop.Name;
                var setting = _settingService.Get(key);
                if (setting != null)
                {
                    settingList.Add(setting);
                }
            }
            _settingService.DeleteSetting(settingList);
        }
    }
}
