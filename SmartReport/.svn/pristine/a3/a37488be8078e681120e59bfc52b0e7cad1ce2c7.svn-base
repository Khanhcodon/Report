using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.FileTransfer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Business.Objects;
using AutoMapper;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : StorePrivateBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 081013</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : BLL tương ứng với bảng StorePrivate trong CSDL</para>
    /// </summary>
    public class StorePrivateBll : ServiceBase
    {
        private readonly IRepository<StorePrivate> _storePrivateRepository;
        private readonly IRepository<StorePrivateUser> _storePrivateUserRepository;
        private readonly IRepository<StorePrivateAttachment> _storePrivateAttachmentRepository;
        private readonly UserBll _userService;
        private readonly DepartmentBll _deptService;
        private readonly IRepository<StorePrivateDocumentCopy> _storePrivateDocumentCopyRepository;
        private readonly FileLocationBll _fileLocationService;
        private readonly FileLocationSettings _fileLocationSettings;
        private readonly MemoryCacheManager _cache;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"> Context.</param>
        /// <param name="userService">Bll tương ứng với bảng User</param>
        /// <param name="deptService"></param>
        /// <param name="fileLocationService">Dal tương ứng với bảng FileLocation trong CSDL</param>
        /// <param name="fileLocationSettings">Cấu hình cho vị trí lưu file</param>
        /// <param name="cache">Quản lý cache</param>
        public StorePrivateBll(IDbCustomerContext context, UserBll userService, DepartmentBll deptService,
                                FileLocationBll fileLocationService,
                                FileLocationSettings fileLocationSettings, MemoryCacheManager cache)
            : base(context)
        {
            _storePrivateRepository = Context.GetRepository<StorePrivate>();
            _storePrivateUserRepository = Context.GetRepository<StorePrivateUser>();
            _userService = userService;
            _deptService = deptService;
            _storePrivateDocumentCopyRepository = Context.GetRepository<StorePrivateDocumentCopy>();
            _fileLocationSettings = fileLocationSettings;
            _fileLocationService = fileLocationService;
            _storePrivateAttachmentRepository = Context.GetRepository<StorePrivateAttachment>();
            _cache = cache;
        }

        /// <summary>
        /// Lấy ra hồ sơ cá nhân theo id và id người tạo
        /// </summary>
        /// <param name="id">Id hồ sơ cá nhân</param>
        /// <param name="userId">Id người tạo</param>
        /// <returns></returns>
        public StorePrivate Get(int id, int userId)
        {
            return _storePrivateRepository.Get(false, s => s.StorePrivateId == id && s.CreatedByUserId == userId);
        }

        /// <summary>
        /// Trả về hồ sơ cá nhân theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StorePrivate Get(int id)
        {
            return _storePrivateRepository.Get(id);
        }

        /// <summary>
        /// Trả về tất cả số hồ sơ cá nhân có lưu cache.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StorePrivateCached> GetsCached()
        {
            var cacheKey = string.Format(CacheParam.PrivateStore, "all");

            return _cache.Get(cacheKey, CacheParam.PrivateStoreCacheTimeOut, () =>
                    {
                        var stores = new List<StorePrivate>();

                        var storePrivates = _storePrivateRepository.Gets(isReadOnly: true,
                                                spec: st => st.Status != (byte)StorePrivateStatus.IsDelete,
                                                preFilter: Context.Filters.Include<StorePrivate>("StorePrivateUsers"));

                        if (!storePrivates.Any())
                        {
                            return new List<StorePrivateCached>();
                        }

                        foreach (var st in storePrivates)
                        {
                            var stUsers = st.StorePrivateUsers;
                            if (stUsers != null && stUsers.Any())
                            {
                                st.HasShared = true;
                                st.UserIdJoined = stUsers.Where(i => i.UserId.HasValue && i.UserId.Value != 0).Select(i => i.UserId.Value);
                                st.DeptIdJoined = stUsers.Where(i => (!i.UserId.HasValue || i.UserId.Value == 0) && i.DepartmentId.HasValue).Select(i => i.DepartmentId.Value);
                            };

                            stores.Add(st);
                        }

                        var result = Mapper.Map<IEnumerable<StorePrivate>, IEnumerable<StorePrivateCached>>(stores);
                        return result;
                    });
        }

        /// <summary>
        /// Trả về danh sách các hồ sơ cá nhân của người dùng
        /// </summary>
        /// <param name="userId">Id người dùng</param>
        /// <param name="parentId">Id hồ sơ cha</param>
        /// <param name="isActive">Trạng thái xác định chỉ lấy hồ sơ đang active</param>
        /// <returns></returns>
        public IEnumerable<StorePrivateCached> GetsStorePrivate(int userId, int parentId, bool isActive = false)
        {
            var allStores = GetsCached();
            var result = allStores.Where(s => (s.CreatedByUserId == userId) && (parentId == 0 || s.ParentId == parentId));
            if (isActive)
            {
                result = result.Where(s => s.Status == (byte)StorePrivateStatus.IsActive);
            }

            return result.OrderBy(s => s.StorePrivateName);
        }

        /// <summary>
        /// Trả về danh sách các hồ sơ người dùng được chia sẻ
        /// </summary>
        /// <param name="userId">Id người dùng</param>
        /// <param name="parentId">Id hồ sơ cha</param>
        /// <param name="isActive">Trạng thái xác định chỉ lấy hồ sơ đang active</param>
        /// <returns></returns>
        public IEnumerable<StorePrivateCached> GetsStoreShared(int userId, int parentId, bool isActive = false)
        {
            var allStores = GetsCached();
            var userDeptExts = _deptService.GetCacheAllUserDepartmentJobTitlesPosition().Where(ud => ud.UserId.Equals(userId))
                                        .Select(ud => ud.DepartmentId);

            var result = allStores.Where(s => s.CreatedByUserId != userId
                                && (parentId == 0 || s.ParentId == parentId)
                                && ((s.UserIdJoined != null && s.UserIdJoined.Contains(userId))
                                    || (s.DeptIdJoined != null && s.DeptIdJoined.Any(d => userDeptExts.Contains(d)))
                                ));

            if (isActive)
            {
                result = result.Where(s => s.Status == (byte)StorePrivateStatus.IsActive);
            }

            return result.OrderBy(s => s.StorePrivateName);
        }

        /// <summary>
        /// Tạo mới hồ sơ cá nhân
        /// </summary>
        /// <param name="storePrivate">Entity</param>
        /// <param name="userIdJoined">Danh sách Id người tham gia</param>
        /// <param name="deptExtJoined">Danh sách deptExt phòng ban tham gia</param>
        public void Create(StorePrivate storePrivate, List<int> userIdJoined, List<int> deptExtJoined)
        {
            if (storePrivate == null)
            {
                throw new ArgumentNullException("storePrivate");
            }

            StorePrivate parent = null;
            if (storePrivate.ParentId.HasValue && storePrivate.ParentId > 0)
            {
                parent = Get(storePrivate.ParentId.Value, storePrivate.CreatedByUserId);
                if (parent == null)
                {
                    throw new Exception("Không tìm thấy hồ sơ cá nhân cha");
                }
            }

            if (_storePrivateRepository.Exist(s => s.StorePrivateName.Equals(storePrivate.StorePrivateName)
                        && s.CreatedByUserId == storePrivate.CreatedByUserId
                        && s.ParentId == storePrivate.ParentId
                        && s.Status != (byte)StorePrivateStatus.IsDelete))
            {
                throw new Exception("Tên hồ sơ đã tồn tại!");
            }

            if (userIdJoined != null && userIdJoined.Any())
            {
                //var allUserIds = _userService.GetsAs(u => u.UserId, true);
                //userIdJoined =
                //    userIdJoined.Where(allUserIds.Contains)
                //        .Where(id => id != storePrivate.CreatedByUserId)
                //        .ToList();

                foreach (var id in userIdJoined)
                {
                    storePrivate.StorePrivateUsers.Add(new StorePrivateUser { UserId = id });
                }
            }

            if (deptExtJoined != null && deptExtJoined.Any())
            {
                //var allUserIds = _userService.GetsAs(u => u.UserId, true);
                //userIdJoined =
                //    userIdJoined.Where(allUserIds.Contains)
                //        .Where(id => id != storePrivate.CreatedByUserId)
                //        .ToList();
                var allDeps = _deptService.GetCacheAllDepartments().Where(d => deptExtJoined.Contains(d.DepartmentId));
                foreach (var deptId in deptExtJoined)
                {
                    var dept = allDeps.SingleOrDefault(d => d.DepartmentId.Equals(deptId));
                    if (dept != null)
                    {
                        storePrivate.StorePrivateUsers.Add(new StorePrivateUser
                        {
                            DepartmentId = deptId,
                            DepartmentIdExt = dept.DepartmentIdExt
                        });
                    }
                }
            }

            storePrivate.StorePrivateIdExt = parent == null ? storePrivate.StorePrivateId.ToString() : string.Format("{0}.{1}", parent.StorePrivateIdExt, storePrivate.StorePrivateId);
            // storePrivate.Level = (byte)storePrivate.StorePrivateIdExt.Split('.').Length;
            _storePrivateRepository.Create(storePrivate);
            Context.SaveChanges();

            ClearCache();
        }

        /// <summary>
        /// Cập nhật thông tin hồ sơ cá nhân
        /// </summary>
        /// <param name="storePrivate">Hồ sơ</param>
        /// <param name="userIdJoined">Danh sách Id người tham gia</param>
        /// <param name="oldName">Tên cũ của hồ sơ</param>
        /// <param name="deptIdJoined"></param>
        public void Update(StorePrivate storePrivate, List<int> userIdJoined, List<int> deptIdJoined, string oldName)
        {
            if (storePrivate == null)
            {
                throw new ArgumentNullException("storePrivate");
            }
            if (_storePrivateRepository.Exist(s => s.StorePrivateName.Equals(storePrivate.StorePrivateName)
                && s.CreatedByUserId == storePrivate.CreatedByUserId && s.StorePrivateName != oldName && s.Status != (byte)StorePrivateStatus.IsDelete))
            {
                throw new EgovException("Tên hồ sơ đã tồn tại!");
            }

            UpdateUserJoined(userIdJoined, storePrivate);

            UpdateDeptJoined(deptIdJoined, storePrivate);

            Context.SaveChanges();
            ClearCache();
        }

        private void UpdateUserJoined(List<int> userIdJoined, StorePrivate storePrivate)
        {
            if (userIdJoined != null && userIdJoined.Any())
            {
                var allUserIds = _userService.GetsAs(u => u.UserId, true);
                userIdJoined =
                    userIdJoined.Where(allUserIds.Contains)
                        .Where(id => id != storePrivate.CreatedByUserId)
                        .ToList();
            }
            else
            {
                userIdJoined = new List<int>();
            }

            IEnumerable<int> userIdsAdd;
            IEnumerable<int> userIdsDelete;
            var isEqual = storePrivate.StorePrivateUsers.Where(u => u.UserId.HasValue).Select(ur => ur.UserId.Value)
                            .CompareTo(userIdJoined, out userIdsDelete, out userIdsAdd);

            if (!isEqual)
            {
                if (userIdsDelete != null && userIdsDelete.Any())
                {
                    var storeUserDelete = _storePrivateUserRepository.Gets(false, s => s.UserId.HasValue && userIdsDelete.Contains(s.UserId.Value));
                    foreach (var storePrivateUser in storeUserDelete)
                    {
                        _storePrivateUserRepository.Delete(storePrivateUser);
                    }
                }
            }

            if (userIdsAdd != null && userIdsAdd.Any())
            {
                foreach (var id in userIdsAdd)
                {
                    _storePrivateUserRepository.Create(new StorePrivateUser { UserId = id, StorePrivateId = storePrivate.StorePrivateId });
                }
            }

            ClearCache();
        }

        private void UpdateDeptJoined(List<int> deptExtJoined, StorePrivate storePrivate)
        {
            if (deptExtJoined == null)
            {
                deptExtJoined = new List<int>();
            }

            IEnumerable<int> deptIdExtsAdd;
            IEnumerable<int> deptIdExtsDelete;

            var isEqual = storePrivate.StorePrivateUsers.Where(u => !u.UserId.HasValue && u.DepartmentId.HasValue).Select(ur => ur.DepartmentId.Value)
                            .CompareTo(deptExtJoined, out deptIdExtsDelete, out deptIdExtsAdd);

            if (!isEqual)
            {
                if (deptIdExtsDelete != null && deptIdExtsDelete.Any())
                {
                    var storeUserDelete = _storePrivateUserRepository.Gets(false, s => !s.UserId.HasValue && deptIdExtsDelete.Contains(s.DepartmentId.Value));
                    foreach (var storePrivateUser in storeUserDelete)
                    {
                        _storePrivateUserRepository.Delete(storePrivateUser);
                    }
                }
            }

            if (deptIdExtsAdd != null && deptIdExtsAdd.Any())
            {
                var allDeps = _deptService.GetCacheAllDepartments().Where(d => deptExtJoined.Contains(d.DepartmentId));
                foreach (var id in deptIdExtsAdd)
                {
                    var dept = allDeps.SingleOrDefault(d => d.DepartmentId.Equals(id));
                    if (dept != null)
                    {
                        _storePrivateUserRepository.Create(new StorePrivateUser
                        {
                            DepartmentId = id,
                            DepartmentIdExt = dept.DepartmentIdExt,
                            StorePrivateId = storePrivate.StorePrivateId
                        });
                    }
                }
            }
            ClearCache();
        }

        /// <summary>
        /// Mở hồ sơ
        /// </summary>
        /// <param name="id">Id hồ sơ</param>
        /// <param name="userId">Id người tạo</param>
        public void Open(int id, int userId)
        {
            var storePrivate = Get(id, userId);
            if (storePrivate == null)
            {
                throw new EgovException("Không tìm thấy hồ sơ cá nhân!");
            }
            storePrivate.Status = (byte)StorePrivateStatus.IsActive;
            Context.SaveChanges();
            ClearCache();
        }

        /// <summary>
        /// Đóng hồ sơ
        /// </summary>
        /// <param name="id">Id hồ sơ</param>
        /// <param name="userId">Id người tạo</param>
        public void Close(int id, int userId)
        {
            var storePrivate = Get(id, userId);
            if (storePrivate == null)
            {
                throw new EgovException("Không tìm thấy hồ sơ cá nhân!");
            }
            storePrivate.Status = (byte)StorePrivateStatus.IsClose;
            Context.SaveChanges();
            ClearCache();
        }

        /// <summary>
        /// Xóa hồ sơ
        /// </summary>
        /// <param name="id">Id hồ sơ</param>
        /// <param name="userId">Id người tạo</param>
        public void Delete(int id, int userId)
        {
            var storePrivate = Get(id, userId);
            if (storePrivate == null)
            {
                throw new EgovException("Không tìm thấy hồ sơ cá nhân!");
            }
            storePrivate.Status = (byte)StorePrivateStatus.IsDelete;
            Context.SaveChanges();
            ClearCache();
        }

        /// <summary>
        /// Thêm văn bản vào hồ sơ cá nhân
        /// </summary>
        /// <param name="storePrivate">Entity</param>
        /// <param name="documentCopyId">Id documentcopy</param>
        /// <param name="documentId">Id văn bản</param>
        /// <param name="isSaveChanges">Có gọi savechanges luôn trong hàm hay không?</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddDocumentToStore(StorePrivate storePrivate, int documentCopyId, Guid documentId, bool isSaveChanges = true)
        {
            if (storePrivate == null)
                throw new ArgumentNullException("storePrivate");

            if (_storePrivateDocumentCopyRepository.Exist(d => d.DocumentCopyId == documentCopyId
                        && d.DocumentId == documentId && d.StorePrivateId == storePrivate.StorePrivateId))
            {
                return;
            }

            _storePrivateDocumentCopyRepository.Create(new StorePrivateDocumentCopy()
            {
                StorePrivateId = storePrivate.StorePrivateId,
                DocumentId = documentId,
                DocumentCopyId = documentCopyId
            });

            if (isSaveChanges)
            {
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Kiểm tra xem người dùng có quyền truy cập vào hồ sơ cá nhân hay không
        /// </summary>
        /// <param name="storePrivateId">Id hồ sơ cá nhân</param>
        /// <param name="userId">Id người dùng</param>
        /// <returns></returns>
        public StorePrivate CheckPermissionStorePrivate(int storePrivateId, int userId)
        {
            if (storePrivateId <= 0)
            {
                return null;
            }

            var result = Get(storePrivateId);
            if (result == null || result.Status == (byte)StorePrivateStatus.IsDelete)
            {
                return null;
            }

            // Kiểm tra nếu người dùng hiện tại là người tạo Sổ cá nhân thì trả về luôn.
            // Trường hợp này thường xuyên xảy ra vì hồ sơ chia sẻ ko có nhiều.
            if (result.CreatedByUserId == userId)
            {
                return result;
            }

            var storePrivateUsers = _storePrivateUserRepository.GetsReadOnly(st => st.StorePrivateId == storePrivateId);
            if (!storePrivateUsers.Any())
            {
                return null;
            }

            // Lấy ra tất cả phòng ban người dùng thuộc vào.
            var userDeptExts = _deptService.GetCacheAllUserDepartmentJobTitlesPosition().Where(ud => ud.UserId.Equals(userId)).Select(ud => ud.DepartmentIdExt);

            foreach (var stUser in storePrivateUsers)
            {
                if ((stUser.UserId.HasValue && stUser.UserId.Value == userId) ||
                        (!string.IsNullOrEmpty(stUser.DepartmentIdExt) && userDeptExts.Any(ud => ud.StartsWith(stUser.DepartmentIdExt))))
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Lấy ra danh sách văn bản thuộc hồ sơ
        /// </summary>
        /// <param name="id">Id hồ sơ cá nhân</param>
        /// <param name="userId">Id người tạo</param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetDocumentsByStorePrivateId(int id, int userId)
        {
            var storePrivate = CheckPermissionStorePrivate(id, userId);

            var result = storePrivate == null ? null : Context.RawProcedure("spGetAllDocumentInStorePrivate", new SqlParameter("@storePrivateId", id));

            return result == null ? null : result.ToList();
        }

        /// <summary>
        /// Loại văn bản khỏi hồ sơ
        /// </summary>
        /// <param name="id">Id hồ sơ</param>
        /// <param name="documentCopyId">Id document copy</param>
        /// <param name="userId">Id người tạo</param>
        public void RemoveDocumentInStorePrivate(int id, int documentCopyId, int userId)
        {
            var existStorePrivate = _storePrivateRepository.Exist(s => s.StorePrivateId == id && s.CreatedByUserId == userId);
            if (!existStorePrivate)
            {
                throw new EgovException("Hồ sơ không tồn tại");
            }
            var storeDocumentCopy =
                _storePrivateDocumentCopyRepository.Get(false,
                    s => s.StorePrivateId == id && s.DocumentCopyId == documentCopyId);
            if (storeDocumentCopy == null)
            {
                throw new EgovException("Văn bản này không tồn tại trong hồ sơ");
            }
            _storePrivateDocumentCopyRepository.Delete(storeDocumentCopy);
            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm tài liệu vào hồ sơ cá nhân
        /// </summary>
        /// <param name="storePrivateId">Id hồ sơ cá nhân</param>
        /// <param name="description">Mô tả về tài liệu</param>
        /// <param name="filename">Tên tài liệu</param>
        /// <param name="stream">File Stream</param>
        public void AddAttachment(int storePrivateId, string description, string filename, Stream stream)
        {
            var currentUser = _userService.CurrentUser;
            var storePrivate = CheckPermissionStorePrivate(storePrivateId, currentUser.UserId);
            if (storePrivate == null)
            {
                throw new EgovException("Bạn không có quyền truy cập vào hồ sơ này");
            }
            var currentFileLocation = _fileLocationService.GetCurrent();
            var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
            var fileInfo = transfer.Upload(stream, FileType.Attach); //TODO: TrungVH - Sau này cần thay thành FileType.StorePrivateAttach
            var attachment = new StorePrivateAttachment
            {
                AttachmentName = filename,
                CreatedByUserId = currentUser.UserId,
                CreatedByUserName = currentUser.Username,
                CreatedOnDate = fileInfo.CreatedDate,
                Description = description,
                FileLocationId = currentFileLocation.FileLocationId,
                FileLocationKey = !string.IsNullOrEmpty(fileInfo.RootFolder) ? fileInfo.RootFolder : null,
                FileName = fileInfo.FileName,
                IdentityFolder = fileInfo.IdentityFolder,
                Size = (int)fileInfo.Length,
                StorePrivateId = storePrivate.StorePrivateId
            };
            try
            {
                _storePrivateAttachmentRepository.Create(attachment);

                Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        /// <summary>
        /// Lấy ra tài liệu trong hồ sơ cá nhân theo Id
        /// </summary>
        /// <param name="id">Id tài liệu</param>
        /// <returns></returns>
        public StorePrivateAttachment GetAttachment(int id)
        {
            return _storePrivateAttachmentRepository.Get(id);
        }

        /// <summary>
        /// Lấy ra tài liệu trong hồ sơ cá nhân theo Id
        /// </summary>
        /// <param name="attachment">Tài liệu</param>
        public void RemoveAttachment(StorePrivateAttachment attachment)
        {
            if (attachment == null)
            {
                throw new ArgumentNullException("attachment");
            }
            _storePrivateAttachmentRepository.Delete(attachment);
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về văn bản trong hồ sơ
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="documentCopyIds"></param>
        /// <returns></returns>
        public IEnumerable<StorePrivateDocumentCopy> GetDocuments(int storeId, List<int> documentCopyIds)
        {
            return _storePrivateDocumentCopyRepository.Gets(false, s => s.StorePrivateId == storeId && documentCopyIds.Contains(s.DocumentCopyId));
        }

        /// <summary>
        /// Xóa văn bản khỏi hồ sơ
        /// </summary>
        /// <param name="documents"></param>
        public void RemoveDocuments(IEnumerable<StorePrivateDocumentCopy> documents)
        {
            foreach (var document in documents)
            {
                _storePrivateDocumentCopyRepository.Delete(document);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Tải tệp đính kèm
        /// </summary>
        /// <param name="fileName">Tên tài liêu</param>
        /// <param name="id">Id tài liệu</param>
        /// <param name="userId"></param>
        /// <returns>Stream</returns>
        public Stream DownloadAttachment(out string fileName, int id, int userId)
        {
            var attachment = GetAttachment(id);
            if (attachment == null)
            {
                throw new EgovException("Không tìm thấy tài liệu");
            }
            fileName = attachment.AttachmentName;
            var storePrivate = CheckPermissionStorePrivate(attachment.StorePrivateId, userId);
            if (storePrivate == null)
            {
                throw new EgovException("Bạn không có quyền xem tài liệu này");
            }
            var fileLocation = attachment.FileLocation;
            if (fileLocation == null)
            {
                throw new EgovException("Không tìm thấy nơi lưu tệp đính kèm");
            }
            var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
            var downloaded =
                transfer.Download(new FileTransferInfo
                {
                    CreatedDate = attachment.CreatedOnDate,
                    FileName = attachment.FileName,
                    FileType = FileType.Attach, //TODO: TrungVH - Sau này cần thay thành FileType.StorePrivateAttach
                    IdentityFolder = attachment.IdentityFolder,
                    RootFolder = attachment.FileLocationKey
                });

            return downloaded;
        }

        /// <summary>
        /// Xoas cache hoof sow ca nhan
        /// </summary>
        public void ClearCache()
        {
            var cacheKey = string.Format(CacheParam.PrivateStore, "all");
            _cache.Remove(cacheKey);
        }
    }
}
