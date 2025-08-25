using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Settings;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Services
{
    public class AppUserService : IAppUserService
    {
        public IAppUserRepo AppUserRepository { get; set; }
        public IMapper mapper { get; set; }
        public ICurrentUser currentUser { get; set; }
        public TokenSetting tokenOptions { get; set; }
        public IRefreshTokenService refreshTokenService { get; set; }
        public IMainTaskService MainTaskService { get; set; }

        public AppUserService(IAppUserRepo appuserrepo,IMapper mapper1,ICurrentUser user1, IOptions<TokenSetting> option,IRefreshTokenService tokenService, IMainTaskService mainTaskService)
        {
            AppUserRepository = appuserrepo;
            mapper = mapper1;
            currentUser = user1;
            tokenOptions = option.Value;
            refreshTokenService = tokenService;
            MainTaskService = mainTaskService;
        }

        public async Task<TechnicianDTO> CreateAppUser(AppUserDTO appuser,string role)
        {
            AppUser user = new AppUser
            {
                FullName = appuser.FullName,
                UserName = appuser.FullName.Replace(" ", "") + Guid.NewGuid(),
                Email = appuser.Email,
                PhoneNumber = appuser.PhoneNumber,
                Address = appuser.Address,
                Gender = appuser.Gender,
                CreatedAt = DateTime.Now,
                LastModifiedAt = DateTime.Now,
                LastModifiedBy = currentUser.Name,
                CreatedBy = currentUser.Name,
            };

            await AppUserRepository.CreateNewAppUser(user,appuser.Password,role);

            return mapper.Map<TechnicianDTO>(user);

        }
        public async Task<TechnicianDTO> DeleteAppUser(string id)
        {
            var user = await AppUserRepository.GetAppUserById(id);

            user.IsDeleted = true;

            user.LastModifiedBy = currentUser.Name;
            user.LastModifiedAt = DateTime.UtcNow;

            await AppUserRepository.DeleteAppUser(user);

            await MainTaskService.UnAssignTechnicianTaskUponDeletion(id);

            return mapper.Map<TechnicianDTO>(user);
        }
        public async Task<TechnicianDTO> UpdateAppUser(string Id,AppUserDTO appdto)
        {
            var user = await AppUserRepository.GetAppUserById(Id);

            user.FullName = appdto.FullName;
            user.Email = appdto.Email;
            user.PhoneNumber = appdto.PhoneNumber;
            user.Address = appdto.Address;
            user.Gender = appdto.Gender;
            user.UserName = appdto.FullName.Replace(" ", "");

            user.LastModifiedBy = currentUser.Name;
            user.LastModifiedAt = DateTime.UtcNow;

            await AppUserRepository.UpdateAppUser(user);

            return mapper.Map<TechnicianDTO>(user);
        }
        public async Task<List<TechnicianDTO>> GetAllTechnicianUsers()
        {
            var user = await AppUserRepository.GetTechniciansUsers();
            var userdto = mapper.Map<List<TechnicianDTO>>(user);

            return userdto;
        }
        public async Task<List<TechnicianDTO>> GetAllTechnicianUsersWithoutTask()
        {
            var user = await AppUserRepository.GetTechniciansUsersWithoutTasks();
            var userdto = mapper.Map<List<TechnicianDTO>>(user);

            return userdto;
        }

        public async Task<TokenResponseDTO> CreateToken(AppUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),                    
                new Claim("FullName", user.FullName),
            };

            var userRoles = await AppUserRepository.GetRoles(user);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: DateTime.UtcNow.AddMinutes(tokenOptions.AccessExpiryInMins),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Key)),
                    SecurityAlgorithms.HmacSha256
                )
            );

            var refreshToken = await refreshTokenService.GenerateRefreshTokenAsync(user.Id);

            return new TokenResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                AccessTokenExpiry = token.ValidTo,
                UserId = user.Id,
                Name = user.FullName,
                Roles = userRoles.ToList()
            };
        }

        public async Task<AppUser> Login(LoginDTO LoginInfo)
        {
            var user = await AppUserRepository.GetAppUserByEmail(LoginInfo.Email);

            if (user == null)
            {
                return null;
            }

            var hasher = new PasswordHasher<AppUser>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, LoginInfo.Password);

            if (result == PasswordVerificationResult.Success)
            {
                return user;
            }

            return null;

        }
        public async Task<TechnicianDTO> GetTechnicianById(string TechId)
        {
            var user = await AppUserRepository.GetAppUserById(TechId);

            if (user == null)
            {
                return null;
            }
            var userdto = mapper.Map<TechnicianDTO>(user);

            return userdto;

        }

        public async Task ChangePassword(string TechId, ChangePasswordDTO Password)
        {
            await AppUserRepository.ChangePassword(TechId,Password.newPassword);
        }

        public async Task<bool> CheckEmail(string newEmail)
        {
           return await AppUserRepository.CheckEmail(newEmail);
        }
    }
}
