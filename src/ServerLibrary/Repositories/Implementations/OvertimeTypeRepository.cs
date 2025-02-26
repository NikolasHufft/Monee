using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class OvertimeTypeRepository(AppDbContext appDbContext) : IGenericRepository<OvertimeType>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.OvertimeType.FirstOrDefaultAsync(p => p.Id == id);
            if (item is null)
            {
                return NotFound();
            }
            appDbContext.OvertimeType.Remove(item);
            await Commit();
            return Success();
        }
        public async Task<List<OvertimeType>> GetAll()
        {
            return await appDbContext.OvertimeType.AsNoTracking().ToListAsync();
        }
        public async Task<OvertimeType> GetById(int id)
        {
            return await appDbContext.OvertimeType.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<GeneralResponse> Insert(OvertimeType entity)
        {
            if (!await CheckName(entity!.Name))
            {
                return new GeneralResponse(false, "Overtime type already added");
            }
            appDbContext.OvertimeType.Add(entity);
            await Commit();
            return Success();
        }
        public async Task<GeneralResponse> Update(OvertimeType entity)
        {
            var item = await appDbContext.OvertimeType.FirstOrDefaultAsync(p => p.Id == entity.Id);
            if (item is null)
            {
                return NotFound();
            }
            item.Name = entity.Name;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new GeneralResponse(false, "Sorry department not found");
        private static GeneralResponse Success() => new GeneralResponse(true, "Process completed");
        private async Task Commit()
        {
            await appDbContext.SaveChangesAsync();
        }

        private async Task<bool> CheckName(string name)
        {
            var entity = await appDbContext.OvertimeType.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return entity is null;
        }
    }
}
