using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Objects.Transfer
{
    /// <summary>
    /// Thông tin trả về sau khi tạo mới văn bản
    /// </summary>
    public class CreateDocumentResponse
    {
        /// <summary>
        /// Văn
        /// </summary>
        public Document Document { get; set; }
    
		/// <summary>
		/// 
		/// </summary>
        public DocumentCopy DocumentCopy { get; set;}
    }
}
