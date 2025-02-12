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
    public class CountryRepository(AppDbContext appDbContext) : IGenericRepository<Country>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var country = await appDbContext.Countries.FindAsync(id);

            if (country == null) return NotFound();

            appDbContext.Countries.Remove(country);
            await Commit();
            return Success();
        }
        public async Task<List<Country>> GetAll()
        {
            return await appDbContext.Countries.ToListAsync();
        }
        public async Task<Country> GetById(int id)
        {
            return await appDbContext.Countries.FindAsync(id);
        }
        public async Task<GeneralResponse> Insert(Country country)
        {
            if (!await CheckName(country.Name))
            {
                return new GeneralResponse(false, "Department already added");
            }
            appDbContext.Countries.Add(country);
            await Commit();
            return Success();
        }

        private async Task<bool> CheckName(string name)
        {
            var country = await appDbContext.Countries.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return country is null;
        }

        public async Task<GeneralResponse> Update(Country country)
        {
            Country branch = await appDbContext.Countries.FindAsync(country.Id);
            if (branch == null) return NotFound();
            branch.Name = country.Name;
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
