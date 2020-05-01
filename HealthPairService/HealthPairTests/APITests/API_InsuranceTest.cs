using System;
using System.Collections.Generic;
using System.Text;
using HealthPairAPI.TransferModels;
using Moq;
using Xunit;

namespace HealthPairAPI.Tests.ApiTransferModel
{
    public class API_InsuranceTest
    {
        private MockRepository mockRepo;

        public API_InsuranceTest()
        {
            this.mockRepo = new MockRepository(MockBehavior.Strict);
        }

        private Transfer_Insurance MakeInsurance()
        {
            return new Transfer_Insurance();
        }

        [Fact]
        public void FirstTestMethod()
        {
            var transInsurance = this.MakeInsurance();
            Assert.True(true);
        }
    }
}
