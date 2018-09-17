
using HelmesAssignment.Api.Initializers;
using HelmesAssignment.Data.Context;
using HelmesAssignment.Data.Respositories.Sector;
using HelmesAssignment.Data.Tests.DB;
using HelmesAssignment.Entites.Sectors;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HelmesAssignment.Data.Tests.Sector
{
    [TestFixture]
    public class Mock
    {
        Mock<DbSet<SectorsTable>> mockSectorSet;
        Mock<HelmesDbContext> mockContext;

        SectorsRepository sectorRepo;

        [SetUp]
        public void SetUp()
        {
            var sectors = new List<SectorsTable>{
            new SectorsTable
            {
                Id = 1,
                SectorId = 11,
                SectorName = "Cars",
                SectorParent = 0
            },
            new SectorsTable
            {
                Id = 2,
                SectorId = 12,
                SectorName = "Mercedes",
                SectorParent = 11
            },
            new SectorsTable
            {
                Id = 3,
                SectorId = 13,
                SectorName = "Volvo",
                SectorParent = 11
            },
            new SectorsTable
            {
                Id = 4,
                SectorId = 14,
                SectorName = "Electronics",
                SectorParent = 0
            },
            new SectorsTable
            {
                Id = 5,
                SectorId = 15,
                SectorName = "Apple",
                SectorParent = 14
            }   
            }.AsQueryable();
            mockSectorSet = new Mock<DbSet<SectorsTable>>();
            mockSectorSet.As<IQueryable<SectorsTable>>().Setup(m => m.Provider).Returns(sectors.Provider);
            mockSectorSet.As<IQueryable<SectorsTable>>().Setup(m => m.Expression).Returns(sectors.Expression);
            mockSectorSet.As<IQueryable<SectorsTable>>().Setup(m => m.ElementType).Returns(sectors.ElementType);
            mockSectorSet.As<IQueryable<SectorsTable>>().Setup(m => m.GetEnumerator()).Returns(sectors.GetEnumerator());


            mockContext = new Mock<HelmesDbContext>(MockBehavior.Default, "connectionString");
            mockContext.Setup(c => c.Sectors).Returns(mockSectorSet.Object);
        }

        [Test]
        public async Task Sector_GetSectorsAsync_ShouldReturnSectorList()
        {
            // Arrange
            sectorRepo = new SectorsRepository(mockContext.Object);

            // Act
            var result = await sectorRepo.GetSectorsAsync();

            // Assert
            Assert.AreNotEqual(result.Count, 0);
        }
    }
}
