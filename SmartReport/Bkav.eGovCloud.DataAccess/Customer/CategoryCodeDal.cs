namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CategoryCodeDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : ICategoryCodeDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng CatalogValue trong CSDL
    /// </summary>
    public class CategoryCodeDal : DataAccessBase, ICategoryCodeDal
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public CategoryCodeDal(IDbCustomerContext context)
            : base(context)
        {
        }
    }
}
