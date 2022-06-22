using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Business.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SettingBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 090812
    /// Author      : TrungVH
    /// Description : BLL tương ứng với bảng Setting trong CSDL
    /// </summary>
    public class SettingBll : ServiceBase
    {
        private readonly IRepository<Setting> _settingRepository;
        private readonly MemoryCacheManager _cacheManager;
        private IDictionary<string, KeyValuePair<int, string>> _allSettingCached;

        ///<summary>
        /// Khởi tạo class <see cref="SettingBll"/>.
        ///</summary>
        ///<param name="context">Admin context</param>
        ///<param name="cacheManager">Cache manager</param>
        public SettingBll(IDbAdminContext context, MemoryCacheManager cacheManager)
            : base(context)
        {
            _settingRepository = Context.GetRepository<Setting>();
            _cacheManager = cacheManager;
        }

        ///<summary>
        /// Khởi tạo class <see cref="SettingBll"/>.
        ///</summary>
        ///<param name="context">Customer context</param>
        ///<param name="cacheManager">Cache manager</param>
        public SettingBll(IDbCustomerContext context, MemoryCacheManager cacheManager)
            : base(context)
        {
            _settingRepository = Context.GetRepository<Setting>();
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Tạo mới cấu hình
        /// </summary>
        /// <param name="setting">Entity cấu hình</param>
        /// <param name="clearCache">Có xóa cache đi không? (Có xóa:true, ngược lại:false)</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity cấu hình truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi key cấu hình đã tồn tại</exception>
        public void Create(Setting setting, bool clearCache = true)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }
            if (_settingRepository.Exist(SettingQuery.WithKey(setting.SettingKey)))
            {
                throw new Exception(string.Format("Cấu hình với key ({0}) đã tồn tại!", setting.SettingKey));
            }
            _settingRepository.Create(setting);
            Context.SaveChanges();
            if (clearCache)
            {
                ClearCache();
            }
        }

        /// <summary>
        /// Kiem tra da ton tai key trong setting chua
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        public bool Exist(string settingKey)
        {
            return _settingRepository.Exist(SettingQuery.WithKey(settingKey));
        }

        /// <summary>
        /// Cập nhật thông tin cấu hình
        /// </summary>
        /// <param name="setting">Entity cấu hình</param>
        /// <param name="oldSettingKey">Key cấu hình trước khi cập nhật</param>
        /// <param name="clearCache">Có xóa cache đi không? (Có xóa:true, ngược lại:false)</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity cấu hình truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi key cấu hình đã tồn tại</exception>
        public void Update(Setting setting, string oldSettingKey, bool clearCache = true)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }
            if (_settingRepository.Exist(SettingQuery.WithKey(setting.SettingKey).And(r => r.SettingKey.ToLower() != oldSettingKey.ToLower())))
            {
                throw new Exception(string.Format("Cấu hình với key ({0}) đã tồn tại!", setting.SettingKey));
            }
            Context.SaveChanges();

            if (clearCache)
            {
                ClearCache();
            }
        }

        /// <summary>
        /// Xóa cấu hình
        /// </summary>
        /// <param name="setting">Entity cấu hình</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity cấu hình truyền vào bị null</exception>
        public virtual void DeleteSetting(Setting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }
            _settingRepository.Delete(setting);
            Context.SaveChanges();

            ClearCache();
        }

        /// <summary>
        /// Xóa cấu hình
        /// </summary>
        /// <param name="settings">Entity cấu hình</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity cấu hình truyền vào bị null</exception>
        public virtual void DeleteSetting(IEnumerable<Setting> settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            foreach (var setting in settings)
            {
                _settingRepository.Delete(setting);
            }
            Context.SaveChanges();

            ClearCache();
        }

        /// <summary>
        /// Lấy ra cấu hình theo id
        /// </summary>
        /// <param name="id">Id của cấu hình</param>
        /// <returns>Entity cấu hình</returns>
        public Setting Get(int id)
        {
            Setting setting = null;
            if (id > 0)
            {
                setting = _settingRepository.Get(id);
            }
            return setting;
        }

        /// <summary>
        /// Lấy ra cấu hình theo key
        /// </summary>
        /// <param name="key">Key của cấu hình</param>
        /// <returns>Entity cấu hình</returns>
        public Setting Get(string key)
        {
            Setting result = null;
            if (!String.IsNullOrEmpty(key))
            {
                key = key.Trim().ToLowerInvariant();
                var settings = GetAllSettings();
                if (settings.ContainsKey(key))
                {
                    var id = settings[key].Key;
                    result = Get(id);
                }
            }
            return result;
        }

        /// <summary>
        /// Lấy ra tất cả các cấu hình
        /// </summary>
        /// <returns>Danh sách các cấu hình dạng từ điển</returns>
        public IDictionary<string, KeyValuePair<int, string>> GetAllSettings()
        {
            if (_allSettingCached != null)
            {
                return _allSettingCached;
            }

            _allSettingCached =  _cacheManager.Get(CacheParam.SettingsAllKey, CacheParam.SettingsAllCacheTimeOut, () =>
            {
                var settings = _settingRepository.Gets(false, null, Context.Filters.Sort<Setting, string>(s => s.SettingKey));
                //format: <key, <id, value>>
                var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
                foreach (var s in settings)
                {
                    var resourceName = s.SettingKey.ToLowerInvariant();
                    if (!dictionary.ContainsKey(resourceName))
                        dictionary.Add(resourceName, new KeyValuePair<int, string>(s.SettingId, s.SettingValue));
                }
                return dictionary;
            });

            return _allSettingCached;
        }

        /// <summary>
        /// Lấy ra giá trị cấu hình theo key
        /// </summary>
        /// <param name="key">Key của cấu hình</param>
        /// <param name="defaultValue">Giá trị mặc định</param>
        /// <typeparam name="T">Kiểu của giá trị cấu hình</typeparam>
        /// <returns>Giá trị của cấu hình</returns>
        public T GetSettingValueByKey<T>(string key, T defaultValue = default(T))
        {
            if (String.IsNullOrEmpty(key))
            {
                return defaultValue;
            }
            key = key.Trim().ToLowerInvariant();
            var settings = GetAllSettings();
            if (settings.ContainsKey(key))
            {
                return settings[key].Value.To<T>();
            }

            return defaultValue;
        }

        /// <summary>
        /// Gán giá trị cho cấu hình
        /// </summary>
        /// <param name="key">Key của cấu hình</param>
        /// <param name="value">Giá trị cần gán</param>
        /// <param name="clearCache">Có xóa cache đi không? (Có xóa:true, ngược lại:false)</param>
        /// <param name="saveChanges"></param>
        /// <typeparam name="T">Kiểu của giá trị</typeparam>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity cấu hình truyền vào bị null</exception>
        public virtual void SetSetting<T>(string key, T value, bool clearCache = true, bool saveChanges = false)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            key = key.Trim().ToLowerInvariant();

            var settings = GetAllSettings();
            Setting setting;
            var valueStr = ConvertHelper.GetTypeConverter(typeof(T)).ConvertToInvariantString(value);
            if (settings.ContainsKey(key))
            {
                if (!settings[key].Value.Equals(valueStr))
                {
                    setting = Get(settings[key].Key);
                    setting.SettingValue = valueStr;
                }
            }
            else
            {
                setting = new Setting
                {
                    SettingKey = key,
                    SettingValue = valueStr,
                };
                _settingRepository.Create(setting);
            }
            if (saveChanges)
            {
                Context.SaveChanges();
            }
            if (clearCache)
            {
                ClearCache();
            }
        }

        /// <summary>
        /// Lưu các cấu hình liên quan đến loại cấu hình được truyền vào
        /// </summary>
        /// <typeparam name="T">Loại cấu hình</typeparam>
        /// <param name="setting">Cấu hình</param>
        public virtual void SaveSetting<T>(T setting) where T : ISettings, new()
        {
            DependencyResolver.Current.GetService<SettingProvider<T>>().SaveSettings(setting);
        }

        /// <summary>
        /// Xóa các cấu hình liên quan đến loại cấu hình được truyền vào
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public virtual void DeleteSetting<T>() where T : ISettings, new()
        {
            DependencyResolver.Current.GetService<SettingProvider<T>>().DeleteSettings();
        }

        /// <summary>
        /// Xóa cache
        /// </summary>
        public virtual void ClearCache()
        {
            // Clear toàn bộ cache do setting ảnh hưởng đến việc xử lý các dữ liệu khác
            _cacheManager.Clear();
            // _cacheManager.Remove(CacheParam.SettingsAllKey);
        }
    }
}
