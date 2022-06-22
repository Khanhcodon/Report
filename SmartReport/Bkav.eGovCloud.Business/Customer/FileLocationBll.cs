using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para> Project: eGov Cloud v1.0</para>
    /// <para> Class : FileLocationBll - public - BLL</para>
    /// <para> Access Modifiers:</para>
    /// <para> Create Date : 070313</para>
    /// <para> Author      : TrungVH</para>
    /// <para> Description : BLL tương ứng với bảng FileLocation trong CSDL</para>
    /// </summary>
    public class FileLocationBll : ServiceBase
    {
        private readonly IRepository<FileLocation> _fileLocationRepository;
        private readonly IRepository<AttachmentDetail> _attachmentDetailRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Context</param>
        public FileLocationBll(IDbCustomerContext context)
            : base(context)
        {
            _fileLocationRepository = Context.GetRepository<FileLocation>();
            _attachmentDetailRepository = Context.GetRepository<AttachmentDetail>();

        }

        /// <summary>
        /// Lấy ra tất cả các vị trí lưu file. Kết quả chỉ đọc
        /// </summary>
        /// <param name="isActivated">Lấy ra tất cả các vị trí lưu file đang hoạt động: true và ngược lại: false. Nếu null sẽ bỏ qua điều kiện này</param>
        /// <returns>Danh sách vị trí lưu file</returns>
        public IEnumerable<FileLocation> Gets(bool? isActivated = null)
        {
            return _fileLocationRepository.GetsReadOnly(FileLocationQuery.WithIsActivated(isActivated));
        }

        /// <summary>
        /// Lấy ra vị trí lưu file theo id
        /// </summary>
        /// <param name="id">Id của vị trí lưu file</param>
        /// <returns>Entity</returns>
        public FileLocation Get(int id)
        {
            FileLocation result = null;
            if (id > 0)
            {
                result = _fileLocationRepository.Get(id);
            }
            return result;
        }

        /// <summary>
        /// Lấy ra vị trí lưu file hiện tại.
        /// </summary>
        /// <returns>Entity</returns>
        public FileLocation GetCurrent()
        {
            var fileLocationActive = _fileLocationRepository.GetsReadOnly(f => f.IsActivated);
            if (fileLocationActive == null || !fileLocationActive.Any())
            {
                throw new Exception("Chưa cấu hình vị trí lưu file");
            }
            if (fileLocationActive.Count() > 1)
            {
                throw new Exception("Đang có nhiều hơn 1 vị trí lưu file");
            }

            return fileLocationActive.First();
        }

        /// <summary>
        /// Tạo mới vị trí lưu file
        /// </summary>
        /// <param name="fileLocation">Entity</param>
        public void Create(FileLocation fileLocation)
        {
            if (fileLocation == null)
            {
                throw new ArgumentNullException("fileLocation");
            }
            if (fileLocation.IsActivated)
            {
                var locationActivated = _fileLocationRepository.Gets(false, f => f.IsActivated);
                if (locationActivated != null && locationActivated.Any())
                {
                    foreach (var location in locationActivated)
                    {
                        location.IsActivated = false;
                    }
                }
            }
            _fileLocationRepository.Create(fileLocation);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật vị trí lưu file
        /// </summary>
        /// <param name="fileLocation">Entity</param>
        public void Update(FileLocation fileLocation)
        {
            if (fileLocation == null)
            {
                throw new ArgumentNullException("fileLocation");
            }
            if (fileLocation.IsActivated)
            {
                var locationActivated = _fileLocationRepository.Gets(false, f => f.IsActivated && f.FileLocationId != fileLocation.FileLocationId);
                if (locationActivated != null && locationActivated.Any())
                {
                    foreach (var location in locationActivated)
                    {
                        location.IsActivated = false;
                    }
                }
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa vị trí lưu file
        /// </summary>
        /// <param name="fileLocation">Entity</param>
        public void Delete(FileLocation fileLocation)
        {
            if (fileLocation == null)
            {
                throw new ArgumentNullException("fileLocation");
            }
            _fileLocationRepository.Delete(fileLocation);
            Context.SaveChanges();
        }

        /// <summary>
        /// Kiểm tra vị trí này đã được sử dụng hay chưa
        /// </summary>
        /// <param name="fileLocationId">Id vị trí lưu file</param>
        /// <returns></returns>
        public bool IsUsed(int fileLocationId)
        {
            return _attachmentDetailRepository.Exist(a => a.FileLocationId == fileLocationId);
        }
    }
}
