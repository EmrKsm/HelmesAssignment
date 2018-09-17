using HelmesAssignment.Data.Context;
using HelmesAssignment.Data.Respositories.Sector;
using HelmesAssignment.Entites.Sectors;
using HelmesAssignment.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmesAssignment.Data.Tests.Sector
{
    [TestFixture]
    public class SUT
    {
        ISectorsRepository<SectorsTable, HelmesAssignment.Entites.Sectors.Sector> sectorsRepo = null;

        [SetUp]
        public void SetUp()
        {
            sectorsRepo = new SectorsRepository(new HelmesDbContext(ConfigReader.GetAppSettingWithName("HelmesDbConnectionString")));
        }

        [Test]
        public async Task Sector_GetSectorsAsync_ShouldReturnSectorList()
        {
            //Arrange

            //Act
            var result = await sectorsRepo.GetSectorsAsync();

            //Assert
            Assert.AreNotEqual(result.Count, 0);
        }
    }
}
