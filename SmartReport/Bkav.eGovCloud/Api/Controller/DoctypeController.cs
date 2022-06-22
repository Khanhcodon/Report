using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Doctype)]
    public class DoctypeController : EgovApiBaseController
    {
        private readonly DocTypeBll _doctypeService;
        private readonly WorkflowHelper _workflowHelper;
        private readonly WorkflowBll _workflowService;

        public DoctypeController()
        {
            _doctypeService = DependencyResolver.Current.GetService<DocTypeBll>();
            _workflowHelper = DependencyResolver.Current.GetService<WorkflowHelper>();
            _workflowService = DependencyResolver.Current.GetService<WorkflowBll>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public DocType Get(Guid id)
        {
            return _doctypeService.Get(id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<DocType> GetsAll()
        {
            var doctypes = _doctypeService.GetsWithoutLazyLoading(x => x.CategoryBusinessId == 4, true);
            foreach (var dt in doctypes)
            {
                dt.Code = null;
                dt.DocTypeStores = new List<DocTypeStore>();
                foreach (var paper in dt.DoctypePapers)
                {
                    paper.IsRequired = paper.IsRequired ?? false;
                }
            }
            return doctypes;
        }

        /// <summary>
        /// Ham lay tat ca doctype
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<DocType> GetsAllDoctypes()
        {
            var doctypes = _doctypeService.GetsWithoutLazyLoading(null, true);
            foreach (var dt in doctypes)
            {
                dt.Code = null;
                dt.DocTypeStores = new List<DocTypeStore>();
                foreach (var paper in dt.DoctypePapers)
                {
                    paper.IsRequired = paper.IsRequired ?? false;
                }
            }
            return doctypes;
        }

        /// <summary>
        /// Trả về danh sách các loại hồ sơ cho phép liên thông
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<dynamic> GetSyncDocTypes()
        {
            var syncPermission = (int)DocTypePermissions.DuocPhepLienThong;
            var docTypes = _doctypeService.Gets(d => d.DocTypePermission.HasValue && (d.DocTypePermission.Value & syncPermission) == syncPermission).Select(u => new
            {
                DocTypeId = u.DocTypeId,
                DocTypeName = u.DocTypeName
            });
            return docTypes;
        }

        //[System.Web.Http.HttpGet]
        //public object GetActionsCreate(Guid docTypeId, int userId)
        //{
        //    if (docTypeId.Equals(new Guid()))
        //    {
        //        return new { error = "Yêu cầu không hợp lệ. Vui lòng thử lại.";
        //    }

        //    // Danh sách các hướng chuyển
        //    var result = new List<Action>();

        //    var userSendId = userId;

        //    // docType
        //    var doctype = _doctypeService.Get(docTypeId);
        //    if (doctype == null)
        //    {
        //        return new { error = "Không tìm thấy loại văn bản." };
        //    }
        //    var workflow = _workflowService.GetIsActived(docTypeId);

        //    // Hướng chuyển theo quy trình
        //    var actionsInWorkflow = _workflowHelper.GetActionsCreate(workflow, userSendId).ToList();
        //    // Loại bỏ các hướng chuyển đặc biệt trong quy trình: Lưu sổ phát hành, Lưu sổ phát hành nội bộ...
        //    RemoveActionsSpecial(ref actionsInWorkflow);
        //    result.AddRange(actionsInWorkflow);

        //    // Xử lý ẩn hiện các hướng chuyển nếu văn bản đang ở trạng thái Dừng xử lý
        //    RecheckActionsForCreate(workflow, ref result);

        //    var actions = GetActions(result, workflow.WorkflowId);
        //    return actions;
        //}
    }
}