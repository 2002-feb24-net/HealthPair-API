using System;
using HealthPairAPI.TransferModels;
using Moq;
using Xunit;

namespace HealthPairAPI.Tests.ApiTransferModel
{
    public class API_PatientTest
    {
        private MockRepository mockRepo;


        public API_PatientTest()
        {
            this.mockRepo = new MockRepository(MockBehavior.Strict);

        }

        private Transfer_Patient MakePatient()
        {
            return new Transfer_Patient();
        }

        [Fact]
        public void FirstTestMethod()
        {
            var transPatient = this.MakePatient();
            Assert.True(true);
        }
    }
}