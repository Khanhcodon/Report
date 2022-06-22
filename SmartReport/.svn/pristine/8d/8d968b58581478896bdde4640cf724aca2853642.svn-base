using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Class : PaperBll - public - BLL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 81112
    /// <para></para> Author      : DungHV
    /// <para></para> Description : Quản lý danh mục giấy tờ egov.
    /// <remarks>
    ///     <para> Quản lý giấy tờ của các loại hồ sơ.</para>
    /// </remarks>
    /// </summary>
    public class PaperBll : ServiceBase
    {
        private readonly IRepository<Paper> _paperRepository;
        private readonly IRepository<DoctypePaper> _doctypePaperRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ResourceBll _resourceService;

        ///<summary>
        /// Khởi tạo class <see cref="PaperBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        /// <param name="resourceService"></param>
        public PaperBll(IDbCustomerContext context, AdminGeneralSettings generalSettings, ResourceBll resourceService)
            : base(context)
        {
            _paperRepository = Context.GetRepository<Paper>();
            _doctypePaperRepository = Context.GetRepository<DoctypePaper>();
            _generalSettings = generalSettings;
            _resourceService = resourceService;
        }

        /// <summary> 
        /// Lấy ra tất cả fee theo điều kiện truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<Paper> Gets(Expression<Func<Paper, bool>> spec)
        {
            return _paperRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra danh sách giấy tờ theo loại hồ sơ id
        /// </summary>
        /// <param name="projector"> </param>
        /// <param name="doctypeid">Id của doctype</param>
        /// <returns>Danh sách lệ phí</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Paper, T>> projector, Guid? doctypeid = null)
        {
            var spec = doctypeid.HasValue
                        ? PaperQuery.WithDocTypeId(doctypeid.Value)
                        : null;
            return _paperRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Trả về danh sách giấy tờ theo loại hồ sơ và loại giấy tờ tương ứng.
        /// <para>(Tienbv@bkav.com 070313)</para>
        /// </summary>
        /// <param name="doctypeId">Document type id</param>
        /// <param name="paperType">Enum.PaperType</param>
        /// <param name="isReadonly">Kết quả chỉ để đọc hay không</param>
        /// <returns>Danh sách giấy tờ tương ứng</returns>
        public IEnumerable<Paper> Gets(Guid doctypeId, PaperType paperType, bool isReadonly = true)
        {
            var doctypePapers = _doctypePaperRepository.Gets(isReadonly, d => d.DocTypeId.Equals(doctypeId));
            if (!doctypePapers.Any())
            {
                return new List<Paper>();
            }

            var paperIds = doctypePapers.Select(dt => dt.PaperId);

            return _paperRepository.Gets(isReadonly, p => paperIds.Contains(p.PaperId) && p.PaperTypeId == (int)paperType);
        }

        /// <summary>
        /// Lấy ra tất cả các giấy tờ có phân trang
        /// <para> (DungHV 200813)</para>
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="doctypeId">Id loại hồ sơ (dùng để tìm kiếm). Nếu để trống sẽ bỏ qua đk tìm kiếm này, nếu có giá trị sẽ tìm tất cả phí thuộc loại hồ sơ truyền vào</param>
        /// <returns>Danh sách giấy tờ</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Paper, T>> projector, string sortBy = null,
                                                        bool isDescending = false,
                                                        Guid? doctypeId = null)
        {
            var spec = PaperQuery.WithDocTypeId(doctypeId);
            var param = string.IsNullOrEmpty(sortBy) ? new[] { "CreatedOnDate", "IsRequierd", "PaperName" } : new[] { sortBy, "CreatedOnDate", "IsRequierd" };
            var sort = Context.Filters.CreateSort<Paper>(isDescending, param);
            var result = _paperRepository.GetsAs(projector, spec, sort);
            return result;
        }

        /// <summary>
        /// Lấy ra giấy tờ theo id
        /// </summary>
        /// <param name="paperId">Id của giấy tờ</param>
        /// <returns>Entity giấy tờ</returns>
        public Paper Get(int paperId)
        {
            Paper result = null;
            if (paperId > 0)
            {
                result = _paperRepository.Get(paperId);
            }
            return result;
        }

        /// <summary>
        /// Lấy ra giấy tờ theo id
        /// </summary>
        /// <param name="name">tên giấy tờ</param>
        /// <returns>Entity giấy tờ</returns>
        public Paper Get(string name)
        {
            return _paperRepository.Get(true, PaperQuery.WithName(name));
        }

        /// <summary>
        /// Tạo mới giấy tờ
        /// </summary>
        /// <param name="paper">Entity giấy tờ</param>
        public void Create(Paper paper)
        {
            if (paper == null)
            {
                throw new ArgumentNullException("paper");
            }

            var existPaper = _paperRepository.GetReadOnly(p => p.PaperName.Equals(paper.PaperName, StringComparison.OrdinalIgnoreCase));

            if (existPaper == null)
            {
                _paperRepository.Create(paper);
            }
            else
            {
                paper = existPaper;
            }

            var doctypePaperExist = _doctypePaperRepository.Exist(dp => dp.PaperId == paper.PaperId && dp.DocTypeId == paper.DocTypeId);
            if (!doctypePaperExist)
            {
                _doctypePaperRepository.Create(new DoctypePaper()
                {
                    DocTypeId = paper.DocTypeId,
                    PaperId = paper.PaperId,
                    IsRequired = true
                });
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới giấy tờ
        /// </summary>
        /// <param name="papers">Danh sách giấy tờ</param>
        /// <param name="ignoreExist"></param>
        public void Create(IEnumerable<Paper> papers, bool ignoreExist)
        {
            if (papers == null || !papers.Any())
            {
                throw new ArgumentNullException("papers");
            }

            var names = papers.Select(x => x.PaperName);
            var exist = _paperRepository.GetsAs(p => p.PaperName, p => names.Contains(p.PaperName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == papers.Count())
                {
                    throw new EgovException(_resourceService.GetResource("Paper.Create.Exist"));
                }

                var list = papers.Where(p => !exist.Contains(p.PaperName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("Paper.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(papers);
            }
        }

        private void Create(IEnumerable<Paper> papers)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in papers)
            {
                _paperRepository.Create(item);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa 1 giấy tờ
        /// </summary>
        /// <param name="paper">Entity giấy tờ</param>
        public void Delete(Paper paper)
        {
            if (paper == null)
            {
                throw new ArgumentNullException("paper");
            }
            _paperRepository.Delete(paper);
            Context.SaveChanges();
        }

       /// <summary>
       /// Chắc chắn giấy tờ tồn tại và trả về giấy tờ đó
       /// </summary>
       /// <param name="paperName">Tên giấy tờ</param>
        /// <param name="doctypeId">Loại hồ sơ</param>
       /// <param name="paperType">Loại giấy tờ</param>
       /// <param name="amount">Số lượng</param>
       /// <param name="userId">Người tạo</param>
       /// <returns>Trả về giấy tờ</returns>
        public Paper EnsureExist(string paperName, Guid doctypeId, PaperType paperType, int amount, int userId)
        {
            var checkPaper = _paperRepository.Get(false, p => p.PaperName.Equals(paperName, StringComparison.OrdinalIgnoreCase) && p.PaperTypeId == (int)paperType);
            if (checkPaper != null)
            {
                if (!_doctypePaperRepository.Exist(dt => dt.PaperId == checkPaper.PaperId && dt.DocTypeId.Equals(doctypeId)))
                {
                    _doctypePaperRepository.Create(new DoctypePaper()
                    {
                        DocTypeId = doctypeId,
                        Amount = checkPaper.Amount,
                        IsRequired = true,
                        PaperId = checkPaper.PaperId,
                        PaperName = checkPaper.PaperName
                    });
                    Context.SaveChanges();
                }
                return checkPaper;
            }

            var newPaper = new Paper
            {
                Amount = amount,
                PaperName = paperName,
                PaperTypeId = (int)PaperType.ThuongBosung,
                IsRequired = true,
                DocTypeId = doctypeId,
                CreatedByUserId = userId
            };
            _paperRepository.Create(newPaper);

            var doctypePaper = new DoctypePaper()
            {
                DocTypeId = doctypeId,
                Amount = amount,
                IsRequired = true,
                PaperId = newPaper.PaperId,
                PaperName = paperName
            };
            _doctypePaperRepository.Create(doctypePaper);

            Context.SaveChanges();

            return newPaper;
        }

        /// <summary>
        /// Cập nhật thông tin giấy tờ
        /// </summary>
        /// <param name="paper">Entity giấy tờ</param>
        /// <param name="oldPaperName">Tên giấy tờ trước khi cập nhật</param>
        public void Update(Paper paper, string oldPaperName)
        {
            if (paper == null)
            {
                throw new ArgumentNullException("paper");
            }
            if (_paperRepository.Exist(PaperQuery.WithName(paper.PaperName).And(r => r.PaperName.ToLower() != oldPaperName.ToLower())))
            {
                throw new EgovException(string.Format("Tên mã ({0}) đã tồn tại!", paper.PaperName));
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của giấy tờ phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhảy số phù hợp, ngược lại: false</returns>
        public bool Exist(Expression<Func<Paper, bool>> spec)
        {
            return _paperRepository.Exist(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SyncDoctype()
        {
            var papers = Gets(null);
            foreach (var paper in papers)
            {
                if (paper.DocTypeId != null && !_doctypePaperRepository.Exist(dt => dt.PaperId == paper.PaperId && dt.DocTypeId == paper.DocTypeId))
                {
                    _doctypePaperRepository.Create(new DoctypePaper()
                    {
                        DocTypeId = paper.DocTypeId,
                        PaperId = paper.PaperId
                    });
                }

                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Xóa doctype paper
        /// </summary>
        /// <param name="doctypeId">Doctype Id</param>
        /// <param name="paperId">Paper Id</param>
        public void DeleteDocTypePaper(Guid doctypeId, int paperId)
        {
            var doctypePapers = _doctypePaperRepository.Gets(false, dp => dp.DocTypeId == doctypeId && dp.PaperId == paperId);
            foreach (var doctypePaper in doctypePapers)
            {
                _doctypePaperRepository.Delete(doctypePaper);
            }
            Context.SaveChanges();

            // Xóa luôn giấy tờ nếu không còn loại hồ sơ nào sử dụng nữa.
            var checkOtherDoctypePapers = _doctypePaperRepository.Gets(false, dp => dp.PaperId == paperId);
            if (!checkOtherDoctypePapers.Any())
            {
                var paper = Get(paperId);
                if (paper != null)
                {
                    Delete(paper);
                }
            }
        }
    }
}
