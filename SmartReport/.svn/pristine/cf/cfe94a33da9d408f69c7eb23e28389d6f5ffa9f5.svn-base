using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using System.Text.RegularExpressions;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : IncreaseBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 140912</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng Increase trong CSDL</para>
    /// </summary>
    public class IncreaseBll : ServiceBase
    {
        private readonly IRepository<Increase> _increaseRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IRepository<Code> _codeRepository;
        private readonly ResourceBll _resourceService;

        ///<summary>
        /// Khởi tạo class <see cref="IncreaseBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        ///<param name="generalSettings">Bll tương ứng với bảng Resource trong CSDL</param>
        ///<param name="resourceService"></param>
        public IncreaseBll(IDbCustomerContext context
            , AdminGeneralSettings generalSettings
            , ResourceBll resourceService)
            : base(context)
        {
            _increaseRepository = Context.GetRepository<Increase>();
            _generalSettings = generalSettings;
            _codeRepository = Context.GetRepository<Code>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Lấy ra tất cả nhảy số. Kết quả chỉdđọc
        /// </summary>
        /// <param name="totalRecords"> Tổng số bản ghi </param>
        /// <param name="currentPage"> Trang hiện tại </param>
        /// <param name="pageSize"> Số bản ghi trên 1 trang </param>
        /// <param name="sortBy"> Sắp xếp theo </param>
        /// <param name="isDescending"> Sắp xếp từ lớn đến nhỏ: true, ngược lại false </param>
        /// <param name="name"> Tên nhảy số </param>
        /// <param name="groupId"></param>
        /// <returns> Danh sách nhảy số </returns>
        public IEnumerable<Increase> Gets(out int totalRecords, int currentPage = 1,
             int? pageSize = null, string sortBy = "", bool isDescending = false,
             string name = "", int? groupId = null)
        {
            var spec = !string.IsNullOrWhiteSpace(name)
                           ? IncreaseQuery.ContainsName(name)
                           : null;
            if (groupId.HasValue)
            {
                if (spec != null)
                {
                    spec = spec.And(p => p.BussinessDocFieldDocTypeGroupId == groupId.Value);
                }
                else
                {
                    spec = p => p.BussinessDocFieldDocTypeGroupId == groupId.Value;
                }
            }

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _increaseRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Increase>(isDescending, sortBy);
            return _increaseRepository.GetsReadOnly(spec, sort, Context.Filters.Page<Increase>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Lấy ra tất cả các nhảy sốg phù hợp với điều kiện truyền vào. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="isReadOnly"></param>
        /// <returns>Danh sách các nhảy số</returns>
        public IEnumerable<Increase> Gets(Expression<Func<Increase, bool>> spec = null, bool isReadOnly = true)
        {
            return _increaseRepository.Gets(isReadOnly, spec);
        }

        /// <summary>
        /// Lấy ra tất cả các nhảy sốg phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="T">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các nhảy số</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Increase, T>> projector, Expression<Func<Increase, bool>> spec = null)
        {
            return _increaseRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Lấy ra nhảy số theo id
        /// </summary>
        /// <param name="increaseId">Id của nhảy số</param>
        /// <returns>Entity nhảy số</returns>
        public Increase Get(int increaseId)
        {
            Increase result = null;
            if (increaseId > 0)
            {
                result = _increaseRepository.Get(increaseId);
            }
            return result;
        }

        /// <summary>
        /// Tạo mới nhảy số
        /// </summary>
        /// <param name="increase">Entity nhảy số</param>
        public void Create(Increase increase)
        {
            if (increase == null)
            {
                throw new ArgumentNullException("increase");
            }

            if (_increaseRepository.Exist(IncreaseQuery.WithName(increase.Name)))
            {
                throw new Exception(string.Format("Nhảy số ({0}) đã tồn tại!", increase.Name));
            }

            _increaseRepository.Create(increase);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới nhảy số
        /// </summary>
        /// <param name="increases">Entity  nhảy số</param>
        /// <param name="ignoreExist">True: Bỏ qua khi biểu mẫu đã tồn tại; False: validate khi biểu mẫu đã tồn tại</param>
        public void Create(IEnumerable<Increase> increases, bool ignoreExist)
        {
            if (increases == null || !increases.Any())
            {
                throw new ArgumentNullException("increases");
            }
            var names = increases.Select(x => x.Name);
            var exist = _increaseRepository.GetsAs(p => p.Name, p => names.Contains(p.Name));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == increases.Count())
                {
                    throw new EgovException(_resourceService.GetResource("Increase.Create.Exist"));
                }

                var list = increases.Where(p => !exist.Contains(p.Name));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("Increase.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(increases);
            }
        }

        private void Create(IEnumerable<Increase> increases)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in increases)
            {
                _increaseRepository.Create(item);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật nhảy số
        /// </summary>
        /// <param name="increase">Entity nhảy số</param>
        /// <param name="oldName"> </param>
        public void Update(Increase increase, string oldName)
        {
            if (increase == null)
            {
                throw new ArgumentNullException("increase");
            }
            if (_increaseRepository.Exist(IncreaseQuery.WithName(increase.Name).And(r => r.Name.ToLower() != oldName.ToLower())))
            {
                throw new Exception(string.Format("Nhảy số ({0}) đã tồn tại!", increase.Name));
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật nhảy số
        /// </summary>
        /// <param name="increase">Entity nhảy số</param>
        public void Update(Increase increase)
        {
            if (increase == null)
            {
                throw new ArgumentNullException("increase");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa 1 nhảy số
        /// </summary>
        /// <param name="increase">Thực thể nhảy số</param>
        public void Delete(Increase increase)
        {
            if (increase == null)
            {
                throw new ArgumentNullException("increase");
            }
            //Kiểm tra ràng buộc trong bảng code
            var codes = _codeRepository.GetsReadOnly(c => c.IncreaseId == increase.IncreaseId);
            var strException = "";
            if (codes.Any())
            {
                foreach (var code in codes)
                {
                    strException = strException == "" ? string.Format("Bảng mã {0}", code.CodeName) : strException + string.Format(", bảng mã {0}", code.CodeName);
                }
                strException = strException == "" ? "" : strException + string.Format(" đã sử dụng nhảy số {0}", increase.Name);
                throw new EgovException(strException);
            }
            _increaseRepository.Delete(increase);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tăng số thứ tự văn bản đã cấp
        /// </summary>
        /// <param name="increase"></param>
        public void IncreaseDocNumber(Increase increase)
        {
            if (increase != null)
            {
                increase.Value = increase.Value + 1;
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Cập nhật đánh số đến số đang được cấp hiện tại.
        /// </summary>
        /// <param name="increase">Đánh số</param>
        /// <param name="template">Template của số đang được cấp</param>
        /// <param name="code">Số đang được cấp: cấp tự động hoặc cấp đánh tay.</param>
        /// <param name="hasSaveChange"></param>
        public void IncreaseFromCode(Increase increase, string template, string code, bool hasSaveChange = false)
        {
            var codeTemp = BuildCode(template, DateTime.Now);
            var numbIdx = codeTemp.IndexOf("$n$", StringComparison.OrdinalIgnoreCase);
            code = code.Substring(numbIdx);

            var regex = new Regex("[0-9]*");
            var currentNumb = regex.Match(code);
            if (currentNumb == null)
            {
                return;
            }

            int ins;
            if (!Int32.TryParse(currentNumb.Value, out ins))
            {
                ins = increase.Value + 1;
            }

            increase.Value = ins;

            if (hasSaveChange)
            {
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Lấy lại số thứ tự văn bản đã cấp
        /// </summary>
        /// <param name="increase"></param>
        internal void DecreaseDocNumber(Increase increase)
        {
            if (increase != null)
            {
                increase.Value = increase.Value - 1;
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Reset lại giá trị nhảy số
        /// </summary>
        /// 
        /// <param name="lstIncrease">Danh sách nhảy số cần reset</param>
        public void ResetIncrease(IEnumerable<Increase> lstIncrease)
        {
            if (lstIncrease == null || !lstIncrease.Any())
            {
                throw new ArgumentNullException("lstIncrease is null.");
            }

            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in lstIncrease)
            {
                item.Value = 0;
            }

            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        private string BuildCode(string codeTemplate, DateTime dateOfIssueCode)
        {
            var result = codeTemplate;
            if (result.ToLower().Contains("$y$"))
            {
                result = result.Replace("$y$", dateOfIssueCode.ToString("yyyy")).Replace("$Y$", dateOfIssueCode.ToString("yyyy"));
            }
            if (result.ToLower().Contains("$m$"))
            {
                result = result.Replace("$m$", dateOfIssueCode.ToString("MM")).Replace("$M$", dateOfIssueCode.ToString("MM"));
            }
            if (result.ToLower().Contains("$d$"))
            {
                result = result.Replace("$d$", dateOfIssueCode.ToString("dd")).Replace("$D$", dateOfIssueCode.ToString("dd"));
            }
            return result;
        }
    }
}
