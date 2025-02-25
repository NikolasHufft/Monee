using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class EmployeeRepository(AppDbContext appDbContext) : IGenericRepository<Employee>
    {
        public async Task<GeneralResponse> DeleteById(int id) 
        {
            var employee = await appDbContext.Employees.FirstOrDefaultAsync(u => u.Id == id);
            if (employee is null)
            {
                return NotFound();
            }

            appDbContext.Employees.Remove(employee);
            await Commit();
            return Success();

        }
        public async Task<List<Employee>> GetAll()
        {
            var employees = await appDbContext.Employees
                .AsNoTracking()
                .Include(town => town.Town)
                    .ThenInclude(city => city!.City)
                    .ThenInclude(country => country!.Country)
                .Include(branch => branch.Branch)
                    .ThenInclude(department => department!.Department)
                    .ThenInclude(generalDepartment => generalDepartment!.GeneralDepartment)
                .ToListAsync();
            return employees;
        }
        public async Task<Employee> GetById(int id)
        {
            var employee = await appDbContext.Employees
                .Include(town => town.Town)
                    .ThenInclude(city => city!.City)
                    .ThenInclude(country => country!.Country)
                .Include(branch => branch.Branch)
                    .ThenInclude(department => department!.Department)
                    .ThenInclude(generalDepartment => generalDepartment!.GeneralDepartment)
                .FirstOrDefaultAsync(employee => employee.Id ==id);
            return employee!;
        }
        public async Task<GeneralResponse> Insert(Employee entity)
        {
            if (await CheckName(entity.Name))
            {
                return new GeneralResponse(false, "Employee already added");
            }

            appDbContext.Employees.Add(entity);
            await Commit();
            return Success();
        }
        public async Task<GeneralResponse> Update(Employee entity)
        {
            var employee = await appDbContext.Employees.FirstOrDefaultAsync(employee => employee.Id == entity.Id);
            if (employee is null)
            {
                return new GeneralResponse(false, "User does not exist");
            }

            employee.Name = entity.Name;
            //employee.LastName = entity.LastName;
            employee.Notes = entity.Notes;
            employee.Address = entity.Address;
            employee.PhoneNumber = entity.PhoneNumber;
            employee.BranchId = entity.BranchId;
            employee.TownId = entity.TownId;
            employee.CivilId = entity.CivilId;
            employee.FileNumber = entity.FileNumber;
            employee.JobTitle = entity.JobTitle;
            employee.PhotoUrl = entity.PhotoUrl;

            appDbContext.Employees.Update(employee);
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
            var entity = await appDbContext.Departments.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return entity is null;
        }
    }
}
