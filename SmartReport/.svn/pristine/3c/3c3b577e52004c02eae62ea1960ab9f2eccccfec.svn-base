using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using Bkav.eGovCloud.DataAccess;
using MySql.Data.MySqlClient;

namespace Bkav.eGovCloud.Business.Tasks
{
	/// <summary>
	/// Thực thi các câu lệnh xử lý dữ liệu cho báo cáo
	/// </summary>
	public class StatisticJob : IeGovJob
	{
		private readonly object _lock = new object();
		private bool _shuttingDown;
		private IEnumerable<MySqlConnection> _connections;

		/// <summary>
		/// C'tor
		/// </summary>
		/// <param name="connectionStrings">Danh sách các kết nối đến db</param>
		public StatisticJob(List<string> connectionStrings)
		{
			_connections = connectionStrings.Select(c => new MySqlConnection(c));

			// Register this job with the hosting environment.
			// Allows for a more graceful stop of the job, in the case of IIS shutting down.
			HostingEnvironment.RegisterObject(this);
		}

		/// <summary>
		/// Thực thi job
		/// </summary>
		public void Execute()
		{
			try
			{
				lock (_lock)
				{
					if (_shuttingDown)
						return;
				}

				foreach (var connection in _connections)
				{
					try
					{
						RepairStatistic(connection);
					}
					catch
					{
						continue;
					}
				}
			}
			finally
			{
				// Always unregister the job when done.
				HostingEnvironment.UnregisterObject(this);
			}
		}

		public void Stop(bool immediate)
		{
			// Locking here will wait for the lock in Execute to be released until this code can continue.
			lock (_lock)
			{
				_shuttingDown = true;
			}

			HostingEnvironment.UnregisterObject(this);
		}

		#region Do Job

		private void RepairStatistic(MySqlConnection connection)
		{
			var cmds = this.Commands;

			using (var context = new EfContext(connection))
			{
				foreach (var cmd in cmds)
				{
					try
					{
						context.RawQuery(cmd);
					}
					catch { }
				}
			}
		}

		private List<string> Commands
		{
			get
			{
				var result = new List<string>();

				// cập nhật store-doc thiếu từ bảng document sang bảng storedoc
				result.Add(@"INSERT INTO store_doc (DocumentId, StoreId)
								select d.DocumentId as DocumentId, d.StoreId as StoreId from
								(
								select d2.* from document d2
								JOIN(SELECT d1.DocCode as DocCode, MIN(d1.DateCreated) as minVal
								  FROM document d1 GROUP BY d1.DocCode) doc on d2.DocCode = doc.DocCode
								WHERE d2.DateCreated = doc.minVal and year(d2.DateCreated) = 2018
								) d
								LEFT JOIN store_doc sd on d.DocumentId = sd.DocumentId
								WHERE sd.StoreDocId is null
								and d.CategoryBusinessId = 1
								and d.DocTypeId is not null
								and d.StoreId is not NULL
								ORDER BY d.DateCreated;
				");

				return result;
			}
		}

		#endregion
	}
}
