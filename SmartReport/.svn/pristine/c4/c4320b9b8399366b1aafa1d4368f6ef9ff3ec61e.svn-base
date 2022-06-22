using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionGroupBll - public - Bll</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 02/01/2015</para>
    /// <para>Author      : TienBV</para>
    /// <para>Description : Bll tương ứng với bảng ProcessFunctionGroup trong CSDL</para>
    /// </summary>
    public class ProcessFunctionGroupBll : ServiceBase
    {
        private readonly IRepository<ProcessFunctionGroup> _processFunctionGroupRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Context</param>
        public ProcessFunctionGroupBll(IDbCustomerContext context)
            : base(context)
        {
            _processFunctionGroupRepository = Context.GetRepository<ProcessFunctionGroup>();
        }

        /// <summary>
        /// Lấy ra tất cả các loại kho. Kết quả chỉ đọc
        /// </summary>
        /// <returns>Danh sách kho</returns>
        public IEnumerable<ProcessFunctionGroup> Gets()
        {
            return _processFunctionGroupRepository.GetsReadOnly();
        }

        /// <summary>
        /// Lấy ra loại kho theo id
        /// </summary>
        /// <param name="id">Id loại kho</param>
        /// <returns>Loại kho</returns>
        public ProcessFunctionGroup Get(int id)
        {
            ProcessFunctionGroup type = null;
            if (id > 0)
            {
                type = _processFunctionGroupRepository.Get(id);
            }
            return type;
        }

        /// <summary>
        /// Thêm mới loại kho
        /// </summary>
        /// <param name="type">Entity loại kho</param>
        public void Create(ProcessFunctionGroup type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            _processFunctionGroupRepository.Create(type);
            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới loại kho
        /// </summary>
        /// <param name="types">Entity loại kho</param>
        public void Create(IEnumerable<ProcessFunctionGroup> types)
        {
            if (types == null || !types.Any())
            {
                throw new ArgumentNullException("types");
            }

            foreach (var type in types)
            {
                _processFunctionGroupRepository.Create(type);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật loại kho
        /// </summary>
        /// <param name="type">Entity loại kho</param>
        public void Update(ProcessFunctionGroup type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa loại kho
        /// </summary>
        /// <param name="type">Entity loại kho</param>
        public void Delete(ProcessFunctionGroup type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            //Todo: xử lý các Node đang sử dụng kho tương ứng

            _processFunctionGroupRepository.Delete(type);
            Context.SaveChanges();
        }
    }
}
