using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.FileTransfer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : BusinessLicenseBll - public - BLL</para>
    /// <para>Create Date : 251013</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng BusinessLicense trong CSDL</para>
    /// </summary>
    public class BusinessLicenseBll : ServiceBase
    {
        private readonly IRepository<BusinessLicense> _businessLicenseRepository;
        private readonly IRepository<BusinessLicenseAttach> _businessLicenseAttachRepository;
        private readonly FileLocationSettings _fileLocationSettings;
        private readonly FileLocationBll _fileLocationService;
        private readonly AdminGeneralSettings _generalSettings;

        /// <summary>
        /// Khởi tạo class <see cref="BusinessTypeBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="fileLocationSettings"></param>
        /// <param name="fileLocationService"></param>
        /// <param name="generalSettings"></param>
        public BusinessLicenseBll(IDbCustomerContext context,
                                    FileLocationSettings fileLocationSettings,
                                    FileLocationBll fileLocationService,
                                    AdminGeneralSettings generalSettings)
            : base(context)
        {
            _businessLicenseRepository = Context.GetRepository<BusinessLicense>();
            _businessLicenseAttachRepository = Context.GetRepository<BusinessLicenseAttach>();
            _fileLocationService = fileLocationService;
            _fileLocationSettings = fileLocationSettings;
            _generalSettings = generalSettings;
        }

        /// <summary>
        /// Xóa 1 giấy phép
        /// </summary>
        /// <param name="businessLicense">Thực thể giấy phép</param>
        public void Delete(BusinessLicense businessLicense)
        {
            if (businessLicense == null)
            {
                throw new ArgumentNullException("businessLicense");
            }

            var businessLicenseAttach = _businessLicenseAttachRepository.Gets(false, b => b.BusinessLicenseId == businessLicense.BusinessLicenseId);
            if (businessLicenseAttach.Any())
            {
                foreach (var licenseAttach in businessLicenseAttach)
                {
                    _businessLicenseAttachRepository.Delete(licenseAttach);
                }
            }
            _businessLicenseRepository.Delete(businessLicense);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lất ra tất cả giấy phép theo điều kiện. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<BusinessLicense> Gets(Expression<Func<BusinessLicense, bool>> spec = null)
        {
            return _businessLicenseRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lất ra tất cả giấy phép theo điều kiện, kết quả sẽ được ánh xạ sang 1 dạng khác do người dùng cung cấp. Kết quả chỉ đọc
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<BusinessLicense, TOutput>> projector, Expression<Func<BusinessLicense, bool>> spec = null)
        {
            return _businessLicenseRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Lấy ra một giấy phép
        /// </summary>
        /// <param name="businessLicenseId">Id của giấy phép</param>
        /// <returns>Entity giấy phép</returns>
        public BusinessLicense Get(int businessLicenseId)
        {
            BusinessLicense businessLicense = null;
            if (businessLicenseId > 0)
            {
                businessLicense = _businessLicenseRepository.Get(businessLicenseId);
            }
            return businessLicense;
        }

        /// <summary>
        /// Lấy ra một giấy phép
        /// </summary>
        /// <param name="docCopyId">Id của doc copy</param>
        /// <returns>Entity giấy phép</returns>
        public BusinessLicense GetByDocCopy(int docCopyId)
        {
            try
            {
                if (docCopyId <= 0)
                {
                    return null;
                }
                var result = _businessLicenseRepository.Gets(false, b => b.DocumentCopyId == docCopyId);
                if (result.Count() == 1)
                {
                    return result.First();
                }
                if (result.Count() > 1)
                {
                    throw new Exception("Có nhiều hơn 1 giấy phép cho hồ sơ tương ứng.");
                }
                return null;   
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của 1 giấy phép theo docCopyId
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Entity giấy phép</returns>
        public bool Exist(Expression<Func<BusinessLicense, bool>> spec = null)
        {
            return _businessLicenseRepository.Exist(spec);
        }

        /// <summary>
        /// Thêm mới giấy phép
        /// </summary>
        /// <param name="businessLicense">Thực thể giấy phép</param>
        /// <param name="tempFile">Tệp đính kèm</param>
        /// <returns></returns>
        public void Create(BusinessLicense businessLicense, Dictionary<string, string> tempFile)
        {
            if (businessLicense == null)
            {
                throw new ArgumentNullException("businessLicense");
            }
            if (Exist(BusinessLicenseQuery.WithCode(businessLicense.LicenseCode)))
            {
                throw new EgovException(string.Format("Mã giấy phép ({0}) đã tồn tại!", businessLicense.LicenseCode));
            }

            //Todo: kiểm tra tồn tại giấy phép nào cho doccopy chưa?
            // ở đây

            _businessLicenseRepository.Create(businessLicense);
            if (tempFile != null)
            {
                string filename;
                var attachFile = UploadFile(tempFile, out filename);
                if (attachFile != null)
                {
                    var businessLicenseAttach = new BusinessLicenseAttach
                                                    {
                                                        BusinessLicenseId = businessLicense.BusinessLicenseId,
                                                        FileLocationId = attachFile.FileLocationId,
                                                        FileLocationKey = attachFile.FileLocationKey,
                                                        FileLocationName = attachFile.FileLocationName,
                                                        IdentityFolder = attachFile.IdentityFolder,
                                                        FilePath = filename
                                                    };
                    _businessLicenseAttachRepository.Create(businessLicenseAttach);
                }
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin giấy phép
        /// </summary>
        /// <param name="businessLicense">Entity giấy phép</param>
        /// <param name="tempFile">Tệp đính kèm</param>
        public void Update(BusinessLicense businessLicense, Dictionary<string, string> tempFile)
        {
            if (businessLicense == null)
            {
                throw new ArgumentNullException("businessLicense");
            }

            if (tempFile != null)
            {
                string filename;
                var attachFile = UploadFile(tempFile, out filename);
                if (attachFile != null)
                {
                    var businessLicenseAttach = new BusinessLicenseAttach
                    {
                        BusinessLicenseId = businessLicense.BusinessLicenseId,
                        FileLocationId = attachFile.FileLocationId,
                        FileLocationKey = attachFile.FileLocationKey,
                        FileLocationName = attachFile.FileLocationName,
                        IdentityFolder = attachFile.IdentityFolder,
                        FilePath = filename
                    };
                    _businessLicenseAttachRepository.Create(businessLicenseAttach);
                }
            }
            Context.SaveChanges();
        }

        private LicenseFile UploadFile(Dictionary<string, string> tempFile, out string filename)
        {
            filename = string.Empty;
            LicenseFile result = null;
            var file = tempFile.First();
            var tempPath = ResourceLocation.Default.FileUploadTemp;
            var currentFileLocation = _fileLocationService.GetCurrent();
            var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
            if (FileManager.Default.Exist(file.Key, tempPath))
            {
                using (var stream = FileManager.Default.Open(file.Key, tempPath))
                {
                    var fileInfo = transfer.Upload(stream, FileType.LicenseAttach);
                    result = new LicenseFile
                    {
                        DateCreated = fileInfo.CreatedDate,
                        FileLocationName = fileInfo.FileName,
                        IdentityFolder = fileInfo.IdentityFolder,
                        FileLocationKey = fileInfo.RootFolder,
                        FileLocationId = currentFileLocation.FileLocationId
                    };
                    filename = tempFile[file.Key];
                }
                //_fileManager.Delete(file.Key, tempPath);  => Cập nhật 2 lần liên tiếp sẽ lỗi, khi nào thì xóa dc?
            }
            return result;
        }

        /// <summary>
        /// Lấy ra tất cả các giấy phép có phân trang
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="businessId">Id của doanh nghiệp. Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các giấy phép của doanh nghiệp truyền vào</param>
        /// <param name="docTypeId">Id của loại giấy phép. Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các giấy phép có mã loại giấy phép là giá trị truyền vào</param>
        ///  <returns>Danh sách các giấy phép đã được phân trang</returns>
        public IEnumerable<BusinessLicense> Gets(out int totalRecords,
                                            int currentPage = 1,
                                            int? pageSize = null,
                                            string sortBy = "",
                                            bool isDescending = false,
                                            int? businessId = null,
                                            Guid? docTypeId = null)
        {
            var spec = BusinessLicenseQuery.WithBusinessId(businessId).And(BusinessLicenseQuery.WithDocTypeId(docTypeId));
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _businessLicenseRepository.Count(spec);
            var sort = Context.Filters.CreateSort<BusinessLicense>(isDescending, sortBy);
            return _businessLicenseRepository.GetsReadOnly(spec, sort, Context.Filters.Page<BusinessLicense>(currentPage, pageSize.Value));
        }
    }
}
