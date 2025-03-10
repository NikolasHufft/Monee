﻿using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class VacationRepository(AppDbContext appDbContext) : IGenericRepository<Vacation>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Vacations.FirstOrDefaultAsync(p => p.EmployeeId == id);
            if (item is null)
            {
                return NotFound();
            }
            appDbContext.Vacations.Remove(item);
            await Commit();
            return Success();
        }
        public async Task<List<Vacation>> GetAll()
        {
            return await appDbContext.Vacations.AsNoTracking().ToListAsync();
        }
        public async Task<Vacation> GetById(int id)
        {
            return await appDbContext.Vacations.FirstOrDefaultAsync(p => p.EmployeeId == id);
        }
        public async Task<GeneralResponse> Insert(Vacation entity)
        {
            appDbContext.Vacations.Add(entity);
            await Commit();
            return Success();
        }
        public async Task<GeneralResponse> Update(Vacation entity)
        {
            var item = await appDbContext.Vacations.FirstOrDefaultAsync(p => p.EmployeeId == entity.EmployeeId);
            if (item is null)
            {
                return NotFound();
            }
            item.StartDate = entity.StartDate;
            //item.EndtDate = entity.EndtDate;
            item.Other = entity.Other;
            //item.NumberOfDays = entity.MedicalPrescription;
            item.Amount = entity.Amount;
            item.VacationTypeId = entity.VacationTypeId;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new GeneralResponse(false, "Sorry department not found");
        private static GeneralResponse Success() => new GeneralResponse(true, "Process completed");
        private async Task Commit()
        {
            await appDbContext.SaveChangesAsync();
        }
    }
}
