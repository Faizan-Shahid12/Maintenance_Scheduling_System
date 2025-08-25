using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Commands
{
    public class EditMaintenanceHistoryCommand : IRequest
    {
        public MaintenanceHistoryDTO MaintenanceHistoryDTO { get; set; }

        public EditMaintenanceHistoryCommand(MaintenanceHistoryDTO dto)
        {
            MaintenanceHistoryDTO = dto;
        }
    }
}
