using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WorkflowDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IWorkflowDal
    /// Create Date : 241012
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng Workflow trong CSDL
    /// </summary>
    public class WorkflowDal : DataAccessBase, IWorkflowDal
    {
         private readonly IRepository<Workflow> _workflowRepository;
        /// <summary>
         /// Khởi tạo class <see cref="WorkflowDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
         public WorkflowDal(IDbCustomerContext context)
            : base(context)
        {
            _workflowRepository = Context.GetRepository<Workflow>();
        }

        #pragma warning disable 1591

        public IEnumerable<Workflow> Gets(Expression<Func<Workflow, bool>> spec = null)
        {
            return _workflowRepository.Find(spec);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Workflow, TOutput>> projector, Expression<Func<Workflow, bool>> spec = null)
        {
            return _workflowRepository.FindAs(projector, spec);
        }

        public Workflow Get(int id)
        {
            return _workflowRepository.One(l => l.WorkflowId == id);
        }

        public void Create(Workflow workflow)
        {
            _workflowRepository.Create(workflow);
        }

        public void Update(Workflow workflow)
        {
            _workflowRepository.Update(workflow);
        }

        public void Delete(Workflow workflow)
        {
            _workflowRepository.Delete(workflow);
        }

        public bool Exist(Expression<Func<Workflow, bool>> spec)
        {
            return _workflowRepository.Any(spec);
        }
        public int Count(Expression<Func<Workflow, bool>> spec = null)
        {
            return _workflowRepository.Count(spec);
        }
    }
}
