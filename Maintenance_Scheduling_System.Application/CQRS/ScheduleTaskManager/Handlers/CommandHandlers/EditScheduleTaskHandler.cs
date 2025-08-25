using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Handlers.CommandHandlers
{
    public class EditScheduleTaskHandler : IRequestHandler<EditScheduleTaskCommand, ScheduleTaskDTO>
    {
        private readonly IScheduleTaskRepo _repo;
        private readonly IMapper _mapper;

        public EditScheduleTaskHandler(IScheduleTaskRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ScheduleTaskDTO> Handle(EditScheduleTaskCommand request, CancellationToken cancellationToken)
        {
            var task = request.Task;
            var dto = request.STDTO;

            task.DueDate = dto.DueDate;
            task.TaskName = dto.TaskName;
            task.Priority = dto.Priority;
            task.Interval = dto.Interval;

            await _repo.UpdateScheduleTask(task);
            return _mapper.Map<ScheduleTaskDTO>(task);
        }
    }
}
