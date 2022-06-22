using Bkav.eGovCloud.Entities;
using System;
using System.Runtime.Serialization;
namespace Bkav.eGovCloud.Api.Dto
{
    /// <summary>
    /// Doctype data transfer object
    /// </summary>
    [DataContract]
    public class DocTypeDto
    {
        /// <summary>
        /// Doctype Id
        /// </summary>
        [DataMember]
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên loại hồ sơ
        /// </summary>
        [DataMember]
        public string DocTypeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thể loại văn bản
        /// </summary>
        [DataMember]
        public int CategoryId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nghiệp vụ xử lý của loại hồ sơ
        /// </summary>
        [DataMember]
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nghiệp vụ xử lý của loại hồ sơ
        /// </summary>
        [DataMember]
        public CategoryBusinessTypes CategoryBusinessIdInEnum { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập đối tượng thể loại văn bản
        /// </summary>
        [DataMember]
        public CategoryDto Category { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập lĩnh vực
        /// </summary>
        [DataMember]
        public int? DocFieldId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập đối tượng lĩnh vực
        /// </summary>
        [DataMember]
        public DocFieldDto DocField { get; set; }
    }
}
