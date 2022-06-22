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
    /// Class : CategoryDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : ICategoryDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Catalog trong CSDL
    /// </summary>
    public class CategoryDal : DataAccessBase, ICategoryDal
    {
        private readonly IRepository<Category> _categoryRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public CategoryDal(IDbCustomerContext context) : base(context)
        {
            _categoryRepository = Context.GetRepository<Category>();
        }

        #pragma warning disable 1591

        public IEnumerable<Category> Gets(Expression<Func<Category, bool>> spec = null, Func<IQueryable<Category>, IQueryable<Category>> preFilter = null, params Func<IQueryable<Category>, IQueryable<Category>>[] postFilters)
        {
            return _categoryRepository.Find(spec, preFilter, postFilters);
        }

        public Category Get(int id)
        {
            return _categoryRepository.One(l => l.CategoryId == id);
        }

        public void Create(Category category)
        {
            _categoryRepository.Create(category);
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }

        public void Delete(Category category)
        {
            _categoryRepository.Delete(category);
        }

        public void Delete(IEnumerable<Category> categorys)
        {
            foreach (var category in categorys)
            {
                _categoryRepository.Delete(category, false);
            }
            Context.SaveChanges();
        }
        public bool Exist(Expression<Func<Category, bool>> spec)
        {
            return _categoryRepository.Any(spec);
        }
        public int Count(Expression<Func<Category, bool>> spec = null)
        {
            return _categoryRepository.Count(spec);
        }
    }
}
