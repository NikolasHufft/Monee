using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class DepartmentRepository(AppDbContext appDbContext) : IGenericRepository<Department>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var department = await appDbContext.Departments.FindAsync(id);

            if (department == null) return NotFound();

            appDbContext.Departments.Remove(department);
            await Commit();
            return Success();
        }
        public async Task<List<Department>> GetAll()
        {
            return await appDbContext.Departments.ToListAsync();
        }
        public async Task<Department> GetById(int id)
        {
            return await appDbContext.Departments.FindAsync(id);
        }
        public async Task<GeneralResponse> Insert(Department entity)
        {
            if (!await CheckName(entity.Name))
            {
                return new GeneralResponse(false, "Department already added");
            }
            appDbContext.Departments.Add(entity);
            await Commit();
            return Success();
        }

        private async Task<bool> CheckName(string name)
        {
            var entity = await appDbContext.Departments.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return entity is null;
        }

        public async Task<GeneralResponse> Update(Department entity)
        {
            Department department = await appDbContext.Departments.FindAsync(entity.Id);
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
