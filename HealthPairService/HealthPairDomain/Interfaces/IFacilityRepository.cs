using HealthPairDomain.InnerModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    public interface IFacilityRepository
    {
        Task<List<Inner_Facility>> GetFacilityAsync(string search = null);
        Task<Inner_Facility> GetFacilityByIdAsync(int id);
        Task<bool> FacilityExistAsync(int id);
        Task<Inner_Facility> AddFacilityAsync(Inner_Facility facility);
        Task UpdateFacilityAsync(Inner_Facility facility);
        Task RemoveFacilityAsync(int id);
    }
}
