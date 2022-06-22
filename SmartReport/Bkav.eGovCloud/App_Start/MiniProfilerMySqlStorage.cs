using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using StackExchange.Profiling;
using StackExchange.Profiling.Helpers;
using StackExchange.Profiling.Helpers.Dapper;
using StackExchange.Profiling.Storage;

namespace Bkav.eGovCloud.App_Start
{
	/// <summary>
	/// Understands how to store a <see cref="MiniProfiler"/> to a MySql database.
	/// </summary>
	public class MySqlStorage : SqlServerStorage
	{
		private List<string> _ignoreActions = new List<string>() {
				"/signalr/", "CheckConnection", "/AvatarProfile/", "profiler/updateclient", "/Webapi", "Error.html",
				"/Admin/", "/Parallel/"
		};

		private List<string> _actionToProfilers = new List<string>()
		{
			"/Document/GetDocumentDetail", "/Transfer/", "/Publish/"
		};

		public MySqlStorage(string connectionString)
			: base(connectionString)
		{

		}

		/// <summary>
		/// Stores to <c>dbo.MiniProfilers</c> under its <see cref="MiniProfiler.Id"/>;
		/// </summary>
		/// <param name="profiler">The Mini Profiler</param>
		public override void Save(MiniProfiler profiler)
		{
			if (profiler.Root == null && profiler.User == "Update")
			{
				if (string.IsNullOrEmpty(profiler.Name) || !_actionToProfilers.Any(a => profiler.Name.StartsWith(a)))
				{
					return;
				}

				Update(profiler);
				return;
			}

			var name = profiler.Root.Name;
			name = new Uri(name).PathAndQuery;

			if (string.IsNullOrEmpty(name) || !_actionToProfilers.Any(a => name.StartsWith(a)))
			{
				return;
			}

			var domainName = new Uri(profiler.Root.Name).Host;

			const string sql =
							@"INSERT INTO miniprofilers
							   (Id,
								RootTimingId,
								Name,
								Started,
								DurationMilliseconds,
								User,
								HasUserViewed,
								MachineName,
								CustomLinksJson,
								ClientTimingsRedirectCount)
							SELECT       @Id,
								@RootTimingId,
								@Name,
								@Started,
								@DurationMilliseconds,
								@User,
								@HasUserViewed,
								@MachineName,
								@CustomLinksJson,
								@ClientTimingsRedirectCount
							FROM (SELECT 1 AS col) AS A
							WHERE NOT EXISTS (SELECT 1 FROM miniprofilers WHERE Id = @Id)";
			try
			{
				using (var conn = GetOpenConnection())
				{
					conn.Execute(
						sql,
						new
						{
							profiler.Id,
							Name = name.Truncate(500),
							Started = profiler.Started.AddHours(7),
							User = profiler.User.Truncate(100),
							RootTimingId = profiler.Root != null ? profiler.Root.Id : (Guid?)null,
							profiler.DurationMilliseconds,
							profiler.HasUserViewed,
							MachineName = domainName.Truncate(100),
							profiler.CustomLinksJson,
							ClientTimingsRedirectCount = profiler.ClientTimings != null ? profiler.ClientTimings.RedirectCount : (int?)null
						});

					var timings = new List<Timing>();
					if (profiler.Root != null)
					{
						profiler.Root.MiniProfilerId = profiler.Id;
						FlattenTimings(profiler.Root, timings);
					}

					SaveTimings(timings, conn);
				}
			}
			catch
			{

			}
		}

		private void Update(MiniProfiler profiler)
		{
			try
			{
				using (var conn = GetOpenConnection())
				{
					var cmd = @"update `miniprofilers` m
								SET m.ClientTimingsRedirectCount = @durations
								WHERE m.`Name` = @name
								ORDER BY RowId DESC LIMIT 1;";

					conn.Execute(cmd,
						new
						{
							durations = profiler.DurationMilliseconds,
							name = profiler.Name
						});
				}
			}
			catch { }
		}

