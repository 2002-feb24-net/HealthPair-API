using HealthPairAPI.TransferModels;
using System;
using HealthPairDataAccess.Repositories;
using HealthPairDomain.Interfaces;

namespace HealthPairAPI.Logic
{
    public class CheckerClass
    {
        private IPatientRepository _pRepo;
        private IProviderRepository _proRepo;

        public CheckerClass(IPatientRepository patientRepo, IProviderRepository provRepo)
        {
            _pRepo = patientRepo;
            _proRepo = provRepo;
        }
        public void Check(Transfer_Appointment appointment)
        {
            if (_pRepo.PatientExistAsync(appointment.PatientId).Result)
            {
                throw new Exception();
            }
            if (_proRepo.ProviderExistAsync(appointment.ProviderId).Result)
            {
                throw new Exception();
            }
        }
    }
}