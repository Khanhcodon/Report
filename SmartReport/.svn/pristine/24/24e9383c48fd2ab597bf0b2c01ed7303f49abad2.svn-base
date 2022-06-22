using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Interface : ISupplementaryDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 240113
    /// <para></para> Author      : TienBV
    /// <para></para> Description : DAL tương ứng với bảng Supplementary trong CSDL
    /// </summary>
    public class SupplementaryDal : ISupplementaryDal
    {
        private readonly IRepository<Supplementary> _supplementaryRepository;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="context"></param>
        public SupplementaryDal(IDbCustomerContext context)
        {
            _supplementaryRepository = context.GetRepository<Supplementary>();
        }

        /// <summary>
        /// Lấy danh sách các yêu cầu bổ sung theo điều kiện truyền vào.
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<Supplementary> Gets(Expression<Func<Supplementary, bool>> spec = null)
        {
            return _supplementaryRepository.Find(spec);
        }

        /// <summary>
        /// Thêm yêu cầu bổ sung
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Supplementary entity)
        {
            _supplementaryRepository.Create(entity);
        }

        /// <summary>
        /// Cập nhật yêu cầu, kêt quả bổ sung.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Supplementary entity)
        {
            _supplementaryRepository.Update(entity);
        }

        /// <summary>
        /// Trả về các yêu cầu bổ sung của doc copy tương ứng.
        /// </summary>
        /// <param name="docCopyId">Document copy id</param>
        /// <returns></returns>
        public Supplementary GetByDocCopy(int docCopyId)
        {
            return _supplementaryRepository.One(s => s.DocumentCopyId == docCopyId && !s.IsSuccess);
        }

        /// <summary>
        /// Trả về yêu cầu bổ sung theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Supplementary Get(int id)
        {
            return _supplementaryRepository.One(id);
        }

        /// <summary>
        /// Trả về yêu cầu bổ sung chưa đc tiếp nhận.
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="docCopyId"></param>
        /// <returns></returns>
        public Supplementary Get(Guid docId, int docCopyId)
        {
            var docCopyStr = string.Format(";{0};", docCopyId.ToString());
            return _supplementaryRepository.One(s => s.DocumentId == docId && s.DocumentCopyIds.Contains(docCopyStr));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<Supplementary, bool>> spec)
        {
            return _supplementaryRepository.Any(spec);
        }
    }
}