		private void SaveTimings(List<Timing> timings, DbConnection conn)
		{
			const string sql = @"INSERT INTO miniprofilertimings
							   (Id,
								MiniProfilerId,
								ParentTimingId,
								Name,
								DurationMilliseconds,
								StartMilliseconds,
								IsRoot,
								Depth,
								CustomTimingsJson)
							SELECT       @Id,
								@MiniProfilerId,
								@ParentTimingId,
								@Name,
								@DurationMilliseconds,
								@StartMilliseconds,
								@IsRoot,
								@Depth,
								@CustomTimingsJson
							FROM (SELECT 1 AS col) AS A
							WHERE NOT EXISTS (SELECT 1 FROM miniprofilertimings WHERE Id = @Id)";

			try
			{
				foreach (var timing in timings)
				{
					conn.Execute(
						sql,
						new
						{
							timing.Id,
							timing.MiniProfilerId,
							timing.ParentTimingId,
							Name = timing.Name.Substring(0, Math.Min(timing.Name.Length, 200)),
							timing.DurationMilliseconds,
							timing.StartMilliseconds,
							timing.IsRoot,
							timing.Depth,
							timing.CustomTimingsJson
						});
				}
			}
			catch { }
		}

		private void SaveClientTimings(List<ClientTimings.ClientTiming> timings, DbConnection conn)
		{
			//const string sql = @"INSERT INTO miniprofilerclienttimings
			//				   ( Id,
			//					 MiniProfilerId,
			//					 Name,
			//					 Start,
			//					 Duration)
			//					SELECT       @Id,
			//						@MiniProfilerId,
			//						@Name,
			//						@Start,
			//						@Duration
			//					FROM (SELECT 1 AS col) AS A
			//					WHERE NOT EXISTS (SELECT 1 FROM miniprofilerclienttimings WHERE Id = @Id)";

			//foreach (var timing in timings)
			//{
			//	conn.Execute(
			//		sql,
			//		new
			//		{
			//			timing.Id,
			//			timing.MiniProfilerId,
			//			Name = timing.Name.Truncate(200),
			//			timing.Start,
			//			timing.Duration
			//		});
			//}
		}

		private void FlattenTimings(Timing timing, List<Timing> timingsCollection)
		{
			timingsCollection.Add(timing);
			if (timing.HasChildren)
			{
				timing.Children.ForEach(x => FlattenTimings(x, timingsCollection));
			}
		}

		/// <summary>
		/// Sets the session to un-viewed 
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="id">The id.</param>
		public override void SetUnviewed(string user, Guid id)
		{
			//using (var conn = GetOpenConnection())
			//{
			//	conn.Execute("UPDATE miniprofilers SET HasUserViewed = 0 WHERE Id = @id AND User = @user", new { id, user });
			//}
		}

		/// <summary>
		/// sets the session to viewed
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="id">The id.</param>
		public override void SetViewed(string user, Guid id)
		{
			//if (this.IPsExcludedFromProfiling.Split(',').Contains(user))
			//	return;

			//using (var conn = GetOpenConnection())
			//{
			//	conn.Execute("UPDATE miniprofilers SET HasUserViewed = 1 WHERE Id = @id AND User = @user", new { id, user });
			//}
		}

		/// <summary>
		/// Returns a list of <see cref="MiniProfiler.Id"/>s that haven't been seen by <paramref name="user"/>.
		/// </summary>
		/// <param name="user">User identified by the current UserProvider"/&gt;.</param>
		/// <returns>the list of keys.</returns>
		public override List<Guid> GetUnviewedIds(string user)
		{
			return new List<Guid>();
			//const string Sql =
			//	@"select Id
			//		from   miniprofilers
			//		where  User = @user
			//		and    HasUserViewed = 0
			//		order  by Started";

			//using (var conn = GetOpenConnection())
			//{
			//	return conn.Query<Guid>(Sql, new { user }).ToList();
			//}
		}

		/// <summary>
		/// Returns a connection to MySql.
		/// </summary>
		protected override DbConnection GetConnection()
		{
			var dbConnection = new MySql.Data.MySqlClient.MySqlConnection(this.ConnectionString);
			return dbConnection;
			//Database dbprofiler = DatabaseFactory.CreateDatabase(this.ConnectionString);
			//return dbprofiler.CreateConnection();
		}
	}
}