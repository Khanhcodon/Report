using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class ExtendFieldBll : ServiceBase
    {
        private readonly IRepository<ExtendField> _exfieldRepository;

        /// <summary>
        /// Contructor <see cref="ExtendFieldBll"/>
        /// </summary>
        /// <param name="context">Context</param>
        public ExtendFieldBll(IDbCustomerContext context)
            : base(context)
        {
            _exfieldRepository = Context.GetRepository<ExtendField>();
        }

        /// <summary> TienBV 011112
        /// Lấy ra danh sách các extend field theo điều kiện truyền vào. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<ExtendField> Gets(Expression<Func<ExtendField, bool>> spec = null)
        {
            return _exfieldRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Tạo mới extend field
        /// </summary>
        /// <param name="extendField">Entity</param>
        public void Create(ExtendField extendField)
        {
            if (extendField == null)
            {
                throw new ArgumentNullException("extendField");
            }
            _exfieldRepository.Create(extendField);
            Context.SaveChanges();
        }
    }
}
