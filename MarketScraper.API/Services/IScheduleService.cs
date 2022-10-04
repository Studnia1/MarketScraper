using Hangfire;
using System.Linq.Expressions;

namespace MarketScraper.API.Services
{
    public interface IScheduleService
    {
        void AddNewScheduledProcess(Expression<Action> methodCall);
        Task Run(IJobCancellationToken token);
        Task RunAtTimeOf(DateTime now);
    }
}