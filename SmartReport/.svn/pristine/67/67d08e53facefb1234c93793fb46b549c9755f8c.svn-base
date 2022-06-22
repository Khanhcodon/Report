using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypePaper - public - Entity
    /// Access Modifiers:
    /// Create Date : 170914
    /// Author      : QuangP
    /// Description : Entity tương ứng với bảng DocTypePaper trong CSDL\
    /// </summary>
    public class DoctypePaper
    {
        /// <summary>
        /// contructer
        /// </summary>
        public DoctypePaper()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctypeId"></param>
        /// <param name="paperId"></param>
        /// <param name="isRequired"></param>
        public DoctypePaper(Guid doctypeId, int paperId, bool isRequired)
        {
            this.DocTypeId = doctypeId;
            this.PaperId = paperId;
            this.IsRequired = isRequired;
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public int PaperId { get; set; }

        /// <summary>
        /// Tên giấy tờ
        /// </summary>
        public string PaperName { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập đối tượng Paper tương ứng
        /// </summary>
        public Paper Paper { get; set; }
    }
}