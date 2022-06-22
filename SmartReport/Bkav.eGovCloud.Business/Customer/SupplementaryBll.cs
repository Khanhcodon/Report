using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>BSO - Phòng 2 - eGov</para>
    /// <para>Project: eGov Cloud - v1.0</para>
    /// <para>Access Level(Class/Struct/Interface) : SupplementaryBll - public - BLL</para>
    /// <para>Access Modifiers:</para>
    /// <para>    * Inherit   : [Class Name]</para>
    /// <para>    * Implement : [Inteface Name], [Inteface Name], ...</para>
    /// <para></para>
    /// <para>Create Date : 240113</para>
    /// <para>Author      : TienBV</para>
    /// <para>Description : Cung cấp các api quản lý bổ sung.</para>
    /// </summary>
    public class SupplementaryBll : ServiceBase
    {
        private readonly IRepository<Supplementary> _supplementatyRepository;
        private readonly IRepository<SupplementaryDetail> _supplementatyDetailRepository;
        private readonly IRepository<RequiredSupplementary> _requiredSupplementaryRepository;
        private readonly IRepository<DocPaper> _docPaperRepository;
        private readonly IRepository<DocFee> _docFeeRepository;
        private readonly FeeBll _feeService;
        private readonly PaperBll _paperService;
        private readonly IRepository<DocType> _doctypeRepository;
        private readonly DocumentBll _documentService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly UserBll _userService;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="documentService">Document Service </param>
        /// <param name="docCopyService"> </param>
        /// <param name="userService"></param>
        /// <param name="paperService"></param>
        /// <param name="feeService"></param>
        public SupplementaryBll(
            IDbCustomerContext context,
            DocumentBll documentService,
            DocumentCopyBll docCopyService,
            UserBll userService,
            PaperBll paperService,
            FeeBll feeService)
            : base(context)
        {
            _supplementatyRepository = Context.GetRepository<Supplementary>();
            _requiredSupplementaryRepository = Context.GetRepository<RequiredSupplementary>();
            _doctypeRepository = Context.GetRepository<DocType>();
            _supplementatyDetailRepository = Context.GetRepository<SupplementaryDetail>();
            _docPaperRepository = Context.GetRepository<DocPaper>();
            _docFeeRepository = Context.GetRepository<DocFee>();

            _documentService = documentService;
            _docCopyService = docCopyService;
            _userService = userService;
            _paperService = paperService;
            _feeService = feeService;
        }

        /// <summary>
        /// Trả về  các yêu cầu  bổ sung theo hồ sơ. Kết quả chỉ đọc
        /// <para> (Tienbv@bkav.com - 160413)</para>
        /// </summary>
        /// <param name="docId">Document Id</param>
        /// <param name="withDetail">Lấy danh sách chi tiết</param>
        /// <returns></returns>
        public IEnumerable<Supplementary> Gets(Guid docId, bool withDetail = true)
        {
            var result = _supplementatyRepository.GetsReadOnly(s => s.DocumentId == docId);
            if (withDetail)
            {
                foreach (var supp in result)
                {
                    supp.SupplementaryDetail = _supplementatyDetailRepository.GetsReadOnly(s => s.SupplementaryId == supp.SupplementaryId);
                }
            }

            return result.OrderBy(s => s.IsSuccess);
        }

        /// <summary>
        /// Trả về danh sách tất cả các yêu cầu bổ sung đang xử lý
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public Supplementary GetNotReceived(Guid docId)
        {
            return _supplementatyRepository.Get(false, s => !s.DateReceived.HasValue && s.DocumentId == docId);
        }

        /// <summary>
        /// <para> Thêm yêu cầu bổ sung</para>
        /// <para> (Tienbv@bkav.com 270213)</para>
        /// </summary>
        /// <param name="entity">Supplementary entity</param>
        public void Create(Supplementary entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }

            var currentSupplementarys = Gets(entity.DocumentId);
            var lastSupp = currentSupplementarys.LastOrDefault();
            var order = lastSupp == null ? 1 : lastSupp.Order + 1;
			
            entity.Order = order;
            _supplementatyRepository.Create(entity);

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật yêu cầu bổ sung
        /// </summary>
        /// <param name="supplementary">Yêu cầu bổ sung cũ</param>
        /// <param name="docPapers">Danh sách giấy tờ</param>
        /// <param name="docFees">Danh sách lệ phí</param>
        /// <param name="detailId">Id yêu cầu bổ sung chi tiết của người dùng hiện tại</param>
        /// <param name="comment">Nội dung yêu cầu mới</param>
        /// <param name="doctypeId">Loại hồ sơ</param>
        /// <param name="user">Người yêu cầu</param>
        public void Update(Supplementary supplementary, List<DocPaper> docPapers, List<DocFee> docFees, int detailId, string comment, Guid doctypeId, User user)
        {            
            var supplementaryDetail = _supplementatyDetailRepository.Get(detailId);
            if (supplementaryDetail == null)
            {
                supplementaryDetail = new SupplementaryDetail()
                {
                    Comment = comment,
                    DateSend = DateTime.Now,
                    SupplementaryId = supplementary.SupplementaryId,
                    UserSendId = user.UserId,
                    UserSendName = user.Username
                };
                _supplementatyDetailRepository.Create(supplementaryDetail);
            }
            else
            {
                supplementaryDetail.Comment = comment;
            }

            // var details = _supplementatyDetailRepository.GetsReadOnly(s => s.SupplementaryId == supplementary.SupplementaryId);
            // supplementary.Details = Newtonsoft.Json.JsonConvert.SerializeObject(details);
            Context.SaveChanges();
        }

        /// <summary>
        /// <para> Cập nhật yêu cẩu bổ sung.</para>
        /// <para> (Tienbv@bkav.com 270213)</para>
        /// </summary>
        /// <param name="entity">Supplementary entity</param>
        public void Update(Supplementary entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// <para>Trả về yêu cầu bổ sung chưa được bàn giao</para>
        /// <para>(Tienbv@bkav.com - 260213)</para>
        /// </summary>
        /// <param name="docCopyId">Document copy id</param>
        /// <returns>Yêu cầu bổ sung chưa được tiếp nhận.</returns>
        public Supplementary GetByDocCopy(int docCopyId)
        {
            if (docCopyId <= 0)
            {
                return null;
            }

            return _supplementatyRepository.Gets(false, s => s.DocumentCopyId == docCopyId && !s.IsSuccess && !s.UserReceivedId.HasValue).LastOrDefault();
        }
        
        /// <summary>
        /// <para> Trả về yêu cầu bổ sung theo id</para>
        /// <para> (Tienbv@bkav.com - 260213)</para>
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="withDetail"></param>
        /// <returns>Supplementary</returns>
        public Supplementary Get(int id, bool withDetail = false)
        {
            var result = _supplementatyRepository.Get(id);
            if (withDetail)
            {
                result.SupplementaryDetail = _supplementatyDetailRepository.Gets(true, s => s.SupplementaryId == id);
            }
            return result;
        }

        /// <summary>
        /// <para> Tiếp nhận bổ sung: </para>
        /// <para>      - Cập nhật kết quả tiếp nhận bổ sung vào bảng bổ sung</para>
        /// <para>      - Cập nhật docpaper, docfee nếu có</para>
        /// <para> (Tienbv@bkav.com 280213)</para>
        /// </summary>
        /// <param name="supplementary"> Bổ sung</param>
        /// <param name="docPapers"> Danh sách doc paper</param>
        /// <param name="docFees"> Danh sách doc fee</param>
        /// <param name="dateCreated"> </param>
        /// <param name="userSendId"> </param>
        public void Receive(Supplementary supplementary, List<DocPaper> docPapers, List<DocFee> docFees, DateTime dateCreated, int userSendId)
        {
            if (!supplementary.DocumentCopyId.HasValue)
            {
                throw new ArgumentException("supplementary.DocumentCopyId bắt buộc phải có giá trị: là documentCopyId thực hiện tiếp nhận bổ sung", "supplementary");
            }

            using (var trans = new TransactionScope())
            {
                Update(supplementary);

                UpdateDocument(supplementary.DocumentId, docPapers, docFees, supplementary.SupplementaryId);

                var docCopy = _docCopyService.Get((int)supplementary.DocumentCopyId);

                // Kết thúc hướng dừng xử lý luôn
                if (docCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ChoKetQuaDungXuLy)
                {
                    //HopCV:170915
                    //Ghi log 
                    var userFinish = _userService.GetFromCache(userSendId);

                    var content = "Văn bản này được kết thúc bởi " + userFinish.FullName + ". Thời gian kết thúc: " + dateCreated.ToString("dd/MM/yyyy HH:mm:ss");
                    _docCopyService.Finish(docCopy, dateCreated, userSendId, content);
                }

                // Cập nhật lại trạng thái hướng yêu cầu dừng xử lý nếu là tiếp nhận dừng xử lý cuối cùng
                var isLastUpdate = IsLastUpdate(supplementary.DocumentId);
                if (isLastUpdate && docCopy.StatusInEnum == DocumentStatus.DungXuLy)
                {
                    _docCopyService.ChangeStatus(docCopy.ParentId.Value, DocumentStatus.DangXuLy);
                }
                Context.SaveChanges();
                trans.Complete();
            }
        }

        private void UpdateDocument(Guid docId, List<DocPaper> docPapers, List<DocFee> docFees, int supplementId)
        {
            var document = _documentService.Get(docId);
            if (document == null)
            {
                throw new Exception("Hồ sơ không tồn tại");
            }

            foreach (var paper in docPapers)
            {
                paper.SupplementaryId = supplementId;
            }

            foreach (var fee in docFees)
            {
                fee.SupplementaryId = supplementId;
            }

            document.DocFees = docFees;
            document.DocPapers = docPapers;
        }
        
        /// <summary>
        /// Kiểm tra có tồn tại yêu cầu dừng xử lý nào chưa dc update ko.
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public bool IsLastUpdate(Guid docId)
        {
            return !_supplementatyRepository.Exist(s => s.DocumentId == docId && s.DocumentCopyId == 0);
        }

        #region Supplementary Admin

        /// <summary>
        /// Lấy ra danh sách yêu cầu bổ sung theo loại hồ sơ id
        /// </summary>
        /// <param name="projector"> </param>
        /// <param name="doctypeid">Id của doctype</param>
        /// <returns>Danh sách lệ phí</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<RequiredSupplementary, T>> projector, Guid? doctypeid = null)
        {
            var spec = doctypeid.HasValue
                        ? RequiredSupplementaryQuery.WithDocTypeId(doctypeid.Value)
                        : null;
            return _requiredSupplementaryRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Tạo một yêu cầu bổ sung mới trên hệ thống
        /// </summary>
        /// <param name="entity"></param>
        public void CreateRequired(RequiredSupplementary entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }
            _requiredSupplementaryRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy yêu cầu bổ sung của hệ thống
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RequiredSupplementary GetRequired(int id)
        {
            return _requiredSupplementaryRepository.Get(id);
        }

        /// <summary>
        /// Cập nhật yêu cầu bổ sung của hệ thống
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateRequired(RequiredSupplementary entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa yêu cầu bổ sung của hệ thống
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteRequired(RequiredSupplementary entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }
            _requiredSupplementaryRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về danh sách các mẫu yêu cầu bổ sung
        /// </summary>
        /// <param name="doctypeId">Id loại hồ sơ</param>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public IEnumerable<RequiredSupplementary> GetRequireds(Guid doctypeId, int userId)
        {
            var result = new List<RequiredSupplementary>();
            var query = RequiredSupplementaryQuery.WithUserId(userId);
            result.AddRange(_requiredSupplementaryRepository.Gets(true, query));

            var doctype = _doctypeRepository.Get(doctypeId);
            if (doctype != null)
            {
                var docfieldId = doctype.DocFieldId;
                query = RequiredSupplementaryQuery.WithDoctypeAndDocField(doctype.DocTypeId, docfieldId, userId);
                result.AddRange(_requiredSupplementaryRepository.Gets(true, query));
            }
            return result;
        }

        /// <summary>
        /// Kết thúc các hồ sơ quá hạn bổ sung mà người dân chưa đến bổ sung
        /// </summary>
        /// <returns></returns>
        public void FinishExpireSupplementary()
        {
            var dateNow = DateTime.Now;
            var adminUser = _userService.GetAdmin();

            var expiresSupp = _supplementatyRepository.Gets(true, x => !x.IsSuccess
                && (x.NewDateAppointed.HasValue
                && x.NewDateAppointed.Value < dateNow
                || x.OldDateAppointed.HasValue
                && x.OldDateAppointed < dateNow)
                && !x.DateReceived.HasValue);

            foreach (var item in expiresSupp)
            {
                var contentFinish = string.Format("Hệ thống ([System]) tự động kết thúc văn bản vào lúc: {0} do người dân không đến bổ sung đúng hạn", dateNow.ToString("dd/MM/yyyy HH:mm:ss"));
                if (item.DocumentCopyId.HasValue)
                {
                    var documentCopy = _docCopyService.Get(item.DocumentCopyId.Value);
                    if (documentCopy != null)
                    {
                        _docCopyService.Finish(documentCopy, dateNow, adminUser.UserId, contentFinish);
                    }
                }
                item.UserReceivedId = adminUser.UserId;
                item.UserReceiveName = "[System]";
                item.CommentReceived = contentFinish;
                item.DateReceived = DateTime.Now;

                Update(item);
            }
        }

        #endregion

        /// <summary>
        /// Hủy yêu cầu bổ sung hiện tại và kiểm tra nếu không có yêu cầu bổ sung nào nữa thì xóa yêu cầu bổ sung gốc đi
        /// Trả về kết quả yêu cầu bổ sung gốc có xóa hay không
        /// </summary>
        /// <param name="detailId">Yêu cầu cần hủy</param>
        /// <returns>True - Nếu xóa luôn yêu cầu bổ sung gốc; false - còn lại</returns>
        public bool CancelRequire(int detailId)
        {
            var result = false;
            var supplementaryDetail = _supplementatyDetailRepository.Get(detailId);
            if (supplementaryDetail == null)
            {
                return result;
            }

            var suppId = supplementaryDetail.SupplementaryId;
            _supplementatyDetailRepository.Delete(supplementaryDetail);
            result = true;

            // Kiểm tra nếu không tồn tại yêu cầu nào nữa thì xóa yêu cầu bổ sung chính đi
            var hasOtherDetailExist = _supplementatyDetailRepository.Exist(s => s.SupplementaryId == suppId && s.SupplementaryDetailId != detailId);
            if (!hasOtherDetailExist)
            {
                var supplementary = _supplementatyRepository.Get(suppId);
                if (supplementary == null)
                {
                    return false;
                }

                _supplementatyRepository.Delete(supplementary);

                // Cập nhật document
                var doc = _documentService.Get(supplementary.DocumentId);
                if (doc == null)
                {
                    return false;
                }

                doc.IsSupplemented = null;
                doc.DateRequireSupplementary = null;
                doc.Status = (int)DocumentStatus.DangXuLy;
                doc.DateAppointed = supplementary.OldDateAppointed;
            }

            Context.SaveChanges();

            return result;
        }

        /// <summary>
        /// Hủy yêu cầu bổ sung
        /// </summary>
        /// <param name="supplementary"></param>
        public void Cancel(Supplementary supplementary)
        {
            supplementary.UserReceivedId = null;
            supplementary.UserReceiveName = null;
            supplementary.DateReceived = null;
            supplementary.CommentReceived = null;
            supplementary.DateBeginProcess = null;
            supplementary.IsSuccess = false;

            // Xóa bỏ các giấy tờ, lệ phí bổ sung
            var docFees = _docFeeRepository.Gets(false, df => df.SupplementaryId == supplementary.SupplementaryId);
            var docPapers = _docPaperRepository.Gets(false, dp => dp.SupplementaryId == supplementary.SupplementaryId);
            foreach (var docFee in docFees)
            {
                _docFeeRepository.Delete(docFee);
            }
            foreach (var paper in docPapers)
            {
                _docPaperRepository.Delete(paper);
            }

            // Cập nhật document
            var doc = _documentService.Get(supplementary.DocumentId);
            doc.IsSupplemented = null;
            doc.DateRequireSupplementary = null;
            doc.Status = (int)DocumentStatus.DungXuLy;
            doc.DateAppointed = supplementary.OldDateAppointed;

            supplementary.NewDateAppointed = null;

            Update(supplementary);
        }
    }
}
