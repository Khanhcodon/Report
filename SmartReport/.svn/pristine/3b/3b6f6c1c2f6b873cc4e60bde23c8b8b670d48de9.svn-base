using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Interface : IApproverDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 230113
    /// <para></para> Author      : TienBV
    /// <para></para> Description : DAL tương ứng với bảng Approver trong CSDL
    /// </summary>
    public class ApproverDal : DataAccessBase, IApproverDal
    {
        private readonly IRepository<Approver> _approverRepository;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="context"></param>
        public ApproverDal(IDbCustomerContext context) : base(context)
        {
            _approverRepository = context.GetRepository<Approver>();
        }

#pragma warning disable 1591
        public IEnumerable<Approver> Gets(Expression<Func<Approver, bool>> spec = null)
        {
            return _approverRepository.Find(spec);
        }

        public void Create(Approver entity)
        {
            _approverRepository.Create(entity);
        }

        public void Update(Approver entity)
        {
            _approverRepository.Update(entity);
        }

        public Approver Get(int id)
        {
            return _approverRepository.One(id);
        }

        public bool Exist(int docCopyId, Guid docId, int userId)
        {
            return _approverRepository.Any(a => a.DocumentCopyId == docCopyId && a.DocumentId == docId && a.UserSendId == userId);
        }

        public Approver Get(int docCopyId, int userId)
        {
            return _approverRepository.One(a => a.DocumentCopyId == docCopyId && a.UserSendId == userId);
        }
    }
}
