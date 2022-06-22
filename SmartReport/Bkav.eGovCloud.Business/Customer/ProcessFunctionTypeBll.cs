using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionTypeBll - public - Bll</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Bll tương ứng với bảng ProcessFunctionType trong CSDL</para>
    /// </summary>
    public class ProcessFunctionTypeBll : ServiceBase
    {
        private readonly IRepository<ProcessFunctionType> _processFunctionTypeRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Context</param>
        public ProcessFunctionTypeBll(IDbCustomerContext context)
            : base(context)
        {
            _processFunctionTypeRepository = Context.GetRepository<ProcessFunctionType>();
        }

        /// <summary>
        /// Lấy ra tất cả các loại funtion. Kết quả chỉ đọc
        /// </summary>
        /// <returns>Danh sách funtion</returns>
        public IEnumerable<ProcessFunctionType> Gets()
        {
            return _processFunctionTypeRepository.GetsReadOnly();
        }

        /// <summary>
        /// Lấy ra loại function theo id
        /// </summary>
        /// <param name="id">Id loại function</param>
        /// <returns>Loại function</returns>
        public ProcessFunctionType Get(int id)
        {
            ProcessFunctionType type = null;
            if (id > 0)
            {
                type = _processFunctionTypeRepository.Get(id);
            }
            return type;
        }

        /// <summary>
        /// Thêm mới loại function
        /// </summary>
        /// <param name="type">Entity loại function</param>
        public void Create(ProcessFunctionType type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            _processFunctionTypeRepository.Create(type);
            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới loại function
        /// </summary>
        /// <param name="types">Entity loại function</param>
        public void Create(IEnumerable<ProcessFunctionType> types)
        {
            if (types == null || !types.Any())
            {
                throw new ArgumentNullException("types");
            }

            foreach (var type in types)
            {
                _processFunctionTypeRepository.Create(type);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật loại function
        /// </summary>
        /// <param name="type">Entity loại function</param>
        public void Update(ProcessFunctionType type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa loại function
        /// </summary>
        /// <param name="type">Entity loại function</param>
        public void Delete(ProcessFunctionType type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            _processFunctionTypeRepository.Delete(type);
            Context.SaveChanges();
        }
    }
}
