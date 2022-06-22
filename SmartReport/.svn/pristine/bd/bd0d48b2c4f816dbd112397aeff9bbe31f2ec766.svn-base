using System.Runtime.Serialization;
namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Công văn trả cho bwss
    /// </summary>
    [DataContract]
    public class CongVan
    {
        /// <summary>
        /// Contructer chuyển từ documentcopy sang CongVan
        /// </summary>
        /// <param name="doc">DocumentCopy</param>
        public CongVan(DocumentCopy doc)
        {
            this.Id = doc.DocumentCopyId;
             //this.TenCongVan = doc.Document.DocType.DocTypeName;
            this.TrichYeu = doc.Document.Compendium;
            this.SoKyHieu = doc.Document.DocCode;
            this.TrangThai = doc.StatusInEnum == DocumentStatus.KetThuc ? 1 : 0;
        }

        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public int LoaiCongVan { set; get; }

        /// <summary>
        /// TenCongVan
        /// </summary>
        [DataMember]
        public string TenCongVan { set; get; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        [DataMember]
        public string TrichYeu { set; get; }

        /// <summary>
        /// Số ký hiệu
        /// </summary>
        [DataMember]
        public string SoKyHieu { set; get; }

        //public string Sodendi { set; get; }

        //public string Nguoikysao { set; get; }

        //public string Nguoikychinh { set; get; }

        //public string Noidendi { set; get; }

        //public string Nguoigui { set; get; }

        //public string Nguoidanggiu { set; get; }

        //public string Ghichu { set; get; }

        //public int DanhmuchosoId { set; get; }

        //public DateTime Ngaydendi { set; get; }

        //public DateTime Ngaycongvan { set; get; }

        //public int Soban { set; get; }

        //public int Sotrang { set; get; }

        //public int Congvandendi { set; get; }

        //public string Mucdokhan { set; get; }

        //public string Vetcongvan { set; get; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        [DataMember]
        public int TrangThai { set; get; }

        //public string Noidung { set; get; }

        //public string Donvi { set; get; }

        //public int Congvantraloi { set; get; }

        //public string Opt1 { set; get; }

        //public string Opt2 { set; get; }

        //public string Opt3 { set; get; }

        //public string Opt4 { set; get; }

        //public string Vetduongdi { set; get; }

        //public string Loainoidung { set; get; }

        //public int HosoId { set; get; }

        //public DateTime Hangiaiquyet { set; get; }

        //public int CongvanVersion { set; get; }

        //public string Socongvantraloi { set; get; }

        //public string Nguoibaocao { set; get; }

        //public string Tiendo { set; get; }

        //public int Nuthientai { set; get; }

        //public int CauhoiId { set; get; }

        //public int Daxem { set; get; }

        //public int LinkAttachment { set; get; }

        //public string Nguoisoan { set; get; }

        //public int Mucuutien { set; get; }

        //public int Mausacvanban { set; get; }

        //public DateTime LastUpdate { set; get; }

        //public string Kyhieu { set; get; }

        //public int VanbantraloiId { set; get; }

        //public string Trichyeukhongdau { set; get; }

        //public string Skey { set; get; }

        //public int Encrypt { set; get; }

        //public int Compress { set; get; }

        //public int Sign { set; get; }

        //public DateTime CreateDate { set; get; }

        //public int Songayquahan { set; get; }

        //public int DuongdiId { set; get; }

        //public string Noidungduongdi { set; get; }

        //public DateTime NgaycongvanBegin { set; get; }

        //public DateTime NgaycongvanEnd { set; get; }

        //public DateTime BeginDate { set; get; }

        //public DateTime EndDate { set; get; }

        //public string Noidendi1 { set; get; }
    }
}