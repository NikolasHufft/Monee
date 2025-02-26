using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class DoctorRepository(AppDbContext appDbContext) : IGenericRepository<Doctor>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Doctors.FirstOrDefaultAsync(p => p.EmployeeId == id);
            if (item is null)
            {
                return NotFound();
            }
            appDbContext.Doctors.Remove(item);
            await Commit();
            return Success();
        }
        public async Task<List<Doctor>> GetAll()
        {
            return await appDbContext.Doctors.AsNoTracking().ToListAsync();
        }
        public async Task<Doctor> GetById(int id)
        {
             return await appDbContext.Doctors.FirstOrDefaultAsync(p => p.EmployeeId == id);
        }
        public async Task<GeneralResponse> Insert(Doctor entity)
        {
            appDbContext.Doctors.Add(entity);
            await Commit();
            return Success();
        }
        public async Task<GeneralResponse> Update(Doctor entity)
        {
            var item = await appDbContext.Doctors.FirstOrDefaultAsync(p => p.EmployeeId == entity.EmployeeId);
            if (item is null)
            {
                return NotFound();
            }
            item.MedicalRecommendation = entity.MedicalRecommendation;
            item.MedicalDiagnose = entity.MedicalDiagnose;
            item.MedicalCertificate = entity.MedicalCertificate;
            item.MedicalPrescription = entity.MedicalPrescription;
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
