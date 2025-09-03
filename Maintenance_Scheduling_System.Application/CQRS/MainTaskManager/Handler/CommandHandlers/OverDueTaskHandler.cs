using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Application.HubInterfaces;
using Maintenance_Scheduling_System.Domain.Enums;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Handler.CommandHandlers
{
    public class OverDueTaskHandler: IRequestHandler<OverDueTaskCommand>
    {
        private readonly IMainTaskRepo _taskRepo;
        private readonly ITaskHub _taskHub;
        private readonly IMapper _mapper;

        public OverDueTaskHandler(IMainTaskRepo taskRepo, ITaskHub taskHub, IMapper mapper)
        {
            _taskRepo = taskRepo;
            _taskHub = taskHub;
            _mapper = mapper;
        }

        public async Task Handle(OverDueTaskCommand request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepo.GetAllTask();
            if (tasks == null) return ;

            foreach (var task in tasks.ToList())
            {
                if (task.DueDate < DateOnly.FromDateTime(DateTime.Now) && task.Status != StatusEnum.Completed)
                {
                    task.Status = StatusEnum.OverDue;
                    await _taskRepo.UpdateTask();
                }
                else if (task.DueDate >= DateOnly.FromDateTime(DateTime.Now) && task.Status != StatusEnum.Completed)
                {
                    task.Status = StatusEnum.Pending;
                    await _taskRepo.UpdateTask();
                }

                var dto = _mapper.Map<MainTaskDTO>(task);

                await _taskHub.SendChangeTaskStatusToClient(dto, task.TechnicianId, "System");
            }
            return ;
        }
    }

}
