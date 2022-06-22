using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class DoctypeFormExfieldDal : DataAccessBase, IDoctypeFormExfieldDal
    {
        private readonly IRepository<DoctypeFormExtendfield> _doctypeExfieldRepository;

        /// <summary>
        /// Khởi tạo <see cref="DoctypeFormExfieldDal"/>
        /// </summary>
        /// <param name="context">Customer context.</param>
        public DoctypeFormExfieldDal(IDbCustomerContext context) : base(context)
        {
            _doctypeExfieldRepository = context.GetRepository<DoctypeFormExtendfield>();
        }

        /// <summary> Tienbv 081112
        /// Thêm quan hệ giữa doctype - form - exfield.
        /// </summary>
        /// <param name="exfieldIds">The exfield ids.</param>
        /// <param name="formid">The form id.</param>
        /// <param name="doctypeId">The doctype id.</param>
        public void Add(IEnumerable<Guid> exfieldIds, Guid formid, Guid doctypeId)
        {
            var existedExfieldIds = _doctypeExfieldRepository
                                    .Find(i => i.FormId == formid && i.DoctypeId == doctypeId)
                                    .Select(i => i.ExtendfieldId);
            var newExfieldIds = exfieldIds.Where(e => !existedExfieldIds.Contains(e)).Select(e => e);
            foreach (var exfieldId in newExfieldIds)
            {
                var newItm = new DoctypeFormExtendfield
                                 {
                                     DoctypeId = doctypeId,
                                     FormId = formid,
                                     ExtendfieldId = exfieldId
                                 };
                _doctypeExfieldRepository.Create(newItm);
            }
        }

        /// <summary> Tienbv 081112
        /// Lấy ra danh sách các record quan hệ giữa doctype - form - extendfield theo điều kiện truyền vào.
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        public IEnumerable<DoctypeFormExtendfield> Gets(Expression<Func<DoctypeFormExtendfield, bool>> spec)
        {
            return _doctypeExfieldRepository.Find(spec);
        }

        /// <summary> Tienbv 091112
        /// <para></para> Xóa các extend field trong form.
        /// <para></para> Sử dụng khi xóa một mẫu form chưa được sử dụng.
        /// </summary>
        /// <param name="formId">The form id.</param>
        public void DeleteExfields(Guid formId)
        {
            var exfields = _doctypeExfieldRepository.Find(e => e.FormId == formId);
            foreach(var exfield in exfields)
            {
                _doctypeExfieldRepository.Delete(exfield);
            }
        }
    }
}
