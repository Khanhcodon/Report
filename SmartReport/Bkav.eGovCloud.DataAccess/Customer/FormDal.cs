using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IFormDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Form trong CSDL
    /// </summary>
    public class FormDal : DataAccessBase, IFormDal
    {
        private IRepository<Form> _formRepository;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public FormDal(IDbCustomerContext context) : base(context)
        {
            _formRepository = Context.GetRepository<Form>();
        }

#pragma warning disable 1591
        
        public void Create(Form form)
        {
            _formRepository.Create(form);
        }

        public Form One(Guid id)
        {
            return _formRepository.One(f => f.FormId == id);
        }

        public void Delete(Form form)
        {
            _formRepository.Delete(form);
        }

        public bool IsUsed(Guid formId)
        {
            return false;
        }

        public void Update(Form form)
        {
            _formRepository.Update(form);
        }

        public void UnActiveOthers(Guid doctypeId, Guid currentId)
        {
            var forms = _formRepository
                .Find(
                    f => f.DocTypeId == doctypeId 
                    && f.IsPrimary 
                    && f.IsActivated == 1
                    && f.FormId != currentId)
                    .ToList();
            if (forms.Any())
            {
                foreach(var form in forms)
                {
                    form.IsActivated = 2;
                    _formRepository.Update(form);
                }
            }
        }

        public bool HasTmp(Guid doctypeId)
        {
            return _formRepository.Any(f => f.IsPrimary && f.IsActivated == 3 && f.DocTypeId == doctypeId);
        }

        public IEnumerable<Form> Gets(Expression<Func<Form, bool>> spec)
        {
            return _formRepository.Find(spec);
        }

        public IEnumerable<Form> Gets(Expression<Func<Form, bool>> spec = null, Func<IQueryable<Form>, IQueryable<Form>> preFilter = null, params Func<IQueryable<Form>, IQueryable<Form>>[] postFilters)
        {
            return _formRepository.Find(spec, preFilter, postFilters);
        }

        public int Count(Expression<Func<Form, bool>> spec = null)
        {
            return _formRepository.Count(spec);
        }
    }
}
