using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Models
{
    public class PrintDocumentModel
    {
        #region Fields

        private IEnumerable<DocumentCopy> _documentCopys;

        #endregion

        #region Instance Properties

        #region Columns

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản, hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số kí hiệu (eOffice), mã hồ sơ (eGate)
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Tên công dân: dùng cho hs một cửa
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày hẹn trả (eGate), ngày giải quyết (eOffice)
        /// </summary>
        public DateTime? DateAppointed { get; set; }

        /// <summary>
        /// Trạng thái xử lý của văn bản. Sử dụng DocumentStatusMachineHelper.ValidateNewDocumentStatus() để kiểm tra trước khi set;
        /// <para> 1: văn bản dự thảo.</para>
        /// <para> 2: văn bản đang xử lý.</para>
        /// <para> 4: văn bản đã kết thúc.</para>
        /// <para> 8: văn bản đã hủy.</para>
        /// <para> 16: văn bản dừng xử lý.</para>
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// <para>Trạng thái của văn bản copy</para>
        /// <para>(CuongNT@bkav.com - 050413)</para>
        /// </summary>
        //[JsonIgnore]
        public DocumentStatus StatusInEnum
        {
            get
            {
                return (DocumentStatus)Status;
            }
        }

        #endregion

        #region Relations

        /// <summary>
        /// Lấy hoặc thiết lập các doccopy của document hiện tại
        /// </summary>
        public virtual IEnumerable<DocumentCopy> DocumentCopys
        {
            get
            {
                return _documentCopys != null ?
                    _documentCopys.Where(dc => dc.DocumentCopyTypeInEnum == Core.Document.DocumentCopyTypes.XuLyChinh
                            || dc.DocumentCopyTypeInEnum == Core.Document.DocumentCopyTypes.DongXuLy)
                    : (_documentCopys = new List<DocumentCopy>());
            }
            set { _documentCopys = value; }
        }

        #endregion

        #endregion

    }
}