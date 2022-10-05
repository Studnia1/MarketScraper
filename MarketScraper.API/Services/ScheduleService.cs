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

        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await RunAtTimeOf(DateTime.Now);
        }

        public async Task RunAtTimeOf(DateTime now)
        {
            using IServiceScope scope = this._serviceProvider.CreateScope();
            var myJobService = scope.ServiceProvider.GetServices<IMarketService>();
            foreach (var job in myJobService)
            {
                AddNewScheduledProcess(() => job.Scrap());
            }
        }

        public void AddNewScheduledProcess(Expression<Action> methodCall)
        {
            RecurringJob.AddOrUpdate(methodCall, fiveMinuteCronExpression);
        }
    }
}
