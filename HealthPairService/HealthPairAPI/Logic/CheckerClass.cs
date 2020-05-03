using HealthPairAPI.TransferModels;
using System;
using HealthPairDataAccess.Repositories;
using HealthPairDomain.Interfaces;

namespace HealthPairAPI.Logic
{
    public class CheckerClass
    {
        private IPatientRepository _pRepo;

        public CheckerClass(IPatientRepository patientRepo)
        {
            _pRepo = patientRepo;
        }
        public void Check(Transfer_Appointment appointment)
        {
            if (_pRepo.PatientExistAsync(appointment.PatientId).Result)
            {
                throw new Exception();
            }
        }
    }
}