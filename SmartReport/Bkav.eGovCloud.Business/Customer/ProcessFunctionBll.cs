using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.FileTransfer;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Customer
{
	/// <summary>
	/// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
	/// <para>Project: eGov Cloud v1.0</para>
	/// <para>Class : ProcessFunctionBll - public - Bll</para>
	/// <para>Access Modifiers: </para>
	/// <para>Create Date : 111212</para>
	/// <para>Author      : TrungVH</para>
	/// <para>Description : Bll tương ứng với bảng ProcessFunction trong CSDL, dùng để cấu hình các node trong cây văn bản đang xử lý trên trang chủ</para>
	/// </summary>
	public class ProcessFunctionBll : ServiceBase
	{
		private readonly IRepository<ProcessFunction> _processFunctionRepository;
		private readonly UserBll _userService;
		private readonly DepartmentBll _departmentService;
		private readonly IRepository<ProcessFunctionAndFilter> _processFunctionAndFilterRepository;
		private readonly IRepository<ProcessFunctionFilter> _processFilterRepository;
		private readonly IRepository<ProcessFunctionGroup> _processGroupRepository;
		private readonly IRepository<ProcessFunctionType> _processTypeRepository;
		private readonly AuthorizeBll _authorizeService;
		private readonly FileLocationBll _fileLocationService;
		private readonly FileLocationSettings _fileLocationSettings;
		private readonly TreeGroupBll _treeGroupService;
		private readonly PermissionSettingBll _permissionSettingService;
		private readonly DocColumnSettingBll _docColumnSettingService;
		private readonly MemoryCacheManager _cacheManager;

		/// <summary>
		/// Khởi tạo
		/// </summary>
		/// <param name="context">Context</param>
		/// <param name="departmentService">Bll tương ứng với bảng Department trong CSDL</param>
		/// <param name="userService">Bll tương ứng với bảng User trong CSDL</param>
		/// <param name="authorizeService">Bll tương ứng với bảng Authorize trong CSDL</param>
		/// <param name="fileLocationService"></param>
		/// <param name="fileLocationSettings"></param>
		/// <param name="treeGroupService"></param>
		/// <param name="permissionSettingService"></param>
		/// <param name="docColumnSettingService"></param>
		/// <param name="cacheManager"></param>
		public ProcessFunctionBll(IDbCustomerContext context,
			DepartmentBll departmentService,
			UserBll userService,
			AuthorizeBll authorizeService,
			FileLocationBll fileLocationService,
			FileLocationSettings fileLocationSettings,
			TreeGroupBll treeGroupService,
			PermissionSettingBll permissionSettingService,
			DocColumnSettingBll docColumnSettingService,
			MemoryCacheManager cacheManager)
			: base(context)
		{
			_processFunctionRepository = Context.GetRepository<ProcessFunction>();
			_processFunctionAndFilterRepository = Context.GetRepository<ProcessFunctionAndFilter>();
			_processFilterRepository = Context.GetRepository<ProcessFunctionFilter>();
			_processGroupRepository = Context.GetRepository<ProcessFunctionGroup>();
			_processTypeRepository = Context.GetRepository<ProcessFunctionType>();
			_userService = userService;
			departmentService.ProcessFunctionService = this;
			_departmentService = departmentService;
			_authorizeService = authorizeService;
			_fileLocationService = fileLocationService;
			_fileLocationSettings = fileLocationSettings;
			_treeGroupService = treeGroupService;
			_permissionSettingService = permissionSettingService;
			_docColumnSettingService = docColumnSettingService;
			_cacheManager = cacheManager;
		}

		/// <summary>
		/// Lấy ra tất cả các funtion
		/// </summary>
		/// <param name="spec">Điều kiện</param>
		/// <returns>Danh sách funtion</returns>
		public IEnumerable<ProcessFunction> Gets(Expression<Func<ProcessFunction, bool>> spec = null)
		{
			return _processFunctionRepository.Gets(false, spec);
		}

		/// <summary>
		/// Lấy ra tất cả các funtion
		/// </summary>
		/// <param name="spec">Điều kiện</param>
		/// <returns>Danh sách funtion</returns>
		public IEnumerable<ProcessFunction> GetsReadOnly(Expression<Func<ProcessFunction, bool>> spec = null)
		{
			return _processFunctionRepository.GetsReadOnly(spec);
		}

		/// <summary>
		/// Lấy tất cả cấu hình đưa và cache
		/// </summary>
		/// <param name="isActivated"></param>
		/// <returns></returns>
		public IEnumerable<ProcessFunction> GetAllCaches(bool? isActivated = null)
		{
			var cfgs = _cacheManager.Get(CacheParam.FunctionAllKey, CacheParam.FunctionAllCacheTimeOut, () =>
			{
				var result = GetsReadOnly();
				return AutoMapper.Mapper.Map<IEnumerable<ProcessFunction>, IEnumerable<ProcessFunctionCached>>(result);
			});

			cfgs = cfgs.Where(p => !isActivated.HasValue || p.IsActivated == isActivated);

			return AutoMapper.Mapper.Map<IEnumerable<ProcessFunctionCached>, IEnumerable<ProcessFunction>>(cfgs); ;
		}

		/// <summary>
		/// Lấy ra tất cả các funtion
		/// </summary>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="spec">Điều kiện</param>
		/// <returns>Danh sách funtion</returns>
		public IEnumerable<T> GetsAs<T>(Expression<Func<ProcessFunction, T>> projector, Expression<Func<ProcessFunction, bool>> spec = null)
		{
			return _processFunctionRepository.GetsAs(projector, spec);
		}

		/// <summary>
		/// Lấy ra funtion theo id
		/// </summary>
		/// <returns>Funtion</returns>
		public ProcessFunction Get(int id)
		{
			ProcessFunction result = null;
			if (id > 0)
			{
				result = _processFunctionRepository.Get(id);
			}
			return result;
		}

		/// <summary>
		/// Trả về node trên cây văn bản từ cache
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ProcessFunction GetFromCache(int id)
		{
			return GetAllCaches().SingleOrDefault(p => p.ProcessFunctionId == id);
		}

		/// <summary>
		/// Lấy ra tất cả các function (các thuộc tính cần thiết để load được tree)
		/// <para> HopCV sửa</para>
		/// <para> Date:020414</para>
		/// <para> Desc: Chỉnh sửa lấy thêm trường Type</para>
		/// </summary>
		/// <returns>Chuỗi json</returns>
		public string GetsForTree(int type = 0)
		{
			return GetAllCaches().Select(
					f =>
					new
					{
						id = f.ProcessFunctionId,
						name = f.Name,
						parentid = f.ParentId,
						icon = f.Icon,
						isActivated = f.IsActivated,
						order = f.Order,
						color = f.Color,
						type = f.Type,
						treeGroupId = f.TreeGroupId
					}).StringifyJs();
		}

		/// <summary>
		/// Lấy ra các function con tương ứng với Id của function cha
		/// </summary>
		/// <param name="parentId">Id function cha</param>
		/// <returns>Chuỗi json</returns>
		public List<dynamic> GetsByParentId(int parentId = 0)
		{
			var user = _userService.CurrentUser;
			var userid = user.UserId;

			var allTreeGroups = _treeGroupService.GetCacheAllTreeGroups(true);
            var allFunctions = GetAllCaches().Where(f => f.IsActivated && f.Type == (int)ProcessFunctionTypes.VanBan);

            // Người xem node
            var allPermission = _permissionSettingService.GetCacheAllPermissionSettings();
            var functions = allFunctions.Where(f => (parentId != 0 && f.ParentId == parentId) || (parentId == 0 && !f.ParentId.HasValue));

			var result = new List<dynamic>();
			foreach (var function in functions)
			{
                var permission = allPermission.FirstOrDefault(p => p.PermissionSettingId == function.PermissionSettingId);
                if (permission != null)
                {
                    if (!HasPermission(permission.ListUserHasPermission,
                                permission.ListPositionHasPermission,
                            permission.ListDepartmentPositionHasPermission, user.UserId))
                    {
                        continue;
                    }
                }

                var treeGroup = allTreeGroups.FirstOrDefault(p => p.TreeGroupId == function.TreeGroupId);
                if (treeGroup == null)
                {
                    continue;
                }

                if (!function.ProcessFunctionTypeId.HasValue)
                {
                    result.Add(
                    new
                    {
                        functionId = function.ProcessFunctionId,
                        id = function.ProcessFunctionId,
                        name = function.Name,
                        @params = "[]",
                        parentId = function.ParentId.HasValue ? function.ParentId.Value : 0,
                        icon = function.Icon,
                        state = allFunctions.Any(f => f.ParentId == function.ProcessFunctionId) ? "closed" : "leaf",
                        color = function.Color,
                        order = function.Order,
                        showTotalInTreeType = function.ShowTotalInTreeType,
                        totalDocumentUnread = 0,
                        totalDocument = 0,
                        defaultSort = "[]",
                        hasUyQuyen = function.HasUyQuyen,
                        userUyQuyen = 0,
                        hasTransferTheoLo = false, //function.HasTransferTheoLo
                        treeGroupId = function.TreeGroupId,
                        treeGroupOrder = 1,
                        hasExportFile = false // function.HasExportFile
                    });

                    continue;
                }
                
                // Xử lý node con đặc biệt
                var type = _processTypeRepository.Get(function.ProcessFunctionTypeId.Value);
                if (type == null)
                {
                    continue;
                }

                var specialChildren = Context.RawQuery(type.Query, new SqlParameter("@userId", userid)) as IEnumerable<IDictionary<string, object>>;
                if (specialChildren == null || !specialChildren.Any())
                {
                    continue;
                }

                var firstDataOfQuery = specialChildren.First();
                if (!firstDataOfQuery.ContainsKey(type.TextField))
                {
                    throw new Exception("Cấu hình sai tên trường hiển thị cho loại node " + type.Name);
                }

                if (type.ListParam.Any(parameter => !firstDataOfQuery.ContainsKey(parameter.ValueField)))
                {
                    throw new Exception("Cấu hình sai tên trường giá trị cho loại node " + type.Name);
                }
                var index = 1;
                foreach (var child in specialChildren)
                {
                    var parameters = new List<Object> { new SqlParameter("@userId", userid), new SqlParameter("@quicksearch", "") };

                    var paramQueryString = new List<ObjectParams>();
                    for (var i = 0; i < type.ListParam.Count; i++)
                    {
                        parameters.Add(new SqlParameter(type.ListParam[i].ParamName, child[type.ListParam[i].ValueField]));
                        paramQueryString.Add(new ObjectParams
                        {
                            Key = type.ListParam[i].ParamName,
                            Value = child[type.ListParam[i].ValueField].ToString(),
                            CompareName = type.ListParam[i].CompareName
                        });
                    }

                    index++;
                    var strIndex = index.ToString();
                    //int totalDocumentUnread = 0,
                    //    totalDocument = 0;
                    var userUyQuyen = new List<int>();
                    result.Add(
                        new
                        {
                            functionId = int.Parse(function.ProcessFunctionId + strIndex),
                            funtionIdNote = function.ProcessFunctionId,
                            id = int.Parse(function.ProcessFunctionId + strIndex),
                            name = child[type.TextField],
                            parentId = function.ProcessFunctionId,
                            icon = function.Icon,
                            @params = paramQueryString.StringifyJs(),
                            state = "leaf",
                            color = function.Color,
                            order = function.Order,
                            showTotalInTreeType = function.ShowTotalInTreeType,
                            totalDocumentUnread = 0,
                            totalDocument = 0,
                            defaultSort = "[]",
                            hasUyQuyen = function.HasUyQuyen,
                            userUyQuyen = userUyQuyen,
                            hasTransferTheoLo = false,
                            treeGroupId = treeGroup.TreeGroupId,
                            treeGroupOrder = treeGroup.Order,
                            hasExportFile = false //function.HasExportFile
                        });
                }
            }

			return result.OrderBy(p => p.GetType().GetProperty("order").GetValue(p, null)).ToList();
		}

		/// <summary>
		/// Trả về danh sách các node văn bản theo user hiện tại.
		/// </summary>
		/// <param name="parentId"></param>
		/// <returns></returns>
		public List<dynamic> GetsFromCache(int parentId = 0)
		{
			var currentUser = _userService.CurrentUser;

			var cacheParams = string.Format(CacheParam.FunctionUserKey, currentUser.UserId, parentId);

			var functions = _cacheManager.Get(cacheParams, CacheParam.FunctionUserKeyCacheTimeOut, () =>
			{
				return GetsByParentId(parentId);
			});

			return functions.ToList();
		}

		/// <summary>
		/// HopCV: 210715
		/// Note: Lấy các node có hướng chuyển theo lô (chưa làm đc lưu cache phần hướng chuyển theo lô nên tạm thời gọi vể server lấy dữ liệu phần này)
		/// Lấy ra các function con tương ứng với Id của function cha
		/// </summary>
		/// <param name="id">Id function cha</param>
		/// <param name="userid">Id của người dùng</param>
		/// <returns>Chuỗi json</returns>
		public List<dynamic> GetsTransferTheoLoByParentId(int? id, int userid)
		{
			var allTreeGroups = _treeGroupService.GetCacheAllTreeGroups(true);
			var allFunctions = GetAllCaches().Where(f => f.IsActivated && f.Type == (int)ProcessFunctionTypes.VanBan);
			var functions = allFunctions.Where(
											f =>
												((id.HasValue && f.ParentId == id.Value)
												|| (!id.HasValue && !f.ParentId.HasValue))
												&& f.HasTransferTheoLo);
			var result = new List<dynamic>();
			var user = _userService.CurrentUser;
			var allPermiss = _permissionSettingService.GetCacheAllPermissionSettings();
			var allDocColumns = _docColumnSettingService.GetAllCaches();

			IEnumerable<int> authorizeUserIds = null;
			if (functions.Count(p => p.HasUyQuyen) > 0)
			{
				authorizeUserIds = _authorizeService.GetAuthorizeUsers(user.UserId, Guid.Empty);
			}

			foreach (var function in functions)
			{
				var permis = allPermiss.FirstOrDefault(p => p.PermissionSettingId == function.PermissionSettingId);// function.PermissionSetting;
				if (permis != null)
				{
					if (!HasPermission(permis.ListUserHasPermission,
						permis.ListPositionHasPermission,
						permis.ListDepartmentPositionHasPermission, user.UserId))
					{
						continue;
					}
				}

				var columnSetting = allDocColumns.FirstOrDefault(p => p.DocColumnSettingId == function.DocColumnSettingId);// function.DocColumnSetting;

				if (function.ProcessFunctionTypeId.HasValue)
				{
					var type = function.ProcessFunctionType;
					if (type != null)
					{
						var dataOfQuery = Context.RawQuery(type.Query, new SqlParameter("@userId", userid)) as IEnumerable<IDictionary<string, object>>;
						if (dataOfQuery != null && dataOfQuery.Any())
						{
							var firstDataOfQuery = dataOfQuery.First();
							if (!firstDataOfQuery.ContainsKey(type.TextField))
							{
								throw new Exception("Cấu hình sai tên trường hiển thị cho loại node " + type.Name);
							}

							if (type.ListParam.Any(parameter => !firstDataOfQuery.ContainsKey(parameter.ValueField)))
							{
								throw new Exception("Cấu hình sai tên trường giá trị cho loại node " + type.Name);
							}

							foreach (var data in dataOfQuery)
							{
								var parameters = new List<Object> { new SqlParameter("@userId", userid), new SqlParameter("@quicksearch", "") };
								// HopCV: 080415
								// Lấy danh sách parameter
								var paramQueryString = new List<ObjectParams>();
								for (var i = 0; i < type.ListParam.Count; i++)
								{
									parameters.Add(new SqlParameter(type.ListParam[i].ParamName, data[type.ListParam[i].ValueField]));
									paramQueryString.Add(new ObjectParams
									{
										Key = type.ListParam[i].ParamName,
										Value = data[type.ListParam[i].ValueField].ToString(),
										CompareName = type.ListParam[i].CompareName
									});
								}

								int totalDocumentUnread = 0,
									totalDocument = 0;
								var userUyQuyen = new List<int>();
								if (function.HasUyQuyen)
								{
									if (authorizeUserIds != null && authorizeUserIds.Any())
									{
										totalDocumentUnread = GetTotalDocumentUnreadUyQuyen(function.QueryCountItemUnread, authorizeUserIds, parameters.ToArray());
										totalDocument = GetTotalDocumentUyQuyen(function.QueryCountAllItems, authorizeUserIds, parameters.ToArray());
										userUyQuyen.AddRange(authorizeUserIds);
									}
								}
								else
								{
									totalDocumentUnread = GetTotalDocumentUnread(function.QueryCountItemUnread, parameters.ToArray());
									totalDocument = GetTotalDocument(function.QueryCountAllItems, parameters.ToArray());
								}

								result.Add(
									new
									{
										functionId = function.ProcessFunctionId,
										id = function.ProcessFunctionId,
										name = data[type.TextField],
										parentId = id.Value,
										icon = function.Icon,
										@params = paramQueryString.StringifyJs(),
										state = allFunctions.Any(f => f.ParentId == function.ProcessFunctionId)
												? "closed"
												: "leaf",
										color = function.Color,
										order = function.Order,
										showTotalInTreeType = function.ShowTotalInTreeType,
										totalDocumentUnread = totalDocumentUnread,
										totalDocument = totalDocument,
										defaultSort = columnSetting == null ? "[]" : columnSetting.SortColumn,
										hasUyQuyen = function.HasUyQuyen,
										userUyQuyen = userUyQuyen,
										hasTransferTheoLo = function.HasTransferTheoLo,
										treeGroupId = function.TreeGroupId,
										treeGroupOrder = allTreeGroups.First(p => p.TreeGroupId == function.TreeGroupId).Order,
										hasExportFile = function.HasExportFile
									});
							}
						}
					}
				}
				else
				{
					var parameters = new List<Object> { new SqlParameter("@userId", userid), new SqlParameter("@quicksearch", "") };

					int totalDocumentUnread = 0,
						totalDocument = 0;
					var userUyQuyen = new List<int>();

					if (function.HasUyQuyen)
					{
						if (authorizeUserIds != null && authorizeUserIds.Any())
						{
							totalDocumentUnread = GetTotalDocumentUnreadUyQuyen(function.QueryCountItemUnread, authorizeUserIds, parameters.ToArray());
							totalDocument = GetTotalDocumentUyQuyen(function.QueryCountAllItems, authorizeUserIds, parameters.ToArray());
							userUyQuyen.AddRange(authorizeUserIds);
						}
					}
					else
					{
						totalDocumentUnread = GetTotalDocumentUnread(function.QueryCountItemUnread, parameters.ToArray());
						totalDocument = GetTotalDocument(function.QueryCountAllItems, parameters.ToArray());
					}

					result.Add(
						new
						{
							functionId = function.ProcessFunctionId,
							id = function.ProcessFunctionId,
							name = function.Name,
							@params = "[]",
							parentId = function.ParentId.HasValue ? function.ParentId.Value : 0,
							icon = function.Icon,
							state = allFunctions.Any(f => f.ParentId == function.ProcessFunctionId) ? "closed" : "leaf",
							color = function.Color,
							order = function.Order,
							showTotalInTreeType = function.ShowTotalInTreeType,
							totalDocumentUnread = totalDocumentUnread,
							totalDocument = totalDocument,
							defaultSort = columnSetting == null ? "[]" : columnSetting.SortColumn,
							hasUyQuyen = function.HasUyQuyen,
							userUyQuyen = userUyQuyen,
							hasTransferTheoLo = function.HasTransferTheoLo,
							treeGroupId = function.TreeGroupId,
							treeGroupOrder = allTreeGroups.First(p => p.TreeGroupId == function.TreeGroupId).Order,
							hasExportFile = function.HasExportFile
						});
				}
			}

			return result;
		}

		/// <summary>
		/// Trả về cây văn bản
		/// </summary>
		/// <returns></returns>
		public List<dynamic> GetDocumentTree()
		{
			var allTreeGroups = _treeGroupService.GetCacheAllTreeGroups(true);
			var allFunctions = GetAllCaches().Where(f => f.IsActivated && f.Type == (int)ProcessFunctionTypes.VanBan);
			var result = new List<dynamic>();
			var user = _userService.CurrentUser;
			var allPermiss = _permissionSettingService.GetCacheAllPermissionSettings();
			var allDocColumns = _docColumnSettingService.GetAllCaches();

			foreach (var function in allFunctions)
			{
				// Kiểm tra quyền xem node
				var permis = allPermiss.FirstOrDefault(p => p.PermissionSettingId == function.PermissionSettingId);// function.PermissionSetting;
				if (permis != null)
				{
					if (!HasPermission(permis.ListUserHasPermission,
						permis.ListPositionHasPermission,
						permis.ListDepartmentPositionHasPermission, user.UserId))
					{
						continue;
					}
				}

				var columnSetting = allDocColumns.FirstOrDefault(p => p.DocColumnSettingId == function.DocColumnSettingId);// function.DocColumnSetting;
				var userUyQuyen = new List<int>();

				if (function.HasUyQuyen)
				{
					var authorizeUserIds = _authorizeService.GetAuthorizeUsers(user.UserId, Guid.Empty);
					if (authorizeUserIds != null && authorizeUserIds.Any())
					{
						userUyQuyen.AddRange(authorizeUserIds);
					}
				}

				var newNode = new
				{
					functionId = function.ProcessFunctionId,
					//id = function.ProcessFunctionId,
					name = function.Name,
					parentid = function.ParentId,
					icon = function.Icon,
					state = allFunctions.Any(f => f.ParentId == function.ProcessFunctionId) ? "closed" : "leaf",
					color = function.Color,
					order = function.Order,
					//group = function.ProcessFunctionGroupId,
					isActive = function.IsActivated,
					filter = GetFilters(function.ProcessFunctionId),
					columnSetting = columnSetting == null ? "[]" : columnSetting.DisplayColumn,
					showTotalInTreeType = function.ShowTotalInTreeType,
					defaultSort = columnSetting == null ? "[]" : columnSetting.SortColumn,
					hasUyQuyen = function.HasUyQuyen,
					userUyQuyen = userUyQuyen,
					hasTransferTheoLo = function.HasTransferTheoLo,
					treeGroupId = function.TreeGroupId,
					treeGroupOrder = allTreeGroups.First(p => p.TreeGroupId == function.TreeGroupId).Order,
					hasExportFile = function.HasExportFile
				};

				result.Add(newNode);
			}
			return result;
		}

		/// <summary>
		/// Trả về danh sách các node có sự thay đổi trên cây văn bản.
		/// </summary>
		/// <param name="lastUpdate">Thời điểm đồng bộ gần nhất dưới client</param>
		/// <returns></returns>
		public object SyncDocumentTree(DateTime lastUpdate)
		{
			var allTreeGroups = _treeGroupService.GetCacheAllTreeGroups(true);
			var allFunctions = GetAllCaches().Where(f => f.IsActivated && f.Type == (int)ProcessFunctionTypes.VanBan);

			// ParentId == 0: lấy ra danh sách các node root - node có parentId = null
			// parentId != 0: lấy ra danh sách các node có ParentId = parentId.
			var functions = allFunctions.Where(f => f.DateModified > lastUpdate);
			var result = new List<dynamic>();
			var user = _userService.CurrentUser;
			var allPermiss = _permissionSettingService.GetCacheAllPermissionSettings();
			var allDocColumns = _docColumnSettingService.GetAllCaches();

			IEnumerable<int> authorizeUserIds = null;
			if (functions.Count(p => p.HasUyQuyen) > 0)
			{
				authorizeUserIds = _authorizeService.GetAuthorizeUsers(user.UserId, Guid.Empty);
			}

			foreach (var function in functions)
			{
				var permis = allPermiss.FirstOrDefault(p => p.PermissionSettingId == function.PermissionSettingId);// function.PermissionSetting;
				if (permis != null)
				{
					if (!HasPermission(permis.ListUserHasPermission,
						permis.ListPositionHasPermission,
						permis.ListDepartmentPositionHasPermission, user.UserId))
					{
						continue;
					}
				}
				var columnSetting = allDocColumns.FirstOrDefault(p => p.DocColumnSettingId == function.DocColumnSettingId);// function.DocColumnSetting;
				var userUyQuyen = new List<int>();

				if (function.HasUyQuyen)
				{
					if (authorizeUserIds != null && authorizeUserIds.Any())
					{
						userUyQuyen.AddRange(authorizeUserIds);
					}
				}

				var newNode = new
				{
					functionId = function.ProcessFunctionId,
					name = function.Name,
					parentid = function.ParentId,
					icon = function.Icon,
					state = allFunctions.Any(f => f.ParentId == function.ProcessFunctionId) ? "closed" : "leaf",
					color = function.Color,
					order = function.Order,
					//group = function.ProcessFunctionGroupId,
					filter = GetFilters(function.ProcessFunctionId),
					isActive = function.IsActivated,
					columnSetting = columnSetting == null ? "[]" : columnSetting.DisplayColumn,
					showTotalInTreeType = function.ShowTotalInTreeType,
					defaultSort = columnSetting == null ? "[]" : columnSetting.SortColumn,
					hasUyQuyen = function.HasUyQuyen,
					userUyQuyen = userUyQuyen.StringifyJs(),
					hasTransferTheoLo = function.HasTransferTheoLo,
					treeGroupId = function.TreeGroupId,
					treeGroupOrder = allTreeGroups.First(p => p.TreeGroupId == function.TreeGroupId).Order,
					hasExportFile = function.HasExportFile
				};

				result.Add(newNode);
			}
			return result;
		}

		private List<dynamic> GetFilters(int functionId)
		{
			var result = new List<dynamic>();
			var processFilterIds = _processFunctionAndFilterRepository
										.Gets(true, i => i.ProcessFunctionId == functionId)
										.Select(i => i.ProcessFunctionFilterId);
			var processFilters = _processFilterRepository.Gets(true, p => processFilterIds.Contains(p.ProcessFunctionFilterId));
			foreach (var filter in processFilters)
			{
				filter.Value = GetFilterValue(filter);
				result.Add(filter);
			}
			return result;
		}

		private string GetFilterValue(ProcessFunctionFilter filter)
		{
			if (filter.IsSqlValue)
			{
				var dataValue = Context.RawQuery(filter.Value).ToList();
				return Json2.Stringify(dataValue);
			}
			return filter.Value.Replace("@userId", _userService.CurrentUser.UserId.ToString());
		}

		/// <summary>
		/// Lấy ra tổng số các văn bản hồ sơ chưa đọc của các function truyền vào
		/// </summary>
		/// <param name="functionIds">Id các function</param>
		/// <param name="userid">Id người dùng hiện tại</param>
		/// <returns>Json</returns>
		public List<dynamic> GetTotalDocumentUnreadMultiFunction(IEnumerable<int> functionIds, int userid)
		{
			var allFunctions = GetAllCaches().Where(f => f.IsActivated && functionIds.Contains(f.ProcessFunctionId));
			var result = new List<dynamic>();

			IEnumerable<int> authorizeUserIds = null;
			if (allFunctions.Any(p => p.HasUyQuyen))
			{
				authorizeUserIds = _authorizeService.GetAuthorizeUsers(userid, Guid.Empty);
			}

			// Todo: cần merge tất cả query cần xử lý và run 1 lần vào db

			foreach (var function in allFunctions)
			{
				var hasShowUnread = function.ShowTotalInTreeType == (int)DisplayTreeType.UnreadOnAll || function.ShowTotalInTreeType == (int)DisplayTreeType.Unread;
				var hasShowTotal = function.ShowTotalInTreeType == (int)DisplayTreeType.UnreadOnAll || function.ShowTotalInTreeType == (int)DisplayTreeType.All;

				if (function.HasUyQuyen)
				{
					var parameters = new List<Object> { new SqlParameter("@userId", userid), new SqlParameter("@quicksearch", "") };
					result.Add(new
					{
						id = function.ProcessFunctionId,
						totalUnread = hasShowUnread ? GetTotalDocumentUnreadUyQuyen(function.QueryCountItemUnread, authorizeUserIds, parameters.ToArray()) : 0,
						total = hasShowTotal ? GetTotalDocumentUyQuyen(function.QueryCountAllItems, authorizeUserIds, parameters.ToArray()) : 0
					});

					continue;
				}

				var isSpecialNode = function.ProcessFunctionTypeId.HasValue;
				if (!isSpecialNode)
				{

					var parameters = new List<Object> { new SqlParameter("@userId", userid), new SqlParameter("@quicksearch", "") };
					result.Add(new
					{
						id = function.ProcessFunctionId,
						totalUnread = hasShowUnread ? GetTotalDocumentUnread(function.QueryCountItemUnread, parameters.ToArray()) : 0,
						total = hasShowTotal ? GetTotalDocument(function.QueryCountAllItems, parameters.ToArray()) : 0
					});

					continue;
				}

                // Xử lý node con đặc biệt
                var type = _processTypeRepository.Get(function.ProcessFunctionTypeId.Value);
                if (type == null)
                {
                    continue;
                }

				var dataOfQuery = Context.RawQuery(type.Query, new SqlParameter("@userId", userid)) as IEnumerable<IDictionary<string, object>>;
				if (dataOfQuery != null && dataOfQuery.Any())
				{
					var firstDataOfQuery = dataOfQuery.First();
					if (type.ListParam.Any(parameter => !firstDataOfQuery.ContainsKey(parameter.ValueField)))
					{
						throw new Exception("Cấu hình sai tên trường giá trị cho loại node " + type.Name);
					}

					foreach (var data in dataOfQuery)
					{
						var parameters = new List<Object> { new SqlParameter("@userId", userid), new SqlParameter("@quicksearch", "") };
						var paramQueryString = string.Empty;
						var paramQueryString2 = new List<ObjectParams>();

						for (var i = 0; i < type.ListParam.Count; i++)
						{
							parameters.Add(new SqlParameter(type.ListParam[i].ParamName, data[type.ListParam[i].ValueField]));

							//HopCV: Thay paramQueryString bằng paramQueryString2 lấy danh sách parameter
							paramQueryString2.Add(new ObjectParams
							{
								Key = type.ListParam[i].ParamName,
								Value = data[type.ListParam[i].ValueField].ToString(),
								CompareName = type.ListParam[i].CompareName
							});
						}
						result.Add(new
						{
							id = function.ProcessFunctionId,
							param = paramQueryString2.Stringify(),
							totalUnread = hasShowUnread ? GetTotalDocumentUnread(function.QueryCountItemUnread, parameters.ToArray()) : 0,
							total = hasShowTotal ? GetTotalDocument(function.QueryCountAllItems, parameters.ToArray()) : 0
						});
					}
				}
			}

			return result;
		}

		private IEnumerable<int> GetDepartmentChildrenId(int parentId, IEnumerable<DepartmentCached> allDepartment)
		{
			var result = new List<int>();
			var children = allDepartment.Where(d => d.ParentId == parentId);
			if (children.Any())
			{
				result.AddRange(children.Select(d => d.DepartmentId));
				foreach (var department in children)
				{
					result.AddRange(GetDepartmentChildrenId(department.DepartmentId, allDepartment));
				}
			}
			return result;
		}

		/// <summary>
		/// Lấy ra tổng số bản ghi chưa đọc tương ứng với từng function
		/// </summary>
		/// <param name="function">Entity function</param>
		/// <param name="userid">Id của người dùng</param>
		/// <param name="paramValue">Giá trị tham số</param>
		/// <returns>Tổng số bản ghi chưa đọc</returns>
		public int GetTotalDocumentUnread(ProcessFunction function, int userid,
			IDictionary<string, string> paramValue)
		{
			var paramUser = new SqlParameter("@userId", userid);
			if (!function.ProcessFunctionTypeId.HasValue)
			{
				if (function.HasUyQuyen)
				{
					var authorizeUserIds = _authorizeService.GetAuthorizeUsers(userid, Guid.Empty);
					if (authorizeUserIds == null || !authorizeUserIds.Any())
					{
						return 0;
					}
					return GetTotalDocumentUnreadUyQuyen(function.QueryCountItemUnread, authorizeUserIds, paramUser);
				}
				return GetTotalDocumentUnread(function.QueryCountItemUnread, paramUser);
			}

			if (paramValue == null || !paramValue.Any())
			{
				return 0;
			}

			var type = function.ProcessFunctionType;
			if (type == null)
			{
				return 0;
			}

			var parameters = new List<Object> { paramUser };
			if (type.ListParam.Any())
			{
				foreach (var parameter in type.ListParam)
				{
					if (!paramValue.ContainsKey(parameter.ParamName))
					{
						throw new Exception("Không tồn tại giá trị cho tham số " + parameter.ParamName);
					}
					parameters.Add(new SqlParameter(parameter.ParamName, paramValue[parameter.ParamName]));
				}
			}

			if (function.HasUyQuyen)
			{
				var authorizeUserIds = _authorizeService.GetAuthorizeUsers(userid, Guid.Empty);
				if (authorizeUserIds == null || !authorizeUserIds.Any())
				{
					return 0;
				}
				return GetTotalDocumentUnreadUyQuyen(function.QueryCountItemUnread, authorizeUserIds, parameters.ToArray());
			}

			return GetTotalDocumentUnread(function.QueryCountItemUnread, parameters.ToArray());
		}

		/// <summary>
		/// Lấy ra danh sách các văn bản hổ sơ mới nhất theo đúng cấu hình của function
		/// </summary>
		/// <param name="removedocumentCopyIds">Danh sách các DocumentCopyId sẽ bị xóa khỏi danh sách hiện tại</param>
		/// <param name="function">Entity function</param>
		/// <param name="paramValue">Giá trị truyền vào cho cho parameter của loại function (nếu function thuộc 1 loại function nào đó)</param>
		/// <param name="toDate">Tới ngày</param>
		/// <param name="currentDocumentCopyIds">Các DocumentCopyId trên danh sách hiện tại</param>
		/// <param name="quickSearch">Chuỗi tìm kiếm nhanh trong trích yếu...</param>
		/// <returns>Danh sách các Dictionary, mỗi 1 dictionary là 1 row trong trong kết quả lấy được, dictionary có dạng: 'columnName:columnValue'</returns>
		public IEnumerable<IDictionary<string, object>> GetDocumentLatestByFunction(
			  out IEnumerable<int> removedocumentCopyIds,
			  ProcessFunction function,
			  DateTime toDate,
			  IEnumerable<int> currentDocumentCopyIds,
			  IDictionary<string, string> paramValue = null,
			  string quickSearch = null)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}

			removedocumentCopyIds = new List<int>();
			var user = _userService.CurrentUser;
			var permis = _permissionSettingService.GetCacheAllPermissionSettings().FirstOrDefault(p => p.PermissionSettingId == function.PermissionSettingId);

			if (permis != null)
			{
				if (!HasPermission(permis.ListUserHasPermission,
					permis.ListPositionHasPermission,
					permis.ListDepartmentPositionHasPermission, user.UserId))
				{
					return new List<IDictionary<string, object>>();
				}
			}

			#region parameters

			var parameters = new List<Object>
			{
				new SqlParameter("@toDate", toDate),
				new SqlParameter("@quicksearch", string.IsNullOrEmpty(quickSearch) ? string.Empty : "%" + quickSearch + "%")
			};

			if (paramValue != null && paramValue.Any() && function.ProcessFunctionTypeId.HasValue)
			{
				//var type = function.ProcessFunctionType;
				//if (type != null && (type.ListParam != null && type.ListParam.Any()))
				//{
				foreach (var parameter in paramValue.Keys)
				{
					if (parameter == "@userId")
					{
						continue;
					}

					parameters.Add(new SqlParameter(parameter, paramValue[parameter]));
				}
				// }               
			}

			#endregion parameters

			//HopCV: 010615
			//Kiểm tra xem node có ủy quyền hay không
			//Nếu là node ủy quyền thì lấy ra danh sách các những người ủy quyền và những người ủy quyền cho người ủy quyền
			parameters.Add(new SqlParameter("@userId", user.UserId));
			if (function.HasUyQuyen)
			{
				var authorizeUserIds = _authorizeService.GetAuthorizeUsers(user.UserId, Guid.Empty);
				if (authorizeUserIds == null || !authorizeUserIds.Any())
				{
					return new List<IDictionary<string, object>>();
				}

				var arrPara = parameters.ToArray();
				removedocumentCopyIds = GetDocumentCopyIdsRemoveUyQuyen(function, authorizeUserIds, currentDocumentCopyIds, arrPara);
				return GetDocumentLatestByFunctionUyQuyen(function, authorizeUserIds, arrPara);
			}
			else
			{
				var arrPara = parameters.ToArray();
				removedocumentCopyIds = GetDocumentCopyIdsRemove(function, currentDocumentCopyIds, arrPara);
				return GetDocumentLatestByFunction(function, arrPara);
			}
		}

		/// <summary>
		/// Lấy toàn bộ danh sách văn bản theo đúng cấu hình function của người dung hiên tại
		/// </summary>
		/// <param name="function">Entity function</param>
		/// <param name="paramValue"></param>
		/// <param name="quickSearch"></param>
		/// <returns></returns>
		public IEnumerable<IDictionary<string, object>> GetAllDocumentByFunction(
			ProcessFunction function,
			IDictionary<string, string> paramValue = null,
			string quickSearch = null)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}

			var user = _userService.CurrentUser;
			var permis = _permissionSettingService.GetCacheAllPermissionSettings()
				.FirstOrDefault(p => p.PermissionSettingId == function.PermissionSettingId);
			if (permis != null)
			{
				if (!HasPermission(permis.ListUserHasPermission,
					permis.ListPositionHasPermission,
					permis.ListDepartmentPositionHasPermission, user.UserId))
				{
					return new List<IDictionary<string, object>>();
				}
			}

			#region parameters
			var parameters = new List<Object>
								 {
									 new SqlParameter("@quicksearch", string.IsNullOrEmpty(quickSearch) ? string.Empty : "%" + quickSearch + "%")
								 };

			if (paramValue != null && paramValue.Any() && function.ProcessFunctionTypeId.HasValue)
			{
				//var type = function.ProcessFunctionType;
				//if (type != null && (type.ListParam != null && type.ListParam.Any()))
				//{
				foreach (var parameter in paramValue.Keys)
				{
					if (parameter == "@userId")
					{
						continue;
					}

					parameters.Add(new SqlParameter(parameter, paramValue[parameter]));
				}
				// }
			}

			#endregion

			//HopCV: 010615
			//Kiểm tra xem node có ủy quyền hay không
			//Nếu là node ủy quyền thì lấy ra danh sách các những người ủy quyền và những người ủy quyền cho người ủy quyền
			parameters.Add(new SqlParameter("@userId", user.UserId));
			if (function.HasUyQuyen)
			{
				var authorizeUserIds = _authorizeService.GetAuthorizeUsers(user.UserId, Guid.Empty);
				if (authorizeUserIds == null || !authorizeUserIds.Any())
				{
					return new List<IDictionary<string, object>>();
				}

				var totalItems = GetTotalDocumentUyQuyen(function.QueryCountAllItems, authorizeUserIds, parameters.ToArray());
				parameters.Add(new SqlParameter("@fromDate", DateTime.MinValue));
				parameters.Add(new SqlParameter("@take", totalItems));

				return GetDocumentOlderByFunctionUyQuyen(function, authorizeUserIds, parameters.ToArray());
			}
			else
			{
				var totalItems = GetTotalDocument(function.QueryCountAllItems, parameters.ToArray());
				parameters.Add(new SqlParameter("@fromDate", DateTime.MinValue));
				parameters.Add(new SqlParameter("@take", totalItems));

				return GetDocumentOlderByFunction(function, parameters.ToArray());
			}
		}

		/// <summary>
		/// Lấy toàn bộ danh sách văn bản theo đúng cấu hình function của người dung hiên tại
		/// </summary>
		/// <param name="function">Entity function</param>
		/// <param name="paramValue"></param>
		/// <param name="docCopyIds"></param>
		/// <param name="quickSearch"></param>
		/// <returns></returns>
		public System.Data.DataTable GetDataExportToFile(
			ProcessFunction function,
			IEnumerable<int> docCopyIds,
			IDictionary<string, string> paramValue = null,
			string quickSearch = null)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}

			var user = _userService.CurrentUser;
			var permis = _permissionSettingService.GetCacheAllPermissionSettings()
				.FirstOrDefault(p => p.PermissionSettingId == function.PermissionSettingId);
			if (permis != null)
			{
				if (!HasPermission(permis.ListUserHasPermission,
					permis.ListPositionHasPermission,
					permis.ListDepartmentPositionHasPermission, user.UserId))
				{
					return null;
				}
			}

			#region parameters
			var parameters = new List<Object>
								 {
									 new SqlParameter("@quicksearch", string.IsNullOrEmpty(quickSearch) ? string.Empty : "%" + quickSearch + "%")
								 };

			if (paramValue != null && paramValue.Any())
			{
				if (function.ProcessFunctionTypeId.HasValue)
				{
					var type = function.ProcessFunctionType;
					if (type != null && (type.ListParam != null && type.ListParam.Any()))
					{
						foreach (var parameter in type.ListParam)
						{
							if (parameter.ParamName == "@userId")
							{
								continue;
							}

							if (!paramValue.ContainsKey(parameter.ParamName))
							{
								throw new Exception("Không tồn tại giá trị cho tham số " + parameter.ParamName);
							}
							parameters.Add(new SqlParameter(parameter.ParamName, paramValue[parameter.ParamName]));
						}
					}
				}
			}

			#endregion

			//HopCV: 010615
			//Kiểm tra xem node có ủy quyền hay không
			//Nếu là node ủy quyền thì lấy ra danh sách các những người ủy quyền và những người ủy quyền cho người ủy quyền
			parameters.Add(new SqlParameter("@userId", user.UserId));
			if (function.HasUyQuyen)
			{
				var authorizeUserIds = _authorizeService.GetAuthorizeUsers(user.UserId, Guid.Empty);
				if (authorizeUserIds == null || !authorizeUserIds.Any())
				{
					return null;
				}

				var totalItems = GetTotalDocumentUyQuyen(function.QueryCountAllItems, authorizeUserIds, parameters.ToArray());
				parameters.Add(new SqlParameter("@fromDate", DateTime.MinValue));
				parameters.Add(new SqlParameter("@take", totalItems));

				return GetDataExportToFileByFunctionUyQuyen(function, docCopyIds, authorizeUserIds, parameters.ToArray());
			}
			else
			{
				var totalItems = GetTotalDocument(function.QueryCountAllItems, parameters.ToArray());
				parameters.Add(new SqlParameter("@fromDate", DateTime.MinValue));
				parameters.Add(new SqlParameter("@take", totalItems));

				return GetDataExportToFileByFunction(function, docCopyIds, parameters.ToArray());
			}
		}

		/// <summary>
		/// Thực thi câu truy vấn lấy dữ liệu export danh sách văn bản ra file
		/// </summary>
		/// <param name="function"></param>
		/// <param name="docCopyIds"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public DataTable GetDataExport(ProcessFunction function, List<int> docCopyIds, int userId)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function is null.");
			}

			if (!function.HasExportFile)
			{
				return null;
			}

			var exportSql = function.QueryExportDataToFile;
			if (string.IsNullOrWhiteSpace(exportSql))
			{
				return null;
			}

			var parameters = new List<Object>
								 {
									 new SqlParameter("@userId",userId)
								 };

			if (function.HasUyQuyen)
			{
				var authorizeUserIds = _authorizeService.GetAuthorizeUsers(userId, Guid.Empty);
				if (authorizeUserIds == null || !authorizeUserIds.Any())
				{
					return null;
				}

				//Todo; xem lại chỗ này: xem để dang  cấu sql là {0}{1} 
				//    "{0} danh sách user ủy quyên ; {1} danh sách id văn bản" 

				//exportSql = string.Format(exportSql,
				//    string.Join(",", authorizeUserIds), string.Join(",", docCopyIds));
				exportSql = string.Format(exportSql, string.Join(",", authorizeUserIds));
				exportSql = exportSql.Replace("#docCopyIds", string.Join(",", docCopyIds));
			}
			else
			{
				//var query = string.Format(function.QueryExportDataToFile, string.Join(",", docCopyIds));
				//exportSql = string.Format(exportSql, string.Join(",", docCopyIds));
				exportSql = string.Format(exportSql, string.Join(",", docCopyIds));
				exportSql = exportSql.Replace("#docCopyIds", string.Join(",", docCopyIds));
			}

			//exportSql = "SELECT DocCode,CitizenName,DateCreated,DateAppointed,DATEDIFF(d.DateAppointed, d.DateCreated) as RemainTime FROM document d INNER JOIN documentcopy dc on dc.DocumentId = d.DocumentId WHERE dc.DocumentCopyId in (1858,1856)";

			var result = Context.RawTable(exportSql, parameters.ToArray());

			return result;
		}

		/// <summary>
		/// Lấy ra danh sách các văn bản hổ sơ cũ hơn theo đúng cấu hình của function
		/// </summary>
		/// <param name="function">Entity function</param>
		/// <param name="pageSize">Số văn bản cần lấy ra</param>
		/// <param name="userId"></param>
		/// <param name="paramValue">Giá trị truyền vào cho cho parameter của loại function (nếu function thuộc 1 loại function nào đó)</param>
		/// <param name="fromDate">Tới ngày</param>
		/// <param name="quickSearch">Chuỗi tìm kiếm nhanh trong trích yếu...</param>
		/// <returns>Danh sách các Dictionary, mỗi 1 dictionary là 1 row trong trong kết quả lấy được, dictionary có dạng: 'columnName:columnValue'</returns>
		public IEnumerable<IDictionary<string, object>> GetDocumentOlderByFunction(ProcessFunction function, DateTime fromDate, int pageSize, int userId, IDictionary<string, string> paramValue = null, string quickSearch = null)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}

			var permis = _permissionSettingService.GetCacheAllPermissionSettings()
				.FirstOrDefault(p => p.PermissionSettingId == function.PermissionSettingId);
			if (permis != null)
			{
				if (!HasPermission(permis.ListUserHasPermission,
					permis.ListPositionHasPermission,
					permis.ListDepartmentPositionHasPermission, userId))
				{
					return new List<IDictionary<string, object>>();
				}
			}

			#region parameters

			var parameters = new List<Object>
								 {
									 new SqlParameter("@fromDate", fromDate),
									 new SqlParameter("@take", pageSize),
									 new SqlParameter("@quicksearch", string.IsNullOrEmpty(quickSearch) ? string.Empty : "%" + quickSearch + "%")
								 };
			if (paramValue != null && paramValue.Any() && function.ProcessFunctionTypeId.HasValue)
			{
				//var type = function.ProcessFunctionType;
				//if (type != null && (type.ListParam != null && type.ListParam.Any()))
				//{
				foreach (var parameter in paramValue.Keys)
				{
					if (parameter == "@userId")
					{
						continue;
					}

					parameters.Add(new SqlParameter(parameter, paramValue[parameter]));
				}
				// }               
			}

			//if (paramValue != null && paramValue.Any())
			//{
			//    if (function.ProcessFunctionTypeId.HasValue)
			//    {
			//        var type = function.ProcessFunctionType;
			//        if (type != null && (type.ListParam != null && type.ListParam.Any()))
			//        {
			//            foreach (var parameter in type.ListParam)
			//            {
			//                if (parameter.ParamName == "@userId")
			//                {
			//                    continue;
			//                }

			//                if (!paramValue.ContainsKey(parameter.ParamName))
			//                {
			//                    throw new Exception("Không tồn tại giá trị cho tham số " + parameter.ParamName);
			//                }
			//                parameters.Add(new SqlParameter(parameter.ParamName, paramValue[parameter.ParamName]));
			//            }
			//        }
			//    }
			//}

			#endregion parameters

			//HopCV: 010615
			//Kiểm tra xem node có ủy quyền hay không
			//Nếu là node ủy quyền thì lấy ra danh sách các những người ủy quyền và những người ủy quyền cho người ủy quyền
			parameters.Add(new SqlParameter("@userId", userId));
			if (function.HasUyQuyen)
			{
				var authorizeUserIds = _authorizeService.GetAuthorizeUsers(userId, Guid.Empty);
				if (authorizeUserIds == null || !authorizeUserIds.Any())
				{
					return new List<IDictionary<string, object>>();
				}

				return GetDocumentOlderByFunctionUyQuyen(function, authorizeUserIds, parameters.ToArray());
			}
			else
			{

				try
				{
					return GetDocumentOlderByFunction(function, parameters.ToArray());
				}
				catch
				{
					return new List<IDictionary<string, object>>();
				}
			}
		}

		/// <summary>
		/// Lấy ra danh sách các văn bản hổ sơ phân trang theo đúng cấu hình của function
		/// </summary>
		/// <param name="totalItems">Tổng số bản ghi</param>
		/// <param name="function">Entity function</param>
		/// <param name="pageSize">Số văn bản cần lấy ra</param>
		/// <param name="paramValue">Giá trị truyền vào cho cho parameter của loại function (nếu function thuộc 1 loại function nào đó)</param>
		/// <param name="page">Trang</param>
		/// <param name="quickSearch">Chuỗi tìm kiếm nhanh trong trích yếu...</param>
		/// <returns>Danh sách các Dictionary, mỗi 1 dictionary là 1 row trong trong kết quả lấy được, dictionary có dạng: 'columnName:columnValue'</returns>
		public IEnumerable<IDictionary<string, object>> GetDocumentPagingByFunction(out int totalItems, ProcessFunction function, int page, int pageSize, IDictionary<string, string> paramValue = null, string quickSearch = null)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}

			var user = _userService.CurrentUser;
			var permis = _permissionSettingService.GetCacheAllPermissionSettings()
				.FirstOrDefault(p => p.PermissionSettingId == function.PermissionSettingId);
			if (permis != null)
			{
				if (!HasPermission(permis.ListUserHasPermission,
					permis.ListPositionHasPermission,
					permis.ListDepartmentPositionHasPermission, user.UserId))
				{
					totalItems = 0;
					return null;
				}
			}

			#region parameters

			var parameters = new List<Object>
								 {
									 new SqlParameter("@quicksearch", string.IsNullOrEmpty(quickSearch) ? string.Empty : "%" + quickSearch + "%")
								 };

			if (paramValue != null && paramValue.Any())
			{
				if (function.ProcessFunctionTypeId.HasValue)
				{
					var type = function.ProcessFunctionType;
					if (type != null && (type.ListParam != null && type.ListParam.Any()))
					{
						foreach (var parameter in type.ListParam)
						{
							if (parameter.ParamName == "@userId")
							{
								continue;
							}

							if (!paramValue.ContainsKey(parameter.ParamName))
							{
								throw new Exception("Không tồn tại giá trị cho tham số " + parameter.ParamName);
							}
							parameters.Add(new SqlParameter(parameter.ParamName, paramValue[parameter.ParamName]));
						}
					}
				}
			}

			#endregion parameters

			//HopCV: 010615
			//Kiểm tra xem node có ủy quyền hay không
			//Nếu là node ủy quyền thì lấy ra danh sách các những người ủy quyền và những người ủy quyền cho người ủy quyền
			parameters.Add(new SqlParameter("@userId", user.UserId));
			if (function.HasUyQuyen)
			{
				var authorizeUserIds = _authorizeService.GetAuthorizeUsers(user.UserId, Guid.Empty);
				if (authorizeUserIds == null || !authorizeUserIds.Any())
				{
					totalItems = 0;
					return null;
				}

				totalItems = GetTotalDocumentUyQuyen(function.QueryCountAllItems, authorizeUserIds, parameters.ToArray());
				parameters.Add(new SqlParameter("@skip", (page - 1) * pageSize));
				parameters.Add(new SqlParameter("@take", pageSize));

				return GetDocumentPagingByFunctionUyQuyen(function, authorizeUserIds, parameters.ToArray());
			}
			else
			{
				totalItems = GetTotalDocument(function.QueryCountAllItems, parameters.ToArray());
				parameters.Add(new SqlParameter("@skip", (page - 1) * pageSize));
				parameters.Add(new SqlParameter("@take", pageSize));

				return GetDocumentPagingByFunction(function, parameters.ToArray());
			}
		}

		/// <summary>
		/// Tạo mới 1 function
		/// </summary>
		/// <param name="function"></param>
		public void Create(ProcessFunction function)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}

			function.DateModified = DateTime.Now;
			_processFunctionRepository.Create(function);
			Context.SaveChanges();
			_cacheManager.Remove(CacheParam.FunctionAllKey);
		}

		/// <summary>
		/// Cập nhật 1 function
		/// </summary>
		/// <param name="function"></param>
		public void Update(ProcessFunction function)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}

			function.DateModified = DateTime.Now;
			Context.SaveChanges();
			_cacheManager.Remove(CacheParam.FunctionAllKey);
		}

		/// <summary>
		/// Xóa 1 function
		/// </summary>
		/// <param name="function">Entity</param>
		/// <param name="isRecursive">Đệ quy để xóa hết các function con</param>
		public void Delete(ProcessFunction function, bool isRecursive)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}

			var children = Gets(f => f.ParentId == function.ProcessFunctionId);
			if (!isRecursive)
			{
				if (children != null && children.Any())
				{
					foreach (var item in children)
					{
						item.ParentId = function.ParentId;
					}
				}
			}
			else
			{
				DeleteRecursive(children);
			}

			var sibling = Gets(f => f.ParentId == function.ParentId && f.ProcessFunctionId != function.ProcessFunctionId).OrderBy(f => f.Order);
			if (sibling.Any())
			{
				var order = 0;
				foreach (var item in sibling)
				{
					item.Order = order;
					order++;
				}
			}

			//Kiem tra va xoa file crystal
			if (!string.IsNullOrEmpty(function.ExportFileConfig))
			{
				try
				{
					var funcFile = Json2.ParseAs<FunctionFile>(function.ExportFileConfig);
					DeleteTemp(funcFile);
				}
				catch { }
			}
			_processFunctionRepository.Delete(function);
			Context.SaveChanges();
			_cacheManager.Remove(CacheParam.FunctionAllKey);
		}

		#region Thao tác vói bẳng ProcessFunctionAndFilter

		/// <summary>
		/// Thêm bộ lọc cho cây văn bản
		/// </summary>
		/// <param name="filter">Bộ lọc</param>
		public void AddFilter(ProcessFunctionAndFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			_processFunctionAndFilterRepository.Create(filter);
			Context.SaveChanges();
		}

		/// <summary>
		/// Thêm bộ lọc cho cây văn bản
		/// </summary>
		/// <param name="filters">Danh sách bộ lọc</param>
		public void AddFilter(IEnumerable<ProcessFunctionAndFilter> filters)
		{
			if (filters == null)
			{
				throw new ArgumentNullException("filters");
			}

			foreach (var filter in filters)
			{
				_processFunctionAndFilterRepository.Create(filter);
			}

			Context.SaveChanges();
		}

		/// <summary>
		/// Xóa bộ lọc cho cây văn bản
		/// </summary>
		/// <param name="filter">Bộ lọc</param>
		public void DeleteFilter(ProcessFunctionAndFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}

			_processFunctionAndFilterRepository.Delete(filter);
			Context.SaveChanges();
		}

		/// <summary>
		/// Xóa bộ lọc cho cây văn bản
		/// </summary>
		/// <param name="filters">Bộ lọc</param>
		public void DeleteFilter(IEnumerable<ProcessFunctionAndFilter> filters)
		{
			if (filters == null)
			{
				throw new ArgumentNullException("filters");
			}

			foreach (var filter in filters)
			{
				_processFunctionAndFilterRepository.Delete(filter);
			}
			Context.SaveChanges();
		}

		/// <summary>
		/// Lấy ra danh sách bộ lọc và cấy văn bản
		/// </summary>
		/// <param name="functionsId">Id cây văn bản</param>
		/// <param name="filterId">Id bộ lọc</param>
		/// <returns></returns>
		public IEnumerable<ProcessFunctionAndFilter> GetProcessFunctionAndFilters(int functionsId, int filterId)
		{
			if (functionsId <= 0 || filterId <= 0)
			{
				return null;
			}

			return _processFunctionAndFilterRepository.GetsReadOnly(p =>
				p.ProcessFunctionId == functionsId
				&& p.ProcessFunctionFilterId == filterId);
		}

		/// <summary>
		/// Lấy ra danh sách bộ lọc và cấy văn bản
		/// </summary>
		/// <param name="spec"></param>
		/// <returns></returns>
		public IEnumerable<ProcessFunctionAndFilter> GetProcessFunctionAndFilters(Expression<Func<ProcessFunctionAndFilter, bool>> spec = null)
		{
			return _processFunctionAndFilterRepository.GetsReadOnly(spec);
		}

		/// <summary>
		///  Lấy ra danh sách bộ lọc và cấy văn bản
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="projector"></param>
		/// <param name="spec"></param>
		/// <returns></returns>
		public IEnumerable<T> GetProcessFunctionAndFilterAs<T>(Expression<Func<ProcessFunctionAndFilter, T>> projector, Expression<Func<ProcessFunctionAndFilter, bool>> spec = null)
		{
			return _processFunctionAndFilterRepository.GetsAs(projector, spec);
		}

		#endregion

		private void DeleteRecursive(IEnumerable<ProcessFunction> functions)
		{
			foreach (var item in functions)
			{
				var children = _processFunctionRepository.Gets(false, f => f.ParentId == item.ProcessFunctionId);
				if (!string.IsNullOrEmpty(item.ExportFileConfig))
				{
					try
					{
						var funcFile = Json2.ParseAs<FunctionFile>(item.ExportFileConfig);
						DeleteTemp(funcFile);
					}
					catch { }
				}
				if (children != null && children.Any())
				{
					DeleteRecursive(children);
				}
				_processFunctionRepository.Delete(item);
			}
		}

		/// <summary>
		/// Trả về danh sách văn bản theo kho.
		/// </summary>
		/// <param name="id">id của kho</param>
		/// <param name="lastUpdate">Thời điểm update gần nhất</param>
		/// <returns></returns>
		public IEnumerable<IDictionary<string, object>> GetDocumentStore(int id, DateTime? lastUpdate = null)
		{
			var result = new List<IDictionary<string, object>>();
			if (!lastUpdate.HasValue)
			{
				lastUpdate = DateTime.MinValue;
			}

			var documentStore = _processGroupRepository.Get(id);
			if (documentStore == null)
			{
				return result;
			}

			var storeQuery = documentStore.DataQuery;
			if (string.IsNullOrWhiteSpace(storeQuery))
			{
				return result;
			}

			var user = _userService.CurrentUser;
			var parameters = new List<Object>
			{
				new SqlParameter("@userId", user.UserId),
				new SqlParameter("@lastUpdate", lastUpdate)
			};

			var checkLimit = documentStore.LimitQuery == 0 || lastUpdate.HasValue;
			var limit = checkLimit ? "" : "LIMIT " + documentStore.LimitQuery;
			storeQuery = storeQuery.Replace("@limit", limit);
			return Context.RawQuery(storeQuery, parameters.ToArray()) as IEnumerable<IDictionary<string, object>>;
		}

		/// <summary>
		/// Đồng bộ kho văn bản với server
		/// </summary>
		/// <param name="id">id kho văn bản</param>
		/// <param name="lastUpdate">Thời điểm cập nhật gần nhất</param>
		/// <returns></returns>
		public IEnumerable<IDictionary<string, object>> SyncDocumentStore(int id, DateTime lastUpdate)
		{
			return GetDocumentStore(id, lastUpdate);
		}

		/// <summary>
		/// Check xem có quyền thao tac vơi node trên cây văn bản hay không
		/// </summary>
		/// <param name="listUserHasPermission">Danh sách người có quyền trên node</param>
		/// <param name="listPositionHasPermission">Danh sách chức vụ có quyển trên node</param>
		/// <param name="listDepartmentPositionHasPermission">Danh sách phòng ban- chức vụ có quyền trên node</param>
		/// <param name="userId">NGười cần check</param>
		/// <returns></returns>
		public bool HasPermission(List<int> listUserHasPermission,
			List<int> listPositionHasPermission,
			List<IDictionary<string, int>> listDepartmentPositionHasPermission,
			int userId)
		{
			if (!listUserHasPermission.Any()
				&& !listPositionHasPermission.Any()
				&& !listDepartmentPositionHasPermission.Any())
			{
				return true;
			}

			if (listUserHasPermission.Any(uid => uid == userId))
			{
				return true;
			}

			var userDepartmentJobtitles = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition()
						.Where(u => u.UserId == userId);

			var positionIds = userDepartmentJobtitles.Select(u => u.PositionId).Distinct();
			if (positionIds.Any(positionId => listPositionHasPermission.Any(pid => pid == positionId)))
			{
				return true;
			}

			var allDepartments = _departmentService.GetCacheAllDepartments(true);
			var departmentPositionIds = userDepartmentJobtitles.Select(u => new { u.DepartmentId, u.PositionId });
			foreach (var departmentPosition in listDepartmentPositionHasPermission)
			{
				if (departmentPosition.ContainsKey("DepartmentId") && departmentPosition.ContainsKey("PositionId"))
				{
					var departmentId = departmentPosition["DepartmentId"];
					var positionId = departmentPosition["PositionId"];
					if (departmentPositionIds.Any(d => d.DepartmentId == departmentId && d.PositionId == positionId))
					{
						return true;
					}
					var children = GetDepartmentChildrenId(departmentId, allDepartments);
					foreach (var child in children)
					{
						if (departmentPositionIds.Any(d => d.DepartmentId == child && d.PositionId == positionId))
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Cập nhật cấu hình file crystal để xuất danh sách văn bản ra file (word, excell)
		/// </summary>
		/// <param name="streamFileCrystal">Luồng file crystal</param>
		/// <param name="fileName">Tên file crystal</param>
		/// <param name="function">Id node</param>
		/// <param name="deleteOldFile"></param>
		public void Update(System.IO.Stream streamFileCrystal, string fileName, ProcessFunction function, bool deleteOldFile = true)
		{
			if (streamFileCrystal == null || streamFileCrystal.Length <= 0)
				throw new ArgumentNullException("streamFileCrystal is not exist.");

			if (string.IsNullOrEmpty(fileName))
				throw new ArgumentNullException("fileName is empty.");

			if (function == null)
				throw new ArgumentNullException("function not found");

			var currentFileLocation = _fileLocationService.GetCurrent();
			var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
			using (streamFileCrystal)
			{
				if (!string.IsNullOrEmpty(function.ExportFileConfig) && deleteOldFile)
				{
					try
					{
						var funcFile = Json2.ParseAs<FunctionFile>(function.ExportFileConfig);
						DeleteTemp(funcFile);
					}
					catch { }
				}

				var fileInfo = transfer.Upload(streamFileCrystal, Bkav.eGovCloud.Core.FileSystem.FileType.Attach);
				var functionFile = new FunctionFile()
				{
					FunctionId = function.ProcessFunctionId,
					DateCreated = fileInfo.CreatedDate,
					FileName = fileInfo.FileName,
					IdentityFolder = fileInfo.IdentityFolder,
					FileLocationKey = fileInfo.RootFolder,
					FileLocationId = currentFileLocation.FileLocationId,
					RealFileName = fileName
				};

				function.ExportFileConfig = functionFile.Stringify();
				Update(function);
			}
		}

		/// <summary>
		/// Tai file crystal
		/// </summary>
		/// <param name="functionFile"></param>
		/// <returns></returns>
		public System.IO.Stream Download(FunctionFile functionFile)
		{
			var fileLocation = _fileLocationService.Get(functionFile.FileLocationId);
			if (fileLocation == null)
			{
				throw new Exception("Không tìm thấy nơi lưu tệp đính kèm");
			}

			var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
			var downloaded =
				transfer.Download(new FileTransferInfo
				{
					CreatedDate = functionFile.DateCreated,
					FileName = functionFile.FileName,
					FileType = Bkav.eGovCloud.Core.FileSystem.FileType.Attach,
					IdentityFolder = functionFile.IdentityFolder,
					RootFolder = functionFile.FileLocationKey
				});

			return downloaded;
		}

		/// <summary>
		/// Xoa file crystal
		/// </summary>
		/// <param name="functionFile"></param>
		private void DeleteTemp(FunctionFile functionFile)
		{
			var fileLocation = _fileLocationService.Get(functionFile.FileLocationId);
			if (fileLocation == null)
			{
				throw new Exception("Không tìm thấy nơi lưu tệp đính kèm");
			}

			var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
			transfer.Delete(new FileTransferInfo
			{
				CreatedDate = functionFile.DateCreated,
				FileName = functionFile.FileName,
				FileType = Bkav.eGovCloud.Core.FileSystem.FileType.Attach,
				IdentityFolder = functionFile.IdentityFolder,
				RootFolder = functionFile.FileLocationKey
			});
		}

		#region method for uy quyen

		/// <summary>
		/// Hàm này dùng cho ủy quyền
		/// </summary>
		/// <param name="function"></param>
		/// <param name="authorizeUserIds"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private IEnumerable<IDictionary<string, object>> GetDocumentLatestByFunctionUyQuyen(ProcessFunction function, IEnumerable<int> authorizeUserIds, params object[] parameters)
		{
			if (authorizeUserIds == null || !authorizeUserIds.Any())
			{
				return null;
			}

			var result = Context.RawQuery(string.Format(function.QueryLatest, string.Join(",", authorizeUserIds)), parameters) as IEnumerable<IDictionary<string, object>>;
			return result;
		}

		/// <summary>
		/// Hàm này dùng cho ủy quyền
		/// </summary>
		/// <param name="function"></param>
		/// <param name="authorizeUserIds"></param>
		/// <param name="currentDocumentCopyIds"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private IEnumerable<int> GetDocumentCopyIdsRemoveUyQuyen(ProcessFunction function, IEnumerable<int> authorizeUserIds, IEnumerable<int> currentDocumentCopyIds, params object[] parameters)
		{
			if ((currentDocumentCopyIds == null || !currentDocumentCopyIds.Any())
				|| (authorizeUserIds == null || !authorizeUserIds.Any()))
			{
				return new List<int>();
			}

			var result = Context.RawQuery(string.Format(function.QueryItemRemove,
				string.Join(",", authorizeUserIds),
				string.Join(",", currentDocumentCopyIds)), parameters) as IEnumerable<IDictionary<string, object>>;
			if (result != null && result.Any())
			{
				if (!result.First().ContainsKey("DocumentCopyId"))
				{
					return new List<int>();
				}
				return result.Select(r => (int)r["DocumentCopyId"]);
			}
			return new List<int>();
		}

		/// <summary>
		/// Hàm này dùng cho ủy quyền
		/// </summary>
		/// <param name="function"></param>
		/// <param name="authorizeUserIds"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private IEnumerable<IDictionary<string, object>> GetDocumentOlderByFunctionUyQuyen(ProcessFunction function, IEnumerable<int> authorizeUserIds, params object[] parameters)
		{
			if (authorizeUserIds == null || !authorizeUserIds.Any())
			{
				return null;
			}

			var result = Context.RawQuery(string.Format(function.QueryOlder, string.Join(",", authorizeUserIds)), parameters) as IEnumerable<IDictionary<string, object>>;
			return result;
		}

		/// <summary>
		/// Hàm này dùng cho ủy quyền
		/// </summary>
		/// <param name="function"></param>
		/// <param name="docCopyIds"></param>
		/// <param name="authorizeUserIds"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private System.Data.DataTable GetDataExportToFileByFunctionUyQuyen(ProcessFunction function,
			IEnumerable<int> docCopyIds,
			IEnumerable<int> authorizeUserIds, params object[] parameters)
		{
			if (authorizeUserIds == null || !authorizeUserIds.Any())
			{
				return null;
			}

			if (docCopyIds == null || !docCopyIds.Any())
			{
				return null;
			}

			var result = Context.RawTable(string.Format(function.QueryExportDataToFile, string.Join(",", docCopyIds), string.Join(",", authorizeUserIds)), parameters) as System.Data.DataTable;
			return result;
		}

		/// <summary>
		///  Hàm này dùng cho ủy quyền
		/// </summary>
		/// <param name="function"></param>
		/// <param name="authorizeUserIds"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private IEnumerable<IDictionary<string, object>> GetDocumentPagingByFunctionUyQuyen(ProcessFunction function, IEnumerable<int> authorizeUserIds, params object[] parameters)
		{
			if (authorizeUserIds == null || !authorizeUserIds.Any())
			{
				return null;
			}

			var result = Context.RawQuery(string.Format(function.QueryPaging, string.Join(",", authorizeUserIds)), parameters) as IEnumerable<IDictionary<string, object>>;
			return result;
		}

		/// <summary>
		/// Hàm này dùng cho ủy quyền
		/// </summary>
		/// <param name="queryCountItemUnread"></param>
		/// <param name="authorizeUserIds"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private int GetTotalDocumentUnreadUyQuyen(string queryCountItemUnread, IEnumerable<int> authorizeUserIds, params object[] parameters)
		{
			if (string.IsNullOrWhiteSpace(queryCountItemUnread) || queryCountItemUnread.StartsWith("select 0", StringComparison.OrdinalIgnoreCase)
				|| authorizeUserIds == null || !authorizeUserIds.Any())
			{
				return 0;
			}

			int total = 0;
			int.TryParse((Context.RawScalar(string.Format(queryCountItemUnread, string.Join(",", authorizeUserIds)), parameters)).ToString(), out total);
			return total;
		}

		private int GetTotalDocumentUyQuyen(string queryCountAllItems, IEnumerable<int> authorizeUserIds, params object[] parameters)
		{
			if (string.IsNullOrWhiteSpace(queryCountAllItems) || queryCountAllItems.StartsWith("select 0", StringComparison.OrdinalIgnoreCase)
				|| (authorizeUserIds == null || !authorizeUserIds.Any()))
			{
				return 0;
			}

			int total = 0;
			int.TryParse((Context.RawScalar(string.Format(queryCountAllItems, string.Join(",", authorizeUserIds)), parameters)).ToString(), out total);
			return total;
		}

		#endregion end uy quyen

		#region method default

		private IEnumerable<IDictionary<string, object>> GetDocumentLatestByFunction(ProcessFunction function, params object[] parameters)
		{
			var result = Context.RawQuery(function.QueryLatest, parameters) as IEnumerable<IDictionary<string, object>>;
			return result;
		}

		private IEnumerable<int> GetDocumentCopyIdsRemove(ProcessFunction function, IEnumerable<int> currentDocumentCopyIds, params object[] parameters)
		{
			if (currentDocumentCopyIds != null && currentDocumentCopyIds.Any())
			{
				var result = Context.RawQuery(string.Format(function.QueryItemRemove, string.Join(",", currentDocumentCopyIds)), parameters) as IEnumerable<IDictionary<string, object>>;
				if (result.Any())
				{
					if (!result.First().ContainsKey("DocumentCopyId"))
					{
						return new List<int>();
					}
					return result.Select(r => (int)r["DocumentCopyId"]);
				}
			}
			return new List<int>();
		}

		private IEnumerable<IDictionary<string, object>> GetDocumentOlderByFunction(ProcessFunction function, params object[] parameters)
		{
			var result = Context.RawQuery(function.QueryOlder, parameters) as IEnumerable<IDictionary<string, object>>;
			return result;
		}

		private System.Data.DataTable GetDataExportToFileByFunction(ProcessFunction function, IEnumerable<int> docCopyIds, params object[] parameters)
		{
			if (docCopyIds == null || !docCopyIds.Any())
			{
				return null;
			}

			var query = string.Format(function.QueryExportDataToFile, string.Join(",", docCopyIds));
			var result = Context.RawTable(query, parameters) as System.Data.DataTable;
			return result;
		}

		private IEnumerable<IDictionary<string, object>> GetDocumentPagingByFunction(ProcessFunction function, params object[] parameters)
		{
			var result = Context.RawQuery(function.QueryPaging, parameters) as IEnumerable<IDictionary<string, object>>;
			return result;
		}

		private int GetTotalDocumentUnread(string queryCountItemUnread, params object[] parameters)
		{
			if (string.IsNullOrWhiteSpace(queryCountItemUnread) || queryCountItemUnread.StartsWith("select 0", StringComparison.OrdinalIgnoreCase))
			{
				return 0;
			}

			int total = 0;
			var result = Context.RawScalar(queryCountItemUnread, parameters);
			if (result == null)
			{
				return total;
			}
			int.TryParse(result.ToString(), out total);
			return total;
		}

		private int GetTotalDocument(string queryCountAllItems, params object[] parameters)
		{
			if (string.IsNullOrWhiteSpace(queryCountAllItems) || queryCountAllItems.StartsWith("select 0", StringComparison.OrdinalIgnoreCase))
			{
				return 0;
			}

			int total = 0;
			var result = Context.RawScalar(queryCountAllItems, parameters);
			if (result == null)
			{
				return total;
			}

			int.TryParse(result.ToString(), out total);
			return total;
		}

		#endregion end default

		/// <summary>
		/// Author:HopCV
		/// Lớp chứa parameter tham số đầu vào của câu truy vấn lấy danh sách văn bản
		/// </summary>        
		class ObjectParams
		{
			public string Key { get; set; }

			public string Value { get; set; }

			public string CompareName { get; set; }
		}
	}
}
