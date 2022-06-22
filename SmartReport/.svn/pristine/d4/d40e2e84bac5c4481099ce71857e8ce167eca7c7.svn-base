using System;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    ///<para> Bkav Corp. - BSO - eGov - eOffice team</para>
    ///<para> Project: eGov Cloud v1.0</para>
    ///<para> Class : CodeBll - public - BLL</para>
    ///<para> Access Modifiers: </para>
    ///<para> Create Date : 061212</para>
    ///<para> Author      : TienBV</para>
    ///<para> Description : BLL tương ứng với bảng DocRelation trong CSDL</para>
    /// </summary>
    public class DocRelationBll : ServiceBase
    {
        // CuongNT@bkav.com - 190713: DocRelation không phải là một nghiệp vụ độc lập cần quan tâm bởi người viết Presentation, mà chỉ là xử lý trung gian --> Không cần xây dựng một Bll cho nó.
        // Vẫn để lại lớp này để mọi người lưu ý, hoặc có thể bàn lại nếu thấy chưa hợp lý.
        #region Readonly & Static Fields

        private readonly IRepository<DocRelation> _docRelationRepository;

        #endregion

        #region C'tors

        /// <summary>
        ///   C'tor
        /// </summary>
        /// <param name="context"> Context </param>
        public DocRelationBll(IDbCustomerContext context)
            : base(context)
        {
            _docRelationRepository = Context.GetRepository<DocRelation>();
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docRelation"></param>
        public void Create(DocRelation docRelation)
        {
            if (docRelation == null)
            {
                throw new ArgumentNullException("docRelation");
            }

            _docRelationRepository.Create(docRelation);
            Context.SaveChanges();
        }

        #endregion

    }
}
