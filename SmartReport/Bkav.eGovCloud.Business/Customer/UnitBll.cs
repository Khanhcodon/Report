using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para> 
    /// <para>Project: eGov Cloud v1.0</para> 
    /// <para>Class : CatalogBll - public - BLL</para> 
    /// <para>Access Modifiers: </para> 
    /// <para>Create Date : 181012</para> 
    /// <para>Author      : TienBV</para> 
    /// <para>Description : Quản lý danh mục egov.</para> 
    /// </author>
    /// <summary> 
    /// <para> Quản lý những nhóm giá trị sẽ bind ra các select box khi soạn form động.</para>
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class UnitBll : ServiceBase
    {/// <summary>
     /// 
     /// </summary>
        private readonly IRepository<Ad_Unit> _adunitRepository;
        private readonly ResourceBll _resourceService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="resourceService"></param>
        public UnitBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _adunitRepository = Context.GetRepository<Ad_Unit>();
            _resourceService = resourceService;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ad_Unit"></param>
        public void Create(Ad_Unit ad_Unit)
        {
            if (ad_Unit == null)
            {
                throw new ArgumentNullException("ad_unit");
            }
            if (_adunitRepository.Exist(c => c.Unit == ad_Unit.Unit))
            {
                throw new EgovException("Tên đơn vị tính đã tồn tại!");
            }
            ad_Unit.IdUnit = Guid.NewGuid();
            _adunitRepository.Create(ad_Unit);
            Context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ad_Unit"></param>
        /// <param name="ignoreExist"></param>
        public void Create(IEnumerable<Ad_Unit> ad_Unit, bool ignoreExist = false)
        {
            if (ad_Unit == null || !ad_Unit.Any())
            {
                throw new ArgumentNullException("ad_unit");
            }

            var names = ad_Unit.Select(c => c.Unit);
            var exists = _adunitRepository.GetsAs(p => p.Unit, p => names.Contains(p.Unit));
            if (exists != null && exists.Any())
            {
                if (!ignoreExist || exists.Count() == ad_Unit.Count())
                {
                    throw new EgovException("Danh mục chỉ tiêu phân tổ đã đã tồn tại!");
                }

                var list = ad_Unit.Where(p => !exists.Contains(p.Unit));
                if (list == null || !list.Any())
                {
                    throw new EgovException("Danh mục chỉ tiêu phân tổ đã tồn tại!");
                }
                Create(list);
            }
            else
            {
                Create(ad_Unit);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ad_Unit"></param>
        private void Create(IEnumerable<Ad_Unit> ad_Unit)
        {
            foreach (var adU in ad_Unit)
            {
                _adunitRepository.Create(adU);
            }
            Context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ad_Unit Get(Guid id)
        {
            var result = _adunitRepository.Get(id);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ad_Unit GetName(Guid? id)
        {
            var result = _adunitRepository.Get(id);
            return result;
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="ad_Unit"></param>
        public void Update(Ad_Unit ad_Unit)
        {
            if (ad_Unit == null)
            {
                throw new ArgumentNullException("ad_unit");
            }

            Context.SaveChanges();
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="ad_Unit"></param>
        public void Remove(Ad_Unit ad_Unit)
        {
            _adunitRepository.Delete(ad_Unit);
            Context.SaveChanges();
        }

        public IEnumerable<Ad_Unit> GetAs(Expression<Func<Ad_Unit, bool>> spec = null)
        {
            return _adunitRepository.GetsReadOnly(spec, Context.Filters.Sort<Ad_Unit, Guid?>(c => c.IdUnit));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        public IEnumerable<Ad_Unit> Gets(string sortBy = "", bool isDescending = false)
        {
            return _adunitRepository.GetsReadOnly(null, Context.Filters.CreateSort<Ad_Unit>(isDescending, sortBy));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<Ad_Unit> Gets(Expression<Func<Ad_Unit, bool>> spec = null)
        {
            return _adunitRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<Ad_Unit> GetsSelects(Expression<Func<Ad_Unit, bool>> spec = null)
        {
            return _adunitRepository.GetsReadOnly(spec, Context.Filters.Sort<Ad_Unit, Guid?>(c => c.IdUnit));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="projector"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Ad_Unit, TOutput>> projector, Expression<Func<Ad_Unit, bool>> spec = null)
        {
            return _adunitRepository.GetsAs(projector, spec);
        }
    }
}
