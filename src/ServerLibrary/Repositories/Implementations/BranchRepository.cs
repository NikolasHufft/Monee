using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class BranchRepository(AppDbContext appDbContext) : IGenericRepository<Branch>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var Branch = await appDbContext.Branches.FindAsync(id);

            if (Branch == null) return NotFound();

            appDbContext.Branches.Remove(Branch);
            await Commit();
            return Success();
        }
        public async Task<List<Branch>> GetAll()
        {
            return await appDbContext.Branches.ToListAsync();
        }
        public async Task<Branch> GetById(int id)
        {
            return await appDbContext.Branches.FindAsync(id);
        }
        public async Task<GeneralResponse> Insert(Branch entity)
        {
            if (!await CheckName(entity.Name))
            {
                return new GeneralResponse(false, "Department already added");
            }
            appDbContext.Branches.Add(entity);
            await Commit();
            return Success();
        }

        private async Task<bool> CheckName(string name)
        {
            var entity = await appDbContext.Branches.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return entity is null;
        }

        public async Task<GeneralResponse> Update(Branch entity)
        {
            Branch branch = await appDbContext.Branches.FindAsync(entity.Id);
            if (branch == null) return NotFound();
            branch.Name = entity.Name;
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
