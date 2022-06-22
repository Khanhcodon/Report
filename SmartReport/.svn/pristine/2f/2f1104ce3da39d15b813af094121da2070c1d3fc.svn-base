using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : TemplateBll - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 190313</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm xử lý template </para>
    /// <para> ( TienBV@bkav.com - 190313) </para>
    /// </summary>
    /// <remarks>
    /// (CuongNT@bkav.com - 110613)
    /// Mẫu in, email, sms có 3 loại MẪU CHUNG: Mẫu chung cho tất cả, Mẫu chung cho 1 lĩnh vực nào đó, mẫu chung cho 1 loại hồ sơ. 
    /// Mỗi mẫu chung lại có các MẪU RIÊNG: 
    /// - Mẫu chung cho tất cả: Mẫu riêng cho lĩnh vực, Mẫu riêng cho loại. 
    /// - Mẫu chung cho 1 lĩnh vực nào đó: Mẫu riêng cho loại. 
    /// - Mẫu chung cho 1 loại hồ sơ: Không có mẫu riêng. 
    /// </remarks>
    public class TemplateBll : ServiceBase
    {
        // TODO: (CuongNT-110613-TienBV-?) BLL này là xử lý chung cho việc lấy mẫu theo phiếu in, email, sms.... xong các hàm bên dưới chưa thấy hỗ trợ cho việc này. Hoặc đang fix cứng Type, hoặc không xử lý Type dẫn tới nếu cấu hình đủ template các loại ra hệ thống sẽ bị sai.

        #region Fields

        private readonly IRepository<Template> _tempateRepository;
        private readonly AdminGeneralSettings _generalSettings;

        #endregion

        #region Contructor

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Setting</param>
        public TemplateBll(IDbCustomerContext context, AdminGeneralSettings generalSettings)
            : base(context)
        {
            _tempateRepository = Context.GetRepository<Template>();
            _generalSettings = generalSettings;
        }

        #endregion

        #region Method For Admin

        /// <summary>
        /// Thêm mẫu.
        /// <para> (Tienbv@bkav.com 190313)</para>
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Template entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _tempateRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa mẫu.
        /// (Tienbv@bkav.com 190313)
        /// </summary>
        /// <param name="entity">Template entity</param>
        public void Delete(Template entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _tempateRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về tất cả template cha có phân trang và sort.
        /// <para>(Tienbv@bkav.com 190313)</para>
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="projector"></param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trong trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">true: lớn -> nhỏ, false: ngược lại</param>
        /// <param name="keySearch">search theo tên</param>
        /// <param name="type"></param>
        /// <returns>Danh sách các template tương ứng</returns>
        public IEnumerable<T> GetParents<T>(out int totalRecords,
            Expression<Func<Template, T>> projector,
            int currentPage = 1,
            int? pageSize = null,
            string sortBy = "",
            bool isDescending = false,
            string keySearch = "",
            int? type = null)
        {
            var spec = !string.IsNullOrWhiteSpace(keySearch)
                        ? TemplateQuery.IsParentAndContainsKey(keySearch)
                        : TemplateQuery.IsParent();
            if (type.HasValue)
            {
                spec = spec.And(p => p.Type == type);
            }
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _tempateRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Template>(isDescending, sortBy);
            return _tempateRepository.GetsAs(projector, spec, sort, Context.Filters.Page<Template>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Cập nhật mẫu.
        /// (Tienbv@bkav.com 190313)
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Template entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về template theo id.
        /// <para> (Tienbv@bkav.com 190313) </para>
        /// </summary>
        /// <param name="id">Template id</param>
        /// <returns></returns>
        public Template Get(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("id không hợp lệ");
            }

            return _tempateRepository.Get(id);
        }

        /// <summary>
        /// Trả về dánh sách các loại hồ sơ đã được cấu hình mẫu phiếu riêng.
        /// </summary>
        /// <param name="parentTemplateId">parent id</param>
        /// <returns></returns>
        public List<string> GetExistDoctypeId(int parentTemplateId)
        {
            var childs = GetChildren(parentTemplateId, c => new { c.ParentId, c.DoctypeId });
            var result = childs.Where(c => c.ParentId != null && c.DoctypeId != null).Select(c => c.DoctypeId != null ? ((Guid)c.DoctypeId).ToString("N") : string.Empty).ToList();
            return result;
        }

        /// <summary>
        /// Trả về danh sách các mẫu con của mẫu cha
        /// </summary>
        /// <param name="id">Template id</param>
        /// <param name="projector"></param>
        /// <returns>Danh sách các mẫu hợp lệ</returns>
        public IEnumerable<T> GetChildren<T>(int id, Expression<Func<Template, T>> projector)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Đầu vào không hợp lệ");
            }
            return _tempateRepository.GetsAs(projector, t => t.IsActive && t.ParentId == id);
        }

        #endregion

        /// <summary>
        /// Trả về mẫu phiếu khả dụng để in.
        /// </summary>
        /// <remarks>
        /// Quy tắc lấy mẫu in:
        ///     - Trước tiên kiểm tra xem có mẫu phiếu in riêng nào được cấu hình cho loại hồ sơ tương ứng ko.
        ///     - Nếu không có mẫu phiếu in cho loại hồ sơ thì kiểm tra mẫu phiếu in được cấu hình cho lĩnh vực tương ứng.
        ///     - Nếu không có mẫu phiếu in cho lĩnh vực thì lấy mẫu phiếu in chung hiện tại.
        /// </remarks>
        /// <param name="templateId">Id mẫu phiếu in cha được chọn trên danh sách.</param>
        /// <param name="docTypeId">Loại hồ sơ của hồ sơ được in.</param>
        /// <param name="docFieldIds">Lĩnh vực tương ứng.</param>
        /// <returns></returns>
        public Template GetTemplateForPrint(int templateId, Guid docTypeId, List<int> docFieldIds)
        {
            var result = GetChildForDocType(templateId, docTypeId, TemplateType.PhieuIn);

            if (result == null && docFieldIds != null && docFieldIds.Any())
            {
                result = GetChildForDocField(templateId, docFieldIds, TemplateType.PhieuIn);
            }

            return result ?? _tempateRepository.Get(templateId);
        }

        /// <summary>
        /// Trả về danh sách các mẫu sms của hệ thống
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Template> GetAllForSms()
        {
            return _tempateRepository.Gets(true, t => (t.Type == (int)TemplateType.Sms) && t.IsActive);
        }

        /// <summary>
        /// Trả về danh sách các mẫu email của hệ thống
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Template> GetAllForMail()
        {
            return _tempateRepository.Gets(true, t => (t.Type == (int)TemplateType.Email) && t.IsActive);
        }

        /// <summary>
        /// Trả về danh sách các mẫu phiếu in có thể sử dụng cho một danh sách hồ sơ.
        /// </summary>
        /// <param name="docCopys">Danh sách hồ sơ cần in.</param>
        /// <param name="userId">Người sử dụng hiện tại.</param>
        /// <returns>Danh sách các mẫu phiếu in hợp lệ.</returns>
        public IEnumerable<Template> GetAvaiablePrints(List<DocumentCopy> docCopys, int userId)
        {
            if (!docCopys.Any())
            {
                return new List<Template>();
            }
            if (docCopys.Count == 1)
            {
                return GetAvaiablePrints(docCopys.First(), userId);
            }

            var permissions = new List<DocumentProcessType>();

            // Mặc định luôn có biểu mẫu tiếp nhận
            permissions.Add(DocumentProcessType.TiepNhan);

            // Kiểm tra mẫu Biên nhận bàn giao, trả kết quả, bổ sung
            bool hasTransferd, hasResult, hasSupplemented;
            hasTransferd = hasResult = hasSupplemented = true;
            foreach (var docCopy in docCopys)
            {
                hasTransferd &= HasTransfered(docCopy, userId);
                hasResult &= docCopy.Document.IsReturned == true;
                hasSupplemented &= docCopy.Document.IsSupplemented == true;
            }
            if (hasTransferd)
            {
                permissions.Add(DocumentProcessType.BanGiao);
            }
            if (hasResult)
            {
                permissions.Add(DocumentProcessType.TraKetQua);
            }
            if (hasSupplemented)
            {
                permissions.Add(DocumentProcessType.TiepNhanBoSung);
            }

            // CuongNT: Lấy thêm các MẪU CHUNG cấu hình cho Lĩnh vực nếu tất cả docCopys thuộc cùng một lĩnh vực. Ngược lại thì chỉ lấy các MẪU CHUNG cho tất cả lĩnh vực, loại.
            var docfields = docCopys.Select(dc => dc.Document.ListDocFieldId).Distinct().ToList();
            var docfieldId = docfields.Count() == 1 ? docfields.First() : new List<int>();

            // CuongNT: Lấy thêm các MẪU CHUNG cấu hình cho Loại hồ sơ nếu tất cả docCopys thuộc cùng một loại. Ngược lại thì chỉ lấy các MẪU CHUNG cho tất cả lĩnh vực, loại.
            //var doctypes = docCopys.Select(dc => dc.Document.DocTypeId).Distinct().ToList();
            //var doctypeId = doctypes.Count() == 1 ? doctypes.First() : Guid.NewGuid();

            return GetAvaiablePrints(permissions, docfieldId);
        }

        /// <summary>
        /// Trả về danh sách các mẫu phiếu in cho thể sử dụng cho hồ sơ hiện tại.
        /// </summary>
        /// <remarks>
        /// Nguyên tắc là những người nào tham gia xử lý hồ sơ này đều có thể in được tất những những mẫu in hiện có cho nó.
        /// </remarks>
        /// <param name="docCopy">Hồ sơ cần in.</param>
        /// <param name="userId">Người sử dụng hiện tại.</param>
        /// <returns>Danh sách các mẫu phiếu in hợp lệ.</returns>
        public IEnumerable<Template> GetAvaiablePrints(DocumentCopy docCopy, int userId)
        {
            if (docCopy == null)
            {
                throw new Exception("Hồ sơ không tồn tại trên hệ thống");
            }

            var document = docCopy.Document;
            var permission = new List<DocumentProcessType> { DocumentProcessType.TiepNhan };

            // Nếu cán bộ hiện tại đã từng bàn giao thì hiện mẫu Biên nhận Bàn giao
            if (HasTransfered(docCopy, userId))
            {
                permission.Add(DocumentProcessType.BanGiao);
            }

            // Nếu đã trả kết quả thì trả về mẫu Biên nhận trả kết quả
            if (document.IsReturned == true)
            {
                permission.Add(DocumentProcessType.TraKetQua);
            }

            // Nếu là đã tiếp nhận bổ sung thì trả về mẫu Biên nhận bổ sung
            if (document.IsSupplemented == true)
            {
                permission.Add(DocumentProcessType.TiepNhanBoSung);
            }

            var docfieldIds = document.ListDocFieldId;

            return GetAvaiablePrints(permission, docfieldIds);
        }

        /// <summary>
        /// HopCV:081015
        /// Trả về danh sách các mẫu phiếu có thể sử dụng cho một danh sách hồ sơ.
        /// </summary>
        /// <param name="docCopys">Danh sách hồ sơ cần lấy mẫu phiếu.</param>
        /// <param name="userId">Người sử dụng hiện tại.</param>
        /// <param name="tempType">Loại mẫu phiếu</param>
        /// <returns>Danh sách các mẫu phiếu hợp lệ.</returns>
        public IEnumerable<Template> GetAvaiableTemps(List<DocumentCopy> docCopys, int userId, TemplateType tempType)
        {
            if (!docCopys.Any())
            {
                return new List<Template>();
            }
            if (docCopys.Count == 1)
            {
                return GetAvaiableTemps(docCopys.First(), userId, tempType);
            }

            var permissions = new List<DocumentProcessType>();

            // Mặc định luôn có biểu mẫu tiếp nhận
            permissions.Add(DocumentProcessType.TiepNhan);

            // Kiểm tra mẫu Biên nhận bàn giao, trả kết quả, bổ sung
            bool hasTransferd, hasResult, hasSupplemented;
            hasTransferd = hasResult = hasSupplemented = true;
            foreach (var docCopy in docCopys)
            {
                hasTransferd &= HasTransfered(docCopy, userId);
                hasResult &= docCopy.Document.IsReturned == true;
                hasSupplemented &= docCopy.Document.IsSupplemented == true;
            }
            if (hasTransferd)
            {
                permissions.Add(DocumentProcessType.BanGiao);
            }
            if (hasResult)
            {
                permissions.Add(DocumentProcessType.TraKetQua);
            }
            if (hasSupplemented)
            {
                permissions.Add(DocumentProcessType.TiepNhanBoSung);
            }

            int type = (int)tempType;
            //Đầu tiên kiểm tra lấy theo Loại văn ban trước
            var doctypeIds = docCopys.Select(dc => dc.Document.DocTypeId.Value).Distinct().ToList();
            var resultDocTypes = GetAvaiableByDocTypes(permissions, doctypeIds, type);
            if (resultDocTypes != null && resultDocTypes.Any())
            {
                return resultDocTypes;
            }

            //Tiếp theo mới lấy theo lĩnh vực(docfield)
            var docfields = docCopys.Select(dc => dc.Document.ListDocFieldId);
            var docfieldIds = new List<int>();
            foreach (var item in docfields)
            {
                docfieldIds.AddRange(item);
            }
            docfieldIds = docfieldIds.Distinct().ToList();
            var resultDocFields = GetAvaiableByDocFields(permissions, docfieldIds, type);
            if (resultDocFields != null && resultDocFields.Any())
            {
                return resultDocFields;
            }

            return GetAvaiableCommons(permissions, type);
        }

        /// <summary>
        /// Trả về danh sách các mẫu phiếu cho thể sử dụng cho hồ sơ hiện tại.
        /// </summary>
        /// <remarks>
        /// Nguyên tắc là những người nào tham gia xử lý hồ sơ này đều có thể in được tất những những mẫu hiện có cho nó.
        /// </remarks>
        /// <param name="docCopy">Hồ sơ cần lấy mẫu.</param>
        /// <param name="userId">Người sử dụng hiện tại.</param>
        /// <param name="tempType"></param>
        /// <returns>Danh sách các mẫu phiếu hợp lệ.</returns>
        public IEnumerable<Template> GetAvaiableTemps(DocumentCopy docCopy, int userId, TemplateType tempType)
        {
            if (docCopy == null)
            {
                throw new Exception("Hồ sơ không tồn tại trên hệ thống");
            }

            var document = docCopy.Document;
            var permission = new List<DocumentProcessType> { DocumentProcessType.TiepNhan };

            // Nếu cán bộ hiện tại đã từng bàn giao thì hiện mẫu Biên nhận Bàn giao
            if (HasTransfered(docCopy, userId))
            {
                permission.Add(DocumentProcessType.BanGiao);
            }

            // Nếu đã trả kết quả thì trả về mẫu Biên nhận trả kết quả
            if (document.IsReturned == true)
            {
                permission.Add(DocumentProcessType.TraKetQua);
            }

            // Nếu là đã tiếp nhận bổ sung thì trả về mẫu Biên nhận bổ sung
            if (document.IsSupplemented == true)
            {
                permission.Add(DocumentProcessType.TiepNhanBoSung);
            }

            int type = (int)tempType;
            return GetTemplates(docCopy, permission, type);
        }

        /// <summary>
        /// HopCV:201015
        /// Lấy mẫu phôi theo văn bản/ hồ sơ copy, quyền hạn đối với mẫu và loại mẫu phôi
        /// </summary>
        /// <param name="document">Văn bản/hồ sơ</param>
        /// <param name="templatePermission">Danh sách quyền đối với mẫu</param>
        /// <param name="type">Loại mẫu phôi</param>
        /// <returns></returns>
        public IEnumerable<Template> GetTemplates(Document document, List<DocumentProcessType> templatePermission, int type)
        {
            if (document == null)
            {
                throw new Exception("Hồ sơ không tồn tại trên hệ thống");
            }

            return GetTemplates(new List<Guid> { document.DocTypeId.Value }, document.ListDocFieldId, templatePermission, type);
        }

        /// <summary>
        /// Lấy mẫu template theo loại văn bản, lĩnh vực, quyền
        /// </summary>
        /// <param name="doctypeIds">Danh sách loại văn bản</param>
        /// <param name="docfieldIds"></param>
        /// <param name="templatePermission">Danh sách quyền đối với mẫu </param>
        /// <param name="type">Loại mẫu phôi</param>
        /// <returns></returns>
        public IEnumerable<Template> GetTemplates(List<Guid> doctypeIds, List<int> docfieldIds,
            List<DocumentProcessType> templatePermission, int type)
        {
            if (doctypeIds != null && doctypeIds.Any())
            {
                //Đầu tiên kiểm tra lấy theo Loại văn ban trước
                var resultDocTypes = GetAvaiableByDocTypes(templatePermission, doctypeIds, type);
                if (resultDocTypes != null && resultDocTypes.Any())
                {
                    return resultDocTypes;
                }
            }

            if (docfieldIds != null && docfieldIds.Any())
            {
                //Tiếp theo mới lấy theo lĩnh vực(docfield
                var resultDocFields = GetAvaiableByDocFields(templatePermission, docfieldIds, type);
                if (resultDocFields != null && resultDocFields.Any())
                {
                    return resultDocFields;
                }
            }

            return GetAvaiableCommons(templatePermission, type);
        }

        /// <summary>
        /// HopCV:201015
        /// Lấy mẫu phôi theo văn bản/ hồ sơ copy , quyền hạn đối với mẫu và loại mẫu phôi
        /// </summary>
        /// <param name="docCopy"></param>
        /// <param name="templatePermission">Danh sách quyền đối với mẫu</param>
        /// <param name="type">Loại mẫu phôi</param>
        /// <returns></returns>
        public IEnumerable<Template> GetTemplates(DocumentCopy docCopy, List<DocumentProcessType> templatePermission, int type)
        {
            if (docCopy == null)
            {
                throw new Exception("Hồ sơ không tồn tại trên hệ thống");
            }
            var document = docCopy.Document;
            return GetTemplates(document, templatePermission, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docCopy"></param>
        /// <returns></returns>
        public Template GetTiepNhanHsmcPrint(DocumentCopy docCopy)
        {
            if (docCopy == null)
            {
                throw new Exception("Hồ sơ không tồn tại trên hệ thống");
            }
            var document = docCopy.Document;
            var permission = new List<DocumentProcessType> { DocumentProcessType.TiepNhan };
            var docfieldIds = document.ListDocFieldId;
            return GetAvaiablePrints(permission, docfieldIds).FirstOrDefault();
        }

        /// <summary>
        /// Trả về danh sách các mẫu phiếu In biên nhân
        /// </summary>
        /// <param name="docType">Mã hồ sơ</param>
        /// <returns></returns>
        public IEnumerable<Template> GetTiepNhanHsmcPrint(DocType docType)
        {
            if (docType == null)
            {
                throw new Exception("null docType");
            }

            var permission = new List<DocumentProcessType> { DocumentProcessType.TiepNhan };

            var docfieldIds = new List<int> { docType.DocFieldId.Value };
            return GetAvaiablePrints(permission, docfieldIds);
        }

        public IEnumerable<Template> GetXlvbPrint()
        {
            return _tempateRepository.Gets(false, t => t.Type == (int)TemplateType.PhieuInXlvb);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docCopy"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Template> GetTransferedPrint(DocumentCopy docCopy, int userId)
        {
            if (docCopy == null)
            {
                throw new Exception("Hồ sơ không tồn tại trên hệ thống");
            }

            var document = docCopy.Document;
            //var permission = new List<DocumentProcessType> { DocumentProcessType.TiepNhan };
            var permission = new List<DocumentProcessType>();
            // Nếu cán bộ hiện tại đã từng bàn giao thì hiện mẫu Biên nhận Bàn giao
            if (HasTransfered(docCopy, userId))
            {
                permission.Add(DocumentProcessType.BanGiao);
            }

            var docfieldIds = document.ListDocFieldId;

            return GetAvaiablePrints(permission, docfieldIds);
        }

        /// <summary>
        /// Trả về danh sách tất cả các mẫu phiếu có thể in cho permission hiện tại. Kết quả chỉ đoc
        /// </summary>
        /// <param name="permission">permission</param>
        /// <returns></returns>
        public IEnumerable<Template> Gets(DocumentProcessType permission)
        {
            var spec = TemplateQuery.WithPermission(new List<DocumentProcessType> { permission })
                        .And(TemplateQuery.IsParent())
                        .And(TemplateQuery.IsActive());
            return _tempateRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Trả về danh sách tất cả các mẫu phiếu có thể in. Kết quả chỉ đoc
        /// </summary>
        /// <param name="spec">spec</param>
        /// <returns></returns>
        public IEnumerable<Template> Gets(Expression<Func<Template, bool>> spec = null)
        {
            return _tempateRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra tempate theo doctypeId
        /// </summary>
        /// <param name="doctypeId"></param>
        /// <returns></returns>
        public IEnumerable<Template> GetsByDoctypeId(Guid doctypeId)
        {
            return _tempateRepository.GetsReadOnly(x => x.DoctypeId == doctypeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="userId"></param>
        /// <param name="docId"></param>
        /// <returns></returns>
        public IEnumerable<IDictionary<string, object>> GetValues(Template template, int userId, Guid? docId = null)
        {
            if (template == null)
            {
                return new List<IDictionary<string, object>>();
            }
            var parameters = GetSqlParameters(userId, docId);
            return GetValueFromDb(template.Sql, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="userId"></param>
        /// <param name="docCopyIds"></param>
        /// <param name="suppId"></param>
        /// <returns></returns>
        public IEnumerable<IDictionary<string, object>> GetValues(Template template, int userId, int suppId, List<int> docCopyIds)
        {
            if (template == null)
            {
                return new List<IDictionary<string, object>>();
            }
            var parameters = GetSqlParameters(userId, docCopyIds, suppId);

            var query = template.Sql;
            if (string.IsNullOrWhiteSpace(query) && template.ParentId.HasValue)
            {
                var parent = Get(template.ParentId.Value);
                query = parent.Sql;
            }

            if (docCopyIds.Any())
            {
                var documentCopyIds = string.Join(",", docCopyIds);
                query = query.Replace("#docCopyIds", documentCopyIds);
            }
            return GetValueFromDb(query, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private IEnumerable<IDictionary<string, object>> GetValueFromDb(string query, params object[] parameters)
        {
            var result = Context.RawQuery(query, parameters) as IEnumerable<IDictionary<string, object>>;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="docCopyIds"></param>
        /// <param name="suppId"></param>
        /// <returns></returns>
        private object[] GetSqlParameters(int userId, List<int> docCopyIds, int suppId)
        {
            var result = new List<object> { 
                new SqlParameter("@UserId", userId) 
            };

            if (docCopyIds.Count == 1)
            {
                result.Add(new SqlParameter("@docCopyId", docCopyIds.First()));
            }

            result.Add(new SqlParameter("@suppId", suppId));

            return result.ToArray();
        }

        private object[] GetSqlParameters(int userId, Guid? docId = null)
        {
            var result = new List<object> { 
                new SqlParameter("@UserId", userId) 
            };
            if (docId != null)
            {
                result.Add(new SqlParameter("@docId", docId));
            }
            return result.ToArray();
        }

        #region Private Methods

        private Template GetChildForDocField(int templateId, List<int> docFieldIds, TemplateType templateType)
        {
            var children = _tempateRepository.GetsReadOnly(t => t.IsActive && t.TemplateId == templateId && t.Type == (int)templateType && !t.DoctypeId.HasValue);
            var result = children.SingleOrDefault(c => docFieldIds.Any(df => df == c.DocFieldId));
            return result;
        }

        private Template GetChildForDocType(int templateId, Guid docTypeId, TemplateType templateType)
        {
            var children = _tempateRepository.GetsReadOnly(t => t.IsActive && t.TemplateId == templateId && t.Type == (int)templateType);
            var result = children.SingleOrDefault(c => c.DoctypeId == docTypeId);
            return result;
        }

        /// <summary>
        /// Trả về giá trị xác định user có bàn giao văn bản đi chưa
        /// Todo: viết vào documentCopyBll
        /// </summary>
        /// <returns></returns>
        private bool HasTransfered(DocumentCopy docCopy, int userId)
        {
            return !docCopy.IsCurrentUser(userId) && docCopy.HasThamGiaXuLy(userId);
            // return docCopy.DocFinishes.Count > 1 && docCopy.DocFinishes.Any(df => df.UserId == userId) && docCopy.DocFinishes.Last().UserId != userId;
        }

        /// <summary>
        /// Trả về danh sách các mẫu phiếu in khả dụng
        /// </summary>
        /// <remarks>
        /// Các mẫu phiếu in sẽ lấy ra bao gồm:
        ///     - Là mẫu phiếu in cha (mẫu đc cấu hình chung cho cả hệ thống hoặc cho 1 lĩnh vực)
        ///     - Tất cả các mẫu phiếu in chung cho toàn hệ thống (permission == 0).
        ///     - Tất cả các mẫu phiếu in chung được cấu hình cho lĩnh vực cần lấy.
        ///     - Tất cả các mẫu phiếu theo permission cần lấy.
        /// </remarks>
        /// <param name="templatePermission">Permisson cần lấy.</param>
        /// <param name="docfieldIds">Lĩnh vực cần lấy.</param>
        /// <returns>Danh sách các mẫu phiếu in khả dụng.</returns>
        private IEnumerable<Template> GetAvaiablePrints(List<DocumentProcessType> templatePermission, List<int> docfieldIds)
        {
            var spec = TemplateQuery.WithPermissionAndDocField(templatePermission, docfieldIds);
            return _tempateRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// HopCV:081015
        /// Trả về danh sách các mẫu khả dụng theo lĩnh vực
        /// </summary>
        /// <remarks>
        /// Các mẫu phiếu  sẽ lấy ra bao gồm:
        ///     - Là mẫu phiếu  cha (mẫu đc cấu hình chung cho cả hệ thống hoặc cho 1 lĩnh vực)
        ///     - Tất cả các mẫu phiếu  chung cho toàn hệ thống (permission == 0).
        ///     - Tất cả các mẫu phiếu  chung được cấu hình cho lĩnh vực cần lấy.
        ///     - Tất cả các mẫu phiếu theo permission cần lấy.
        /// </remarks>
        /// <param name="templatePermission">Permisson cần lấy.</param>
        /// <param name="docfieldIds">Lĩnh vực cần lấy.</param>
        /// <param name="type">Loại mẫu (- 1: Phiếu in, - 2: Email, - 3: SMS )</param>
        /// <returns>Danh sách các mẫu phiếu khả dụng.</returns>
        private IEnumerable<Template> GetAvaiableByDocFields(List<DocumentProcessType> templatePermission, List<int> docfieldIds, int type)
        {
            var spec = TemplateQuery.WithPermissionAndDocField(templatePermission, docfieldIds, type);
            return _tempateRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// HopCV:081015
        /// Trả về danh sách các mẫu khả dụng loại văn bản
        /// </summary>
        /// <remarks>
        /// Các mẫu phiếu  sẽ lấy ra bao gồm:
        ///     - Là mẫu phiếu  cha (mẫu đc cấu hình chung cho cả hệ thống hoặc cho 1 lĩnh vực)
        ///     - Tất cả các mẫu phiếu  chung cho toàn hệ thống (permission == 0).
        ///     - Tất cả các mẫu phiếu  chung được cấu hình cho lĩnh vực cần lấy.
        ///     - Tất cả các mẫu phiếu theo permission cần lấy.
        /// </remarks>
        /// <param name="templatePermission">Permisson cần lấy.</param>
        /// <param name="docTypeIds">Loại văn bản.</param>
        /// <param name="type">Loại mẫu (- 1: Phiếu in, - 2: Email, - 3: SMS )</param>
        /// <returns>Danh sách các mẫu phiếu khả dụng.</returns>
        private IEnumerable<Template> GetAvaiableByDocTypes(List<DocumentProcessType> templatePermission, List<Guid> docTypeIds, int type)
        {
            var spec = TemplateQuery.WithPermissionAndDocType(templatePermission, docTypeIds, type);
            return _tempateRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// HopCV:081015
        /// Trả về danh sách các mẫu khả dụng loại văn bản
        /// </summary>
        /// <remarks>
        /// Các mẫu phiếu  sẽ lấy ra bao gồm:
        ///     - Là mẫu phiếu  cha (mẫu đc cấu hình chung cho cả hệ thống hoặc cho 1 lĩnh vực)
        ///     - Tất cả các mẫu phiếu  chung cho toàn hệ thống (permission == 0).
        ///     - Tất cả các mẫu phiếu  chung được cấu hình cho lĩnh vực cần lấy.
        ///     - Tất cả các mẫu phiếu theo permission cần lấy.
        /// </remarks>
        /// <param name="templatePermission">Permisson cần lấy.</param>
        /// <param name="type">Loại mẫu (- 1: Phiếu in, - 2: Email, - 3: SMS )</param>
        /// <returns>Danh sách các mẫu phiếu khả dụng.</returns>
        private IEnumerable<Template> GetAvaiableCommons(List<DocumentProcessType> templatePermission, int type)
        {
            var spec = TemplateQuery.WithPermission(templatePermission, type);
            return _tempateRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public Stream DownloadContentFile(Template template)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }
            Stream stream = null;
            if (template.ContentFileLocalName != null)
            {
                stream = FileManager.Default.Open(template.ContentFileLocalName, ResourceLocation.Default.FileUploadTemp);
            }

            return stream;
        }

        /// <summary>
        /// Lấy mẫu theo điều kiện
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="projector"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Template, T>> projector, Expression<Func<Template, bool>> spec = null)
        {
            return _tempateRepository.GetsAs(projector, spec);
        }

        #endregion

    }
}