using HealthPairDomain.InnerModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    public interface IInsuranceRepository
    {
        Task<IEnumerable<InnerInsurance>> GetInsuranceAsync(string search = null);

        //Task<IEnumerable<InnerInsurance>> GetInsurancByInsuranceProvidereAsync(string search = null);

        Task<InnerInsurance> GetInsuranceByIdAsync(int id);

        //Task<IEnumerable<InnerInsurance>> GetInsuranceByPatientNameAsync(string search = null);

        Task<bool> InsuranceExistAsync(int id);

        Task<InnerInsurance> AddInsuranceAsync(InnerInsurance insurance);

        Task RemoveInsuranceAsync(int id);

        Task Changed(InnerInsurance insurance);
    }
}
