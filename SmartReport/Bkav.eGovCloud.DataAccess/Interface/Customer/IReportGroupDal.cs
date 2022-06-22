using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IReportGroupDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : DAL tương ứng với bảng ReportGroup - quản lý các nhóm báo cáo
    /// </summary>
    public interface IReportGroupDal
    {
        /// <summary>
        /// Trả về danh sách tất cả các nhóm báo cáo hiện có
        /// </summary>
        /// <returns></returns>
        IEnumerable<ReportGroup> Gets();

        /// <summary>
        /// Trả về dánh sách các nhóm báo cáo theo danh sách id
        /// </summary>
        /// <param name="ids">Danh sách nhóm báo cáo id</param>
        /// <returns>Danh sách nhóm báo cáo tương ứng</returns>
        IEnumerable<ReportGroup> Gets(List<int> ids);

        /// <summary>
        /// Trả về nhóm báo cáo theo id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Nhóm báo cáo tương ứng</returns>
        ReportGroup Get(int id);

        /// <summary>
        /// Tạo mới nhóm báo cáo
        /// </summary>
        /// <param name="entity">Entity</param>
        void Create(ReportGroup entity);

        /// <summary>
        /// Cập nhật nhóm báo cáo
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(ReportGroup entity);

        /// <summary>
        /// Xóa nhóm báo cáo
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(ReportGroup entity);

    }
}
