using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.Entity.Validation;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class CalendarBll
    {
        private readonly IRepository<Calendar> _calendarRepository;
        private readonly IRepository<CalendarDetail> _calendarDetailRepository;
        private readonly IRepository<CalendarResource> _calendarResourceRepository;
        private IDbContext _context;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"></param>
        public CalendarBll(IDbCustomerContext context)
        {
            _context = context;
            _calendarRepository = _context.GetRepository<Calendar>();
            _calendarDetailRepository = _context.GetRepository<CalendarDetail>();
            _calendarResourceRepository = _context.GetRepository<CalendarResource>();
        }

        /// <summary>
        /// Lấy lịch theo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="withContent"></param>
        /// <returns></returns>
        public Calendar Get(int id, bool withContent = true)
        {
            var result = _calendarRepository.Get(id);
            if (result == null)
            {
                return null;
            }

            if (withContent)
            {
                result.Contents = _calendarDetailRepository.Gets(false, c => c.CalendarId == result.CalendarId);
            }

            return result;
        }

        /// <summary>
        /// Cập nhật lịch vào hệ thống
        /// </summary>
        /// <param name="model"></param>
        public void Update(Calendar model)
        {
            try
            {
                var calendar = Get(model.CalendarId);
                if (calendar == null)
                {
                    throw new ArgumentNullException("calendar");
                }

                calendar.Title = model.Title;
                calendar.TitlePublish = model.TitlePublish;
                calendar.PlacePublish = model.PlacePublish;
                calendar.UserPrimaryPublish = Microsoft.JScript.GlobalObject.unescape(model.UserPrimaryPublish);
                calendar.HasPublish = model.HasPublish;
                calendar.Place = model.Place;
                calendar.BeginTime = model.BeginTime;
                calendar.DepartmentIdExt = model.DepartmentIdExt;
                calendar.Order = model.Order;

                var detailIds = new List<int>();
                foreach (var contentModel in model.Contents)
                {
                    if (contentModel.CalendarDetailId == 0)
                    {
                        contentModel.CalendarId = calendar.CalendarId;
                        contentModel.UserPrimary = Microsoft.JScript.GlobalObject.unescape(contentModel.UserPrimary);
                        contentModel.UserSecondary = Microsoft.JScript.GlobalObject.unescape(contentModel.UserSecondary);
                        _calendarDetailRepository.Create(contentModel);
                        continue;
                    }

                    var detail = _calendarDetailRepository.Get(contentModel.CalendarDetailId);
                    detail.Content = contentModel.Content;
                    detail.Department = contentModel.Department;
                    detail.Joined = contentModel.Joined;
                    detail.Note = contentModel.Note;
                    detail.UserPrimary = Microsoft.JScript.GlobalObject.unescape(contentModel.UserPrimary);
                    detail.UserSecondary = Microsoft.JScript.GlobalObject.unescape(contentModel.UserSecondary);
                    detail.Prepare = contentModel.Prepare;

                    _context.SaveChanges();
                    detailIds.Add(detail.CalendarDetailId);
                }

                if (detailIds.Any())
                {
                    var deleteDetails = _calendarDetailRepository.Gets(false, c => c.CalendarId == calendar.CalendarId && !detailIds.Contains(c.CalendarDetailId));
                    foreach (var detail in deleteDetails)
                    {
                        _calendarDetailRepository.Delete(detail);
                    }
                }

                _context.SaveChanges();
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
        /// Xóa lịch
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Calendar entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var trans = new TransactionScope(TransactionScopeOption.Required))
            {
                var details = _calendarDetailRepository.Gets(false, c => c.CalendarId == entity.CalendarId);
                foreach (var detail in details)
                {
                    _calendarDetailRepository.Delete(detail);
                }

                _calendarRepository.Delete(entity);

                _context.SaveChanges();

                trans.Complete();
            }
        }

        /// <summary>
        /// Thêm lịch mới
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Calendar entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.DateCreated = DateTime.Now;

            using (var tran = new TransactionScope(TransactionScopeOption.Required))
            {
                entity.UserPrimaryPublish = Microsoft.JScript.GlobalObject.unescape(entity.UserPrimaryPublish);
                _calendarRepository.Create(entity);
                _context.SaveChanges();

                foreach (var content in entity.Contents)
                {
                    content.CalendarId = entity.CalendarId;
                    content.UserPrimary = Microsoft.JScript.GlobalObject.unescape(content.UserPrimary);
                    content.UserSecondary = Microsoft.JScript.GlobalObject.unescape(content.UserSecondary);
                    _calendarDetailRepository.Create(content);
                }

                _context.SaveChanges();

                tran.Complete();
            }

        }

        /// <summary>
        /// Lấy ra lịch đăng ký đã được duyệt theo khoảng thời gian
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="userId"></param>
        /// <param name="withContent"></param>
        /// <returns></returns>
        public IEnumerable<Calendar> GetAccepteds(DateTime from, DateTime to, int userId = 0, bool withContent = true)
        {
            var result = _calendarRepository.Gets(true, c =>
                                    c.BeginTime >= from && c.BeginTime <= to
                                    && c.IsAccepted == true
                                    && c.IsPrivate == false
                                    && c.BeginTime.HasValue
                                    && (userId == 0 || c.UserCreatedId == userId))
                                    .OrderBy(c => c.BeginTime.Value.TimeOfDay);

            if (withContent)
            {
                var calendarIds = result.Select(c => c.CalendarId);
                var details = _calendarDetailRepository.GetsReadOnly(c => calendarIds.Contains(c.CalendarId));

                foreach (var calendar in result)
                {
                    var contents = details.Where(c => c.CalendarId == calendar.CalendarId);
                    calendar.Contents = contents;
                }
            }

            return result;
        }

        /// <summary>
        /// Trả về Lịch họp hôm nay
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="withContent"></param>
        /// <returns></returns>
        public IEnumerable<Calendar> GetToday(DateTime from, DateTime to, bool withContent = true)
        {
            var result = _calendarRepository.Gets(true, c =>
                                       c.BeginTime >= from && c.BeginTime <= to
                                       && c.IsAccepted == true
                                       && c.HasPublish
                                       && c.BeginTime.HasValue
                                       && c.IsPrivate == false)
                                       .OrderBy(c => c.Order);

            if (withContent)
            {
                var calendarIds = result.Select(c => c.CalendarId);
                var details = _calendarDetailRepository.GetsReadOnly(c => calendarIds.Contains(c.CalendarId));

                foreach (var calendar in result)
                {
                    var contents = details.Where(c => c.CalendarId == calendar.CalendarId);
                    calendar.Contents = contents;
                }
            }

            return result;
        }

        /// <summary>
        /// Lấy ra lịch cá nhân theo khoảng thời gian
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="userId"></param>
        /// <param name="withContent"></param>
        /// <returns></returns>
        public IEnumerable<Calendar> GetsPrivate(DateTime from, DateTime to, int userId = 0, bool withContent = true)
        {
            var result = _calendarRepository.Gets(true, c =>
                                    c.BeginTime >= from && c.BeginTime <= to
                                    && c.IsAccepted == true
                                    && c.IsPrivate == true
                                    && (userId == 0 || c.UserCreatedId == userId))
                                    .OrderBy(c => c.BeginTime);

            if (withContent)
            {
                var calendarIds = result.Select(c => c.CalendarId);
                var details = _calendarDetailRepository.GetsReadOnly(c => calendarIds.Contains(c.CalendarId));

                foreach (var calendar in result)
                {
                    var contents = details.Where(c => c.CalendarId == calendar.CalendarId);
                    calendar.Contents = contents;
                }
            }

            return result;
        }

        /// <summary>
        /// Lấy lịch chưa được duyệt theo loại
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="withContent"></param>
        /// <returns></returns>
        public IEnumerable<Calendar> GetNotAccepteds(int userId = 0, bool withContent = true)
        {
            var result = _calendarRepository.Gets(true, c => (!c.IsAccepted.HasValue || c.IsAccepted.Value == false)
                            && (userId == 0 || c.UserCreatedId == userId))
                            .OrderBy(c => c.BeginTime);

            if (withContent)
            {
                var calendarIds = result.Select(c => c.CalendarId);
                var details = _calendarDetailRepository.GetsReadOnly(c => calendarIds.Contains(c.CalendarId));

                foreach (var calendar in result)
                {
                    var contents = details.Where(c => c.CalendarId == calendar.CalendarId);
                    calendar.Contents = contents;
                }
            }

            return result;
        }

        /// <summary>
        /// Trả về danh sách lịch không được duyệt
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Calendar> GetDoesNotAccept(DateTime dateBegin, int userId = 0)
        {
            return _calendarRepository.Gets(true, c => c.IsAccepted.Value == false
                            && c.BeginTime >= dateBegin
                            && (userId == 0 || c.UserCreatedId == userId))
                            .OrderBy(c => c.BeginTime);
        }

        /// <summary>
        /// Trả về danh sách các đơn vị chuẩn bị
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetDepartment()
        {
            return _calendarDetailRepository.GetsAs(c => c.Department, c => !string.IsNullOrWhiteSpace(c.Department)).OrderBy(c => c).Take(20);
        }

        /// <summary>
        /// Trả về danh sách tài nguyên
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<CalendarResource> GetResources(int userId = 0)
        {
            return _calendarResourceRepository.GetsReadOnly(r => userId == 0 || r.UserId == userId);
        }

        /// <summary>
        /// Thêm tài nguyên
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="userId"></param>
        public void AddResource(string resource, int userId)
        {
            resource = resource.Trim();
            if (_calendarResourceRepository.Exist(r => r.UserId == userId && r.Name.Equals(resource, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            var newResource = new CalendarResource()
            {
                Name = resource,
                UserId = userId
            };

            _calendarResourceRepository.Create(newResource);
            _context.SaveChanges();
        }

        /// <summary>
        /// Xóa tài nguyên
        /// </summary>
        /// <param name="id"></param>
        public void DeleteResource(int id)
        {
            var resource = _calendarResourceRepository.Get(id);
            if (resource != null)
            {
                _calendarResourceRepository.Delete(resource);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Duyệt lịch họp
        /// </summary>
        /// <param name="calendar"></param>
        /// <param name="isAccept"></param>
        public void Accept(Calendar calendar, bool isAccept)
        {
            if (calendar == null)
            {
                throw new ArgumentNullException("calendar");
            }

            calendar.IsAccepted = isAccept;
            _context.SaveChanges();
        }
    }
}
