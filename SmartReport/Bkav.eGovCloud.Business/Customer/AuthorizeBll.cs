using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CodeBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 300712
    /// Author      : DungHV
    /// Description : BLL tương ứng với bảng Authorize trong CSDL
    /// </summary>
    public class AuthorizeBll : ServiceBase
    {
        private readonly IRepository<Authorize> _authorizeRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly UserBll _userService;
        private readonly MemoryCacheManager _cacheManager;

        ///<summary>
        /// Khởi tạo class <see cref="AuthorizeBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        ///<param name="generalSettings"></param>
        ///<param name="cacheManager"></param>
        ///<param name="userService"></param>
        public AuthorizeBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings, 
            MemoryCacheManager cacheManager,
             UserBll userService)
            : base(context)
        {
            _authorizeRepository = Context.GetRepository<Authorize>();
            _generalSettings = generalSettings;
            _userService = userService;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Lấy ra danh sách ủy quyền. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">Bộ lọc</param>
        /// <returns>Danh sách ủy quyền</returns>
        public IEnumerable<Authorize> Gets(Expression<Func<Authorize, bool>> spec = null)
        {
            return _authorizeRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra danh sách ủy quyền. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="authorizeUserId"> Id người ủy quyền</param>
        /// <param name="isReadOnly"></param>
        /// <returns>Danh sách ủy quyền</returns>
        public IEnumerable<Authorize> Gets(int authorizeUserId, bool isReadOnly = true)
        {
            return _authorizeRepository.Gets(isReadOnly, p => p.AuthorizeUserId == authorizeUserId);
        }

        /// <summary>
        /// Lấy ra danh sách ủy quyền. Kết quả chỉ để đọc
        /// </summary>
        /// <returns>Danh sách ủy quyền</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Authorize, T>> projector)
        {
            return _authorizeRepository.GetsAs(projector);
        }

        /// <summary>
        /// Lấy danh sách ủy quyền
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="totalRecords"></param>
        /// <param name="projector"></param>
        /// <param name="spec"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(out int totalRecords,
                                       Expression<Func<Authorize, T>> projector,
                                       Expression<Func<Authorize, bool>> spec = null,
                                       int currentPage = 1,
                                       int? pageSize = null,
                                       string sortBy = "",
                                       bool isDescending = false)
        {
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _authorizeRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Authorize>(isDescending, sortBy);
            var paging = Context.Filters.Page<Authorize>(currentPage, pageSize.Value);
            return _authorizeRepository.GetsAs(projector, spec, sort, paging);
        }

        /// <summary>
        /// Lấy ra ủy quyền theo id
        /// </summary>
        /// <param name="authorizeId">Id của ủy quyền</param>
        /// <returns>Entity ủy quyền</returns>
        public Authorize Get(int authorizeId)
        {
            Authorize result = null;
            if (authorizeId > 0)
            {
                result = _authorizeRepository.Get(authorizeId);
            }
            return result;
        }

        /// <summary>
        /// Lấy ra ủy quyền theo id
        /// </summary>
        /// <param name="authorizeId">Id của ủy quyền</param>
        /// <param name="authorizeUserId">id người ủy quyền</param>
        /// <returns>Entity ủy quyền</returns>
        public Authorize Get(int authorizeId, int authorizeUserId)
        {
            Authorize result = null;
            if (authorizeId > 0)
            {
                result = _authorizeRepository.Get(false, p => p.AuthorizeId == authorizeId && p.AuthorizeUserId == authorizeUserId);
            }
            return result;
        }

        /// <summary>
        /// Trả về danh sách cán bộ đang ủy quyền xử lý cho cán bộ được yêu cầu authorizedUserId
        /// </summary>
        /// <param name="authorizedUserId"></param>
        /// <param name="docTypeId"> </param>
        /// <returns></returns>
        public IEnumerable<int> GetAuthorizeUsers(int authorizedUserId, Guid docTypeId)
        {
            var authors = GetUyQuyensCached(authorizedUserId);

            var results = new List<int>();
            if (authors != null && authors.Any())
            {
                foreach (var item in authors)
                {
                    if (string.IsNullOrEmpty(item.DocTypeId) || docTypeId == Guid.Empty)
                    {
                        results.Add(item.AuthorizeUserId);
                    }
                    else
                    {
                        if (item.DocTypes.Contains(docTypeId))
                        {
                            results.Add(item.AuthorizeUserId);
                        }
                    }
                }
                return results.Distinct();
            }

            return results;
        }

        /// <summary>
        /// Tạo mới ủy quyền
        /// </summary>
        /// <param name="authorize">Entity ủy quyền</param>
        public void Create(Authorize authorize)
        {
            if (authorize == null)
            {
                throw new ArgumentNullException("authorize");
            }

            if (authorize.AuthorizedUserId == authorize.AuthorizeUserId)
            {
                throw new Exception("AuthorizedUser and AuthorizeUser was not match!");
            }

            if (string.IsNullOrEmpty(authorize.AuthorizedUserName) || string.IsNullOrEmpty(authorize.AuthorizeUserName))
            {
                var allUsers = _userService.GetAllCached();

                if (string.IsNullOrEmpty(authorize.AuthorizedUserName))
                {
                    var authorizedUser = allUsers.First(p => p.UserId == authorize.AuthorizedUserId);
                    authorize.AuthorizedUserName = string.Format("{0}({1})", authorizedUser.FullName, authorizedUser.Username);
                }

                if (string.IsNullOrEmpty(authorize.AuthorizeUserName))
                {
                    var authorizeUser = allUsers.First(p => p.UserId == authorize.AuthorizedUserId);
                    authorize.AuthorizeUserName = string.Format("{0}({1})", authorizeUser.FullName, authorizeUser.Username);
                }
            }

            _authorizeRepository.Create(authorize);
            Context.SaveChanges();

            ClearCache(authorize.AuthorizedUserId);
        }

        /// <summary>
        /// Xóa 1 ủy quyền
        /// </summary>
        /// <param name="authorize">Thực thể ủy quyền</param>
        public void Delete(Authorize authorize)
        {
            if (authorize == null)
            {
                throw new ArgumentNullException("authorize");
            }
            _authorizeRepository.Delete(authorize);
            Context.SaveChanges();
            ClearCache(authorize.AuthorizedUserId);
        }

        /// <summary>
        /// Xóa  ủy quyền
        /// </summary>
        /// <param name="authorizes">Thực thể ủy quyền</param>
        public void Delete(IEnumerable<Authorize> authorizes)
        {
            if (authorizes == null || !authorizes.Any())
            {
                throw new ArgumentNullException("authorize");
            }

            foreach (var authorize in authorizes)
            {
                _authorizeRepository.Delete(authorize);

                ClearCache(authorize.AuthorizedUserId);
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin ủy quyền
        /// </summary>
        /// <param name="authorize">Entity ủy quyền</param>
        public void Update(Authorize authorize)
        {
            if (authorize == null)
            {
                throw new ArgumentNullException("authorize");
            }
            Context.SaveChanges();

            ClearCache(authorize.AuthorizedUserId);
        }

        /// <summary>
        ///  HopCV:010615
        ///  Lấy danh sách người ủy quyền cho người khác (Bao gồm người ủy quyền cho người ủy quyền)
        /// </summary>
        /// <param name="authorizedUserId">Id người nhận ủy quyền</param>
        /// <param name="date">Thời gian để kiểm tra</param>
        /// <returns></returns>
        public IEnumerable<Authorize> GetUyQuyens(int authorizedUserId, DateTime date)
        {
            //Lấy ra danh sách người ủy quyền trong khoảng thời gian đã truyền vào
            var authorizes = _authorizeRepository.Gets(false,
                p => p.Active
                    && p.AuthorizedUserId == authorizedUserId
                    && p.DateBegin <= date
                    && p.DateEnd >= date
                    && (p.Permission == 2 || p.Permission == 3));

            if (authorizes == null || !authorizes.Any())
            {
                return new List<Authorize>();
            }

            //Tạo danh sách lưu trữ
            var results = new List<Authorize>();
            var outResults = new List<Authorize>();
            var outExitsResultIds = new List<int>();
            var existAuthorizeUserId = new List<int>() { 
                authorizedUserId
            };

            results.AddRange(authorizes);

            GetAuthorizeByDeQuy(authorizes, existAuthorizeUserId, date, out outResults, out outExitsResultIds);

            results.AddRange(outResults);

            return results;
        }

        /// <summary>
        ///  HopCV:010615
        ///  Lấy danh sách người ủy quyền cho người khác (Bao gồm người ủy quyền cho người ủy quyền)
        /// </summary>
        /// <param name="authorizedUserId">Id người nhận ủy quyền</param>
        /// <returns></returns>
        public IEnumerable<Authorize> GetUyQuyensCached(int authorizedUserId)
        {
            var cacheKey = string.Format(CacheParam.AuthorizeKey, authorizedUserId);
            var result = _cacheManager.Get<IEnumerable<AuthorizeCached>>(cacheKey, CacheParam.AuthorizeKeyCacheTimeOut, () =>
            {
                var data =  GetUyQuyens(authorizedUserId, DateTime.Now);
                return AutoMapper.Mapper.Map<IEnumerable<Authorize>, IEnumerable<AuthorizeCached>>(data);
            });

            return AutoMapper.Mapper.Map<IEnumerable<AuthorizeCached>, IEnumerable<Authorize>>(result);
        }

        /// <summary>
        /// Clear ủy quyền cache
        /// </summary>
        /// <param name="authorizedUserId">Người ủy quyền</param>
        /// <returns></returns>
        public void ClearCache(int authorizedUserId)
        {
            var cacheKey = string.Format(CacheParam.AuthorizeKey, authorizedUserId);
            _cacheManager.Remove(cacheKey);
        }

        /// <summary>
        /// HopCV:010615
        /// Lấy danh sách id người ủy quyền cho người khác
        /// </summary>
        /// <param name="authorizedUserId">Id người nhận ủy quyền</param>
        /// <param name="date">Thời gian để kiểm tra</param>
        /// <returns></returns>
        public IEnumerable<int> GetUserIdUyQuyen(int authorizedUserId, DateTime date)
        {
            var authors = GetUyQuyens(authorizedUserId, date);
            if (authors == null || !authors.Any())
            {
                return new List<int>();
            }

            return authors.Select(p => p.AuthorizeUserId).Distinct();
        }

        /// <summary>
        /// HopCV:010615
        /// Lấy danh sách các ủy quyền theo người nhận ủy quyền
        /// </summary>
        /// <param name="authorizedUserIds">Danh sách Id người nhận ủy quyền</param>
        /// <param name="date">Thời gian để kiểm tra</param>
        /// <returns></returns>
        private IEnumerable<Authorize> GetChildren(IEnumerable<int> authorizedUserIds, DateTime date)
        {
            if (authorizedUserIds == null || !authorizedUserIds.Any())
            {
                return new List<Authorize>();
            }

            return _authorizeRepository.Gets(false,
                p => p.Active
                    && p.DateBegin <= date
                    && p.DateEnd >= date
                    && authorizedUserIds.Contains(p.AuthorizedUserId)
                    && (p.Permission == 2 || p.Permission == 3));
        }

        /// <summary>
        /// hopcv:010615
        /// lấy danh sách các ủy quyền theo người nhận ủy quyền
        /// </summary>
        /// <param name="authorizes">Guoi nhan uy quyen</param>
        /// <param name="existAuthorizeIds"></param>
        /// <param name="outExistAuthorizeIds"></param>
        /// <param name="date"></param>
        /// <param name="outAuthorizes"></param>
        private void GetAuthorizeByDeQuy(IEnumerable<Authorize> authorizes,
            IEnumerable<int> existAuthorizeIds, DateTime date,
            out List<Authorize> outAuthorizes, out List<int> outExistAuthorizeIds)
        {
            var results = new List<Authorize>();
            var exist = new List<int>();
            exist.AddRange(existAuthorizeIds);

            //danh sach nguoi uy quyen
            var authorizeUserIds = authorizes.Select(p => p.AuthorizeUserId);

            //Lay danh sach nguoi duoc uy quen
            var allChilds = GetChildren(authorizeUserIds, date);
            if (allChilds != null && allChilds.Any())
            {
                //add vào danh sách đã từng lấy dữ liệu
                exist.AddRange(authorizeUserIds);
                results.AddRange(allChilds);

                //Loại bỏ những ngưỡi đã từng lấy dữ liệu duoc uy quyen
                allChilds = allChilds.Where(p => !exist.Contains(p.AuthorizeUserId));
                if (allChilds != null && allChilds.Any())
                {
                    var outResults = new List<Authorize>();
                    var outResultIds = new List<int>();
                    GetAuthorizeByDeQuy(allChilds, exist, date, out outResults, out outResultIds);
                    results.AddRange(outResults);
                    exist.AddRange(outResultIds);
                }
            }

            outAuthorizes = results;
            outExistAuthorizeIds = exist;
        }

        /// <summary>
        /// HopCV:010615
        /// Lấy danh sách các ủy quyền theo người nhận ủy quyền
        /// </summary>
        /// <param name="authorizes"></param>
        /// <param name="date"></param>
        /// <param name="outAuthorizes"></param>
        private void GetAuthorizeByDeQuy(IEnumerable<Authorize> authorizes, DateTime date, out List<Authorize> outAuthorizes)
        {
            outAuthorizes = new List<Authorize>();
            var authorizeUserIds = authorizes.Select(p => p.AuthorizeUserId);
            var allChilds = GetChildren(authorizeUserIds, date);
            if (allChilds != null && allChilds.Any())
            {
                outAuthorizes.AddRange(allChilds);
                List<Authorize> outResults;
                GetAuthorizeByDeQuy(allChilds, date, out outResults);

                outAuthorizes.AddRange(outResults);
            }
        }
    }
}
