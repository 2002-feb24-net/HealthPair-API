using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HealthPairDataAccess.DataModels;
using HealthPairDataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HealthPairDataAccess.Tests.Repositories
{
    public class DataFacilityTest
    {


        public DataFacilityTest()
        {

        }


        // [Fact]
        // public async Task GetFacility_ShouldReturnFacility_WhenFacilityExists()
        // {   //Arrange
        //     var Facility = new Data_Facility();

        //     var _ctx = new Mock<HealthPairContext>();
        //     var dbSetMock = new Mock<DbSet<Data_Facility>>();

        //     _ctx.Setup(x => x.Set<Data_Facility>()).Returns(dbSetMock.Object);

        //     //Act

        //     //Assert
        // }
    }
}