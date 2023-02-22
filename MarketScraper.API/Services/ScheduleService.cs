using Hangfire;
using System.Linq.Expressions;

namespace MarketScraper.API.Services
{
    public class ScheduleService : IScheduleService
    {
        private const string fiveMinuteCronExpression = "*/1 * * * *";

        private readonly IServiceProvider _serviceProvider;

        public ScheduleService(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public void Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
             RunAtTimeOf(DateTime.Now);
        }

        public void  RunAtTimeOf(DateTime now)
        {
            using IServiceScope scope = this._serviceProvider.CreateScope();
            var myJobService = scope.ServiceProvider.GetServices<IMarketService>();
            foreach (var job in myJobService)
            {
                AddNewScheduledProcess(() => job.IterateScrapThroughAllGames());
            }
        }

        public void AddNewScheduledProcess(Expression<Action> methodCall)
        {
            RecurringJob.AddOrUpdate(methodCall, fiveMinuteCronExpression);
        }
    }
}
