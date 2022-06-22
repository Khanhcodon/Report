namespace Bkav.eGovCloud.Core.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// Hàm thực thi task
        /// </summary>
        void Execute();

        /// <summary>
        /// Thứ tự thực thi
        /// </summary>
        int Order { get; }
    }
}
