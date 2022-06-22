using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bkav.eGovCloud.Business.Admin
{
    public class DataFieldBll : ServiceBase
    {

        private readonly IRepository<DataField> _dataFieldRepository;
        private readonly IRepository<Relation> _relationRepository;

        public DataFieldBll(IDbCustomerContext context) : base(context)
        {
            _dataFieldRepository = context.GetRepository<DataField>();
            _relationRepository = context.GetRepository<Relation>();
        }

        public DataField Get(int id)
        {
            if (id <= 0) return null;

            return _dataFieldRepository.Get(id);
        }

        public void Update(DataField dataField)
        {
            if (dataField == null)
            {
                throw new ArgumentNullException("dataField");
            }

            var currentDataField = _dataFieldRepository.Get(false, a => a.DataFieldId == dataField.DataFieldId);

            if (currentDataField != null)
            {
                currentDataField.Description = dataField.Description;
                currentDataField.IsActivated = dataField.IsActivated;
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa bỏ một data field
        /// </summary>
        /// <param name="data field"></param>
        public void Delete(int id)
        {
            var dataField = _dataFieldRepository.Get(false, a => a.DataFieldId == id);
            if (dataField == null)
            {
                return;
            }

            _dataFieldRepository.Delete(dataField);
            Context.SaveChanges();
        }

        public IEnumerable<DataField> Gets(Expression<Func<DataField, bool>> spec = null)
        {
            return _dataFieldRepository.GetsReadOnly(spec);
        }

        public string GetDataFieldName(int? dataFieldId)
        {
            if (!dataFieldId.HasValue)
                return string.Empty;

            var dataField = _dataFieldRepository.Get(dataFieldId);

            if (dataField == null)
                return string.Empty;

            return dataField.FieldName;
        }
    }
}
