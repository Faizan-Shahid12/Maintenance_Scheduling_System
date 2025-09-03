using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.External_Libraries.Hubs
{
    public class TaskHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userClaim = Context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userClaim != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user:{userClaim.Value}");
                Console.WriteLine($"Connection {Context.ConnectionId} added to group user:{userClaim.Value}");
            }
            else
            {
                Console.WriteLine($"Connection {Context.ConnectionId} has no NameIdentifier claim.");
            }

            if (Context.User.IsInRole("Admin"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
                Console.WriteLine($"Connection {Context.ConnectionId} added to group Admins");
            }

            await base.OnConnectedAsync();
        }

    }
}
