using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionType - public - Entity</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Entity tương ứng với bảng ProcessFunctionType trong CSDL</para>
    /// </summary>
    public class ProcessFunctionType
    {
        private ICollection<ProcessFunction> _processFunctions;

        /// <summary>
        /// Lấy hoặc thiết lập Id loại function
        /// </summary>
        public int ProcessFunctionTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên loại function
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query để lấy ra loại
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Cột hiển thị
        /// </summary>
        public string TextField { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các tham số điều kiện
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// Lấy ra danh sách các tham số điều kiện
        /// </summary>
        /// <exception cref="Exception"></exception>
        public List<ProcessFunctionTypeParam> ListParam
        {
            get
            {
                try
                {
                    return Json2.ParseAs<List<ProcessFunctionTypeParam>>(Param);
                }
                catch (Exception ex)
                {
                    throw new Exception("Danh sách các tham số điều kiện bị sai định dạng", ex);
                }
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Các function
        /// </summary>
        public virtual ICollection<ProcessFunction> ProcessFunctions
        {
            get { return _processFunctions ?? (_processFunctions = new List<ProcessFunction>()); }
            set { _processFunctions = value; }
        }
    }
}
