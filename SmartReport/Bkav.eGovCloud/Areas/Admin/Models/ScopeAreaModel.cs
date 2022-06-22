using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ScopeAreaValidator))]
    public class ScopeAreaModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập ScopeArea Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập từ khóa
        /// </summary>
        [LocalizationDisplayName("ScopeArea.CreateOrEdit.Fields.Key.Label")]
        public string Key { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên scopearea
        /// </summary>
        [LocalizationDisplayName("ScopeArea.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mô tả cho ScopeArea
        /// </summary>
        [LocalizationDisplayName("ScopeArea.CreateOrEdit.Fields.Description.Label")]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách scope được truy cập
        /// </summary>
        public string Scopes { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<int> ClientIds { get; set; }
    }

    public class ClientScopeModel
    {
        public ClientScopeModel(int clientId, int scopeAreaId)
        {
            this.ClientId = clientId;
            this.ScopeAreaId = scopeAreaId;
        }

        /// <summary>
        /// Lấy hoặc thiết lập ClientScope Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ClientId
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ScopeAreaId
        /// </summary>
        public int ScopeAreaId { get; set; }
    }

    /// <summary>
    /// Scope
    /// </summary>
    public class ScopeModel
    {
        public ScopeModel(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Tên scope
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
    }
}