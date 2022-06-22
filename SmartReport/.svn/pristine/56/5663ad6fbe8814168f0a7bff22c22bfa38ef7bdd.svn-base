using Bkav.eGovCloud.Business.Admin.ParseQuery;
using Bkav.eGovCloud.Business.BI.ParseQuery;
using Bkav.eGovCloud.Core.Logging;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DatabaseType = Bkav.eGovCloud.Entities.DatabaseType;

namespace Bkav.eGovCloud.Business.Admin
{
    public class DataSourceBll : ServiceBase
    {
        private IRepository<DataSource> _dataSourceRepository;
        private IRepository<E_DataTable> _dataTableRepository;
        private IRepository<DataField> _dataFieldRepository;
        private IRepository<SqlTemplate> _templateRepository;
        private IRepository<DataFieldTemplate> _dataFieldTemplateRepository;
        private IRepository<DataFieldMath> _dataFieldMathRepository;
        private IRepository<Relation> _relationRepository;

        private IParseQuery parseQuery;

        public DataSourceBll(IDbCustomerContext context)
            : base(context)
        {
            _dataSourceRepository = context.GetRepository<DataSource>();
            _dataTableRepository = context.GetRepository<E_DataTable>();
            _dataFieldRepository = context.GetRepository<DataField>();
            _dataFieldMathRepository = context.GetRepository<DataFieldMath>();
            _templateRepository = context.GetRepository<SqlTemplate>();
            _dataFieldTemplateRepository = context.GetRepository<DataFieldTemplate>();
            _relationRepository = context.GetRepository<Relation>();
        }

        #region Data Source
        /// <summary> Tienbv 081112
        /// Lấy ra danh sách các mẫu form theo điều kiện kỹ thuật truyền vào. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="isReadOnly"></param>
        public IEnumerable<DataSource> Gets(Expression<Func<DataSource, bool>> spec = null, bool isReadOnly = true)
        {
            return _dataSourceRepository.Gets(isReadOnly, spec);
        }

        public DataSource Get(int id)
        {
            if (id <= 0) return null;
            return _dataSourceRepository.Get(id);
        }

        public void Create(DataSource entity)
        {
            if (entity == null)
            {
                return;
            }

            string connectionStr = string.Empty;
            switch (entity.DatabaseType)
            {
                case (int)DatabaseType.MySql:
                    connectionStr = ConnectionUtil.GenerateMySqlConnectionString(entity.Server, entity.DatabaseName, entity.Username, entity.Password, entity.Port);
                    break;
                case (int)DatabaseType.SqlServer:
                    connectionStr = ConnectionUtil.GenerateSqlConnectionString(entity.Server, entity.DatabaseName, entity.Username, entity.Password);
                    break;
                case (int)DatabaseType.Oracle:
                    break;
            }

            entity.ConnectionString = connectionStr;
            _dataSourceRepository.Create(entity);
            Context.SaveChanges();

            var dsId = entity.DataSourceId;

            var newTableIds = SyncTables(dsId);

            foreach (var tableId in newTableIds)
            {
                SyncFields(tableId);
            }
        }

        /// <summary>
        /// Cập nhật thông tin đường dẫn
        /// </summary>
        /// <param name="domain">Entity domain</param>
        /// <param name="oldDomainName">Tên domain trước khi cập nhật</param>
        /// <param name="domainIds">Mảng id các domain con</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity domain truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi tên domain đã tồn tại</exception>
        public void Update(DataSource dataSource)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException("dataSource");
            }

            var currentDataSource = _dataSourceRepository.Get(false, a => a.DataSourceId == dataSource.DataSourceId);

            if (currentDataSource != null)
            {
                currentDataSource.Name = dataSource.Name;
                currentDataSource.DateModified = DateTime.Now;
                currentDataSource.Customer = dataSource.Customer;
                currentDataSource.DatabaseType = dataSource.DatabaseType;
                currentDataSource.Server = dataSource.Server;
                currentDataSource.Port = dataSource.Port;
                currentDataSource.DatabaseName = dataSource.DatabaseName;
                currentDataSource.Username = dataSource.Username;
                currentDataSource.Password = dataSource.Password;
                currentDataSource.Description = dataSource.Description;
            }

