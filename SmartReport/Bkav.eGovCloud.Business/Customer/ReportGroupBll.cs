using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportBll - public - Bll
    /// Access Modifiers: 
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Cung cấp các API xử lý báo báo
    /// </summary>
    public class ReportGroupBll : ServiceBase
    {
        private readonly IRepository<ReportGroup> _reportGroupRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ReportGroupBll(IDbCustomerContext context)
            : base(context)
        {
            _reportGroupRepository = Context.GetRepository<ReportGroup>();
        }

        /// <summary>
        /// Trả về nhóm báo cáo tương ứng theo id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Nhóm báo cáo tương ứng</returns>
        public ReportGroup GetGroup(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return _reportGroupRepository.Get(id);
        }

        /// <summary>
        /// Trả về danh sách tất cả các nhóm báo cáo hiện có. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec"></param>
        /// <returns>Danh sách tất cả nhóm báo cáo</returns>
        public IEnumerable<ReportGroup> GetGroups(Expression<Func<ReportGroup, bool>> spec = null)
        {
            return _reportGroupRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Trả về danh sách các nhóm báo cáo theo danh sách id truyền vào
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns>Danh sách nhóm báo cáo tương ứng</returns>
        public IEnumerable<ReportGroup> GetGroups(List<int> ids)
        {
            return _reportGroupRepository.GetsReadOnly(r => ids.Contains(r.ReportGroupId));
        }

        /// <summary>
        /// Tạo mới nhóm báo cáo
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <exception cref="ArgumentNullException">Dữ liệu null</exception>
        public void CreateGroup(ReportGroup entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _reportGroupRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin nhóm báo cáo
        /// </summary>
        /// <param name="entity">entity</param>
        /// <exception cref="ArgumentNullException">Dữ liệu null</exception>
        public void UpdateGroup(ReportGroup entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa nhóm báo cáo
        /// </summary>
        /// <param name="entity">entity</param>
        public void DeleteGroup(ReportGroup entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa nhóm báo cáo theo id
        /// </summary>
        /// <param name="id">id</param>
        public void DeleteGroup(int id)
        {
            var reportGroup = GetGroup(id);
            if (reportGroup != null)
            {
                _reportGroupRepository.Delete(reportGroup);
                Context.SaveChanges();
            }
        }
    }
}
