using Azure.Identity;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Maintenance_Scheduling_System.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.Repositories
{
    public class EquipmentRepository : IEquipmentRepo
    {
        public Maintenance_DbContext DbContext { get; set; }

        public EquipmentRepository(Maintenance_DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task AssignNewWorkShopLocation(int equipId,int WorkShopId,string Username)
        {
            var equip = await DbContext.Equipment.FindAsync(equipId);

            equip.WorkshopId = WorkShopId;
            equip.LastModifiedAt = DateTime.UtcNow;
            equip.LastModifiedBy = Username;

            DbContext.Equipment.Update(equip);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Equipment> CreateNewEquipment(Equipment equip)
        {
            await DbContext.Equipment.AddAsync(equip);
            await DbContext.SaveChangesAsync();
            return equip;

        }

        public async Task DeleteEquipment(Equipment equip1)
        {
            equip1.IsDeleted = true;
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateEquipment(Equipment equip)
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<Equipment>> GetAllEquipment()
        {
            return await DbContext.Equipment.Include(ws => ws.WorkShopLocation)
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }
        public async Task<List<Equipment>> GetAllArchivedEquipment()
        {
            return await DbContext.Equipment.Include(ws => ws.WorkShopLocation)
                .Where(e => !e.IsDeleted && e.IsArchived)
                .ToListAsync();
        }

        public async Task<List<Equipment>> GetEquipmentByName(string name)
        {
            return await DbContext.Equipment.Include(ws => ws.WorkShopLocation)
                .Where(e => !e.IsDeleted && e.Name.ToLower() == name.ToLower())
                .ToListAsync();
        }

        public async Task<Equipment> GetEquipmentById(int equipId)
        {
            var equip = await DbContext.Equipment.Include(ws => ws.WorkShopLocation)
                .Where(e => e.EquipmentId == equipId && !e.IsDeleted)
                .FirstOrDefaultAsync();

            if (equip == null) 
                throw new Exception("Equipment not found");

            return equip;
        }

        public async Task<Equipment> GetEquipmentByBarCodeId(string barCodeId)
        {
            if (!Guid.TryParse(barCodeId, out var BarCodeIdinGuid))
                throw new ArgumentException("Invalid Barcode Id format", nameof(barCodeId));

            var equip = await DbContext.Equipment.Include(ws => ws.WorkShopLocation)
                .Where(e => e.BarCodeId == BarCodeIdinGuid && !e.IsDeleted)
                .FirstOrDefaultAsync();

            if (equip == null)
                throw new Exception("Equipment not found");

            return equip;
        }
    }
}
