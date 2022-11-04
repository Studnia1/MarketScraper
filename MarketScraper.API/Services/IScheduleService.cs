using Hangfire;
using System.Linq.Expressions;

namespace MarketScraper.API.Services
{
    public interface IScheduleService
    {
        void AddNewScheduledProcess(Expression<Action> methodCall);
        void Run(IJobCancellationToken token);
        void RunAtTimeOf(DateTime now);
    }
}