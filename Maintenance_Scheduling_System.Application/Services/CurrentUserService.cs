using Maintenance_Scheduling_System.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maintenance_Scheduling_System.Application.Services
{
    public class CurrentUserService : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public string Name
        {
            get
            {
                var name = _httpContextAccessor.HttpContext?.User?.FindFirstValue("FullName");
                return string.IsNullOrEmpty(name) ? "System" : name;
            }
        }

        public string Role
        {
            get
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return "System";
                }

                if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
                {
                    return "Admin";
                }
                else if (_httpContextAccessor.HttpContext.User.IsInRole("Technician"))
                {
                    return "Technician";
                }

                return "System";
            }
        }

    }

}
