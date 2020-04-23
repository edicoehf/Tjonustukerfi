using System;
using FluentScheduler;
using ThjonustukerfiWebAPI.Schedules.Tasks;

namespace ThjonustukerfiWebAPI.Schedules
{
    /// <summary>Scheduler used to schedule tasks</summary>
    public class Scheduler : Registry
    {
        public Scheduler()
        {
            // run every week at zero will make it run the first week as well
            Schedule<ArchiveTask>().ToRunEvery(0).Weeks().On(DayOfWeek.Sunday).At(3, 0);
        }
    }
}