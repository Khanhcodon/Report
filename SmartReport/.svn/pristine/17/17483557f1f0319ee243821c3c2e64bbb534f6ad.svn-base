using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportGroupDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : DAL tương ứng với bảng Report - quản lý các báo cáo
    /// </summary>
    public class ReportGroupDal : DataAccessBase, IReportGroupDal
    {
        private readonly IRepository<ReportGroup> _reportGroupRepository;
        
        /// <summary>
        /// Khởi tạo class <see cref="ReportGroupDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
         public ReportGroupDal(IDbCustomerContext context)
            : base(context)
        {
            _reportGroupRepository = Context.GetRepository<ReportGroup>();
        }

        #pragma warning disable 1591

        public IEnumerable<Entities.Customer.ReportGroup> Gets()
        {
            return _reportGroupRepository.Find();
        }

        public IEnumerable<Entities.Customer.ReportGroup> Gets(List<int> ids)
        {
            return _reportGroupRepository.Find(r => ids.Contains(r.ReportGroupId));
        }

        public ReportGroup Get(int id)
        {
            return _reportGroupRepository.One(id);
        }

        public void Create(Entities.Customer.ReportGroup entity)
        {
            _reportGroupRepository.Create(entity);
        }

        public void Update(Entities.Customer.ReportGroup entity)
        {
            _reportGroupRepository.Update(entity);
        }

        public void Delete(Entities.Customer.ReportGroup entity)
        {
            _reportGroupRepository.Delete(entity);
        }
    }
}
