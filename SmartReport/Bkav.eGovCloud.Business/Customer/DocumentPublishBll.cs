using System;
using System.Linq;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using System.Collections.Generic;
using Bkav.eGovCloud.Entities;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : DocumentPublishBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 110114</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng DocumentPublish trong CSDL</para>
    /// </summary>
    public class DocumentPublishBll : ServiceBase
    {
        private readonly IRepository<DocPublish> _docpublishRepository;
        private readonly NotificationBll _notifyService;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"> Context.</param>
        /// <param name="notifyService"></param>
        public DocumentPublishBll(IDbCustomerContext context, NotificationBll notifyService)
            : base(context)
        {
            _docpublishRepository = Context.GetRepository<DocPublish>();
            _notifyService = notifyService;
        }

        /// <summary>
        /// Kiểm tra code đã publish chưa
        /// </summary>
        /// <param name="code"></param>
        /// <returns>true: đã có; false: chưa có</returns>
        public bool Exist(string code)
        {
            return _docpublishRepository.Exist(d => d.DocCode == code && d.DatePublished.Year == DateTime.Now.Year);
        }

        /// <summary>
        /// Tạo mới hồ sơ sổ văn bản
        /// </summary>
        /// <param name="docpublish">Entity phát hành văn bản</param>
        public void Create(DocPublish docpublish)
        {
            if (docpublish == null)
            {
                throw new ArgumentNullException("docpublish");
            }

            _docpublishRepository.Create(docpublish);
        }

        /// <summary>
        /// Trả về danh sách các hồ sơ, văn bản đang chờ gửi liên thông
        /// </summary>
        /// <returns></returns>
        public List<DocPublish> GetPending()
        {
            var result = _docpublishRepository
                                .Gets(false, d => d.IsPending && d.HasLienThong
                                    && !d.HasSendFail
                                    && !string.IsNullOrEmpty(d.AddressCode)
                                    && d.AddressId.HasValue).ToList();
            return result;
        }

        /// <summary>
        /// Trả về danh sách các docpublish theo điều kiện kỹ thuật truyền vào
        /// </summary>
        /// <param name="isReadonly"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<DocPublish> Gets(bool isReadonly, Expression<Func<DocPublish, bool>> spec)
        {
            return _docpublishRepository.Gets(isReadonly, spec);
        }

        /// <summary>
        /// Trả về tất cả các hồ sơ văn bản đang gửi liên thông
        /// </summary>
        /// <returns></returns>
        public List<DocPublish> GetLienThongs()
        {
            return _docpublishRepository.Gets(true, d => !d.IsPending
                            && d.HasLienThong
                            && d.AddressId.HasValue && !d.IsResponsed).ToList();
        }

        /// <summary>
        /// Trả về tất cả các hồ sơ văn bản đang gửi liên thông
        /// </summary>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <param name="isHSMC">Lấy hồ sơ một cửa</param>
        /// <returns></returns>
        public List<DocPublish> GetLienThongs(DateTime from, DateTime to, bool? isHSMC = null)
        {
            return _docpublishRepository.Gets(true, d => !d.IsPending &&
                        d.HasLienThong &&
                        d.AddressId.HasValue &&
                        (!isHSMC.HasValue || d.IsHsmc == isHSMC) &&
                        d.DatePublished >= from &&
                        d.DatePublished <= to
                    ).ToList();
        }

        /// <summary>
        /// Trả về số lượng hồ sơ văn bản đang gửi liên thông
        /// </summary>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <returns></returns>
        public int CountLienThong(DateTime from, DateTime to)
        {
            return _docpublishRepository.Count(d => !d.IsPending &&
                        d.HasLienThong &&
                        d.AddressId.HasValue &&
                        d.DatePublished >= from &&
                        d.DatePublished <= to
                    );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docPublishId"></param>
        /// <returns></returns>
        public DocPublish Get(int docPublishId)
        {
            return _docpublishRepository.Get(docPublishId);
        }

        /// <summary>
        /// Trả về danh sách phát hành của hồ sơ hiện tại
        /// </summary>
        /// <param name="docCopyId"></param>
        /// <returns></returns>
        public List<DocPublish> GetPendings(int docCopyId)
        {
            return _docpublishRepository.Gets(false, d => d.IsPending && d.DocumentCopyId == docCopyId).ToList();
        }

        /// <summary>
        /// Trả về tổng số cơ quan đã phát hành nếu thực hiện phát hành tiếp
        /// </summary>
        /// <param name="addressIds"></param>
        /// <param name="docCopyId"></param>
        /// <returns></returns>
        public int GetTotalAddressExist(int docCopyId, List<int> addressIds)
        {
            return _docpublishRepository.Count(d => d.AddressId.HasValue && addressIds.Contains(d.AddressId.Value) && d.DocumentCopyId == docCopyId); ;
        }

        /// <summary>
        /// Trả về danh sách đang liên thông của hồ sơ hiện tại
        /// </summary>
        /// <param name="docCopyId">Hồ sơ</param>
        /// <returns></returns>
        public List<DocPublish> GetSentPublishes(int docCopyId = 0)
        {
            return _docpublishRepository.Gets(false, d => !d.IsPending && d.HasLienThong && (docCopyId == 0 || d.DocumentCopyId == docCopyId)).ToList();
        }

        /// <summary>
        /// Trả về danh sách các liên thông của văn bản hiện tại, kết quả chỉ để đọc
        /// </summary>
        /// <param name="docCopyId">Id văn bản</param>
        /// <returns></returns>
        public List<DocPublish> GetPublishes(int docCopyId)
        {
            return _docpublishRepository.Gets(true, d => d.HasLienThong && d.DocumentCopyId == docCopyId && d.AddressId.HasValue).ToList();
        }

        /// <summary>
        /// Trả về danh sách các phát hành của văn bản hiện tại
        /// </summary>
        /// <param name="docCopyId">Id văn bản</param>
        /// <returns></returns>
        public List<DocPublish> GetPublishesForDelete(int docCopyId)
        {
            return _docpublishRepository.Gets(false, d => d.DocumentCopyId == docCopyId && d.AddressId.HasValue).ToList();
        }

        /// <summary>
        /// Xác nhận đã phát hành hồ sơ thành công.
        /// Những DocumentId được confirm sẽ không xuất hiện trong kết quả trả về của GetPendingDocumentIds
        /// </summary>
        /// <param name="docPublishId">Id hồ sơ</param>
        public void ConfirmSent(int docPublishId)
        {
            var publish = Get(docPublishId);
            if (publish != null)
            {
                publish.IsPending = false;
                publish.HasSendFail = false;
                publish.DateSent = DateTime.Now;
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Update tiến độ liên thông hồ sơ
        /// </summary>
        /// <param name="docCode">Mã hồ sơ</param>
        /// <param name="addressCode">Mã cơ quan gửi tiến độ</param>
        /// <param name="traces">Tiến độ</param>
        public void UpdateTraces(string docCode, string addressCode, string traces)
        {
            var publish = _docpublishRepository.Gets(false,
                                    d => !d.IsPending && d.HasLienThong && !d.IsResponsed &&
                                    d.DocCode.Equals(docCode, StringComparison.OrdinalIgnoreCase) &&
                                    d.AddressCode.Equals(addressCode, StringComparison.OrdinalIgnoreCase));

            if (!publish.Any())
            {
                return;
            }

            if (publish.Count() > 1)
            {
                throw new Exception("UpdateTraces: Có nhiều hơn 1 lần publish hồ sơ " + docCode);
            }

            var itmPublish = publish.First();
            itmPublish.Traces = traces;

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật hồi báo
        /// </summary>
        /// <param name="docCopyResponsed"></param>
        /// <param name="addressId"></param>
        /// <param name="docCopyRelation"></param>
        /// <param name="note"></param>
        public void UpdateResponsed(DocumentCopy docCopyResponsed, int addressId, DocumentCopy docCopyRelation, string note = "")
        {
            var publish = _docpublishRepository.Get(false, p => p.AddressId == addressId &&
                                                                p.DocumentCopyId == docCopyRelation.DocumentCopyId &&
                                                                !p.IsResponsed &&
                                                                p.DocCode == docCopyRelation.Document.DocCode);
            if (publish != null)
            {
                publish.IsResponsed = true;
                publish.DocumentCopyResponsed = docCopyResponsed.DocumentCopyId;
                publish.DocCodeResponsed = docCopyResponsed.Document.DocCode;
                publish.DateResponsed = DateTime.Now; // docCopyResponsed.Document.DatePublished;
                publish.Note = note;
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Cập nhật hồi báo
        /// </summary>
        /// <param name="docCopyResponsed"></param>
        /// <param name="addressId"></param>
        /// <param name="docCopyRelation"></param>
        /// <param name="note"></param>
        public void UpdateResponsed(DocPublish publish, DocumentCopy docCopyResponsed, string note = "")
        {
            if (publish != null)
            {
                publish.IsResponsed = true;
                publish.DocumentCopyResponsed = docCopyResponsed.DocumentCopyId;
                publish.DocCodeResponsed = docCopyResponsed.Document.DocCode;
                publish.DateResponsed = DateTime.Now; // docCopyResponsed.Document.DatePublished;
                publish.Note = note;
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Update trạng thái khi gửi lỗi
        /// </summary>
        /// <param name="docPublish">Văn bản gửi lỗi</param>
        /// <param name="message">Nội dung lỗi</param>
        public void UpdateSendFail(DocPublish docPublish, string message)
        {
            docPublish.HasSendFail = true;
            docPublish.Note = message;

            try
            {
                var entity = new Notifications()
                {
                    UserId = docPublish.UserPublishId,
                    DateCreated = DateTime.Now,
                    Avatar = "",
                    Body = string.Format("Văn bản {0} lỗi: {1}", docPublish.DocCode, message),
                    Title = "Thông báo gửi liên thông lỗi",
                    IsReaded = false,
                    IsSent = false,
                    IsNew = true,
                    IsDeleted = false,
                    IsSystemNotify = true,
                    JsonData = "",
                    AppName = "documents",
					Token = ""
                };

                _notifyService.Create(entity);
            }
            catch
            {

            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cap nhat trang thai da gui sang Chi dao dieue hanh
        /// </summary>
        /// <param name="docCopyId"></param>
        /// <param name="bStatus"></param>
        public void UpdateCDDH(int docCopyId, bool bStatus)
        {
            if (docCopyId <= 0)
            {
                return;
            }

            var docPublishes = _docpublishRepository.Gets(false, d => d.DocumentCopyId == docCopyId);
            foreach (var docCopy in docPublishes)
            {
                docCopy.IsSentCDDH = bStatus;
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docCopyId"></param>
        public void Deletes(int docCopyId)
        {
            var docPublishes = GetPublishesForDelete(docCopyId);
            if (docPublishes != null && docPublishes.Any())
            {
                foreach (var docPublish in docPublishes)
                {
                    _docpublishRepository.Delete(docPublish);
                }
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Thay đổi số ký hiệu khi văn thư cập nhật mà văn bản chưa gửi liên thông đi.
        /// </summary>
        /// <param name="documentCopyId"></param>
        /// <param name="newDocCode"></param>
        public void ChangeDocCode(int documentCopyId, string newDocCode)
        {
            // Lưu ý không cập nhật văn bản đã gửi chỗ này do cơ quan nhận đã nhận theo mã cũ.
            // Những văn bản này cần check lại thực tế sẽ thế nào.
            var pendings = GetPendings(documentCopyId);
            if (pendings == null || !pendings.Any())
            {
                return;
            }

            foreach (var pending in pendings)
            {
                pending.DocCode = newDocCode;
            }

            SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="limitNum"></param>
        /// <returns></returns>
        public string GetForCDDH(DateTime fromDate, int limitNum = 10)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fromDate", fromDate));
            parameters.Add(new SqlParameter("@limit", limitNum));

            var result = Context.RawProcedure("GetForCDDH", parameters.ToArray());
            return JsonConvert.SerializeObject(result);
        }
		
		/// <summary>
		/// Cập nhật trạng thái văn bản liên thông
		/// </summary>
		/// <param name="docId">Mã văn bản liên thông (lưu trong trường Address)</param>
		/// <param name="organId">Mã định danh nơi gửi</param>
		/// <param name="doccode">Số ký hiệu văn bản</param>
		/// <param name="status">Trạng thái</param>
		/// <param name="userUpdate">Người cập nhật</param>
		/// <param name="message">Thông tin cập nhật</param>
		/// <returns></returns>
		public bool UpdateStatusLT(Guid docId, string organId, string doccode, int status, string userUpdate, string message)
		{
			var docPublish = _docpublishRepository.Get(false, d => d.DocumentId == docId && d.AddressCode.Equals(organId) && d.DocCode.Equals(doccode));
			if(docPublish == null)
			{
				throw new Exception("Lỗi không tìm thấy cơ quan nhận với mã: " + organId);
			}

			docPublish.Status = status;
			docPublish.Note = message;

			return true;
		}

	}
}
