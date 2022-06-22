using Bkav.eGovCloud.Business.Tasks;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.NotificationService;
using FluentScheduler;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Helper
{
	/// <summary>
	/// 
	/// </summary>
	public class eGovScheduler : Registry
	{
		private Dictionary<string, string> _connections;

		/// <summary>
		/// 
		/// </summary>
		public MemoryCacheManager Cache { get; set; }

		/// <summary>
		/// Lịch trình chạy các job tự động.
		/// </summary>
		/// <param name="cache"></param>
		/// <remarks>
		/// Cần code sao có thể tách ra chạy service riêng, hiện tạm thời đang cho chạy chung cùng eGov
		/// </remarks>
		public eGovScheduler(MemoryCacheManager cache)
		{
			// Mặc định các schedule được cho phép chạy song song với cùng schedule trước đó đang chạy
			// Sử dụng NonReentrantAsDefault để không cho phép chạy cùng schedule nếu shedule trước đang chạy.
			NonReentrantAsDefault();

			Cache = cache;
			LogService(new List<string>() { "eGovScheduler" });

			if (DataSettings.DatabaseIsInstalled())
			{
				// Todo: cần xử lý tối ưu phần này.
				// Khi cài bản tập trung, mỗi source sẽ được cấu hình các binding domain khác nhau.
				// Chỗ này cần select các domain được cầu hình với source tương ứng, sau đó mới select các connectin theo domain.

				// Việc lấy tất cả connection này khi chạy các source sẽ bị chồng chéo lên nhau.
				_connections = GetConnections();
			}

			Register();
		}

		private void LogService(List<string> message)
		{
			var logFolder = CommonHelper.MapPath("~/Logs");
			var logFile = System.IO.Path.Combine(logFolder, "logservice_" + DateTime.Now.ToString("ddMMyyyy"));
			try
			{
				System.IO.File.AppendAllLines(logFile, message);
			}
			catch { }
		}

		private void Register()
		{
			#region Các dịch vụ chung

			// Xóa Thư mục tạm lúc 24h
			Schedule<EmptyTempFolderJob>().ToRunEvery(1).Days().At(24, 00);

			#endregion

			var connections = _connections.Values.ToList();

			LogService(new List<string>() { "connections " + connections.Count() });
#if !DEBUG
			// Tự động xóa bảng log hàng tuần lúc 23h00
			Schedule(() => { return new EmptyLogTableJob(connections); }).ToRunEvery(1).Weeks().On(DayOfWeek.Sunday).At(23, 00);

			// Tự động gửi Sms sau mỗi 20 phút trong giờ làm việc
			// Schedule(() => { return new SendSmsJob(connections); }).ToRunEvery(20).Minutes().Between(7, 00, 19, 00);

			// Tự động gửi email sau mỗi 20 phút trong giờ làm việc.
			// Schedule(() => { return new SendEmailJob(connections); }).ToRunEvery(20).Seconds().Between(7, 00, 19, 00);

            // Tự động gửi Sms sau mỗi 20 phút trong giờ làm việc theo  api
            Schedule(() => { return new SendSmsJobApi(connections); }).ToRunNow().AndEvery(2).Minutes();

            // Tự động gửi Sms sau mỗi 20 phút trong giờ làm việc theo api
            Schedule(() => { return new SendEmailJobApi(connections); }).ToRunNow().AndEvery(2).Minutes();

			// Reset nhảy số đầu năm
			Schedule(() => { return new ResetIncreateJob(connections); }).ToRunEvery(1).Years().On(1).At(00, 01);

			// Chạy câu lệnh tổng hợp dữ liệu báo cáo.
			Schedule(() => { return new StatisticJob(connections); }).ToRunEvery(1).Days().At(00, 00);
			
            // Reset nhảy số cho hồ sơ một cửa
            Schedule(() => { return new ResetIncreateHSMCJob(connections); }).ToRunNow().AndEvery(1).Days().At(00, 01);

            if (Cache != null)
            {
                // Xử lý các tác vụ trong hàng đợi.
                Schedule(() => { return new SendNotificationJob(_connections, Cache); }).ToRunEvery(5).Seconds().Between(6, 00, 22, 00);
            }

            // Chạy báo cáo tự động
            Schedule(() => { return new GetDocTypeScheduleJob(connections); }).ToRunNow();
#else
            // Tự động xóa bảng log hàng ngày lúc 23h00
            Schedule(() => { return new EmptyLogTableJob(connections); }).ToRunNow().AndEvery(1).Days().At(23, 00);


            // Tự động gửi Sms sau mỗi 20 phút trong giờ làm việc theo  api
            Schedule(() => { return new SendSmsJobApi(connections); }).ToRunNow().AndEvery(2).Minutes();


            // Tự động gửi Sms sau mỗi 20 phút trong giờ làm việc theo api
            Schedule(() => { return new SendEmailJobApi(connections); }).ToRunNow().AndEvery(2).Minutes();


            // Reset nhảy số đầu năm
            Schedule(() => { return new ResetIncreateJob(connections); }).ToRunNow().AndEvery(1).Days().At(00, 01);

            // Reset nhảy số cho hồ sơ một cửa
            Schedule(() => { return new ResetIncreateHSMCJob(connections); }).ToRunNow().AndEvery(1).Days().At(00, 01);

            // Chạy câu lệnh tổng hợp dữ liệu báo cáo.
            Schedule(() => { return new StatisticJob(connections); }).ToRunNow().AndEvery(1).Days().At(00, 00);

			if (Cache != null)
			{
				// Xử lý các tác vụ trong hàng đợi.
				Schedule(() => { return new SendNotificationJob(_connections, Cache); }).ToRunNow().AndEvery(5).Seconds();
			}

            // Chạy báo cáo tự động
            Schedule(() => { return new GetDocTypeScheduleJob(connections); }).ToRunNow();
#endif
        }

		private Dictionary<string, string> GetConnections()
		{
			var result = new Dictionary<string, string>();

#if QuanTriTapTrungEdition

            using (var db = new EfAdminContext(new MySqlConnection(DataSettings.Current.DataConnectionString)))
            {
                var domainConnections = db.RawQuery("select d.DomainName, c.ConnectionRaw from domain d join `connection` c on d.ConnectionId = c.ConnectionId where d.IsActivated = 1;");
                foreach (var connection in (domainConnections as IEnumerable<IDictionary<string, object>>))
                {
                    result.Add(connection["DomainName"].ToString(), connection["ConnectionRaw"].ToString());
                }
            }

#else

			using (var db = new MySqlConnection(DataSettings.Current.DataConnectionString))
			{
				result.Add("BkaveGov", DataSettings.Current.DataConnectionString);
			}

#endif
			return result;
		}
	}
}