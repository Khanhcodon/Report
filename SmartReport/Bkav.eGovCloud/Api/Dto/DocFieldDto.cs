using System.Runtime.Serialization;
namespace Bkav.eGovCloud.Api.Dto
{
    /// <summary>
    /// Docfield data transfer object
    /// </summary>
    [DataContract]
    public class DocFieldDto
    {
        /// <summary>
        /// Docfield id
        /// </summary>
        [DataMember]
        public int DocFieldId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên lĩnh vực
        /// </summary>
        [DataMember]
        public string DocFieldName { get; set; }
    }
}
