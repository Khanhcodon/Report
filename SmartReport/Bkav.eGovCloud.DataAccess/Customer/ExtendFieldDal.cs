using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Class : ExtendFieldDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para>     * Inherit : DataAccessBase
    /// <para></para>     * Implement : IExtendFieldDal
    /// <para></para> Create Date : 270612
    /// <para></para> Author      : TrungVH
    /// <para></para> Editor      : TienBV
    /// <para></para> Description : DAL tương ứng với bảng ExtendField trong CSDL
    /// <para></para> Lưu thông tin các trường nhập liệu của các hồ sơ.
    /// </summary>
    public class ExtendFieldDal : DataAccessBase, IExtendFieldDal
    {
        private readonly IRepository<ExtendField> _extendFieldRepository;
        private readonly IRepository<DoctypeFormExtendfield> _doctypeExfieldRepository;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public ExtendFieldDal(IDbCustomerContext context) : base(context)
        {
            _extendFieldRepository = Context.GetRepository<ExtendField>();
            _doctypeExfieldRepository = Context.GetRepository<DoctypeFormExtendfield>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public ExtendField One(System.Linq.Expressions.Expression<System.Func<ExtendField, bool>> spec = null)
        {
            return _extendFieldRepository.One(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exfield"></param>
        public void Add(ExtendField exfield)
        {
            _extendFieldRepository.Create(exfield);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exfield"></param>
        public void Update(ExtendField exfield)
        {
            _extendFieldRepository.Update(exfield);
        }

        /// <summary> TienBV 011112
        /// Lấy ra danh sách các extend field theo điều kiện truyền vào.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<ExtendField> Gets(Expression<Func<ExtendField, bool>> spec = null)
        {
            return _extendFieldRepository.Find(spec);
        }

        /// <summary> TienBV 071112
        /// Lấy ra danh sách các extend field của loại hồ sơ.
        /// </summary>
        /// <param name="doctypeId">The doctype guid id.</param>
        /// <returns></returns>
        public IEnumerable<ExtendField> Gets(Guid doctypeId)
        {
            var exfields = _extendFieldRepository.Find();
            var doctypeExfield = _doctypeExfieldRepository.Find(dt => dt.DoctypeId == doctypeId);
            if(exfields == null || doctypeExfield == null)
            {
                return new List<ExtendField>();
            }
            return (from e in exfields
                   join dx in doctypeExfield on e.ExtendFieldId equals dx.ExtendfieldId
                   select e).Distinct();
        }

        /// <summary> Tienbv 081112
        /// <para></para> Tạo trường mở rộng mới.
        /// <para></para> Được sử dụng khi tạo mới hoặc update form động.
        /// </summary>
        /// <param name="newItm">The extendfield obj.</param>
        public void Create(ExtendField newItm)
        {
            _extendFieldRepository.Create(newItm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public IEnumerable<ExtendField> Gets(IEnumerable<Guid> Ids)
        {
            var result = new List<ExtendField>();
            foreach (var id in Ids)
            {
                result.Add(_extendFieldRepository.One(id));
            }
            return result;
        }
    }
}
