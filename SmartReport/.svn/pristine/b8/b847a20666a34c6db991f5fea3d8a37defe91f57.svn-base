using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Models
{
    public class CalendarModel
    {
        public int CalendarId { get; set; }

        public DateTime BeginTime { get; set; }

        public string Date
        {
            get
            {
                return string.Format("{0} {1}, {2}", BeginTime.DayOfDayName(), BeginTime.DayOfWeekName(), BeginTime.ToString("dd/MM"));
            }
        }

        public string Time
        {
            get
            {
                return BeginTime.ToString(@"HH \g\iờ mm");
            }
        }

        public string Title { get; set; }

        public string TitlePublish { get; set; }

        /// <summary>
        /// Thiết lập địa điểm họp công khai
        /// </summary>
        public string PlacePublish { get; set; }

        /// <summary>
        /// Thiết lập người chủ trì công khai
        /// </summary>
        public string UserPrimaryPublish { get; set; }
        
        public string Place { get; set; }

        public bool IsPrivate { get; set; }

        public bool? IsAccepted { get; set; }

        public string DepartmentIdExt { get; set; }

        public int UserCreatedId { get; set; }

        public string UserCreatedName { get; set; }

        public bool IsToday
        {
            get
            {
                return BeginTime.ToString("d") == DateTime.Today.ToString("d");
            }
        }

        public IEnumerable<CalendarDetailModel> Contents { get; set; }
    }

    public class CalendarDetailModel
    {
        public string Content { get; set; }

        public string UserPrimary { get; set; }

        public string UserSecondary { get; set; }

        public string Department { get; set; }

        public string Joined { get; set; }

        public string Note { get; set; }

        public string Prepare { get; set; }
    }
}