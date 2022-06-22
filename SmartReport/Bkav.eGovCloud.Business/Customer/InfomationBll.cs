using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : InfomationBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 17082013</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng Infomation trong CSDL</para>
    /// </summary>
    public class InfomationBll : ServiceBase
    {
        private readonly IRepository<Infomation> _infomationRepository;
        private readonly MemoryCacheManager _cacheManager;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cacheManager"></param>
        public InfomationBll(IDbCustomerContext context, MemoryCacheManager cacheManager)
            : base(context)
        {
            _infomationRepository = Context.GetRepository<Infomation>();
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Tạo mới cơ quan 
        /// </summary>
        /// <param name="infomation">đối tượng cơ quan </param>
        public void Create(Infomation infomation)
        {
            if (infomation == null)
            {
                throw new ArgumentNullException("infomation");
            }
            _infomationRepository.Create(infomation);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy tất cả danh sách cơ quan. Kết quả chỉ đọc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Infomation> Gets()
        {
            return _infomationRepository.GetsReadOnly();
        }

        /// <summary>
        /// Lấy cơ quan đầu tiên
        /// </summary>
        /// <returns></returns>
        public Infomation First()
        {
            var result = _infomationRepository.GetsReadOnly().FirstOrDefault();
            if (result == null)
            {
                result = new Infomation();
            }
            return result;
        }

        /// <summary>
        /// LLấy tất cả danh sách cơ quan. Kết quả chỉ đọc(Danh sách này sẽ được cache lại trong 60 phút)
        /// </summary>
        /// <returns></returns>
        public string GetCurrentOfficeName()
        {
            return _cacheManager.Get(CacheParam.InfomationAllKey, CacheParam.InfomationAllCacheTimeOut, () => GetCurrentOffice());
        }

        /// <summary>
        /// LLấy tất cả danh sách cơ quan. Kết quả chỉ đọc(Danh sách này sẽ được cache lại trong 60 phút)
        /// </summary>
        /// <returns></returns>
        public string GetCurrentSystemName()
        {
            return GetCurrentSystem();
        }

        public bool IsDisplayName() {
            return IsDisplaySystemName();
        }
        /// <summary>
        /// Lấy cơ quan ngoài theo id
        /// </summary>
        /// <param name="id">Id của cơ quan</param>
        /// <returns></returns>
        public Infomation Get(int id)
        {
            return _infomationRepository.Get(id);
        }

        /// <summary>
        /// cập nhật cơ quan ngoài
        /// </summary>
        /// <param name="infomation">đối tượng cơ quan</param>
        public void Update(Infomation infomation)
        {
            if (infomation == null)
            {
                throw new ArgumentNullException("infomation");
            }
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.InfomationAllKey);
        }

        /// <summary>
        /// Trả về tên cơ quan hiện tại
        /// </summary>
        /// <returns></returns>
        private string GetCurrentOffice()
        {
            var result = "";
            var officeInfomation = Gets();
            if (officeInfomation.Any())
            {
                result = officeInfomation.First().Name;
            }

            return result;
        }

        /// <summary>
        /// Trả về tên he thong hien tai
        /// </summary>
        /// <returns></returns>
        private string GetCurrentSystem()
        {
            var result = "";
            var officeInfomation = Gets();
            if (officeInfomation.Any())
            {
                result = officeInfomation.First().SystemName;
            }

            return result;
        }
        /// <summary>
        /// Trả về tên he thong hien tai
        /// </summary>
        /// <returns></returns>
        private bool IsDisplaySystemName()
        {
            bool result = true ;
            var officeInfomation = Gets();
            if (officeInfomation.Any())
            {
                result = officeInfomation.First().IsDisplaySystemName;
            }

            return result;
        }
    }
}
