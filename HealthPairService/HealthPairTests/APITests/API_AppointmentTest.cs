using HealthPairAPI.TransferModels;
using Moq;
using System;
using Xunit;

namespace HealthPairAPI.Tests.ApiTransferModel
{
    public class API_AppointmentTest
    {
        private MockRepository mockRepo;
        public API_AppointmentTest()
        {
            this.mockRepo = new MockRepository(MockBehavior.Strict);
        }
        private Transfer_Appointment MakeAppointment()
        {
            return new Transfer_Appointment();
        }

        [Fact]

        public void FirstTest()
        {
            var transAppointment = this.MakeAppointment();
            Assert.True(true);
        }
    }
}
