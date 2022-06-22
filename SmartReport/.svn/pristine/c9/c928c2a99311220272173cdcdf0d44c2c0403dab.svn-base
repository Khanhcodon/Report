using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : SignatureBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 150414</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : BLL tương ứng với bảng Signature trong CSDL</para>
    /// </summary>
    public class SignatureBll : ServiceBase
    {
        private readonly IRepository<Signature> _signatureRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public SignatureBll(IDbCustomerContext context)
            : base(context)
        {
            _signatureRepository = Context.GetRepository<Signature>();
        }

        /// <summary>
        /// Lấy ra danh sách các chữ ký của người dùng theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        public IEnumerable<Signature> Gets(Expression<Func<Signature, bool>> spec = null)
        {
            return _signatureRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra danh sách các loại văn bản, biểu mẫu  - mẫu phôi  theo điều kiện kỹ thuật truyền vào.
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="spec">The spec.</param>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Signature, T>> projector, Expression<Func<Signature, bool>> spec)
        {
            return _signatureRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Kiểm tra xem có phần tử nào phù hợp với điều kiện truyền vào hay không
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public bool Exist(Expression<Func<Signature, bool>> spec)
        {
            return _signatureRepository.Exist(spec);
        }

        /// <summary>
        /// Tạo mới chữ ký người dùng
        /// </summary>
        /// <param name="signature">Entity signature</param>
        public void Create(Signature signature)
        {
            if (signature == null)
            {
                throw new ArgumentNullException("signature");
            }
            _signatureRepository.Create(signature);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật chữ ký người dùng
        /// </summary>
        /// <param name="signature">Entity signature</param>
        public void Update(Signature signature)
        {
            if (signature == null)
            {
                throw new ArgumentNullException("signature");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa chữ ký người dùng
        /// </summary>
        /// <param name="signature">Entity signature</param>
        public void Delete(Signature signature)
        {
            if (signature == null)
            {
                throw new ArgumentNullException("signature");
            }
            _signatureRepository.Delete(signature);
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về Signature theo id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Signature Get(int id)
        {
            return _signatureRepository.Get(id);
        }
    }
}
