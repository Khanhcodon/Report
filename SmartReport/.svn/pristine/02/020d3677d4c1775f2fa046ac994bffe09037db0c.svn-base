using System.Web;
using System.Web.Mvc;
using System.Linq;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Utils
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGov Online Dept </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : DocumentHelper - public - Helper </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 17062014</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Lớp hỗ trợ xử lý document. </para>
    /// <para> ( TienBV@bkav.com - 17062014) </para>
    /// </summary>
    public static class DocumentHelper
    {
        /// <summary>
        /// Trả về template văn bản theo nghiệp vụ
        /// </summary>
        /// <param name="categoryBusiness"></param>
        /// <returns></returns>
        public static string GetDocumentTemplate(CategoryBusinessTypes categoryBusiness)
        {
            var _interfaceCfgService = DependencyResolver.Current.GetService<InterfaceConfigBll>();
            var cfgTemp = _interfaceCfgService.GetCacheAllInterfaceConfigs()
                                    .FirstOrDefault(p => p.CategoryBusinessId == (int)categoryBusiness);

            var result = cfgTemp == null ? string.Empty : cfgTemp.Template;

            return result;
        }

        /// <summary>
        /// Trả về template của văn bản hiện tại.
        /// </summary>
        /// <param name="document">Document</param>
        /// <param name="documentCopy">Văn bản đang mở</param>
        /// <param name="workflow">Quy trình hiện tại</param>
        /// <param name="userId">Người dùng đang mở văn bản</param>
        /// <returns></returns>
        public static string GetDocumentTemplate(Document document, DocumentCopy documentCopy, Workflow workflow, int userId)
        {
            var _docTimeLine = DependencyResolver.Current.GetService<DocTimelineBll>();
            var _interfaceCfgService = DependencyResolver.Current.GetService<InterfaceConfigBll>();

            var result = string.Empty;
            if (workflow == null || !workflow.InterfaceConfigId.HasValue)
            {
                var cfgTemp = _interfaceCfgService.GetCacheAllInterfaceConfigs()
                                    .FirstOrDefault(p => p.CategoryBusinessId == document.CategoryBusinessId);

                result = cfgTemp == null ? string.Empty : cfgTemp.Template;
            }
            else
            {
                result = _interfaceCfgService.GetTemplateFromCache(workflow.InterfaceConfigId.Value);
            }

            return result;

            //// Trường hợp văn bản hiện tại không thuộc node nào (văn bản xin ý kiến, thông báo)
            //// - Nếu có cấu hình giao diện cho quy trình thì lấy giao diện của quy trình.
            //// - Nếu không thì lấy giao diện của nghiệp vụ.
            ////|| (docFinish != null && docFinish.DocFinishTypeInEnum == DocFinishType.KhongThamGiaXuLy)
            ////        || (docFinish != null && docFinish.DocFinishTypeInEnum == DocFinishType.GiamSat))
            //if (!documentCopy.NodeCurrentId.HasValue)
            //{
            //    if (workflow == null || !workflow.InterfaceConfigId.HasValue)
            //    {
            //        var cfgTemp = _interfaceCfgService.GetCacheAllInterfaceConfigs()
            //                            .FirstOrDefault(p => p.CategoryBusinessId == document.CategoryBusinessId);

            //        result = cfgTemp == null ? string.Empty : cfgTemp.Template;
            //    }
            //    else
            //    {
            //        result = _interfaceCfgService.GetTemplateFromCache(workflow.InterfaceConfigId.Value);
            //    }
            //}
            //else
            //{
            //    int currentNodeId;

            //    // Trường hợp nếu người đang mở văn bản là người đang giữ thì set lấy giao diện từ node hiện tại
            //    if (userId == documentCopy.UserCurrentId)
            //    {
            //        currentNodeId = documentCopy.NodeCurrentId.Value;
            //    }
            //    else
            //    {
            //        // Không thì phải lấy node gần nhất mà người đang mở đã từng xử lý.
            //        // Todo: để tạm, do phần xử lý doctimeline đang lỗi => sau khi sửa lại cập nhật DocTimeLine chính xác thì thêm vào đây
            //        // Và bỏ đi điều kiện ở trên ( userId != documentCopy.UserCurrentId)
            //        var timeLine = _docTimeLine.GetLastProcessed(documentCopy.DocumentCopyId, userId);
            //        currentNodeId = (timeLine == null || !timeLine.NodeId.HasValue) ? 0 : timeLine.NodeId.Value;
            //    }

            //    var path = workflow.JsonInObject;
            //    var currentNode = path.GetNode(currentNodeId);

            //    if (currentNode == null || !currentNode.TemplateId.HasValue)
            //    {
            //        if (!workflow.InterfaceConfigId.HasValue)
            //        {
            //            var cfgTemp = _interfaceCfgService.GetCacheAllInterfaceConfigs()
            //                .FirstOrDefault(p => p.CategoryBusinessId == document.CategoryBusinessId);

            //            result = cfgTemp == null ? string.Empty : cfgTemp.Template;
            //        }
            //        else
            //        {
            //            result = _interfaceCfgService.GetTemplateFromCache(workflow.InterfaceConfigId.Value);
            //        }
            //    }
            //    else
            //    {
            //        result = _interfaceCfgService.GetTemplateFromCache(currentNode.TemplateId.Value);
            //    }
            //}

            //return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="categoryBusinessId"></param>
        /// <param name="currentNodeId"></param>
        /// <returns></returns>
        public static string GetDocumentTemplate(Workflow workflow, int categoryBusinessId, int? currentNodeId)
        {
            var _interfaceCfgService = DependencyResolver.Current.GetService<InterfaceConfigBll>();

            var result = string.Empty;
            if (workflow == null || !workflow.InterfaceConfigId.HasValue)
            {
                var cfgTemp = _interfaceCfgService.GetCacheAllInterfaceConfigs()
                                    .FirstOrDefault(p => p.CategoryBusinessId == categoryBusinessId);

                result = cfgTemp == null ? string.Empty : cfgTemp.Template;
            }
            else
            {
                result = _interfaceCfgService.GetTemplateFromCache(workflow.InterfaceConfigId.Value);
            }

            return result;

            // TienBV: tạm bỏ lấy theo quy trình
            // Trường hợp văn bản hiện tại không thuộc node nào (văn bản xin ý kiến, thông báo)
            // - Nếu có cấu hình giao diện cho quy trình thì lấy giao diện của quy trình.
            // - Nếu không thì lấy giao diện của nghiệp vụ.
            //if (!currentNodeId.HasValue || workflow == null)
            //{
            //    if (workflow == null || !workflow.InterfaceConfigId.HasValue)
            //    {
            //        var cfgTemp = _interfaceCfgService.GetCacheAllInterfaceConfigs()
            //                         .FirstOrDefault(p => p.CategoryBusinessId == categoryBusinessId);

            //        result = cfgTemp == null ? string.Empty : cfgTemp.Template;
            //    }
            //    else
            //    {
            //        result = _interfaceCfgService.GetTemplateFromCache(workflow.InterfaceConfigId.Value);
            //    }

            //    return result;
            //}

            //if (!currentNodeId.HasValue)
            //{
            //    return result;
            //}

            //var path = workflow.JsonInObject;
            //var currentNode = path.GetNode((int)currentNodeId);
            //if (currentNode == null)
            //{
            //    return result;
            //}

            //if (!currentNode.TemplateId.HasValue)
            //{
            //    if (!workflow.InterfaceConfigId.HasValue)
            //    {
            //        var cfgTemp = _interfaceCfgService.GetCacheAllInterfaceConfigs()
            //             .FirstOrDefault(p => p.CategoryBusinessId == categoryBusinessId);

            //        result = cfgTemp == null ? string.Empty : cfgTemp.Template;
            //    }
            //    else
            //    {
            //        result = _interfaceCfgService.GetTemplateFromCache(workflow.InterfaceConfigId.Value);
            //    }
            //}
            //else
            //{
            //    result = _interfaceCfgService.GetTemplateFromCache(currentNode.TemplateId.Value);
            //}

            //return result;
        }
    }
}
