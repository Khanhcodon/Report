using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Core;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Bkav.eGovCloud.Api.Controller
{
	public class StatisticApiController : EgovApiBaseController
	{
		private readonly StatisticBll _statisticService;
		private readonly DocumentCopyBll _documentCopyService;
		private readonly DocumentBll _docService;

		private const string DEFAULT_DATETIME_FORMAT = "dd/MM/yyyy";

		public StatisticApiController()
		{
			_statisticService = DependencyResolver.Current.GetService<StatisticBll>();
			_documentCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
			_docService = DependencyResolver.Current.GetService<DocumentBll>();
		}

        public dynamic GetDocumentByDate(int datekey)
        {
            var docs = _docService.Gets(true, d => d.DateCreated.ToString("yyyyMMdd") == datekey.ToString() && (d.Status == 4 || d.Status == 8));
            if (docs != null || docs.Any())
            {
                var result = docs.Select(d => new {
                    Ten_bao_cao = d.Compendium,
                    Don_vi_gui = d.InOutPlace,
                    Don_vi_nhan = d.OrganizationCode,
                    Status = d.Status,
                    Thoi_gian_gui = d.DateArrived,
                    datekey = datekey
                });

                return result;
            }

            return null;
        }

		#region API cho trang DVC

		[System.Web.Http.HttpGet]
		public ProgressStatistic GetStatistic(int type, int year, int haftyear, int quarter, int month)
		{
			DateTime start;
			DateTime end;
			GetStatisticTime(type, year, haftyear, quarter, month, out start, out end);
			var result = _statisticService.GetProgressStatistic(true, false, true, start, end);
			return result;
		}

		[System.Web.Http.HttpGet]
		public dynamic GetTotalDocument(int type, int year, int haftyear, int quarter, int month)
		{
			DateTime start;
			DateTime end;
			GetStatisticTime(type, year, haftyear, quarter, month, out start, out end);
			var totalVBDen = _statisticService.GetTotalDocument(true, start, end);
			var totalVBDi = _statisticService.GetTotalDocument(true, start, end, 2);
			return new
			{
				TotalVBDen = totalVBDen,
				TotalVBDi = totalVBDi
			};
		}

		/// <summary>
		/// Trả về thống kế xử lý hsmc, xlvb
		/// </summary>
		/// <param name="type">Kiểu thời gian: 1 - theo năm; 2 - theo nửa năm; 3 - theo quý; 4 - theo tháng; còn lại - tùy chọn</param>
		/// <param name="year"></param>
		/// <param name="haftyear"></param>
		/// <param name="quarter"></param>
		/// <param name="month"></param>
		/// <returns></returns>
		[System.Web.Http.HttpGet]
		public IEnumerable<ProgressStatistic> GetStatisticDetail(int type, int year, int haftyear, int quarter, int month)
		{
			DateTime start;
			DateTime end;
			GetStatisticTime(type, year, haftyear, quarter, month, out start, out end);
			var result = _statisticService.GetProgressStatisticDetail(true, false, true, start, end);
			return result;
		}

		#endregion

		#region Giám sát HSMC

		[System.Web.Http.HttpGet]
		public dynamic GetStatistic(DateTime from, DateTime to, bool hasHsmc = false, bool hasXlvb = true, bool? hasOldDocument = true)
		{
			var result = _statisticService.GetProgressStatistic(hasOldDocument.Value, hasXlvb, hasHsmc, from, to);

			var docPublishs = _statisticService.CountLienThong(from, to);
			var docOnlines = 0;
#if HoSoMotCuaEdition
            docOnlines = _statisticService.CountDocumentOnline(hasOldDocument.Value, from, to);
#endif

			return new
			{
				docs = result,
				customerCount = 0,
				docPublisheds = docPublishs,
				docOnlines = docOnlines,
			};
		}

		/// <summary>
		/// Trả về thống kê xử lý hsmc, vb từ trang Giasm sát
		/// </summary>
		/// <param name="from">Thời gian bắt đầu lấy thống kê.</param>
		/// <param name="to">Thời gian kết thúc lấy thống kê</param>
		/// <param name="hasHsmc">Trạng thái xác định lấy dữ liệu hồ sơ một cửa.</param>
		/// <param name="hasXlvb">Trạng thái xác định lấy dữ liệu xử lý văn bản.</param>
		/// <param name="hasOldDocument">Trạng thái xác định lấy dữ liệu tồn kỳ trước.</param>
		/// <param name="groupBy">Nhóm mặc định</param>
		/// <returns></returns>
		[System.Web.Http.HttpGet]
		public dynamic GetStatisticDetail(DateTime from, DateTime to, bool hasHsmc = false, bool hasXlvb = true, bool? hasOldDocument = true, string groupBy = "DoctypeName")
		{
			var result = _statisticService.GetProgressStatisticDetail(hasOldDocument.Value, hasXlvb, hasHsmc, from, to, groupBy);

			var customers = 0;

			var docPublishs = _statisticService.CountLienThong(from, to);
			var docOnlines = 0;
#if HoSoMotCuaEdition
            docOnlines = _statisticService.CountDocumentOnline(hasOldDocument.Value, from, to);
            customers =  _statisticService.CountCustomer(from, to);
#endif

			return new
			{
				docs = result,
				docPublisheds = docPublishs,
				docOnlines = docOnlines,
				customerCount = customers,
			};
		}

		[System.Web.Http.HttpGet]
		public IEnumerable<DocumentOverdue> GetAllDocumentOverdue(DateTime from, DateTime to, bool hasHsmc = false, bool hasXlvb = true, bool hasOldDocument = false)
		{
			return _statisticService.GetDocumentOverdues(hasOldDocument, from, to, hasHsmc, hasXlvb);
		}

		[System.Web.Http.HttpGet]
		public IEnumerable<DocumentOverdue> GetAllDocumentDungHan(DateTime from, DateTime to, bool hasHsmc = false, bool hasXlvb = true, bool hasOldDocument = false)
		{
			return _statisticService.GetDocumentDungHans(hasOldDocument, from, to, hasHsmc, hasXlvb);
		}

		[System.Web.Http.HttpGet]
		public IEnumerable<DocumentOverdue> GetAllDocumentTreHan(DateTime from, DateTime to, bool hasHsmc = false, bool hasXlvb = true, bool hasOldDocument = false)
		{
			var result = _statisticService.GetDocumentTreHans(hasOldDocument, from, to, hasHsmc, hasXlvb);
			return result;
		}

		[System.Web.Http.HttpGet]
		public IEnumerable<DocumentOverdue> GetAllDocumentChuaDenHan(DateTime from, DateTime to, bool hasHsmc = false, bool hasXlvb = true, bool hasOldDocument = false)
		{
			return _statisticService.GetDocumentChuaDenHans(hasOldDocument, from, to, hasHsmc, hasXlvb);
		}

		[System.Web.Http.HttpGet]
		public IEnumerable<DocumentOverdue> GetAllDocumentQuaHan(DateTime from, DateTime to, bool hasHsmc = false, bool hasXlvb = true, bool hasOldDocument = false)
		{
			return _statisticService.GetDocumentQuaHans(hasOldDocument, from, to, hasHsmc, hasXlvb);
		}

		[System.Web.Http.HttpGet]
		public IEnumerable<DocumentOverdue> GetOverdueByWorkflow(DateTime from, DateTime to)
		{
			return _statisticService.GetOverdueByWorkflow(from, to);
		}

		[System.Web.Http.HttpGet]
		public DocumentOverdueDetail GetProcessDocumentDetail(int docCopyId)
		{
			var docCopy = _documentCopyService.Get(docCopyId);
			var doc = docCopy.Document;
			var docTimeline = new DocumentOverdueDetail();
			docTimeline.CitizenName = doc.CitizenName;
			docTimeline.DocCode = doc.DocCode;
			docTimeline.DocTypeName = doc.DocTypeName;
			var timelines = _statisticService.GetDocumentProcessDetail(docCopy);
			docTimeline.ListTimeLine = timelines;

			return docTimeline;
		}

		#endregion

		#region Cache

		/// <summary>
		/// Xóa cache thống kê
		/// </summary>
		/// <returns></returns>
		[System.Web.Http.HttpGet]
		public bool ClearCache()
		{
			_statisticService.ClearCache();

			return true;
		}

		#endregion

		#region Liên thông

		/// <summary>
		/// 
		/// </summary>
		/// <param name="hasOldDocument"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <returns></returns>
		[System.Web.Http.HttpGet]
		public IEnumerable<LienThongDto> GetLienThongs(bool hasOldDocument, DateTime from, DateTime to)
		{
			var result = _statisticService.GetLienThongs(from, to);
			return result;
		}

		[System.Web.Http.HttpGet]
		public PublicStat PublisStat(string fromDate, string toDate)
		{
			var domains = new Dictionary<string, string>();
			var from = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
			var to = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

#if QuanTriTapTrungEdition
			using (var db = new EfAdminContext(new MySqlConnection(DataSettings.Current.DataConnectionString)))
			{
				var domainConnections = db.RawQuery("select d.CustomerName as DomainName, c.ConnectionRaw from domain d join `connection` c on d.ConnectionId = c.ConnectionId where d.IsActivated = 1 and d.CustomerName not like 'eGov%';");
				foreach (var connection in (domainConnections as IEnumerable<IDictionary<string, object>>))
				{
					domains.Add(connection["DomainName"].ToString(), connection["ConnectionRaw"].ToString());
				}
			}
#else
			using (var db = new MySqlConnection(DataSettings.Current.DataConnectionString))
			{
				domains.Add("BkaveGov", DataSettings.Current.DataConnectionString);
			}
#endif

			if (domains.Count == 0)
			{
				return null;
			}

			var statOrgans = new List<PublicStat>();
			var cmd = @"select CONCAT(d.c, ';', d1.c) as result from
						((SELECT COUNT(1) as c
						from document d
						WHERE d.`Status` not in (1, 8, 32) and Original = 2
							and d.DateCreated > @from and DateCreated <= @to) as d
						join (select count(DISTINCT DocumentId) as c
						from doc_publish
						WHERE HasLienThong = 1 AND IsPending = 0 AND DatePublished > @from and DatePublished <= @to) as d1);";
			var paras = new List<MySqlParameter>();
			paras.Add(new MySqlParameter("from", from));
			paras.Add(new MySqlParameter("to", to));

			var totalSent = 0;
			var totalReceived = 0;

			foreach (var domain in domains)
			{
				var newStat = new PublicStat();
				newStat.OrganName = domain.Key;

				var connection = new MySqlConnection(domain.Value);
				using (var context = new EfContext(connection))
				{
					var vb = context.RawQuery(cmd, paras.ToArray()); // den;di
					var sqlresult = Json2.ParseAs<List<Dictionary<string, string>>>(Json2.Stringify(vb));
					var vbdendi = sqlresult[0]["result"].Split(';');
					if (vbdendi.Length != 2) continue;

					newStat.Received = int.Parse(vbdendi[0]);
					newStat.Sent = int.Parse(vbdendi[1]);
					newStat.All = newStat.Received + newStat.Sent;
					newStat.LastUpdate = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

					totalReceived += newStat.Received;
					totalSent += newStat.Sent;
				}

				statOrgans.Add(newStat);
			}

			var result = new PublicStat();
			result.Item = statOrgans;
			result.All = totalSent + totalReceived;
			result.Sent = totalSent;
			result.Received = totalReceived;
			result.OrganName = "";
			result.LastUpdate = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

			return result;
		}

		#endregion

		#region Đăng ký qua mạng

		[System.Web.Http.HttpGet]
		public ProgressStatistic GetDocumentOnlineStatictis(DateTime from, DateTime to, bool? hasOldDocument = true)
		{
			var result = _statisticService.GetOnlineStatistic(hasOldDocument.Value, from, to);
			return result;
		}

		[System.Web.Http.HttpGet]
		public IEnumerable<ProgressStatistic> GetDocumentOnlineDetails(bool hasOldDocument, DateTime from, DateTime to)
		{
			return _statisticService.GetOnlineStatisticDetail(hasOldDocument, from, to);
		}

		[System.Web.Http.HttpGet]
		public IEnumerable<DocumentOverdue> GetAllDocumentOnline(bool hasOldDocument, DateTime from, DateTime to)
		{
			return _statisticService.GetDocumentOnlines(hasOldDocument, from, to);
		}

		[System.Web.Http.HttpGet]
		public ProgressStatistic GetAllDocumentOnlineDVC(int type, int year, int haftyear, int quarter, int month)
		{
			DateTime from;
			DateTime to;
			GetStatisticTime(type, year, haftyear, quarter, month, out from, out to);

			return _statisticService.GetOnlineStatistic(true, from, to);
		}

		[System.Web.Http.HttpGet]
		public IEnumerable<ProgressStatistic> GetDocumentOnlineDetailsDVC(int type, int year, int haftyear, int quarter, int month)
		{
			DateTime from;
			DateTime to;
			GetStatisticTime(type, year, haftyear, quarter, month, out from, out to);
			return _statisticService.GetOnlineStatisticDetail(true, from, to);
		}

		#endregion

		//#region Xử lý văn bản
		[System.Web.Http.HttpGet]
		public dynamic GetsVbDen(StatisticsCriteriaModel model)
		{
			var total = 0;
			var result = _statisticService.GiamSatTong_SoVanBanDen(model.StoreId, model.From, model.To, model.GroupBy);

			return result;
		}

		[System.Web.Http.HttpGet]
		public dynamic GetsVbDi(StatisticsCriteriaModel model)
		{
			var total = 0;
			var result = _statisticService.GiamSatTong_SoVanBanDi(model.StoreId, model.From, model.To, model.GroupBy);

			return result;
		}

		public dynamic GetsGeneral(StatisticsCriteriaModel model)
		{
			var total = 0;
			var result = _statisticService.GiamSatTong_SoVanBanDi(model.StoreId, model.From, model.To, model.GroupBy);

			return result;
		}
		//#endregion

		#region Private Methods

		private void GetStatisticTime(int type, int year, int haftyear, int quarter, int month, out DateTime start, out DateTime end)
		{
			int startMonth, startDay, endMonth, endDay;

			startMonth = startDay = 1;
			endDay = 30;
			switch (type)
			{
				case 1: // theo năm
					endMonth = 12;
					endDay = 31;
					break;
				case 2: // nửa năm
					if (haftyear == 1)
					{
						// Nửa năm đầu
						endMonth = 6;
					}
					else
					{
						startMonth = 7;
						endMonth = 12; endDay = 31;
					}
					break;
				case 3: // quý
					if (quarter == 1)
					{
						endMonth = 3; endDay = 31;
					}
					else if (quarter == 2)
					{
						startMonth = 4;
						endMonth = 6;
					}
					else if (quarter == 3)
					{
						startMonth = 7;
						endMonth = 9;
					}
					else
					{
						startMonth = 10;
						endMonth = 12; endDay = 31;
					}
					break;
				case 4: // Tháng
					startMonth = endMonth = month;
					endDay = DateTime.DaysInMonth(year, month);
					break;
				default:
					startMonth = endMonth = DateTime.Now.Month;
					endDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
					break;
			}
			start = new DateTime(year, startMonth, startDay, 0, 0, 0);
			end = new DateTime(year, endMonth, endDay, 23, 59, 59);
		}

		#endregion
	}

	public class PublicStat
	{
		public int All { get; set; }

		public string OrganId { get; set; }

		public string OrganName { get; set; }

		public int Received { get; set; }

		public int Sent { get; set; }

		public string LastUpdate { get; set; }

		public IEnumerable<PublicStat> Item { get; set; }
	}
}