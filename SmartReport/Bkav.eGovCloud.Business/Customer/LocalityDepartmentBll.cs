using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LocalityDepartment = Bkav.eGovCloud.Entities.Customer.LocalityDepartment;

namespace Bkav.eGovCloud.Business.Customer
{
    public class LocalityDepartmentBll : ServiceBase
	{
		#region Readonly & Static Fields

		private readonly IRepository<LocalityDepartment> _localityDepartmentRepository;
		private readonly DepartmentBll _departmentService;
		private readonly LocalityBll _localityService;
		private readonly AdminGeneralSettings _generalSettings;
		private readonly ResourceBll _resourceService;

		#endregion

		#region C'tors

		///<summary>
		///  Khởi tạo class <see cref="LocalityDepartmentBll" />.
		///</summary>
		///<param name="generalSettings"> Cấu hình chung </param>
		///<param name="resourceService"> Bll liên quan đến bảng Resource trong CSDL</param>
		public LocalityDepartmentBll(
			IDbCustomerContext context,
			LocalityBll localityService,
			AdminGeneralSettings generalSettings,
			 DepartmentBll departmentService,
			ResourceBll resourceService):base(context)
		{
			_localityDepartmentRepository = Context.GetRepository<LocalityDepartment>();
			_departmentService = departmentService;
			_localityService = localityService;
			_generalSettings = generalSettings;
			_resourceService = resourceService;
		}

		#endregion

		#region Quản lý địa bàn và phòng ban

		public IEnumerable<LocalityDepartment> GetDepartmentIds(Expression<Func<LocalityDepartment, bool>> spec)
		{
			return _localityDepartmentRepository.GetsReadOnly(spec);
		}

		public void Delete(LocalityDepartment localityDepartment)
        {
			_localityDepartmentRepository.Delete(localityDepartment);
			Context.SaveChanges();
		}

		public void Delete(IEnumerable<LocalityDepartment> localityDepartments)
		{
			foreach (var item in localityDepartments)
			{
				Delete(item);
			}
		}

		public void Create(LocalityDepartment localityDepartment)
		{

			_localityDepartmentRepository.Create(localityDepartment);
			Context.SaveChanges();
		}

		public IEnumerable<LocalityDepartment> Gets(bool isReadOnly,
							Expression<Func<LocalityDepartment, bool>> spec = null,
							Func<IQueryable<LocalityDepartment>, IQueryable<LocalityDepartment>> preFilter = null,
							params Func<IQueryable<LocalityDepartment>, IQueryable<LocalityDepartment>>[] postFilter)
        {
			return _localityDepartmentRepository.Gets(false, spec, preFilter, postFilter);
        }

		public void Create(IEnumerable<LocalityDepartment> localityDepartments)
		{
			foreach (var element in localityDepartments)
			{
				_localityDepartmentRepository.Create(element);
			}
			Context.SaveChanges();
		}
		#endregion
	}
}
