using Maintenance_Scheduling_System.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Services.BackgroundServices
{
    public class TaskBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TaskBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var taskService = scope.ServiceProvider.GetRequiredService<IMainTaskService>();
                await taskService.OverDueTask();

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }

}
