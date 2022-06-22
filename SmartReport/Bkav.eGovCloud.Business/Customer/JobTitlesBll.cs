using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;
using System.Data.SqlClient;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : JobTitlesBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 270812</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Modify Date: 050912</para>
    /// <para>Modifier: GiangPN</para>
    /// <para>Description : BLL tương ứng với bảng JobTitles trong CSDL</para>
    /// </summary>
    public class JobTitlesBll : ServiceBase
    {
        private readonly IRepository<JobTitles> _jobTitlesRepository;
        private readonly IRepository<UserDepartmentJobTitlesPosition> _userDepartmentJobTitlesPositionRepository;
        private readonly MemoryCacheManager _cacheManager;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="JobTitlesBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="resourceService"></param>
        public JobTitlesBll(IDbCustomerContext context, MemoryCacheManager cacheManager, ResourceBll resourceService)
            : base(context)
        {
            _jobTitlesRepository = Context.GetRepository<JobTitles>();
            _userDepartmentJobTitlesPositionRepository = Context.GetRepository<UserDepartmentJobTitlesPosition>();
            _cacheManager = cacheManager;
            _resourceService = resourceService;
        }

        /// <summary>
        /// Lấy ra tất cả chức danh. Kết quả chỉ đọc
        /// </summary>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="jobTitlesName">Tên chức danh (nếu tên chức danh không trống thì sẽ tìm kiếp tất cả chức danh có tên gần giống với tên chức danh truyền vào)</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <returns>Danh sách chức danh</returns>
        public IEnumerable<JobTitles> Gets(string sortBy = "", bool isDescending = false, string jobTitlesName = "")
        {
            var spec = !string.IsNullOrWhiteSpace(jobTitlesName)
                        ? JobTitlesQuery.ContainsKey(jobTitlesName)
                        : null;
            var sort = Context.Filters.CreateSort<JobTitles>(isDescending, sortBy);
            return _jobTitlesRepository.GetsReadOnly(spec, sort);
        }

        public IEnumerable<JobTitles> GetAll()
        {
            return _jobTitlesRepository.GetsReadOnly();
        }

        /// <summary>
        /// Lấy ra danh sách tất cả các chức danh (Danh sách này sẽ được cache lại trong 60 phút)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<JobTitlesCached> GetCacheAllJobtitles()
        {
            return _cacheManager.Get(CacheParam.JobtitlesAllKey, CacheParam.JobtitlesAllCacheTimeOut, () =>
            {
                var result = Gets("JobTitlesName");
                return AutoMapper.Mapper.Map<IEnumerable<JobTitles>, IEnumerable<JobTitlesCached>>(result);
            });
        }

        /// <summary>
        /// Lấy ra một chức danh
        /// </summary>
        /// <param name="jobTitlesId">Id của chức danh</param>
        /// <returns>Entity chức danh</returns>
        public JobTitles Get(int jobTitlesId)
        {
            JobTitles jobTitles = null;
            if (jobTitlesId > 0)
            {
                jobTitles = _jobTitlesRepository.Get(jobTitlesId);
            }
            return jobTitles;
        }

        /// <summary>
        /// Lấy ra mức ưu tiên lớn nhất của chức danh.
        /// </summary>
        /// <returns>Mức ưu tiên</returns>
        public int GetMaxPriorityLevel()
        {
            var maxPriorityLevel = 0;
            var listLevel = _jobTitlesRepository.GetsAs(t => t.PriorityLevel).Distinct();
            if (listLevel.Any())
            {
                maxPriorityLevel = listLevel.Max();
            }
            return maxPriorityLevel;
        }

        /// <summary>
        /// Thêm mới chức danh
        /// </summary>
        /// <param name="jobTitles">Thực thể chức danh</param>
        /// <returns></returns>
        public void Create(JobTitles jobTitles)
        {
            if (jobTitles == null)
            {
                throw new ArgumentNullException("jobTitles");
            }
            if (_jobTitlesRepository.Exist(JobTitlesQuery.WithJobTitlesName(jobTitles.JobTitlesName)))
            {
                throw new EgovException(string.Format("chức danh ({0}) đã tồn tại!", jobTitles.JobTitlesName));
            }
            _jobTitlesRepository.Create(jobTitles);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.JobtitlesAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// Tạo mới chức danh
        /// </summary>
        /// <param name="jobTitles">Danh sách chức danh</param>
        /// <param name="ignoreExist"></param>
        public void Create(IEnumerable<JobTitles> jobTitles, bool ignoreExist)
        {
            if (jobTitles == null || !jobTitles.Any())
            {
                throw new ArgumentNullException("jobTitles");
            }
            var names = jobTitles.Select(x => x.JobTitlesName);
            var exist = _jobTitlesRepository.GetsAs(p => p.JobTitlesName, p => names.Contains(p.JobTitlesName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == jobTitles.Count())
                {
                    throw new EgovException(_resourceService.GetResource("JobTitles.Create.Exist"));
                }

                var list = jobTitles.Where(p => !exist.Contains(p.JobTitlesName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("JobTitles.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(jobTitles);
            }
        }

        public void CreateByAPI(IEnumerable<JobTitles> jobtitles, bool ignoreExist)
        {
            if (jobtitles == null || !jobtitles.Any())
            {
                throw new ArgumentNullException("jobtitles");
            }

            var names = jobtitles.Select(x => x.JobTitlesName);
            var exist = _jobTitlesRepository.GetsAs(p => p.JobTitlesName, p => names.Contains(p.JobTitlesName));

            if (exist != null && exist.Any())
            {
                var list = jobtitles.Where(p => !exist.Contains(p.JobTitlesName));
                Create(list);
            }
            else
            {
                Create(jobtitles);
            }
        }

        private void Create(IEnumerable<JobTitles> jobTitles)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in jobTitles)
            {
                _jobTitlesRepository.Create(item);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.JobtitlesAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// Cập nhật thông tin chức danh
        /// </summary>
        /// <param name="jobTitles">Entity chức danh</param>
        /// <param name="oldJobTitlesName">Tên chức danh trước khi cập nhật</param>
        public void Update(JobTitles jobTitles, string oldJobTitlesName)
        {
            if (jobTitles == null)
            {
                throw new ArgumentNullException("jobTitles");
            }
            if (_jobTitlesRepository.Exist(JobTitlesQuery.WithJobTitlesName(jobTitles.JobTitlesName).And(r => r.JobTitlesName.ToLower() != oldJobTitlesName.ToLower())))
            {
                throw new EgovException(string.Format("Chức danh ({0}) đã tồn tại!", jobTitles.JobTitlesName));
            }
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.JobtitlesAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// Cập nhật mức ưu tiên
        /// </summary>
        /// <param name="ids">Danh sách id của chức danh theo đúng thứ tự</param>
        public void UpdatePriority(int[] ids)
        {
            var jobtitles = _jobTitlesRepository.Gets(false);
            if (jobtitles != null && jobtitles.Any())
            {
                var priorityLevel = 0;
                foreach (var id in ids)
                {
                    priorityLevel++;
                    var jobtitlesUpdate = jobtitles.Single(j => j.JobTitlesId == id);
                    jobtitlesUpdate.PriorityLevel = priorityLevel;

                }
                Context.SaveChanges();
                _cacheManager.Remove(CacheParam.JobtitlesAllKey);
                _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
            }
        }

        /// <summary>
        /// Xóa 1 chức danh
        /// </summary>
        /// <param name="jobTitles">Thực thể chức danh</param>
        public void Delete(JobTitles jobTitles)
        {
            if (jobTitles == null)
            {
                throw new ArgumentNullException("jobTitles");
            }
            var userDepartmentJobTitlesPositions = _userDepartmentJobTitlesPositionRepository.Gets(false, x => x.JobTitlesId == jobTitles.JobTitlesId);
            if (userDepartmentJobTitlesPositions != null && userDepartmentJobTitlesPositions.Any())
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (var userDepartmentJobTitlesPosition in userDepartmentJobTitlesPositions)
                {
                    _userDepartmentJobTitlesPositionRepository.Delete(userDepartmentJobTitlesPosition);
                }
                Context.Configuration.AutoDetectChangesEnabled = false;
            }
            _jobTitlesRepository.Delete(jobTitles);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.JobtitlesAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }
        public IEnumerable<dynamic> getIDbyName(string name, int  level)
        {
            var query = @"SELECT * FROM jobtitles WHERE  JobTitlesName = @name and PriorityLevel = @level LIMIT 1";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("name", name));
            parameters.Add(new SqlParameter("level", level));
            var result = Context.RawQuery(query, parameters.ToArray());
            return result;
        }
    }
}
