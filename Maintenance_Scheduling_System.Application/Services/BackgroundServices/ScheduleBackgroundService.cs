using Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Commands;
using Maintenance_Scheduling_System.Application.Interfaces;
using MediatR;
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
                var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await _mediator.Send(new AutomaticallyGenerateCommand());
                await _mediator.Send(new AutomaticallyUnactivateCommand());

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

}
