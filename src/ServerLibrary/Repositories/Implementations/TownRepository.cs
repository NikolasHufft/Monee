using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class TownRepository(AppDbContext appDbContext) : IGenericRepository<Town>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var town = await appDbContext.Towns.FindAsync(id);

            if (town == null) return NotFound();

            appDbContext.Towns.Remove(town);
            await Commit();
            return Success();
        }
        public async Task<List<Town>> GetAll()
        {
            return await appDbContext.Towns.ToListAsync();
        }
        public async Task<Town> GetById(int id)
        {
            return await appDbContext.Towns.FindAsync(id);
        }
        public async Task<GeneralResponse> Insert(Town town)
        {
            if (!await CheckName(town.Name))
            {
                return new GeneralResponse(false, "Department already added");
            }
            appDbContext.Towns.Add(town);
            await Commit();
            return Success();
        }

        private async Task<bool> CheckName(string name)
        {
            var town = await appDbContext.Towns.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return town is null;
        }

        public async Task<GeneralResponse> Update(Town entity)
        {
            Town town = await appDbContext.Towns.FindAsync(entity.Id);
            if (town == null) return NotFound();
            town.Name = entity.Name;
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