            string connectionStr = string.Empty;
            switch (currentDataSource.DatabaseType)
            {
                case (int)DatabaseType.MySql:
                    connectionStr = ConnectionUtil.GenerateMySqlConnectionString(currentDataSource.Server, currentDataSource.DatabaseName, currentDataSource.Username, currentDataSource.Password, currentDataSource.Port);
                    break;
                case (int)DatabaseType.SqlServer:
                    connectionStr = ConnectionUtil.GenerateSqlConnectionString(currentDataSource.Server, currentDataSource.DatabaseName, currentDataSource.Username, currentDataSource.Password);
                    break;
                case (int)DatabaseType.Oracle:
                    break;
            }

            currentDataSource.ConnectionString = connectionStr;

            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa bỏ một domain
        /// </summary>
        /// <param name="domain"></param>
        public void Delete(int id)
        {
            var datasource = _dataSourceRepository.Get(false, a => a.DataSourceId == id);
            if (datasource == null)
            {
                return;
            }

            _dataSourceRepository.Delete(datasource);
            Context.SaveChanges();
        }
        #endregion

        #region Data Tables

        public IEnumerable<E_DataTable> GetTables(int datasourceId)
        {
            return _dataTableRepository.Gets(true, tb => tb.DataSourceId == datasourceId);
        }

        public E_DataTable GetTable(int datatableId)
        {
            if (datatableId <= 0) return null;
            return _dataTableRepository.Get(datatableId);
        }

        public IEnumerable<int> SyncTables(int datasourceId)
        {
            var result = new List<int>();

            var datasource = Get(datasourceId);
            if (datasource == null) return result;

            var connection = datasource.ConnectionString;
            var newTables = GetTables(connection, datasource.DatabaseType);
            if (!newTables.Any()) return result;

            var currentTables = GetTables(datasourceId);
            var hasCurrent = currentTables.Any();

            foreach (var table in newTables)
            {
                if (!hasCurrent || !currentTables.Any(tb => tb.Name.Equals(table.Name)))
                {
                    table.DataSourceId = datasourceId;
                    _dataTableRepository.Create(table);
                    Context.SaveChanges();

                    // Sync các trường trong bảng
                    SyncFields(table.DataTableId);

                    result.Add(table.DataTableId);
                }

                continue;
            }

            return result;
        }

        public IEnumerable<Relation> GetRelations(int sourceId)
        {
            return _relationRepository.GetsReadOnly(r => r.SourceTableId == sourceId);   
        }

        public int GetJoinId(int sourceTableId)
        {
            if (_relationRepository.Gets(false, p => p.SourceTableId == sourceTableId).Count() > 0)
                return _relationRepository.Gets(false).Max(x => x.JoinId);
            else
                return 0;
        }
        public Relation GetRelation(int relationId)
        {
            return _relationRepository.Get(relationId);
        }

        public void DeleteRelation(Relation relation)
        {
            _relationRepository.Delete(relation);
            var remainRelations = _relationRepository.Gets(false, p => p.SourceTableId == relation.SourceTableId
                && p.RelationId != relation.RelationId && p.JoinId == relation.JoinId);
            if (remainRelations.Count() > 0)
                remainRelations.ElementAt(0).JoinOperators = null;

            Context.SaveChanges();
        }

        #endregion

        #region Template

        public SqlTemplate GetTemplate(int templateId)
        {
            return _templateRepository.Get(true, tb => tb.TemplateId == templateId);
        }

        public IEnumerable<SqlTemplate> GetTemplates(int datasourceId)
        {
            return _templateRepository.Gets(true, tb => tb.DataSourceId == datasourceId);
        }

