using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Bkav.eGovCloud.Core.Logging;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Constant;
using MySql.Data.MySqlClient;
using System.Linq.Expressions;

namespace Bkav.eGovCloud.Business.Admin
{
    public class E_DataTableBll : ServiceBase
    {
        private readonly IRepository<E_DataTable> _dataTableRepository;
        private readonly IRepository<DataField> _dataFieldRepository;
        private readonly IRepository<Relation> _relationRepository;

        public E_DataTableBll(IDbCustomerContext context) : base(context)
        {
            _dataTableRepository = context.GetRepository<E_DataTable>();
            _dataFieldRepository = context.GetRepository<DataField>();
            _relationRepository = context.GetRepository<Relation>();
        }

        public E_DataTable Get(int id)
        {
            if (id <= 0) return null;

            return _dataTableRepository.Get(id);
        }

        public void Update(E_DataTable dataTable)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("dataTable");
            }

            var currentDataTable = _dataTableRepository.Get(false, a => a.DataTableId == dataTable.DataTableId);

            if (currentDataTable != null)
            {
                currentDataTable.Description = dataTable.Description;
                currentDataTable.IsActivated = dataTable.IsActivated;
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa bỏ một datatable
        /// </summary>
        /// <param name="datatable"></param>
        public void Delete(int id)
        {
            var dataTable = _dataTableRepository.Get(false, a => a.DataTableId == id);
            if (dataTable == null)
            {
                return;
            }

            _dataTableRepository.Delete(dataTable);
            Context.SaveChanges();
        }

        public string GetTableQuery(E_DataTable table)
        {
            if (table == null) return null;

            var fields = _dataFieldRepository.GetsReadOnly(f => f.DataTableId == table.DataTableId);
            if (!fields.Any()) return null;

            var relations = _relationRepository.Gets(true, r => r.SourceTableId == table.DataTableId);


            var query = new StringBuilder();
            query.Append("Select ");
            var cols = fields.Select(f => string.Format("{0}.{1} as \"{2}\"", table.Name, f.FieldName, string.IsNullOrWhiteSpace(f.Description) ? f.FieldName : f.Description));



            query.Append(String.Join(", ", cols));

            query.AppendLine("From " + table.Name);

            var orderBy = fields.LastOrDefault(f => f.FieldName.EndsWith("key"));
            if (orderBy != null)
            {
                query.AppendLine(string.Format("Order By {0} Desc ", orderBy.FieldName));
            }

            query.Append("Limit 200;");

            return query.ToString();
        }
        public dynamic GetModel(string query, string connString, int databaseType)
        {
            dynamic result = null;
            switch (databaseType)
            {
                case (int)EnumDatabaseType.MySql:
                    result = GetModelMySql(query, connString, new List<MySqlParameter>());
                    break;
                case (int)EnumDatabaseType.SqlServer:
                    result = GetModelSqlServer(query, connString, new List<SqlParameter>());
                    break;
                case (int)EnumDatabaseType.Oracle:
                    break;
                case (int)EnumDatabaseType.HiveSQL:
                    break;
            }

            return result;
        }

        public dynamic GetModelMySql(string query, string connString, List<MySqlParameter> param)
        {
            using (var context = new EfContext(new MySqlConnection(connString)))
            {
                try
                {
                    var command = "";
                    if (query.StartsWith("call", StringComparison.OrdinalIgnoreCase))
                    {
                        command = query.Split(' ').Last();
                        var result = context.RawProcedure(command, param.ToArray());
                        return result;
                    }
                    else
                    {
                        command = query;
                        var result = context.RawQuery(query, param.ToArray());
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    var msg = new List<string> { ex.Message + ": " + connString, ex.StackTrace };
                    StaticLog.Log(msg);
                    return null;
                }
            }
        }
        public dynamic GetModelSqlServer(string query, string connString, List<SqlParameter> param)
        {
            using (var context = new EfContext(new SqlConnection(connString)))
            {
                try
                {
                    var command = "";
                    if (query.StartsWith("call", StringComparison.OrdinalIgnoreCase))
                    {
                        command = query.Split(' ').Last();
                        var result = context.RawProcedure(command, param.ToArray());
                        return result;
                    }
                    else
                    {
                        command = query;
                        var result = context.RawQuery(query, param.ToArray());
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    var msg = new List<string> { ex.Message, ex.StackTrace };
                    StaticLog.Log(msg);
                    return null;
                }
            }
        }

        public IEnumerable<E_DataTable> Gets(Expression<Func<E_DataTable, bool>> spec = null)
        {
            return _dataTableRepository.GetsReadOnly(spec);
        }

        public string GetDataTableName(int? dataTableId)
        {
            var dataTable = _dataTableRepository.Get(dataTableId);
            if (dataTable == null)
            {
                return string.Empty;
            }

            return dataTable.Name;
        }

        public List<E_DataTable> GetRelationTables(int tableId)
        {
            var list = new List<E_DataTable>();
            var relations = _relationRepository.Gets(true, p => p.SourceTableId == tableId && p.JoinOperators == null);

            if (relations != null)
            {
                E_DataTable tempDataTable = null;
                foreach (var relation in relations)
                {
                    tempDataTable = _dataTableRepository.Get(relation.TargetTableId);
                    if (tempDataTable != null)
                        list.Add(tempDataTable);
                }
            }

            return list;
        }
    }
}
