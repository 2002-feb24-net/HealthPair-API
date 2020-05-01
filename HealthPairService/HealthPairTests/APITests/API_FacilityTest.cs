using System;
using System.Collections.Generic;
using System.Text;
using HealthPairAPI.TransferModels;
using Moq;
using Xunit;

namespace HealthPairAPI.Tests.ApiTransferModel
{
    public class API_FacilityTest
    {
        private MockRepository mockRepo;

        public API_FacilityTest()
        {
            this.mockRepo = new MockRepository(MockBehavior.Strict);
        }

        private Transfer_Facility MakeFacility()
        {
            return new Transfer_Facility();
        }

        [Fact]
        public void FirstTest()
        {
            var transFacility = new Transfer_Facility();
            transFacility.FacilityId = 1;
            transFacility.FacilityName = "Fake Name";
            transFacility.FacilityAddress1 = "Fake Address";
            transFacility.FacilityCity = "Fake City";
            transFacility.FacilityPhoneNumber = 5552555555;
            transFacility.FacilityZipcode = 89345;
            transFacility.FacilityState = "Fake State";
            Assert.True(true);


        }
    }
}
