using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Queries;
using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Handler.QueryHandler
{
    public class GetMainTasksByEquipmentIdHandler : IRequestHandler<GetMainTasksByEquipmentIdQuery, List<MainTaskDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IMainTaskRepo _taskRepo;

        public GetMainTasksByEquipmentIdHandler(IMapper mapper, IMainTaskRepo taskRepo)
        {
            _mapper = mapper;
            _taskRepo = taskRepo;
        }

        public async Task<List<MainTaskDTO>> Handle(GetMainTasksByEquipmentIdQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepo.GetAllTaskByEquipId(request.EquipId);
            return _mapper.Map<List<MainTaskDTO>>(tasks);
        }
    }
}
