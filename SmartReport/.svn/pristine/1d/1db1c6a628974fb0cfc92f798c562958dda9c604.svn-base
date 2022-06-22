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
    /// <para>Class : FormGroupBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 011013</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng FormGroup trong CSDL</para>
    /// </summary>
    public class FormGroupBll : ServiceBase
    {
        private readonly IRepository<FormGroup> _formGroupRepository;
        private readonly IRepository<Form> _formRepository;
        private readonly FormBll _formService;
        private readonly ResourceBll _resourceService;

        ///<summary>
        /// Khởi tạo class <see cref="IncreaseBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        ///<param name="formservice">Bll tương ứng với bảng form</param>
        ///<param name="resourceservice">Bll tương ứng với bảng resource</param>
        public FormGroupBll(IDbCustomerContext context, FormBll formservice, ResourceBll resourceservice)
            : base(context)
        {
            _formGroupRepository = Context.GetRepository<FormGroup>();
            _formRepository = Context.GetRepository<Form>();
            _formService = formservice;
            _resourceService = resourceservice;
        }

        /// <summary>
        /// Lấy ra tất cả nhóm biểu mẫu. Kết quả chỉ đọc
        /// </summary>
        /// <param name="sortBy"> Sắp xếp theo </param>
        /// <param name="isDescending"> Sắp xếp từ lớn đến nhỏ: true, ngược lại false </param>
        /// <returns> Danh sách nhóm biểu mẫu </returns>
        public IEnumerable<FormGroup> Gets(string sortBy, bool isDescending = false)
        {
            var sort = Context.Filters.CreateSort<FormGroup>(isDescending, sortBy);
            return _formGroupRepository.GetsReadOnly(null, sort);
        }

        /// <summary>
        /// Lấy ra tất cả nhóm biểu mẫu. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="isReadOnly"></param>
        /// <returns> Danh sách nhóm biểu mẫu </returns>
        public IEnumerable<FormGroup> Gets(Expression<Func<FormGroup, bool>> spec = null, bool isReadOnly = true)
        {
            return _formGroupRepository.Gets(isReadOnly, spec);
        }

        /// <summary> 
        /// Lấy ra tất cả nhóm biểu mẫu. Kết quả chỉ đọc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FormGroup> Gets()
        {
            return _formGroupRepository.GetsReadOnly();
        }

        /// <summary>
        /// Lấy ra tất cả các nhóm biểu mẫu phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="T">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách nhóm biểu mẫu</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<FormGroup, T>> projector, Expression<Func<FormGroup, bool>> spec = null)
        {
            return _formGroupRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Lấy ra nhóm biểu mẫu theo id
        /// </summary>
        /// <param name="formGroupId">Id của nhóm biểu mẫu</param>
        /// <returns>Entity nhóm biểu mẫu</returns>
        public FormGroup Get(int formGroupId)
        {
            FormGroup result = null;
            if (formGroupId > 0)
            {
                result = _formGroupRepository.Get(formGroupId);
            }
            return result;
        }

        /// <summary>
        /// Tạo mới nhóm biểu mẫu
        /// </summary>
        /// <param name="formGroup">Entity nhóm biểu mẫu</param>
        public void Create(FormGroup formGroup)
        {
            if (formGroup == null)
            {
                throw new ArgumentNullException("formGroup");
            }
            if (_formGroupRepository.Exist(FormGroupQuery.WithName(formGroup.FormGroupName)))
            {
                throw new Exception(string.Format("Nhóm biểu mẫu ({0}) đã tồn tại!", formGroup.FormGroupName));
            }
            _formGroupRepository.Create(formGroup);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới nhóm biểu mẫu
        /// </summary>
        /// <param name="formGroups">Entity nhóm biểu mẫu</param>
        /// <param name="ignoreExist"></param>
        public void Create(IEnumerable<FormGroup> formGroups, bool ignoreExist)
        {
            if (formGroups == null || !formGroups.Any())
            {
                throw new ArgumentNullException("formGroup");
            }
            var names = formGroups.Select(x => x.FormGroupName);
            var exist = _formGroupRepository.GetsAs(p => p.FormGroupName, p => names.Contains(p.FormGroupName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == formGroups.Count())
                {
                    throw new EgovException(_resourceService.GetResource("FormGroup.Create.Exist"));
                }

                var list = formGroups.Where(p => !exist.Contains(p.FormGroupName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("FormGroup.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(formGroups);
            }
        }

        private void Create(IEnumerable<FormGroup> formGroups)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in formGroups)
            {
                _formGroupRepository.Create(item);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật nhóm biểu mẫu
        /// </summary>
        /// <param name="formGroup">Entity nhóm biểu mẫu</param>
        /// <param name="oldName"> </param>
        /// <param name="formIds">Danh sách id mẫu sử dụng</param>
        public void Update(FormGroup formGroup, string oldName, IEnumerable<Guid> formIds = null)
        {
            if (formGroup == null)
            {
                throw new ArgumentNullException("formGroup");
            }
            if (_formGroupRepository.Exist(FormGroupQuery.WithName(formGroup.FormGroupName).And(r => r.FormGroupName.ToLower() != oldName.ToLower())))
            {
                throw new Exception(string.Format("Nhóm biểu mẫu ({0}) đã tồn tại!", formGroup.FormGroupName));
            }

            if (formIds != null && formIds.Any())
            {
                var exists = _formService.Gets(p => p.FormGroupId == formGroup.FormGroupId, false);
                if (exists != null && exists.Any())
                {
                    var existIds = exists.Select(p => p.FormId);
                    var removeFormIds = existIds.Where(p => !formIds.Contains(p));
                    if (removeFormIds != null && removeFormIds.Any())
                    {
                        var removeForms = _formService.Gets(p => removeFormIds.Contains(p.FormId), false);
                        if (removeForms != null && removeForms.Any())
                        {
                            foreach (var item in removeForms)
                            {
                                item.FormGroupId = null;
                            }
                        }
                    }

                    var addFormIds = formIds.Where(p => !existIds.Contains(p));
                    if (addFormIds != null && addFormIds.Any())
                    {
                        var addForms = _formService.Gets(p => addFormIds.Contains(p.FormId), false);
                        if (addForms != null && addForms.Any())
                        {
                            foreach (var item in addForms)
                            {
                                item.FormGroupId = formGroup.FormGroupId;
                            }
                        }
                    }
                }
                else
                {
                    var forms = _formService.Gets(p => formIds.Contains(p.FormId), false);
                    if (forms != null && forms.Any())
                    {
                        foreach (var item in forms)
                        {
                            item.FormGroupId = formGroup.FormGroupId;
                        }
                    }
                }
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa 1 nhóm biểu mẫu
        /// </summary>
        /// <param name="formGroup">Thực thể nhóm biểu mẫu</param>
        public void Delete(FormGroup formGroup)
        {
            if (formGroup == null)
            {
                throw new ArgumentNullException("formGroup");
            }
            //Kiểm tra ràng buộc trong bảng form
            if (_formService.Exist(f => f.FormGroupId == formGroup.FormGroupId))
            {
                throw new EgovException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.FormGroup.Delete.Exception.Form"));
            }
            _formGroupRepository.Delete(formGroup);
            Context.SaveChanges();
        }
    }
}
