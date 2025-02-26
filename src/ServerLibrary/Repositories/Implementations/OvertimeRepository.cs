using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class OvertimeRepository(AppDbContext appDbContext) : IGenericRepository<Overtime>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.OverTimes.FirstOrDefaultAsync(p => p.EmployeeId == id);
            if (item is null)
            {
                return NotFound();
            }
            appDbContext.OverTimes.Remove(item);
            await Commit();
            return Success();
        }
        public async Task<List<Overtime>> GetAll()
        {
            return await appDbContext.OverTimes.AsNoTracking().ToListAsync();
        }
        public async Task<Overtime> GetById(int id)
        {
            return await appDbContext.OverTimes.FirstOrDefaultAsync(p => p.EmployeeId == id);
        }
        public async Task<GeneralResponse> Insert(Overtime entity)
        {
            appDbContext.OverTimes.Add(entity);
            await Commit();
            return Success();
        }
        public async Task<GeneralResponse> Update(Overtime entity)
        {
            var item = await appDbContext.OverTimes.FirstOrDefaultAsync(p => p.EmployeeId == entity.EmployeeId);
            if (item is null)
            {
                return NotFound();
            }
            item.StartDate = entity.StartDate;
            item.EndtDate = entity.EndtDate;
            //item.NumberOfDays = entity.NumberOfDays;
            item.Rate = entity.Rate;
            item.Other = entity.Other;
            item.OvertimeTypeId = entity.OvertimeTypeId;
            item.Amount =entity.Amount;
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
