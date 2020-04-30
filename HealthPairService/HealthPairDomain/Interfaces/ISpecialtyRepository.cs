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
        Task<List<Inner_Specialty>> GetSpecialtyAsync(string search = null);
        Task<Inner_Specialty> GetSpecialtyByIdAsync(int id);
        Task<bool> SpecialtyExistAsync(int id);
        Task<Inner_Specialty> AddSpecialtyAsync(Inner_Specialty specialty);
        Task<bool> RemoveSpecialtyAsync(int id);
    }
}
