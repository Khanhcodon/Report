using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Entities.Common;
using MySql.Data.MySqlClient;
using Bkav.eGovCloud.Business.BI.ParseQuery;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// </summary>
    public class ReportModeBll : ServiceBase
    {
        private readonly IRepository<ReportModes> _reportModeRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly DocTypeBll _doctypeservice;

        /// <summary>
        /// Khởi tạo class <see cref="ReportModeBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings"></param>
        //TODO: Anh GiangPN - Tham số đầu vào IPositionDal positionDal nên thay thành PositionBll và IJobTitlesDal jobTitlesDal nên thay thành JobTitleBll
        public ReportModeBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings,
            DocTypeBll doctypeservice)
            : base(context)
        {
            _generalSettings = generalSettings;
            _reportModeRepository = Context.GetRepository<ReportModes>();
            _doctypeservice = doctypeservice;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ReportModes Get(string code)
        {
            return _reportModeRepository.Get(false,c=>c.Code == code);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReportModes Get(int id)
        {
            return _reportModeRepository.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportMode"></param>
        public ReportModes GetId(int? id)
        {
            return _reportModeRepository.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportMode"></param>
        public void Create(ReportModes reportMode, IEnumerable<Guid> docTypeIds = null)
        {
            if (reportMode == null)
            {
                throw new ArgumentNullException("ReportMode");
            }
            if (docTypeIds != null && docTypeIds.Any())
            {
                var exists = _doctypeservice.Gets(p => p.ReportModeId == reportMode.ReportModeId, false);
                if (exists != null && exists.Any())
                {
                    var existIds = exists.Select(p => p.DocTypeId);
                    var removeDocTypeIds = existIds.Where(p => !docTypeIds.Contains(p));
                    if (removeDocTypeIds != null && removeDocTypeIds.Any())
                    {
                        var removeForms = _doctypeservice.Gets(p => removeDocTypeIds.Contains(p.DocTypeId), false);
                        if (removeForms != null && removeForms.Any())
                        {
                            foreach (var item in removeForms)
                            {
                                item.ReportModeId = null;
                            }
                        }
                    }

                    var addDocTypeIds = docTypeIds.Where(p => !existIds.Contains(p));
                    if (addDocTypeIds != null && addDocTypeIds.Any())
                    {
                        var addForms = _doctypeservice.Gets(p => addDocTypeIds.Contains(p.DocTypeId), false);
                        if (addForms != null && addForms.Any())
                        {
                            foreach (var item in addForms)
                            {
                                item.ReportModeId = reportMode.ReportModeId;
                            }
                        }
                    }
                }
                else
                {
                    var forms = _doctypeservice.Gets(p => docTypeIds.Contains(p.DocTypeId), false);
                    if (forms != null && forms.Any())
                    {
                        foreach (var item in forms)
                        {
                            item.ReportModeId = reportMode.ReportModeId;
                        }
                    }
                }
            }
            _reportModeRepository.Create(reportMode);
            Context.SaveChanges();
        }

        public void Update(ReportModes reportMode, IEnumerable<Guid> docTypeIds = null)
        {
            if (reportMode == null)
            {
                throw new ArgumentNullException("ReportMode");
            }
            if (docTypeIds != null && docTypeIds.Any())
            {
                var exists = _doctypeservice.Gets(p => p.ReportModeId == reportMode.ReportModeId, false);
                if (exists != null && exists.Any())
                {
                    var existIds = exists.Select(p => p.DocTypeId);
                    var removeDocTypeIds = existIds.Where(p => !docTypeIds.Contains(p));
                    if (removeDocTypeIds != null && removeDocTypeIds.Any())
                    {
                        var removeForms = _doctypeservice.Gets(p => removeDocTypeIds.Contains(p.DocTypeId), false);
                        if (removeForms != null && removeForms.Any())
                        {
                            foreach (var item in removeForms)
                            {
                                item.ReportModeId = null;
                            }
                        }
                    }

                    var addDocTypeIds = docTypeIds.Where(p => !existIds.Contains(p));
                    if (addDocTypeIds != null && addDocTypeIds.Any())
                    {
                        var addForms = _doctypeservice.Gets(p => addDocTypeIds.Contains(p.DocTypeId), false);
                        if (addForms != null && addForms.Any())
                        {
                            foreach (var item in addForms)
                            {
                                item.ReportModeId = reportMode.ReportModeId;
                            }
                        }
                    }
                }
                else
                {
                    var forms = _doctypeservice.Gets(p => docTypeIds.Contains(p.DocTypeId), false);
                    if (forms != null && forms.Any())
                    {
                        foreach (var item in forms)
                        {
                            item.ReportModeId = reportMode.ReportModeId;
                        }
                    }
                }
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportMode"></param>
        public void Delete(ReportModes reportMode)
        {
            if (reportMode == null)
            {
                throw new ArgumentNullException("ReportMode");
            }

            _reportModeRepository.Delete(reportMode);
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<ReportModes> Gets(Expression<Func<ReportModes, bool>> spec = null)
        {
            return _reportModeRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<ReportModes, TOutput>> projector, Expression<Func<ReportModes, bool>> spec = null)
        {
            return _reportModeRepository.GetsAs(projector, spec);
        }
    }
}