        public IEnumerable<DataField> GetTemplateDetails(int templateId)
        {
            var dataFieldTemplates = _dataFieldTemplateRepository.Gets(true, tb => tb.TemplateId == templateId);

            return dataFieldTemplates.Select(s => new DataField
            {
                DataFieldId = s.DataFieldTemplateId,
                Datatype = s.Datatype,
                FieldName = s.FieldName
            });
        }

        public List<object> GetFieldTemplateValue(int id, string formula)
        {
            var result = new List<object>();
            var datafield = _dataFieldTemplateRepository.Get(id);
            if (datafield == null)
            {
                return result;
            }

            var template = _templateRepository.Get(datafield.TemplateId);
            if (template == null)
            {
                return result;
            }

            var dataSource = _dataSourceRepository.Get(template.DataSourceId);
            if (dataSource == null)
            {
                return result;
            }

            var command = string.Empty;
            try
            {
                switch (dataSource.DatabaseType)
                {
                    case (int)DatabaseType.MySql:
                        parseQuery = new ParseQueryMySQL();
                        command = parseQuery.ParseQueryTemplateGetValue(formula, datafield.FieldName, datafield.Datatype, template.QueryString);
                        using (var context = new EfContext(new MySqlConnection(dataSource.ConnectionString)))
                        {
                            var dataTables = context.RawQuery(command);
                            result = Json2.ParseAs<List<object>>(Json2.Stringify(dataTables));
                        }
                        break;
                    case (int)DatabaseType.SqlServer:
                        break;
                    case (int)DatabaseType.Oracle:
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = new List<string>();
                msg.Add(ex.Message);
                msg.Add(ex.StackTrace);
                StaticLog.Log(msg);
                result = null;
            }

            return result;
        }

        #endregion

        #region Data Fields

        public IEnumerable<DataField> GetFields(int datatableId, bool hasReadOnly = true)
        {
            return _dataFieldRepository.Gets(hasReadOnly, df => df.DataTableId == datatableId);
        }

        public IEnumerable<DataFieldMath> GetFieldMaths(int datatableId)
        {
            return _dataFieldMathRepository.Gets(true, df => df.DataTableId == datatableId);
        }

        public void SyncFields(int datatableId)
        {
            var datatable = GetTable(datatableId);
            if (datatable == null) return;

            var datasource = Get(datatable.DataSourceId);
            if (datasource == null) return;

            var connection = datasource.ConnectionString;
            var fields = GetFields(connection, datatable.Name, datasource.DatabaseType);
            if (!fields.Any()) return;

            var currentFields = GetFields(datatableId, false);
            var hasCurrent = currentFields.Any();

            foreach (var field in fields)
            {
                DataField currentField = hasCurrent ? currentFields.SingleOrDefault(tb => tb.FieldName.Equals(field.FieldName)) : null;

                if (currentField == null)
                {
                    field.DataTableId = datatableId;
                    _dataFieldRepository.Create(field);
                }else
                {
                    currentField.Datatype = field.Datatype;
                }

                continue;
            }

            foreach(var oldField in currentFields)
            {
                if(!fields.Any(f => f.FieldName.Equals(oldField.FieldName)))
                {
                    _dataFieldRepository.Delete(oldField);
                }
            }

            Context.SaveChanges();
        }

        public List<object> GetFieldValue(int id, string formula)
        {
            var result = new List<object>();
            var datafield = _dataFieldRepository.Get(id);
            if (datafield == null)
            {
                return result;
            }

            var dataTable = _dataTableRepository.Get(datafield.DataTableId);
            if (dataTable == null)
            {
                return result;
            }

            var dataSource = _dataSourceRepository.Get(dataTable.DataSourceId);
            if (dataSource == null)
            {
                return result;
            }

            var command = string.Empty;


            try
            {
                switch (dataSource.DatabaseType)
                {
                    case (int)DatabaseType.MySql:

                        parseQuery = new ParseQueryMySQL();
                        //command = parseQuery.ParseQueryGetValues(formula, datafield.FieldName, datafield.Datatype, dataTable.Name);
                        //using (var context = new EfContext(new MySqlConnection(dataSource.ConnectionString)))
                        //{
                        //    var dataTables = context.RawQuery(command);
                        //    result = Json2.ParseAs<List<object>>(Json2.Stringify(dataTables));
                        //}
                        break;
                    case (int)DatabaseType.SqlServer:
                        break;
                    case (int)DatabaseType.Oracle:
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = new List<string>();
                msg.Add(ex.Message);
                msg.Add(ex.StackTrace);
                StaticLog.Log(msg);
                result = null;
            }

            return result;
        }

        #endregion

        #region Relation

        public void AddRelation(Relation entity)
        {
            _relationRepository.Create(entity);
            Context.SaveChanges();
        }

        #endregion

        #region Private

        private IEnumerable<E_DataTable> GetTables(string connection, int databaseType)
        {
            string command = string.Empty;
            switch (databaseType)
            {
                case (int)DatabaseType.MySql:
                    command = "SELECT TABLE_NAME as Name, TABLE_COMMENT as Description FROM information_schema.tables WHERE TABLE_SCHEMA ='{0}';";
                    using (var context = new EfContext(new MySqlConnection(connection)))
                    {
                        var databaseName = context.Database.Connection.Database;
                        command = string.Format(command, databaseName);

                        return GetTableDetails(context, connection, command);
                    }
                case (int)DatabaseType.SqlServer:
                    command = "SELECT TABLE_NAME as Name FROM information_schema.tables;";
                    using (var context = new EfContext(new SqlConnection(connection)))
                    {
                        return GetTableDetails(context, connection, command);
                    }

                default:
                    return null;
            }

        }

        private static IEnumerable<E_DataTable> GetTableDetails(EfContext context, string connection, string command)
        {
            try
            {
                var dataTables = context.RawQuery(command);
                var result = Json2.ParseAs<IEnumerable<E_DataTable>>(Json2.Stringify(dataTables));
                return result;
            }
            catch (Exception ex)
            {
                var msg = new List<string>();
                msg.Add(ex.Message + ", connection: " + connection);
                msg.Add(ex.StackTrace);
                StaticLog.Log(msg);
                return null;
            }
        }

        private IEnumerable<DataField> GetFields(string connection, string tableName, int databaseType)
        {
            string command = string.Empty;
            switch (databaseType)
            {
                case (int)DatabaseType.MySql:
                    command = @"SELECT COLUMN_Name as 'FieldName', COLUMN_COMMENT as 'Description', DATA_TYPE as 'Datatype', ORDINAL_POSITION as 'Order' FROM information_schema.columns WHERE table_schema = '{0}' AND table_name = '{1}';";
                    using (var context = new EfContext(new MySqlConnection(connection)))
                    {
                        var databaseName = context.Database.Connection.Database;
                        command = string.Format(command, databaseName, tableName);

                        return GetFieldDetails(context, connection, tableName, command);
                    }
                case (int)DatabaseType.SqlServer:
                    command = @"SELECT COLUMN_Name as 'FieldName', DATA_TYPE as 'Datatype', ORDINAL_POSITION as 'Order' FROM information_schema.columns WHERE table_name = '{0}';";
                    using (var context = new EfContext(new SqlConnection(connection)))
                    {
                        command = string.Format(command, tableName);

                        return GetFieldDetails(context, connection, tableName, command);
                    }

                default:
                    return null;
            }
        }

        private static IEnumerable<DataField> GetFieldDetails(EfContext context, string connection, string tableName, string command)
        {
            try
            {
                var dataFields = context.RawQuery(command);
                var result = Json2.ParseAs<IEnumerable<DataField>>(Json2.Stringify(dataFields));
                return result;
            }
            catch (Exception ex)
            {
                var msg = new List<string>();
                msg.Add(ex.Message + ", connection: " + connection);
                msg.Add(ex.StackTrace);
                StaticLog.Log(msg);
                return null;
            }
        }

        #endregion
    }
}
