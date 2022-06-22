using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eGov team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : FormGroupBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 30122014</para>
    /// <para>Author      : QuangP</para>
    /// <para>Description : BLL tương ứng với bảng EgovJob trong CSDL</para>
    /// </summary>
    public class EgovJobBll : ServiceBase
    {
        private readonly IRepository<EgovJob> _EgovJobRepository;
        private readonly DocumentBll _documentService;
        private readonly DocumentCopyBll _documentCopyService;
        private readonly EdocBll _edocService;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="resourceService"></param>
        /// <param name="documentService"></param>
        /// <param name="documentCopyService"></param>
        /// <param name="edocService"></param>
        public EgovJobBll(IDbCustomerContext context, ResourceBll resourceService, DocumentBll documentService, DocumentCopyBll documentCopyService, EdocBll edocService)
            : base(context)
        {
            _EgovJobRepository = Context.GetRepository<EgovJob>();
            _documentService = documentService;
            _documentCopyService = documentCopyService;
            _edocService = edocService;
            _resourceService = resourceService;
        }

        /// <summary>
        /// Lấy ra tất cả EgovJob theo điều kiện truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<EgovJob> Gets(Expression<Func<EgovJob, bool>> spec)
        {
            return _EgovJobRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra tất cả EgovJob theo điều kiện truyền vào
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EgovJob> Gets()
        {
            return _EgovJobRepository.Gets(false);
        }

        /// <summary>
        /// Lấy ra EgovJob theo id
        /// </summary>
        /// <returns></returns>
        public EgovJob Get(int id)
        {
            return _EgovJobRepository.Get(id);
        }

        /// <summary>
        /// Thêm mới EgovJob
        /// </summary>
        /// <param name="entity">Entity EgovJob</param>
        public void Create(EgovJob entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _EgovJobRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity">Entity EgovJob</param>
        public void Update(EgovJob entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Chạy 1 Job
        /// </summary>
        /// <param name="entity"></param>
        public void ExecuteJob(EgovJob entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.LastRun = DateTime.Now;
            entity.NextRun = DateTime.Now.AddSeconds(entity.Interval);
            Context.SaveChanges();
        }

        /// <summary>
        /// Dừng job nếu lỗi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="error"></param>
        public void StopJobForError(EgovJob entity, string error)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            //entity.IsActivated = false;
            //entity.LastError = DateTime.Now.ToString() + error;
            Context.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity">Entity EgovJob</param>
        public void Delete(EgovJob entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _EgovJobRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Đồng bộ lượng timer trong enum và db
        /// </summary>
        public void SyncJob()
        {
            var jobsInDb = Gets();
            var jobsInEnum = _resourceService.EnumToSelectList<EgovJobEnum>();
            if (jobsInEnum.Any())
            {
                foreach (var timerInEnum in jobsInEnum)
                {
                    var flag = false;
                    foreach (var timerInDb in jobsInDb)
                    {
                        if (timerInDb.JobType == Convert.ToInt32(timerInEnum.Value))
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        Create(new EgovJob
                        {
                            Interval = 0,
                            JobType = Convert.ToInt32(timerInEnum.Value),
                            Name = timerInEnum.Text,
                            IsActivated = false,
                        });
                    }
                }
            }

            if (jobsInDb.Any())
            {
                foreach (var timerInDb in jobsInDb)
                {
                    var flag = false;
                    foreach (var timerInEnum in jobsInEnum)
                    {
                        if (timerInDb.JobType == Convert.ToInt32(timerInEnum.Value))
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        Delete(timerInDb);
                    }
                }
            }
        }
    }
}