using System.ComponentModel;

namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// Danh sách các service sử dụng trong hệ thống
    /// </summary>
    public enum Service
    {
        ///// <summary>
        ///// eDocService
        ///// </summary>
        //[Description("http://10.2.78.177/edxmlservices.asmx ")]
        //EdocService,

        /// <summary>
        /// Serivce tìm kiếm
        /// </summary>
        [Description("http://localhost:8983/solr")]
        SearchService,

        ///// <summary>
        ///// Service SSO
        ///// </summary>
        //[Description("http://www.sso.com/SingleSignOnService.svc")]
        //SSOService,

        /// <summary>
        /// File Server
        /// </summary>
        [Description("http://fileservice.com/FileTransfer.svc ")]
        FileServer,

        /// <summary>
        /// MainServer
        /// </summary>
        [Description("https://egov.bkav.com/api/mainserver.svc")]
        MainServer,

        /// <summary>
        /// Service eO
        /// </summary>
        [Description("https://eofficeweb.bkav.com/eoffice.asmx")]
        EoService,
    }
}