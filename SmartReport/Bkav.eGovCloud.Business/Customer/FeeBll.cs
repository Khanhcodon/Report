using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : FeeBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 11112</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : Quản lý danh mục lệ phí egov.</para>
    /// <remarks>
    ///     <para> Quản lý lệ phí của các loại hồ sơ.</para>
    /// </remarks>
    /// </summary>
    public class FeeBll : ServiceBase
    {
        private readonly IRepository<Fee> _feeRepository;
        private readonly IRepository<DoctypeFee> _doctypeFeeRepository;
        private readonly ResourceBll _resourceService;

        ///<summary>
        /// Khởi tạo class <see cref="FeeBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        ///<param name="resourceService"></param>
        public FeeBll(IDbCustomerContext context,
            ResourceBll resourceService)
            : base(context)
        {
            _feeRepository = Context.GetRepository<Fee>();
            _doctypeFeeRepository = Context.GetRepository<DoctypeFee>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Trả về danh sách lệ phí theo loại hồ sơ và loại lệ phí tương ứng. Kết quả chỉ đọc
        /// <para> (Tienbv@bkav.com 070313)</para>
        /// </summary>
        /// <param name="doctypeId">Document type id</param>
        /// <param name="feeType">Enum.FeeType</param>
        /// <param name="isReadOnly">Kết quả chỉ để đọc hay không</param>
        /// <returns>Danh sách lệ phí tương ứng</returns>
        public IEnumerable<Fee> Gets(Guid doctypeId, Entities.FeeType feeType, bool isReadOnly = true)
        {
            var spec = FeeQuery.WithDocTypeAndFeeType(doctypeId, feeType);
            return _feeRepository.Gets(isReadOnly, spec);
        }

        /// <summary>
        /// Trả về danh sách các lệ phí theo id
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns>Danh sách lệ phí, kết quả chỉ để đọc</returns>
        public IEnumerable<Fee> Gets(List<int> ids)
        {
            return _feeRepository.Gets(true, f => ids.Contains(f.FeeId));
        }

        /// <summary>
        /// Lấy ra tất cả các loại phí có phân trang
        /// <para> (DungHV 200813)</para>
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="doctypeId">Id loại hồ sơ (dùng để tìm kiếm). Nếu để trống sẽ bỏ qua đk tìm kiếm này, nếu có giá trị sẽ tìm tất cả phí thuộc loại hồ sơ truyền vào</param>
        /// <returns>Danh sách phí</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Fee, T>> projector, string sortBy = null,
                                                        bool isDescending = false,
                                                        Guid? doctypeId = null)
        {
            var spec = FeeQuery.WithDocTypeId(doctypeId);
            var param = string.IsNullOrEmpty(sortBy) ? new[] { "CreatedOnDate", "IsRequierd", "FeeName" } : new[] { sortBy, "CreatedOnDate", "IsRequierd" };
            var sort = Context.Filters.CreateSort<Fee>(isDescending, param);
            var result = _feeRepository.GetsAs(projector, spec, sort);
            return result;
        }

        /// <summary> 
        /// Kiểm tra xem có giá trị nào phù hợp với điều kiện truyền vào hay không
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public bool Exist(Expression<Func<Fee, bool>> spec)
        {
            return _feeRepository.Exist(spec);
        }

        /// <summary>
        /// Lấy ra lệ phí theo id
        /// </summary>
        /// <param name="feeId">Id của lệ phí</param>
        /// <returns>Entity lệ phí</returns>
        public Fee Get(int feeId)
        {
            Fee result = null;
            if (feeId > 0)
            {
                result = _feeRepository.Get(feeId);
            }
            return result;
        }

        /// <summary>
        /// Lấy ra lệ phí
        /// </summary>
        /// <param name="feeName">Tên lệ phí</param>
        /// <returns>Entity lệ phí</returns>
        public Fee Get(string feeName)
        {
            return _feeRepository.Get(true, FeeQuery.WithName(feeName));
        }

        /// <summary>
        /// Tạo mới lệ phí
        /// </summary>
        /// <param name="fee">Entity lệ phí</param>
        public void Create(Fee fee)
        {
            if (fee == null)
            {
                throw new ArgumentNullException("fee");
            }

            if (string.IsNullOrWhiteSpace(fee.FeeName))
            {
                throw new ArgumentNullException("FeeName");
            }

            if (fee.Price <= 0)
            {
                return;
            }

            var existFee = _feeRepository.GetReadOnly(f => f.FeeName.Equals(fee.FeeName, StringComparison.OrdinalIgnoreCase) && f.DocTypeId.Equals(fee.DocTypeId));

            if (existFee == null)
            {
                _feeRepository.Create(fee);
            }
            else
            {
                fee = existFee;
            }

            if (!_doctypeFeeRepository.Exist(df => df.FeeId == fee.FeeId && df.DocTypeId == fee.DocTypeId))
            {
                _doctypeFeeRepository.Create(new DoctypeFee()
                {
                    DocTypeId = fee.DocTypeId,
                    FeeId = fee.FeeId
                });
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới lệ phí
        /// </summary>
        /// <param name="fees">Danh sách lệ phí</param>
        /// <param name="ignoreExist">True: bỏ qua các lệ phí đã tồn tại; False: validate lệ phí đã tồn tại</param>
        public void Create(IEnumerable<Fee> fees, bool ignoreExist)
        {
            if (fees == null || !fees.Any())
            {
                throw new ArgumentNullException("fees");
            }

            var names = fees.Select(x => x.FeeName);
            var exist = _feeRepository.GetsAs(p => p.FeeName, p => names.Contains(p.FeeName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == fees.Count())
                {
                    throw new EgovException(_resourceService.GetResource("Fee.Create.Exist"));
                }

                var list = fees.Where(p => !exist.Contains(p.FeeName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("Fee.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(fees);
            }
        }

        private void Create(IEnumerable<Fee> fees)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var fee in fees)
            {
                _feeRepository.Create(fee);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa 1 lệ phí
        /// </summary>
        /// <param name="fee">Entity lệ phí</param>
        public void Delete(Fee fee)
        {
            if (fee == null)
            {
                throw new ArgumentNullException("fee");
            }
            _feeRepository.Delete(fee);
            Context.SaveChanges();
        }

        /// <summary>
        /// Chắc chắn lệ phí tồn tại và trả về lệ phí đó
        /// </summary>
        /// <param name="feeName">Tên lệ phí</param>
        /// <param name="doctypeId">Loại hồ sơ</param>
        /// <param name="feeType">Loại giấy tờ</param>
        /// <param name="price">Lệ phí</param>
        /// <param name="userId">Người tạo</param>
        /// <returns>Lệ phí tương ứng</returns>
        public Fee EnsureExist(string feeName, Guid doctypeId, Entities.FeeType feeType, int price, int userId)
        {
            var checkFee = _feeRepository.Get(false, p => p.FeeName.Equals(feeName, StringComparison.OrdinalIgnoreCase) && p.FeeTypeId == (int)feeType);
            if (checkFee != null)
            {
                if (!_doctypeFeeRepository.Exist(df => df.FeeId == checkFee.FeeId && df.DocTypeId.Equals(doctypeId)))
                {
                    _doctypeFeeRepository.Create(new DoctypeFee()
                    {
                        DocTypeId = doctypeId,
                        FeeId = checkFee.FeeId
                    });

                    Context.SaveChanges();
                }
                return checkFee;
            }

            var newFee = new Fee
            {
                Price = price,
                FeeName = feeName,
                FeeTypeId = (int)Entities.FeeType.ThuongBosung,
                IsRequired = true,
                DocTypeId = doctypeId,
                CreatedByUserId = userId
            };
            _feeRepository.Create(newFee);

            var doctypeFee = new DoctypeFee()
            {
                DocTypeId = doctypeId,
                FeeId = newFee.FeeId
            };
            _doctypeFeeRepository.Create(doctypeFee);

            Context.SaveChanges();

            return newFee;
        }

        /// <summary>
        /// Cập nhật thông tin lệ phí
        /// </summary>
        /// <param name="fee">Entity lệ phí</param>
        /// <param name="oldFeeName">Tên lệ phí trước khi cập nhật</param>
        public void Update(Fee fee, string oldFeeName)
        {
            if (fee == null)
            {
                throw new ArgumentNullException("fee");
            }
            if (_feeRepository.Exist(FeeQuery.WithName(fee.FeeName).And(r => r.FeeName.ToLower() != oldFeeName.ToLower())))
            {
                throw new Exception(string.Format("Tên phí ({0}) đã tồn tại!", fee.FeeName));
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa lệ phí của loại hồ sơ
        /// </summary>
        /// <param name="doctypeId">Doctype Id</param>
        /// <param name="feeId">Fee id</param>
        public void DeleteDocTypeFee(Guid doctypeId, int feeId)
        {
            var fee = Get(feeId);
            if (fee != null)
            {
                Delete(fee);
            }
        }
    }
}
