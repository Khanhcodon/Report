using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : PrinterBll - public - BLL</para>
    /// <para>Create Date : 280314</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : BLL tương ứng với bảng printer trong CSDL</para>
    /// </summary>
    public class PrinterBll : ServiceBase
    {
        private readonly IRepository<Entities.Customer.Printer> _printerRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly UserBll _userService;

        /// <summary>
        /// Khởi tạo class <see cref="BusinessTypeBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        /// <param name="userService">userService</param>
        public PrinterBll(IDbCustomerContext context, AdminGeneralSettings generalSettings, UserBll userService)
            : base(context)
        {
            _printerRepository = Context.GetRepository<Entities.Customer.Printer>();
            _generalSettings = generalSettings;
            _userService = userService;
        }

        /// <summary>
        /// Xóa 1 máy in
        /// </summary>
        /// <param name="printer">Entities printer</param>
        public void Delete(Entities.Customer.Printer printer)
        {
            if (printer == null)
            {
                throw new ArgumentNullException("printer");
            }
            _printerRepository.Delete(printer);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lất ra tất cả các máy in theo điều kiện. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns>Danh sách máy in theo điều kiện</returns>
        public IEnumerable<Entities.Customer.Printer> Gets(Expression<Func<Entities.Customer.Printer, bool>> spec = null)
        {
            return _printerRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Kiểm trả máy in có tồn tại hay chưa
        /// </summary>
        /// <returns>True : tồn tại ; False: chưa tồn tại</returns>
        public bool Exist(Expression<Func<Entities.Customer.Printer, bool>> spec = null)
        {
            return _printerRepository.Exist(spec);
        }

        /// <summary>
        /// Lấy ra một máy in
        /// </summary>
        /// <param name="printerId">Id của máy in</param>
        /// <returns>Entity máy in</returns>
        public Entities.Customer.Printer Get(int printerId)
        {
            Entities.Customer.Printer printer = null;
            if (printerId > 0)
            {
                printer = _printerRepository.Get(printerId);
            }
            return printer;
        }

        /// <summary>
        /// Lấy ra một máy in. Kết quả chỉ đọc
        /// </summary>
        /// <param name="printerName">Tên của máy in</param>
        /// <returns>Entity may in</returns>
        public Entities.Customer.Printer Get(string printerName)
        {
            Entities.Customer.Printer printer = null;
            if (string.IsNullOrWhiteSpace(printerName))
            {
                printer = _printerRepository.GetReadOnly(b => b.PrinterName == printerName);
            }
            return printer;
        }

        /// <summary>
        /// Thêm mới máy in
        /// </summary>
        /// <param name="printer">Thực thể máy in</param>
        /// <returns></returns>
        public void Create(Entities.Customer.Printer printer)
        {
            if (printer == null)
            {
                throw new ArgumentNullException("printer");
            }
            if (Exist(PrinterQuery.WithName(printer.PrinterName)))
            {
                throw new EgovException(string.Format("Thể loại máy in ({0}) đã tồn tại!", printer.PrinterName));
            }
            _printerRepository.Create(printer);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin máy in
        /// </summary>
        /// <param name="printer">Entity máy in</param>
        public void Update(Entities.Customer.Printer printer)
        {
            if (printer == null)
            {
                throw new ArgumentNullException("printer");
            }

            if (Exist(PrinterQuery.WithName(printer.ShareName)))
            {
                throw new EgovException(string.Format("Thể loại máy in ({0}) đã tồn tại!", printer.ShareName));
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ra tất cả các doanh nghiệp có phân trang
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        ///  <returns>Danh sách các doanh nghiệp đã được phân trang</returns>
        public IEnumerable<Entities.Customer.Printer> Gets(out int totalRecords,
                                            int currentPage = 1,
                                            int? pageSize = null,
                                            string sortBy = "",
                                            bool isDescending = false)
        {
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _printerRepository.Count();
            var sort = Context.Filters.CreateSort<Entities.Customer.Printer>(isDescending, sortBy);
            return _printerRepository.GetsReadOnly(null, sort, Context.Filters.Page<Entities.Customer.Printer>(currentPage, pageSize.Value));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Entities.Customer.Printer GetDefaultPrinter()
        {
            var user = _userService.CurrentUser;
            var depts = user.UserDepartmentJobTitless;
            var printers = Gets();
            Entities.Customer.Printer result = null;
            foreach (var printer in printers)
            {
                //Nếu máy in được cấu hình cho user này
                if (!string.IsNullOrEmpty(printer.UserIds))
                {
                    var userIds = Json2.ParseAs<List<int>>(printer.UserIds);
                    if (userIds.Contains(user.UserId))
                    {
                        return printer;
                    }
                }

                //Nếu máy in được cấu hình cho phòng ban/vị trí của user
                if (!string.IsNullOrEmpty(printer.DepartmentPositions))
                {
                    if (depts != null && depts.Count() > 0)
                    {
                        var deptPositions = Json2.ParseAs<List<dynamic>>(printer.DepartmentPositions);
                        foreach (var dept in depts)
                        {
                            foreach (var deptPosition in deptPositions)
                            {
                                var positionId = Int32.Parse(deptPosition.PositionId.ToString());
                                var deptId = Int32.Parse(deptPosition.DepartmentId.ToString());
                                if (dept.DepartmentId == deptId && (positionId == 0 || positionId == dept.PositionId))
                                {
                                    return printer;
                                }
                            }
                        }
                    }
                }

                //Nếu máy in dùng chung
                if (string.IsNullOrEmpty(printer.UserIds) && string.IsNullOrEmpty(printer.DepartmentPositions))
                {
                    result = printer;
                }
            }

            //Nếu user này không được cấu hình trong bất cứ máy in nào, chọn máy đầu tiên trong danh sách để in
            if (result == null)
            {
                result = printers.FirstOrDefault(p => p.IsActivated == true && p.IsShared == true);
            }
            return result;
        }
    }
}
