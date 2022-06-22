using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Office - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Office trong CSDL
    /// </summary>
    [DataContract]
    public class Office
    {
        private ICollection<Office> _officeChildren;

        /// <summary>
        /// Lấy hoặc thiết lập Id văn phòng liên thông
        /// </summary>
        [DataMember]
        public int OfficeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên văn phòng
        /// </summary>
        [DataMember]
        public string OfficeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mã văn phòng
        /// </summary>
        [DataMember]
        public string OfficeCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn phòng cha
        /// </summary>
        [DataMember]
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số điện thoại
        /// </summary>
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Email
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Địa chỉ
        /// </summary>
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Đường dẫn file service
        /// </summary>
        [DataMember]
        public string FileService { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Đường dẫn data service
        /// </summary>
        [DataMember]
        public string DataService { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mật khẩu
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string LastPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IsMe { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public int? UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Các văn phòng con
        /// </summary>
        public virtual ICollection<Office> OfficeChildren
        {
            get { return _officeChildren ?? (_officeChildren = new List<Office>()); }
            set { _officeChildren = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Văn phòng cha
        /// </summary>
        public virtual Office OfficeParent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string OnlineServiceUrl { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ProcessServiceUrl
        /// </summary>
        [DataMember]
        public string ProcessServiceUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ReportServiceUrl { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập IsOnlineRegister
        /// </summary>
        [DataMember]
        public bool IsOnlineRegister { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int LevelId { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[DataMember]
        //public virtual Level Level { get; set; }
    }
}
