using System;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;
using System.Collections.Generic;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDocumentPublishDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 100114
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng DocumentPublish trong CSDL
    /// </summary>
    public interface IDocumentPublishDal
    {
        /// <summary>
        /// Thêm đối tượng văn bản phát hành
        /// </summary>
        /// <param name="documentPublish">đối tượng văn bản phát hành</param>
        void Create(DocumentPublish documentPublish);

    }
}
