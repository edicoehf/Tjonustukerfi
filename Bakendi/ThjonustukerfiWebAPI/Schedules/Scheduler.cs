using FluentScheduler;
using ThjonustukerfiWebAPI.Schedules.Tasks;

namespace ThjonustukerfiWebAPI.Schedules
{
    /// <summary>Scheduler used to schedule tasks</summary>
    public class Scheduler : Registry
    {
        public Scheduler()
        {
            Schedule<ArchiveTask>().ToRunEvery(10).Seconds();
        }
    }
}