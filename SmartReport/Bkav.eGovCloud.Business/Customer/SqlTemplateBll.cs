using Bkav.eGovCloud.Core.Logging;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DatabaseType = Bkav.eGovCloud.Entities.DatabaseType;


namespace Bkav.eGovCloud.Business.Admin
{
    public class SqlTemplateBll : ServiceBase
    {
        private IRepository<SqlTemplate> _biSqlTemplateRepository;
        private IRepository<DataFieldTemplate> _biDataFieldTemplateRepository;
        private IRepository<DataSource> _biDataSourceRepository;

        public SqlTemplateBll(IDbCustomerContext context)
            : base(context)
        {
            _biSqlTemplateRepository = context.GetRepository<SqlTemplate>();
            _biDataFieldTemplateRepository = context.GetRepository<DataFieldTemplate>();
            _biDataSourceRepository = context.GetRepository<DataSource>();
        }

        public IEnumerable<SqlTemplate> Gets(int datasourceId = 0)
        {
            if (datasourceId == 0)
            {
                return null;
            }
            return _biSqlTemplateRepository.Gets(true, d => d.DataSourceId == datasourceId);
        }

        public SqlTemplate Get(int id)
        {
            if (id <= 0) return null;
            return _biSqlTemplateRepository.Get(id);
        }

        public void Create(SqlTemplate entity, out string message)
        {
            message = string.Empty;
            if (entity == null)
            {
                return;
            }

            var datasource = _biDataSourceRepository.Get(true, ds => ds.DataSourceId == entity.DataSourceId);

            if (ValidateQuery(entity.QueryString, datasource.DatabaseType))
            {
                message = "Query không được phép chứa các lệnh thay đổi dữ liệu!";
                throw new ArgumentException("Query không được phép chứa các lệnh thay đổi dữ liệu!");
            }

            var sqlTamplete = _biSqlTemplateRepository.Gets(true, s => s.Name.ToLower().Equals(entity.Name));
            if (sqlTamplete.Any())
            {
                message = "Tên đã tồn tại template đã tồn tại!";
                throw new ArgumentException("Tên đã tồn tại template đã tồn tại!");
            }

            entity.QueryString = entity.QueryString.Replace(";", "");

            DataTable datas = GetDataFieldFromSchema(datasource.ConnectionString, datasource.DatabaseType, entity.QueryString);
            if (ValidateFieldName(datas))
            {
                message = "Tên trường thông tin chưa ký tự đặc biệt!";
                throw new ArgumentException("Tên trường thông tin chưa ký tự đặc biệt!");
            }


            _biSqlTemplateRepository.Create(entity);
            Context.SaveChanges();

            var templateId = entity.TemplateId;


            AddDataFields(templateId, datas);
        }

        /// <summary>
        /// Cập nhật thông tin đường dẫn
        /// </summary>
        /// <param name="domain">Entity domain</param>
        /// <param name="oldDomainName">Tên domain trước khi cập nhật</param>
        /// <param name="domainIds">Mảng id các domain con</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity domain truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi tên domain đã tồn tại</exception>
        public void Update(SqlTemplate sqlTemplate, out string message)
        {
            message = string.Empty;
            if (sqlTemplate == null)
            {
                throw new ArgumentNullException("sqlTemplate");
            }

            var currentTemplate = _biSqlTemplateRepository.Get(false, a => a.TemplateId == sqlTemplate.TemplateId);
            if (currentTemplate == null)
            {
                throw new ArgumentNullException("sqlTemplate");
            }

            var datasource = _biDataSourceRepository.Get(true, ds => ds.DataSourceId == currentTemplate.DataSourceId);

            if (ValidateQuery(sqlTemplate.QueryString, datasource.DatabaseType))
            {
                message = "Query không được phép chứa các lệnh thay đổi dữ liệu!";
                throw new ArgumentException("sqlTemplate");
            }

            var sqlTamplete = _biSqlTemplateRepository.Gets(true, s => s.TemplateId != sqlTemplate.TemplateId && s.Name.ToLower().Equals(sqlTemplate.Name));
            if (sqlTamplete.Any())
            {
                message = "Tên đã tồn tại template đã tồn tại!";
                throw new ArgumentException("sqlTemplate");
            }

            currentTemplate.Name = sqlTemplate.Name;
            currentTemplate.DateModified = DateTime.Now;
            currentTemplate.QueryString = sqlTemplate.QueryString.Replace(";", "");

            DataTable datas = GetDataFieldFromSchema(datasource.ConnectionString, datasource.DatabaseType, currentTemplate.QueryString);

            if (ValidateFieldName(datas))
            {
                message = "Tên trường thông tin chưa ký tự đặc biệt!";
                throw new ArgumentException("sqlTemplate");
            }

            Context.SaveChanges();


            RemoveDataFields(currentTemplate.TemplateId);
            AddDataFields(currentTemplate.TemplateId, datas);
        }

