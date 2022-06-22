using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EmbryonicFormDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IEmbryonicFormDal
    /// Create Date : 030314
    /// Author      : HopCV
    /// Description : DAL tương ứng với bảng EmbryonicForm trong CSDL
    /// </summary>
    public class EmbryonicFormDal : DataAccessBase, IEmbryonicFormDal
    {
        private readonly IRepository<EmbryonicForm> _embryonicFormRepository;

        /// <summary>
        /// Khởi tạo class <see cref="AttachmentDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public EmbryonicFormDal(IDbCustomerContext context)
            : base(context)
        {
            _embryonicFormRepository = Context.GetRepository<EmbryonicForm>();
        }

        /// <summary>
        /// Trả về danh sách các đối tượng mẫu phôi.
        /// </summary>
        /// <param name="spec">Điề kiện</param>
        /// <returns>Danh sách các đối tượng mẫu phôi</returns>
        public IEnumerable<EmbryonicForm> Gets(Expression<Func<EmbryonicForm, bool>> spec = null)
        {
            return _embryonicFormRepository.Find(spec);
        }

        /// <summary> 
        /// <para>Trả về danh sách các đối tượng mẫu phôi. </para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các đối tượng mẫu phôi</returns>
        public IEnumerable<EmbryonicForm> Gets(Expression<Func<EmbryonicForm, bool>> spec = null, Func<IQueryable<EmbryonicForm>, IQueryable<EmbryonicForm>> preFilter = null, params Func<IQueryable<EmbryonicForm>, IQueryable<EmbryonicForm>>[] postFilters)
        {
            return _embryonicFormRepository.Find(spec, preFilter, postFilters);
        }

        /// <summary>
        /// Trả về danh sách các đối tượng người dùng định nghĩa
        /// </summary>
        /// <typeparam name="TOutput">Trả về 1 kiểu đối tượng nào đó do người dung định nghĩa trong câu truy vấn</typeparam>
        /// <param name="projector"></param>
        /// <param name="spec">Diều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các đối tượng người dùng định nghĩa</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<EmbryonicForm, TOutput>> projector, Expression<Func<EmbryonicForm, bool>> spec = null, Func<IQueryable<EmbryonicForm>, IQueryable<EmbryonicForm>> preFilter = null,
            params Func<IQueryable<EmbryonicForm>, IQueryable<EmbryonicForm>>[] postFilters)
        {
            return _embryonicFormRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        /// <summary>
        /// Trả về 1 mẫu phôi
        /// </summary>
        /// <param name="id"> Id của mẫu phôi</param>
        /// <returns>Một mẫu phôi</returns>
        public EmbryonicForm Get(int id)
        {
            return _embryonicFormRepository.One(id);
        }

        /// <summary>
        /// Tạo mới mẫu phôi
        /// </summary>
        /// <param name="embryonicForm">Đối tượng mẫu phôi</param>
        public void Create(EmbryonicForm embryonicForm)
        {
            _embryonicFormRepository.Create(embryonicForm);
        }

        /// <summary>
        /// Cập nhật thôn tin mẫu phôi
        /// </summary>
        /// <param name="embryonicForm">Đối tượng mẫu phôi</param>
        public void Update(EmbryonicForm embryonicForm)
        {
            _embryonicFormRepository.Update(embryonicForm);
        }

        /// <summary>
        /// Xóa mẫu phôi
        /// </summary>
        /// <param name="embryonicForm">Đối tượng mẫu phôi</param>
        public void Delete(EmbryonicForm embryonicForm)
        {
            _embryonicFormRepository.Delete(embryonicForm);
        }

        /// <summary>
        /// Trả về số phần tử của mẫu phôi
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Số phần tử</returns>
        public int Count(Expression<Func<EmbryonicForm, bool>> spec = null)
        {
            return Gets(spec).Count();
        }

        /// <summary>
        /// Trả về 1 datatble chưa thông tin của mẫu phôi
        /// </summary>
        /// <param name="embryonicForm">Mẫu phôi cần lấy ra</param>
        /// <param name="parameters">Tham số truyền vào để lấy giá trị thực thi trong câu truy vấn</param>
        /// <returns></returns>
        public DataTable GetDataByEmbryonicForm(EmbryonicForm embryonicForm, params object[] parameters)
        {
            DataTable result = Context.RawTable(embryonicForm.SqlQuery, parameters);
            return result;
        }
    }
}
