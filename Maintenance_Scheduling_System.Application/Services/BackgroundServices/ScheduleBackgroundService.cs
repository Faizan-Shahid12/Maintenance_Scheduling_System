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
    public class ScheduleBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ScheduleBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var ScheduleService = scope.ServiceProvider.GetRequiredService<IMaintenanceScheduleService>();
                await ScheduleService.AutomaticallyUnactivate();
                await ScheduleService.AutomaticallyGenerate();

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

}
