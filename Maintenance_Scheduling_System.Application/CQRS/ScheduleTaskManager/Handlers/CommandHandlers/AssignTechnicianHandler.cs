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
    public class AssignTechnicianHandler : IRequestHandler<AssignTechnicianCommand, ScheduleTaskDTO>
    {
        private readonly IScheduleTaskRepo _repo;
        private readonly IMapper _mapper;

        public AssignTechnicianHandler(IScheduleTaskRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ScheduleTaskDTO> Handle(AssignTechnicianCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.TechId))
                request.Task.TechnicianId = request.TechId;

            await _repo.UpdateScheduleTask(request.Task);
            return _mapper.Map<ScheduleTaskDTO>(request.Task);
        }
    }
}
