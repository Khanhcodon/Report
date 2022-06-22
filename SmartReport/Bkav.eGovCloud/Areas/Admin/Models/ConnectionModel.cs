using System;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Areas.Admin.Validator;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ConnectionValidator))]
    public class ConnectionModel
    {
        public int ConnectionId { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Connection.Fields.DatabaseType.Label")]
        public Entities.DatabaseType DbType { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Connection.Fields.ConnectionName.Label")]
        public string ConnectionName { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Connection.Fields.Server.Label")]
        public string ServerName { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Connection.Fields.Username.Label")]
        public string Username { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Connection.Fields.Password.Label")]
        public string Password { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Connection.Fields.Database.Label")]
        public string Database { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Connection.Fields.Port.Label")]
        public Int16? Port { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Connection.Fields.Port.Label")]
        public bool IseGovOnlineDb { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Connection.Fields.IsCreateDatabase.Label")]
        public bool IsCreateDatabaseIfNotExist { get; set; }

        public string ConnectionRaw { get; set; }
    }
}