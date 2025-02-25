using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class CityRepository(AppDbContext appDbContext) : IGenericRepository<City>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var city = await appDbContext.Cities.FindAsync(id);

            if (city == null) return NotFound();

            appDbContext.Cities.Remove(city);
            await Commit();
            return Success();
        }
        public async Task<List<City>> GetAll()
        {
            return await appDbContext.Cities.AsNoTracking().Include(p => p.Country).ToListAsync();
        }
        public async Task<City> GetById(int id)
        {
            return await appDbContext.Cities.FindAsync(id);
        }
        public async Task<GeneralResponse> Insert(City Cities)
        {
            if (!await CheckName(Cities.Name))
            {
                return new GeneralResponse(false, "Department already added");
            }
            appDbContext.Cities.Add(Cities);
            await Commit();
            return Success();
        }

        private async Task<bool> CheckName(string name)
        {
            var entity = await appDbContext.Branches.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return entity is null;
        }

        public async Task<GeneralResponse> Update(City entity)
        {
            City city = await appDbContext.Cities.FindAsync(entity.Id);
            if (city == null) return NotFound();
            city.Name = entity.Name;
            city.CountryId = entity.CountryId;
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
