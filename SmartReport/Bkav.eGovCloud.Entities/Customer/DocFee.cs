namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    ///<para></para> Bkav Corp. - BSO - eGov - eOffice team
    ///<para></para> Project: eGov Cloud v1.0
    ///<para></para> Class : Catalog - public - Entity
    ///<para></para> Access Modifiers: 
    ///<para></para> Create Date : 041212
    ///<para></para> Author      : TienBV
    ///<para></para> Description : Entity tương ứng với bảng DocFee trong CSDL.
    ///<para></para> Lưu thông tin các lệ phí của hồ sơ.
    /// </summary>
    public class DocFee
    {
        /// <summary>
        /// Auto increment key
        /// </summary>
        public int DocFeeId { get; set; }

        /// <summary>
        /// Get or set the document id.
        /// </summary>
        public System.Guid DocumentId { get; set; }

        /// <summary>
        /// Get or set the FeeName
        /// </summary>
        public string FeeName { get; set; }

        /// <summary>
        /// Get or set the price of Fee
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Get or set the doc-fee type
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Get or set the doc-fee type in enum
        /// </summary>
        public Entities.FeeType TypeInEnum
        {
            get { return (Entities.FeeType)Type; }
            set { Type = (int) value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id lần yêu cầu bổ sung
        /// </summary>
        public int? SupplementaryId { get; set; }
        ///// <summary>
        ///// Get or set the Document
        ///// </summary>
        //public Document Document { get; set; }
    }
}
