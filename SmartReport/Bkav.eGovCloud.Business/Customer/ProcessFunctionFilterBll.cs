using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionFilterBll - public - Bll</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 05/01/2015</para>
    /// <para>Author      : TienBV</para>
    /// <para>Description : Bll tương ứng với bảng ProcessFunctionFilter trong CSDL</para>
    /// </summary>
    public class ProcessFunctionFilterBll : ServiceBase
    {
        private readonly IRepository<ProcessFunctionFilter> _processFunctionFilterRepository;
        private readonly IRepository<ProcessFunctionAndFilter> _processFunctionAndFilterRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Context</param>
        public ProcessFunctionFilterBll(IDbCustomerContext context)
            : base(context)
        {
            _processFunctionFilterRepository = Context.GetRepository<ProcessFunctionFilter>();
            _processFunctionAndFilterRepository = Context.GetRepository<ProcessFunctionAndFilter>();
        }

        /// <summary>
        /// Lấy ra tất cả các bộ lọc của node. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<ProcessFunctionFilter> Gets(Expression<Func<ProcessFunctionFilter, bool>> spec = null)
        {
            return _processFunctionFilterRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy danh sách bộ lọc theio điều kiên
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="projector"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<ProcessFunctionFilter, T>> projector, Expression<Func<ProcessFunctionFilter, bool>> spec = null)
        {
            return _processFunctionFilterRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Lấy ra loại bộ lọc của node theo id
        /// </summary>
        /// <param name="id">Id loại function</param>
        /// <returns>Loại function</returns>
        public ProcessFunctionFilter Get(int id)
        {
            ProcessFunctionFilter type = null;
            if (id > 0)
            {
                type = _processFunctionFilterRepository.Get(id);
            }
            return type;
        }

        /// <summary>
        /// Trả về danh sách filter của process function
        /// </summary>
        /// <param name="id">Id Process function</param>
        /// <returns></returns>
        public IEnumerable<ProcessFunctionFilter> GetFilterNotInFunction(int id)
        {
            if (id <= 0)
            {
                return new List<ProcessFunctionFilter>();
            }

            var filterIds = _processFunctionAndFilterRepository.Gets(true, f => f.ProcessFunctionId == id);
            return _processFunctionFilterRepository.Gets(true, f => !filterIds.Any(fi => fi.ProcessFunctionFilterId == f.ProcessFunctionFilterId));
        }

        /// <summary>
        /// Thêm mới bộ lọc của node
        /// </summary>
        /// <param name="type">bộ lọc của node</param>
        public void Create(ProcessFunctionFilter type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            _processFunctionFilterRepository.Create(type);
            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới bộ lọc của node
        /// </summary>
        /// <param name="types">bộ lọc của node</param>
        public void Create(IEnumerable<ProcessFunctionFilter> types)
        {
            if (types == null || !types.Any())
            {
                throw new ArgumentNullException("types");
            }

            foreach (var type in types)
            {
                _processFunctionFilterRepository.Create(type);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật bộ lọc của node
        /// </summary>
        /// <param name="type">bộ lọc của node</param>
        public void Update(ProcessFunctionFilter type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa bộ lọc của node
        /// </summary>
        /// <para>Entity bộ lọc của node</para>
        public void Delete(ProcessFunctionFilter type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            _processFunctionFilterRepository.Delete(type);
            Context.SaveChanges();
        }
    }
}
