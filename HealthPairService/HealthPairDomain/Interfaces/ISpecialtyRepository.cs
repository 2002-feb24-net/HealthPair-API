using HealthPairDomain.InnerModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<InnerSpeciality>> GetspecialityAsync(string search = null);

        Task<InnerSpeciality> GetspecialityByIdAsync(int id);

        //Task<IEnumerable<InnerSpeciality>> GetspecialityByPatientNameAsync(string search = null);

        //Task<IEnumerable<InnerSpeciality>> GetspecialityByProviderId(int id);

        Task<bool> SpecialityExistAsync(int id);

        Task<InnerSpeciality> AddspecialityAsync(InnerSpeciality speciality);

        Task<bool> RemoveSpecialityAsync(int id);

        EntityState Changed(InnerSpeciality speciality);
    }
}
