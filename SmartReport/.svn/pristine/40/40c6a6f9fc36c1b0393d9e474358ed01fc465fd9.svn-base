using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Bkav.eGovCloud.Api.Dto;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Office)]
    public class OrganizationController : EgovApiBaseController
    {
        private readonly DepartmentBll _deptService;
        private readonly AddressBll _addressService;
        private readonly TransferSettings _tranferSettings;

        private const int CACHE_TIME = 2 * 60; // 2 phút

        /// <summary>
        /// C'tor
        /// </summary>
        public OrganizationController()
        {
            _addressService = DependencyResolver.Current.GetService<AddressBll>();
            _deptService = DependencyResolver.Current.GetService<DepartmentBll>();
            _tranferSettings = DependencyResolver.Current.GetService<TransferSettings>();
        }

        /// <summary>
        /// Trả về danh sách tất cả các cơ quan trong danh bạ liên thông
        /// </summary>
        /// <returns>Danh sách cơ quan</returns>
        [System.Web.Http.HttpGet]
        [OutputCache(Duration = CACHE_TIME)]
        public List<Organization> All()
        {
            var result = new List<Organization>();

            var addresses = _addressService.Gets();
            result = addresses.Select(a => new Organization()
            {
                OrganId = a.EdocId,
                OrganName = a.Name
            }).ToList();

            return result;
        }

        /// <summary>
        /// Thêm một cơ quan vào danh bạ liên thông và trả về giá trị xác định thêm thành công hay không
        /// </summary>
        /// <param name="entity">Cơ quan</param>
        /// <returns>true - thêm thành công; false - còn lại</returns>
        /// <exception cref="ArgumentNullException">Tham số null</exception>
        [System.Web.Http.HttpPost]
        public bool Create([FromBody]Organization entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                _addressService.Create(new Address()
                {
                    EdocId = entity.OrganId,
                    Name = entity.OrganName,
                    Email = "",
                    AddressString = "",
                    Fax = "",
                    Phone = "",
                    PhoneExt = ""
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Cập nhật cơ quan trong danh bạ liên thông và trả về giá trị xác định việc cập nhật có thành công hay không.
        /// </summary>
        /// <param name="entity">Cơ quan</param>
        /// <returns>true - cập nhật thành công; false - còn lại</returns>
        [System.Web.Http.HttpPost]
        public bool Update([FromBody]Organization entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var address = _addressService.GetByeDocId(entity.OrganId);
            if (address == null)
            {
                return false;
            }

            try
            {
                address.Name = entity.OrganName;
                _addressService.Update(address);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa cơ quan trong hệ thống liên thông và trả về giá trị xác định việc xóa có thành công hay không
        /// </summary>
        /// <param name="organId">Mã định danh cơ quan</param>
        /// <returns>True - xóa thành công; false - còn lại</returns>
        [System.Web.Http.HttpPost]
        public bool Delete(string organId)
        {
            if (string.IsNullOrEmpty(organId))
            {
                throw new ArgumentNullException("organId");
            }

            var address = _addressService.GetByeDocId(organId);
            if (address == null)
            {
                return true;
            }

            try
            {
                _addressService.Delete(address);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Trả về cấu hình liên thông của cơ quan hiện tại
        /// </summary>
        /// <returns>Cơ quan hiện tại</returns>
        [OutputCache(Duration = CACHE_TIME)]
        public Organization GetCurentOrgan()
        {
            var address = _addressService.GetCurrent();
            if (address == null)
            {
                return null;
            }

            return new Organization()
            {
                OrganId = address.EdocId,
                OrganName = address.Name
            };
        }

        /// <summary>
        /// Trả về danh sách mã định danh của cơ quan hiện tại.
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = CACHE_TIME)]
        public IEnumerable<Organization> GetCurentOrgans()
        {
            return _tranferSettings.GetCurrents().Select(u => new Organization() {
                OrganId = u.OrganId,
                OrganName = u.OrganName
            });
        }

        /// <summary>
        /// Thiết lập cơ quan hiện tại và trả về giá trị xác định thiết lập thành công hay không
        /// </summary>
        /// <param name="organization">Cơ quan</param>
        /// <returns>True - thiết lập thành công; false - còn lại</returns>
        [System.Web.Http.HttpPost]
        public bool SetCurentOrgan([FromBody]Organization organization)
        {
            if (organization == null)
            {
                throw new ArgumentNullException("organization");
            }

            try
            {
                var address = _addressService.GetByeDocId(organization.OrganId);
                if (address == null)
                {
                    _addressService.Create(new Address()
                    {
                        EdocId = organization.OrganId,
                        Name = organization.OrganName,
                        IsMe = true,
                        AddressString = ""
                    });
                }
                else
                {
                    address.IsMe = true;
                    address.Name = organization.OrganName;
                    _addressService.Update(address);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy ra danh sách người dùng và phòng ban
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [OutputCache(Duration = CACHE_TIME)]
        public string GetDeptAndUsers()
        {
            var results = _deptService.GetsAs(p => new
            {
                DepartmentId = p.DepartmentId,
                ParentId = p.ParentId,
                DepartmentName = p.DepartmentName,
                DepartmentIdExt = p.DepartmentIdExt,
                DepartmentPath = p.DepartmentPath,
                Order = p.Order,
                Level = p.Level,
                Users = p.UserDepartmentJobTitlesPositions.Select(x => x.User)
                .Select(x => new
                {
                    x.UserId,
                    x.Username,
                    x.FullName
                })
            }, true).Stringify();

            return results;
        }
    }
}