using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : WorkflowQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 241012
    /// Author      : GiangPN
    /// Description : Các điều kiện truy vấn cho bảng Workflow
    /// </summary>
    public static class WorkflowQuery
    {
        /// <summary>
        /// WorkflowId == workflowId
        /// </summary>
        /// <param name="workflowId">Id của chức vụ.</param>
        /// <returns></returns>
        public static Expression<Func<Workflow, bool>> WithId(int workflowId)
        {
            return s => s.WorkflowId == workflowId;
        }

        /// <summary>
        /// WorkflowName == workflowName
        /// </summary>
        /// <param name="workflowName">Tên chức vụ.</param>
        /// <returns></returns>
        public static Expression<Func<Workflow, bool>> WithWorkflowName(string workflowName)
        {
            return s => s.WorkflowName == workflowName;
        }

        /// <summary>
        /// IsActivated == isActivated
        /// </summary>
        /// <param name="isActivated">Trạng thái kích hoạt</param>
        /// <returns></returns>
        public static Expression<Func<Workflow, bool>> WithIsActivated(bool? isActivated = null)
        {
            return s => !isActivated.HasValue || s.IsActivated == isActivated;
        }
    }
}
