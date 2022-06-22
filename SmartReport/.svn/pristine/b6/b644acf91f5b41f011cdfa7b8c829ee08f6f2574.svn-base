#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

#endregion

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>Bkav Corp. - BSO - eGov - eOffice team
    ///   Project: eGov Cloud v1.0
    ///   Class : DocFinishBll - public - Bll
    ///   Access Modifiers: 
    ///   Create Date : 240113
    ///   Author : TienBV@bkav.com</author>
    /// <summary>
    ///   <para> Các hàm xử lý bảng công văn người tham gia </para>
    ///   <para>(TienBv@bkav.com - 240113)</para>
    /// </summary>
    public class DocFinishBll : ServiceBase
    {
        #region Readonly & Static Fields

        private readonly IRepository<DocFinish> _docfinishRepository;

        #endregion

        #region C'tors

        /// <summary>
        ///   C'tor
        /// </summary>
        /// <param name="context"> Context </param>
        public DocFinishBll(IDbCustomerContext context)
            : base(context)
        {
            _docfinishRepository = Context.GetRepository<DocFinish>();
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///   <para> Thêm người đã xem vào công văn. </para>
        ///   (TienBV@bkav.com - 240113)
        /// </summary>
        /// <param name="entity"> entity </param>
        public void Create(DocFinish entity)
        {
            //if (entity == null)
            //{
            //    throw new ArgumentNullException("entity");
            //}

            //var finish = Get(entity.DocumentCopyId, entity.UserId);
            //if (finish == null)
            //{
            //    _docfinishRepository.Create(entity);
            //    Context.SaveChanges();
            //}
            //else
            //{
            //    finish.IsViewed = entity.IsViewed;
            //    if (entity.DocFinishTypeInEnum == DocFinishType.ThamGiaXuLy)
            //    {
            //        finish.DocFinishType = entity.DocFinishType;
            //    }
            //    Context.SaveChanges();
            //}
        }


        /// <summary>
        ///   <para> Thêm người đã xem vào công văn. </para>
        ///   (TienBV@bkav.com - 240113)
        /// </summary>
        /// <param name="entities"> entity </param>
        public void Create(IEnumerable<DocFinish> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }

            foreach (var entity in entities)
            {
                _docfinishRepository.Create(entity);
            }

            Context.SaveChanges();
        }


        /// <summary>
        ///   <para> Xóa 1 bản ghi DocFinish </para>
        ///   (GiangPN@bkav.com - 220213)
        /// </summary>
        /// <param name="docFinish"> entity DocFinish </param>
        public void Delete(DocFinish docFinish)
        {
            if (docFinish == null)
            {
                throw new ArgumentNullException("docFinish");
            }
            _docfinishRepository.Delete(docFinish);
            Context.SaveChanges();
        }

        /// <summary>
        ///   <para> Xóa nhiều bản ghi DocFinish </para>
        ///   (GiangPN@bkav.com - 220213)
        /// </summary>
        public void Delete(IEnumerable<DocFinish> docFinishs)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var docFinish in docFinishs)
            {
                _docfinishRepository.Delete(docFinish);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        /// <summary>
        ///   <para> Kiểm tra quyền view </para>
        /// </summary>
        /// <param name="docId"> Document id </param>
        /// <param name="documentCopyId"> Document copy Id </param>
        /// <param name="userId"> User send id </param>
        /// <param name="type"> </param>
        /// <returns> </returns>
        public bool Exits(Guid docId, int documentCopyId, int userId, DocFinishType type)
        {
            return true;
            //var spec = DocFinishQuery.Exits(docId, documentCopyId, userId, type);
            //return _docfinishRepository.Exist(spec);
        }

        /// <summary>
        ///   Trả về docfinish theo doccopy và user
        /// </summary>
        /// <param name="docCopyId"> </param>
        /// <param name="userId"> </param>
        /// <param name="type"> </param>
        /// <returns> </returns>
        public DocFinish Get(int docCopyId, int userId, DocFinishType type)
        {
            var docFinishType = (int)type;
            return _docfinishRepository.Get(false, c => c.DocumentCopyId == docCopyId && c.UserId == userId && c.DocFinishType == docFinishType);
        }

        /// <summary>
        /// Trả về docfinish theo doccopy và user
        /// </summary>
        /// <param name="docCopyId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DocFinish Get(int docCopyId, int userId)
        {
            return _docfinishRepository.Get(false, c => c.DocumentCopyId == docCopyId && c.UserId == userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docCopyId"></param>
        /// <returns></returns>
        public IEnumerable<DocFinish> Gets(int docCopyId)
        {
            return _docfinishRepository.Gets(true, d => d.DocumentCopyId == docCopyId);
        }

        /// <summary>
        ///   <para> Lấy danh sách DocFinish theo điều kiện truyền vào </para>
        ///   (TrungVH@bkav.com - 040413)
        /// </summary>
        /// <param name="spec"> Điều kiện </param>
        /// <returns> Danh sách DocFinish </returns>
        public IEnumerable<DocFinish> Gets(Expression<Func<DocFinish, bool>> spec)
        {
            return _docfinishRepository.Gets(false, spec);
        }

        /// <summary>
        ///   <para> Lấy danh sách DocFinish theo điều kiện truyền vào </para>
        ///   (TrungVH@bkav.com - 040413)
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="spec"> Điều kiện </param>
        /// <returns> Danh sách DocFinish </returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<DocFinish, T>> projector, Expression<Func<DocFinish, bool>> spec)
        {
            return _docfinishRepository.GetsAs(projector, spec);
        }

        /// <summary>
        ///   Cập nhật
        /// </summary>
        /// <param name="docFinish"> </param>
        public void Update(DocFinish docFinish)
        {
            if (docFinish == null)
            {
                throw new ArgumentNullException("docFinish");
            }
            Context.SaveChanges();
        }

        #endregion
    }
}