using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Hosting;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using MySql.Data.MySqlClient;

namespace Bkav.eGovCloud.Business.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class DocTypeJobOutOfDate : IeGovJob
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private string _apiUrl;
        private Guid _docTypeId;
        private bool _isActive;
        private bool _isActiveAlert;
        private bool _isActiveAlertOut;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="connectionStrings">Danh sách các kết nối đến db</param>
        public DocTypeJobOutOfDate(string url, Guid docTypeId, DocTypeTimeJob timeJobId)
        {
            _apiUrl = url;
            _docTypeId = docTypeId;
            _isActive = timeJobId.IsActive;
            _isActiveAlert = timeJobId.IsActiveAlert;
            _isActiveAlertOut = timeJobId.IsActiveAlertOut;

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

                if (_isActiveAlertOut == true)
                {
                    ExecuteJobOutOfDate();
                }
            }
            catch (Exception ex)
            {

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

        /// <summary>
        /// 
        /// </summary>
        private void ExecuteJobOutOfDate()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    _isActive = false;
                    _isActiveAlert = false;
                    var url = _apiUrl + "Document/SaveDocDraftDueOutOfDateNew?docTypeId=" + _docTypeId
                        + "&isActive=" + _isActive + "&isActiveAlert=" + _isActiveAlert + "&isActiveAlertOut=" + _isActiveAlertOut;
                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var results = response.Content.ReadAsStringAsync().Result as string;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}
