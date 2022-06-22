using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.DataAccess.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SettingDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : ISettingDal
    /// Create Date : 090812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Setting trong CSDL
    /// </summary>
    public class SettingDal : DataAccessBase, ISettingDal
    {
        private readonly IRepository<Setting> _settingRepository;

        /// <summary>
        /// Khởi tạo class <see cref="SettingDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public SettingDal(IDbCustomerContext context) : base(context)
        {
            _settingRepository = Context.GetRepository<Setting>();
        }

        /// <summary>
        /// Khởi tạo class <see cref="SettingDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public SettingDal(IDbAdminContext context)
            : base(context)
        {
            _settingRepository = Context.GetRepository<Setting>();
        }

        #pragma warning disable 1591

        public IEnumerable<Setting> Gets(Expression<Func<Setting, bool>> spec = null)
        {
            return _settingRepository.Find(spec);
        }

        public Setting Get(int id)
        {
            return _settingRepository.One(r => r.SettingId == id);
        }

        public Setting Get(string resourceKey)
        {
            return _settingRepository.One(r => r.SettingKey == resourceKey);
        }

        public void Create(Setting resource)
        {
            _settingRepository.Create(resource);
        }

        public void Update(Setting resource)
        {
            _settingRepository.Update(resource);
        }

        public void Delete(Setting resource)
        {
            _settingRepository.Delete(resource);
        }

        public bool Exist(Expression<Func<Setting, bool>> spec)
        {
            return _settingRepository.Any(spec);
        }
    }
}
