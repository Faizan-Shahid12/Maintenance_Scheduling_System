using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Commands
{
    public class CreateMaintenanceHistoryFromTaskCommand : IRequest
    {
        public CreateMaintenanceHistoryDTO MainHistoryDTO { get; set; }
        public MainTask Task { get; set; }
        public Equipment Equip { get; set; }

        public CreateMaintenanceHistoryFromTaskCommand(CreateMaintenanceHistoryDTO mainHistoryDTO, MainTask task, Equipment equip)
        {
            MainHistoryDTO = mainHistoryDTO;
            Task = task;
            Equip = equip;
        }
    }
}
