using System;
using System.Runtime.Serialization;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// <para> Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para> Project: eGov Cloud v1.0</para>
    /// <para> Class : File - public - Entity</para>
    /// <para> Access Modifiers:</para>
    /// <para> Create Date : 21082014</para>
    /// <para> Author      : ManhNHc</para>
    /// <para> Description : Entity tương ứng với bảng File(File đính kèm văn bản quy phạm) trong CSDL</para>
    /// </summary>
    public class File
    {
        /// <summary>
        ///
        /// </summary>
        public File()
        {
        }

        /// <summary>
        /// contructer
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileLocalName"></param>
        /// <param name="size"></param>
        /// <param name="fileExtension"></param>
        public File(string fileName, string fileLocalName, int size, string fileExtension)
        {
            this.FileName = fileName;
            this.FileLocalName = fileLocalName;
            this.Size = size;
            this.FileExtension = fileExtension;
            this.IsDeleted = false;
            this.CreatedOnDate = DateTime.Now;
        }

        /// <summary>
        /// Id cuar file
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// Tên file thật
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Tên file lưu trong forder
        /// </summary>
        public string FileLocalName { get; set; }

        /// <summary>
        /// Kích cỡ file
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Định dạng đuôi file
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Trạng thái file
        /// </summary>
        [DataMember]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Ngày tao file
        /// </summary>
        public DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên thư mục tự sinh
        /// </summary>
        public string IdentityFolder { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của cấu hình thư mục gốc phía service
        /// </summary>
        public string FileLocationKey { get; set; }

        /// <summary>
        /// Id vị trí lưu file
        /// </summary>
        public int FileLocationId { get; set; }

        /// <summary>
        /// Phiên bản file
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? LawId { get; set; }

    }
}