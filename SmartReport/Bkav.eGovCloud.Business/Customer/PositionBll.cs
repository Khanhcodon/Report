using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// <para>Class : PositionBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 270812</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Modify Date: 050912</para>
    /// <para>Modifier: GiangPN</para>
    /// <para>Description : BLL tương ứng với bảng Position trong CSDL</para>
    /// </summary>
    public class PositionBll : ServiceBase
    {
        private readonly IRepository<Position> _positionRepository;
        private readonly IRepository<UserDepartmentJobTitlesPosition> _userDepartmentJobTitlesPositionRepository;
        private readonly IRepository<Report> _reportRepository;
        private readonly MemoryCacheManager _cacheManager;
        private readonly IRepository<ProcessFunction> _processFunctionRepository;
        private readonly IRepository<Workflow> _workflowRepository;
        private readonly PermissionSettingBll _permissionSettingService;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="PositionBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="reportService">Bll tương ứng với bảng Report trong CSDL</param>
        /// <param name="processFunctionService">Bll tương ứng với bảng ProcessFunction trong CSDL</param>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="workflowService"></param>
        /// <param name="permissionSettingService"></param>
        /// <param name="resourceService"></param>
        public PositionBll(IDbCustomerContext context,
            ReportBll reportService,
            ProcessFunctionBll processFunctionService,
            MemoryCacheManager cacheManager,
            WorkflowBll workflowService,
            PermissionSettingBll permissionSettingService,
            ResourceBll resourceService)
            : base(context)
        {
            _positionRepository = Context.GetRepository<Position>();
            _userDepartmentJobTitlesPositionRepository = Context.GetRepository<UserDepartmentJobTitlesPosition>();
            _processFunctionRepository = Context.GetRepository<ProcessFunction>();
            _reportRepository = Context.GetRepository<Report>();
            _workflowRepository = Context.GetRepository<Workflow>();
            _cacheManager = cacheManager;
            _permissionSettingService = permissionSettingService;
            _resourceService = resourceService;
        }

        /// <summary>
        /// Lấy ra tất cả chức vụ
        /// </summary>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="positionName">Tên chức vụ (nếu tên chức vụ không bằng trống thì sẽ lấy ra tất cả các chức vụ có tên gần giống với tên chức vụ truyền vào)</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <returns>Danh sách chức vụ</returns>
        public IEnumerable<Position> Gets(string sortBy = "", bool isDescending = false, string positionName = "")
        {
            var spec = !string.IsNullOrWhiteSpace(positionName)
                        ? PositionQuery.ContainsKey(positionName)
                        : null;
            var sort = Context.Filters.CreateSort<Position>(isDescending, sortBy);
            return _positionRepository.GetsReadOnly(spec, sort);
        }
        public IEnumerable<Position> GetAll()
        {
            return _positionRepository.GetsReadOnly();
        }
        /// <summary>
        /// Lấy ra tất cả các chức vụ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Position, TOutput>> projector)
        {
            return _positionRepository.GetsAs(projector);
        }

        /// <summary>
        /// Lấy ra danh sách tất cả các chức vụ (Danh sách này sẽ được cache lại trong 60 phút)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PositionCached> GetCacheAllPosition()
        {
            return _cacheManager.Get(CacheParam.PositionAllKey, CacheParam.PositionAllCacheTimeOut, () =>
            {
                var result = Gets("PositionName");
                return AutoMapper.Mapper.Map<IEnumerable<Position>, IEnumerable<PositionCached>>(result);
            });
        }

		/// <summary>
		/// Trả về tất cả các chức vụ được quyền duyệt trong đơn vị
		/// </summary>
		/// <returns></returns>
		public IEnumerable<PositionCached> GetAllApprovers()
		{
			return GetCacheAllPosition().Where(p => p.IsApproved);
		}

        /// <summary>
        /// Lấy ra một chức vụ
        /// </summary>
        /// <param name="positionId">Id của chức vụ</param>
        /// <returns>Entity chức vụ</returns>
        public Position Get(int positionId)
        {
            Position position = null;
            if (positionId > 0)
            {
                position = _positionRepository.Get(positionId);
            }
            return position;
        }
		
        /// <summary>
        /// Lấy ra mức ưu tiên lớn nhất của chức vụ.
        /// </summary>
        /// <returns>Mức ưu tiên</returns>
        public int GetMaxPriorityLevel()
        {
            var maxPriorityLevel = 0;
            var listLevel = _positionRepository.GetsAs(t => t.PriorityLevel).Distinct();
            if (listLevel.Any())
            {
                maxPriorityLevel = listLevel.Max();
            }
            return maxPriorityLevel;
        }

        /// <summary>
        /// Thêm mới chức vụ
        /// </summary>
        /// <param name="position">Thực thể chức vụ</param>
        /// <returns></returns>
        public void Create(Position position)
        {
            if (position == null)
            {
                throw new ArgumentNullException("position");
            }
            if (_positionRepository.Exist(PositionQuery.WithPositionName(position.PositionName)))
            {
                throw new EgovException(string.Format("Chức vụ ({0}) đã tồn tại!", position.PositionName));
            }
            _positionRepository.Create(position);
            Context.SaveChanges();

            _cacheManager.Remove(CacheParam.PositionAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// Thêm mới chức vụ
        /// </summary>
        /// <param name="positions">Danh sách chức vụ</param>
        /// <param name="ignoreExist">True:Bỏ qua khi tạo mới những chưc vụ đã tồn tại, False: validate khi đã tồn tại</param>
        public void Create(IEnumerable<Position> positions, bool ignoreExist)
        {
            if (positions == null || !positions.Any())
            {
                throw new ArgumentNullException("positions");
            }

            var names = positions.Select(x => x.PositionName);
            var exist = _positionRepository.GetsAs(p => p.PositionName, p => names.Contains(p.PositionName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == positions.Count())
                {
                    throw new EgovException(_resourceService.GetResource("Position.Create.Exist"));
                }

                var list = positions.Where(p => !exist.Contains(p.PositionName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("Position.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(positions);
            }
        }
        public void CreateByAPI(IEnumerable<Position> positions, bool ignoreExist)
        {
            if (positions == null || !positions.Any())
            {
                throw new ArgumentNullException("positions");
            }

            var names = positions.Select(x => x.PositionName);
            var exist = _positionRepository.GetsAs(p => p.PositionName, p => names.Contains(p.PositionName));

            if (exist != null && exist.Any())
            {
                var list = positions.Where(p => !exist.Contains(p.PositionName));
                Create(list);
            }
            else
            {
                Create(positions);
            }
        }


        private void Create(IEnumerable<Position> positions)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in positions)
            {
                _positionRepository.Create(item);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.JobtitlesAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// Cập nhật thông tin chức vụ
        /// </summary>
        /// <param name="position">Entity chức vụ</param>
        /// <param name="oldPositionName">Tên chức vụ trước khi cập nhật</param>
        public void Update(Position position, string oldPositionName)
        {
            if (position == null)
            {
                throw new ArgumentNullException("position");
            }
            if (_positionRepository.Exist(PositionQuery.WithPositionName(position.PositionName).And(r => r.PositionName.ToLower() != oldPositionName.ToLower())))
            {
                throw new EgovException(string.Format("Chức vụ ({0}) đã tồn tại!", position.PositionName));
            }
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.PositionAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// Cập nhật mức ưu tiên
        /// </summary>
        /// <param name="positionIds">Danh sách id của chức vụ theo đúng thứ tự</param>
        public void Update(Dictionary<int, int> positionIds)
        {
            var positions = _positionRepository.Gets(false);
            if (positions != null && positions.Any())
            {
                foreach (var pos in positionIds)
                {
                    var id = pos.Key;
                    var priorityLevel = pos.Value;
                    var positionsUpdate = positions.Single(p => p.PositionId == id);
                    positionsUpdate.PriorityLevel = priorityLevel;

                }
                Context.SaveChanges();
                _cacheManager.Remove(CacheParam.PositionAllKey);
                _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
            }
        }

        /// <summary>
        /// Xóa 1 chức vụ
        /// </summary>
        /// <param name="position">Thực thể chức vụ</param>
        public void Delete(Position position)
        {
            if (position == null)
            {
                throw new ArgumentNullException("position");
            }

            var reports = _reportRepository.Gets(false);
            if (reports != null && reports.Any())
            {
                foreach (var report in reports)
                {
                    if (report.ListDepartmentPositionHasPermission.Any())
                    {
                        report.ListDepartmentPositionHasPermission = report.ListDepartmentPositionHasPermission.Where(dp => dp.ContainsKey("PositionId"))
                                                                        .Where(dp => dp["PositionId"] != position.PositionId)
                                                                        .ToList();
                    }
                    if (report.ListPositionHasPermission.Any())
                    {
                        report.ListPositionHasPermission = report.ListPositionHasPermission.Where(p => p != position.PositionId).ToList();
                    }
                }
            }

            //var processFunctions = _processFunctionRepository.Gets(false);
            var processFunctions = _permissionSettingService.Gets();
            if (processFunctions != null && processFunctions.Any())
            {
                foreach (var processFunction in processFunctions)
                {
                    if (processFunction.ListDepartmentPositionHasPermission.Any())
                    {
                        processFunction.ListDepartmentPositionHasPermission = processFunction.ListDepartmentPositionHasPermission.Where(dp => dp.ContainsKey("PositionId"))
                                                                        .Where(dp => dp["PositionId"] != position.PositionId)
                                                                        .ToList();
                    }
                    if (processFunction.ListPositionHasPermission.Any())
                    {
                        processFunction.ListPositionHasPermission = processFunction.ListPositionHasPermission.Where(p => p != position.PositionId).ToList();
                    }
                }
            }
            var allWorkflows = _workflowRepository.Gets(false);
            if (allWorkflows.Any())
            {
                foreach (var workflow in allWorkflows)
                {
                    var path = workflow.JsonInObject;
                    if (path == null || (path.Nodes == null || !path.Nodes.Any()))
                    {
                        continue;
                    }

                    foreach (var node in path.Nodes)
                    {
                        if (node.Address != null && !node.Address.Any())
                        {
                            continue;
                        }

                        foreach (var address in node.Address)
                        {
                            if (address.LevelQueries != null && address.LevelQueries.Any())
                            {
                                address.LevelQueries =
                                    address.LevelQueries.Where(l => l.PositionId != position.PositionId)
                                        .ToList();
                            }
                            if (address.RelationQueries != null && address.RelationQueries.Any())
                            {
                                address.RelationQueries =
                                    address.RelationQueries.Where(r => r.PositionId != position.PositionId)
                                        .ToList();
                            }
                            if (address.PositionQueries != null && address.PositionQueries.Any())
                            {
                                address.PositionQueries =
                                    address.PositionQueries.Where(p => p.PositionId != position.PositionId)
                                        .ToList();
                            }
                        }
                    }
                    workflow.Json = path.Stringify();
                }
            }

            var userDepartmentJobTitlesPositions = _userDepartmentJobTitlesPositionRepository.Gets(false, x => x.PositionId == position.PositionId);
            if (userDepartmentJobTitlesPositions != null && userDepartmentJobTitlesPositions.Any())
            {
                foreach (var userDepartmentJobTitlesPosition in userDepartmentJobTitlesPositions)
                {
                    _userDepartmentJobTitlesPositionRepository.Delete(userDepartmentJobTitlesPosition);
                }
            }
            _positionRepository.Delete(position);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.PositionAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        public IEnumerable<dynamic> getIDbyNameandLevel(string name, int _level)
        {
            var query = @"SELECT * FROM position WHERE PositionName = @name AND PriorityLevel = @_level LIMIT 1";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("name", name));
            parameters.Add(new SqlParameter("_level", _level));
            var result = Context.RawQuery(query, parameters.ToArray());
            return result;
        }

        public IEnumerable<dynamic> getIDbyName(string name)
        {
            var query = @"SELECT * FROM position WHERE PositionName = @name LIMIT 1";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("name", name));
            var result = Context.RawQuery(query, parameters.ToArray());
            return result;
        }
    }
}
