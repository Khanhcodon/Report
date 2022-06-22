using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : InfringeListBll - public - BLL</para>
    /// <para>Access Modifiers:</para>
    /// <para>Create Date : 010516</para>
    /// <para>Author      : DungNVL</para>
    /// <para>Description : BLL tương ứng với bảng CheckInfringe trong CSDL</para>
    /// </summary>
    public class InfringeListBll
    {
        private readonly IRepository<CheckInfringe> _checkinfringeRepository;
        private IDbContext _context;

        /// <summary>
        /// Hàm lấy giá trị ngày tháng là ngày đầu tiên của tuần được chọn
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <returns></returns>
        private DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="context"></param>
        public InfringeListBll(IDbCustomerContext context)
        {
            _context = context;
            _checkinfringeRepository = _context.GetRepository<CheckInfringe>();
        }
        /// <summary>
        /// Xem các vi phạm
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CheckInfringe> GetReadOnlys()
        {
            return _checkinfringeRepository.Gets(true, null).OrderBy(c => c.InfringeUserId);
        }

        /// <summary>
        /// Cập nhật lại vi phạm
        /// </summary>
        /// <param name="entity"></param>
        public void Update(CheckInfringe entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Xóa Vi phạm
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(CheckInfringe entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _checkinfringeRepository.Delete(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới vi phạm
        /// </summary>
        /// <param name="entity"></param>
        public void Create(CheckInfringe entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _checkinfringeRepository.Create(entity);
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                string rs = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    rs = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    Console.WriteLine(rs);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        rs += "<br />" + string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(rs);
            }
        }

        /// <summary>
        /// Trả về Tiêu chí có ID bằng với giá trị đầu vào
        /// </summary>
        /// <returns></returns>
        public CheckInfringe GetbyID(int ID)
        {
            return _checkinfringeRepository.Get(false, x => x.CheckInfringeId == ID);
        }
        
        /// <summary>
        /// Hàm Kiểm tra xem có bao nhiều trường thuộc rateemloyeeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int CheckByRateEmployeeID(int id)
        {
            return _checkinfringeRepository.Gets(true, x => x.RateEmployeeId == id).Count();
        }
        
        /// <summary>
        /// Trả về Tiêu chí có của user có id bằng với giá trị đầu vào
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CheckInfringe> Get(int userId)
        {
            return _checkinfringeRepository.Gets(false, x => x.InfringeUserId == userId && x.IsActived == true).OrderBy(c => c.RateEmployeeId);
        }

        /// <summary>
        /// Trả về danh sách tất cả các lịch đã được duyệt theo tuần
        /// </summary>
        /// <param name="week">Tuần cần lấy</param>
        /// <param name="year">Tuần cần lấy</param>
        /// <returns></returns>
        public IEnumerable<CheckInfringe> GetByDatetime(int week, int year)
        {
            var weeknumber = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            //Todo: viết thêm vào
            DateTime from = FirstDateOfWeek(year, week);
            DateTime to = FirstDateOfWeek(year, week).AddDays(6);
            return GetbyDatetime(from, to);
        }

        /// <summary>
        /// Trả về danh sách tất cả các vi phạm đã được duyệt theo một khoảng thời gian
        /// </summary>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <returns></returns>
        public IEnumerable<CheckInfringe> GetbyDatetime(DateTime from, DateTime to)
        {

            //Todo: viết thêm vào

            return _checkinfringeRepository.Gets(true,
                 c => c.Date >= from.Date
                 && c.Date <= to.Date
                 && c.IsActived == true
                 ).OrderByDescending(d=>d.Date);

        }
        
        /// <summary>
        /// Trả về danh sách tất cả các vi pham  được duyệt theo ngày
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CheckInfringe> GetByDatetime(DateTime date)
        {

            return _checkinfringeRepository.Gets(true, c => c.Date.Year == date.Year
                && c.Date.Month == date.Month
                && c.Date.Day == date.Day
                 && c.IsActived == true).OrderBy(c => c.InfringeUserId).ThenBy(c => c.RateEmployeeId); ;

        }

        /// <summary>
        /// Trả về danh sách tất cả các vi phạm đã được duyệt theo một khoảng thời gian
        /// </summary>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <param name="userid">Đến khoảng thời gian</param>
        /// <returns></returns>
        public IEnumerable<CheckInfringe> GetbyDatetimeByUser(DateTime from, DateTime to, int userid)
        {
            //Todo: viết thêm vào
            return _checkinfringeRepository.Gets(true, c =>  c.Date >= from.Date
                 && c.Date <= to.Date
                 && c.IsActived == true
                 && c.InfringeUserId == userid).OrderByDescending(c => c.Date);
        }

        /// <summary>
        /// Trả về danh sách tất cả các vi phạm đã được duyệt theo một khoảng thời gian
        /// </summary>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <param name="listuser">Đến khoảng thời gian</param>
        /// <returns></returns>
        public IEnumerable<CheckInfringe> GetByDateTimeByUserRole(DateTime from, DateTime to, List<int> listuser)
        {
            //Todo: viết thêm vào
            return _checkinfringeRepository.Gets(true,
                c => c.Date >= from.Date
                && c.Date <= to.Date
                && c.IsActived == true
                && listuser.Contains(c.InfringeUserId)
                );
        }
    }
}
