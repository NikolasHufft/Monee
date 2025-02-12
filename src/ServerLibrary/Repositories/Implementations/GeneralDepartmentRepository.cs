using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class GeneralDepartmentRepository(AppDbContext appDbContext) : IGenericRepository<GeneralDepartment>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var department = await appDbContext.GeneralDepartments.FindAsync(id);
            
            if (department == null) return NotFound();

            appDbContext.GeneralDepartments.Remove(department);
            await Commit();
            return Success();
        }
        public async Task<List<GeneralDepartment>> GetAll()
        {
             return await appDbContext.GeneralDepartments.ToListAsync();
        }
        public async Task<GeneralDepartment> GetById(int id)
        {
            GeneralDepartment? department = await appDbContext.GeneralDepartments.FindAsync(id);
            return department!;
        }
        public async Task<GeneralResponse> Insert(GeneralDepartment entity)
        {
            if (!await CheckName(entity.Name))
            {
                return new GeneralResponse(false, "General Department already added");
            }
            appDbContext.GeneralDepartments.Add(entity);
            await Commit();
            return Success();
        }

        private async Task<bool> CheckName(string name)
        {
            var entity = await appDbContext.GeneralDepartments.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return entity is null;
        }

        public async Task<GeneralResponse> Update(GeneralDepartment entity)
        {
            GeneralDepartment? department = await appDbContext.GeneralDepartments.FindAsync(entity.Id);
            if (department == null) return NotFound();
            department.Name = entity.Name;
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
