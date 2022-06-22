using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Business.FileTransfer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using System.Web;
using System.Net;
namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
	/// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
	/// <para>Project: eGov Cloud v1.0</para>
	/// <para>Class : AttachmentBll - public - BLL</para>
	/// <para>Access Modifiers:</para> 
	/// <para>Create Date : 140313</para>
	/// <para>Author      : TrungVH</para>
	/// <para>Description : BLL tương ứng với bảng Role trong CSDL</para>
	/// </summary>
    public class AttachmentDetailBll : ServiceBase
    {
        private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IRepository<AttachmentDetail> _attachmentDetailRepository;
        private readonly FileLocationBll _fileLocationService;
        private readonly FileLocationSettings _fileLocationSettings;
        private readonly FileManager _fileManager;
        private readonly UserBll _userService;
        private readonly StorePrivateBll _storePrivateService;
        private readonly DocumentPermissionHelper _documentPermissionHelper;

        /// <summary>
		/// Khởi tạo
		/// </summary>
		/// <param name="context">Context</param>
		/// <param name="fileLocationService">Dal tương ứng với bảng FileLocation trong CSDL</param>
		/// <param name="fileLocationSettings">Cấu hình cho vị trí lưu file</param>
		/// <param name="userService">Bll tương ứng với bản User trong CSDL </param>
		/// <param name="documentPermissionHelper">Bll nghiệp vụ check quyền trên văn bản/hồ sơ </param>
		/// <param name="storePrivateService">Bll nghiệp vụ sổ hồ sơ cá nhân</param>
		public AttachmentDetailBll(
            IDbCustomerContext context,
            FileLocationBll fileLocationService,
            FileLocationSettings fileLocationSettings,
            UserBll userService,
            StorePrivateBll storePrivateService,
            DocumentPermissionHelper documentPermissionHelper)
			: base(context)
		{
            _attachmentRepository = Context.GetRepository<Attachment>();
            _attachmentDetailRepository = Context.GetRepository<AttachmentDetail>();
            _fileLocationService = fileLocationService;
            _userService = userService;
            _fileLocationSettings = fileLocationSettings;
            _fileManager = FileManager.Default;
            _documentPermissionHelper = documentPermissionHelper;
            _storePrivateService = storePrivateService;
        }

        /// <summary>
		/// Lấy ra tất cả các file đính kèm
		/// </summary>
		/// <param name="documentId">Id văn bản, hồ sơ</param>
		/// <returns>Danh sách file đính kèm</returns>
		public IEnumerable<AttachmentDetail> Gets(int id)
        {
            return _attachmentDetailRepository.GetsReadOnly(a => a.AttachmentId == id);
        }

        /// <summary>
        /// Lấy ra tất cả các file đính kèm
        /// </summary>
        /// <param name="spec">Bộ lọc</param>
        /// <returns>Danh sách file đính kèm</returns>
        public IEnumerable<AttachmentDetail> Gets(Expression<Func<AttachmentDetail, bool>> spec = null)
        {
            return _attachmentDetailRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra tất cả các file đính kèm
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="documentId">Id văn bản, hồ sơ</param>
        /// <returns>Danh sách file đính kèm</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<AttachmentDetail, T>> projector, int id)
        {
            return _attachmentDetailRepository.GetsAs(projector, a => a.AttachmentId == id);
        }
    }
}
