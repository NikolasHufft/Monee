using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class SanctionRepository(AppDbContext appDbContext) : IGenericRepository<Sanction>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Sanctions.FirstOrDefaultAsync(p => p.EmployeeId == id);
            if (item is null)
            {
                return NotFound();
            }
            appDbContext.Sanctions.Remove(item);
            await Commit();
            return Success();
        }
        public async Task<List<Sanction>> GetAll()
        {
            return await appDbContext.Sanctions.AsNoTracking().ToListAsync();
        }
        public async Task<Sanction> GetById(int id)
        {
            return await appDbContext.Sanctions.FirstOrDefaultAsync(p => p.EmployeeId == id);
        }
        public async Task<GeneralResponse> Insert(Sanction entity)
        {
            appDbContext.Sanctions.Add(entity);
            await Commit();
            return Success();
        }
        public async Task<GeneralResponse> Update(Sanction entity)
        {
            var item = await appDbContext.Sanctions.FirstOrDefaultAsync(p => p.EmployeeId == entity.EmployeeId);
            if (item is null)
            {
                return NotFound();
            }
            item.Punishment = entity.Punishment;
            item.Other = entity.Other;
            item.SanctionTypeId = entity.SanctionTypeId;
            item.Date = entity.Date;
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
