﻿using HealthPairDomain.InnerModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    public interface IFacilityRepository
    {
        Task<IEnumerable<InnerFacility>> GetFacilityAsync(string search = null);

        Task<InnerFacility> GetFacilityByIdAsync(int id);

        //Task<IEnumerable<InnerFacility>> GetFacilityByProviderNameAsync(string search = null);

        Task<bool> FacilityExistAsync(int id);

        Task<InnerFacility> AddFacilityAsync(InnerFacility facility);

        Task RemoveFacilityAsync(int id);

        Task ChangedAsync(InnerFacility facility);
    }
}