        private bool ValidateQuery(string query, int databaseType)
        {
            List<string> commandList = new List<string>();
            bool isEdit = false;
            switch (databaseType)
            {
                case (int)DatabaseType.MySql:
                    commandList = new List<string>() { "INSERT", "UPDATE", "DELETE", "DROP", "ALTER", "CREATE", "BACKUP" };
                    isEdit = commandList.Any(s => query.Contains(s));
                    break;
                case (int)DatabaseType.SqlServer:
                    commandList = new List<string>() { "INSERT", "UPDATE", "DELETE", "DROP", "ALTER", "CREATE", "BACKUP" };
                    isEdit = commandList.Any(s => query.Contains(s));
                    break;
                case (int)DatabaseType.Oracle:
                    break;
            }

            return isEdit;
        }

        /// <summary>
        /// Xóa bỏ một domain
        /// </summary>
        /// <param name="domain"></param>
        public void Delete(int id)
        {
            var datasource = _biSqlTemplateRepository.Get(false, a => a.TemplateId == id);
            if (datasource == null)
            {
                return;
            }

            RemoveDataFields(datasource.TemplateId);
            _biSqlTemplateRepository.Delete(datasource);
            Context.SaveChanges();
        }

        #region Private

        private DataTable GetDataFieldFromSchema(string connectString, int databaseType, string queryString)
        {
            DataTable returnValue = new DataTable();
            switch (databaseType)
            {
                case (int)DatabaseType.MySql:
                    using (MySqlConnection conn = new MySqlConnection(connectString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(queryString, conn);
                        DbDataAdapter da = new MySqlDataAdapter(cmd);
                        da.FillSchema(returnValue, SchemaType.Source);
                    }
                    break;
                case (int)DatabaseType.SqlServer:
                    using (SqlConnection conn = new SqlConnection(connectString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(queryString, conn);
                        DbDataAdapter da = new SqlDataAdapter(cmd);
                        da.FillSchema(returnValue, SchemaType.Source);
                    }
                    break;
                case (int)DatabaseType.Oracle:
                    break;
            }

            return returnValue;
        }

        private void RemoveDataFields(int templateId)
        {
            var dataFields = _biDataFieldTemplateRepository.Gets(false, tb => tb.TemplateId == templateId);
            foreach (var item in dataFields)
            {
                _biDataFieldTemplateRepository.Delete(item);
            }

            Context.SaveChanges();
        }

        private bool ValidateFieldName(DataTable dataFields)
        {
            List<string> wildcardCharacters = new List<string>() { "*", "?", "[", "!", "-", "#", "%", "]", "^" };
            bool isError = false;
            for (int i = 0; i < dataFields.Columns.Count; i++)
            {
                isError = wildcardCharacters.Any(s => dataFields.Columns[i].ColumnName.Contains(s));
            }

            return isError;
        }

        private void AddDataFields(int templateId, DataTable dataFields)
        {
            DataFieldTemplate dataFieldTemplate;
            for (int i = 0; i < dataFields.Columns.Count; i++)
            {
                dataFieldTemplate = new DataFieldTemplate()
                {
                    TemplateId = templateId,
                    FieldName = dataFields.Columns[i].ColumnName,
                    Datatype = Regex.Replace(dataFields.Columns[i].DataType.ToString().Replace("System.", ""), @"\d", ""),
                };

                _biDataFieldTemplateRepository.Create(dataFieldTemplate);
            }

            Context.SaveChanges();
        }
        #endregion
    }
}
