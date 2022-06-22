using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    /// <summary>
    /// Kế thừa lại từ class AbstractValidator
    /// Note: Chỉ sử dụng class này đối với những class cho phép tạo theo lô
    /// </summary>
    /// <typeparam name="T">Đối tượng cần validate</typeparam>
    public abstract class PacketValidator<T> : AbstractValidator<T> where T : class
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public PacketValidator() : base() { }

        /// <summary>
        ///  Validate khi tạo theo lô
        /// </summary>
        /// <param name="model">Đối tượng validate</param>
        /// <param name="value">Dữ liệu property validate</param>
        /// <returns></returns>
        public abstract bool ValidCreatePacket(T model, dynamic value);
    }
}