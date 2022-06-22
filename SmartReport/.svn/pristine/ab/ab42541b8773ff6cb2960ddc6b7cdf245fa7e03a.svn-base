using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// </summary>
    public class ReportQueryFilterBll : ServiceBase
    {
        private readonly IRepository<ReportQueryFilter> _reportQueryFilterRepository;
        private readonly AdminGeneralSettings _generalSettings;

        /// <summary>
        /// Khởi tạo class <see cref="DepartmentBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="userService">Bll tương ứng với bảng User trong CSDL</param>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="generalSettings"></param>
        //TODO: Anh GiangPN - Tham số đầu vào IPositionDal positionDal nên thay thành PositionBll và IJobTitlesDal jobTitlesDal nên thay thành JobTitleBll
        public ReportQueryFilterBll(IDbCustomerContext context,
                    AdminGeneralSettings generalSettings)
            : base(context)
        {
            _reportQueryFilterRepository = Context.GetRepository<ReportQueryFilter>();
            _generalSettings = generalSettings;
        }
        public ReportQueryFilter Get(int reportQueryFilterId)
        {
            ReportQueryFilter reportQuery = null;
            if (reportQueryFilterId > 0)
            {
                reportQuery = _reportQueryFilterRepository.Get(reportQueryFilterId);
            }

            return reportQuery;
        }
        #region ReportQueryFilter
        public void Update(List<ReportQueryFilter> filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException("ReportQuery");
            }
            Context.SaveChanges();
        }
        public void Update(ReportQueryFilter filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException("ReportQueryFilter");
            }
            Context.SaveChanges();
        }
        #endregion ReportQueryFilter
    }
}
