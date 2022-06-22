using FluentScheduler;
using System.Web.Hosting;

namespace Bkav.eGovCloud.Business.Tasks
{
    /// <summary>
    /// Interface đại diện cho 1 Job trong schedule.
    /// </summary>
    public interface IeGovJob : IJob, IRegisteredObject
    {
    }
}
