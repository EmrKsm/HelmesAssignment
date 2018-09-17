using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelmesAssignment.Data.Context;
using HelmesAssignment.Entites.Sectors;

namespace HelmesAssignment.Data.Respositories.Sector
{
    public class SectorsRepository : ISectorsRepository<SectorsTable,HelmesAssignment.Entites.Sectors.Sector>
    {
        private readonly HelmesDbContext _dbContext = null;

        public SectorsRepository(HelmesDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException("context");
            _dbContext.Database.Connection.Open();
        }

        public void Dispose()
        {
            if (_dbContext.Database.Connection.State == System.Data.ConnectionState.Open)
                _dbContext.Database.Connection.Close();
            if (_dbContext != null)
                _dbContext.Dispose();
        }

        public async Task<List<Entites.Sectors.Sector>> FillSectorList()
        {
            List<Entites.Sectors.Sector> sectorList = await _dbContext.Sectors
                                                                    .Select(x => new Entites.Sectors.Sector
                                                                    {
                                                                        SectorId = x.SectorId,
                                                                        SectorName = x.SectorName,
                                                                        ParentId = x.SectorParent
                                                                    })
                                                                    .ToListAsync();

            foreach (var sector in sectorList)
                FillSectorChildren(sector, sectorList);

            sectorList.RemoveAll(x => x.ParentId != 0);

            return sectorList;
        }

        private void FillSectorChildren(Entites.Sectors.Sector sector, List<Entites.Sectors.Sector> sectorList)
        {
            sector.ChildSectors = sectorList.Where(s => s.ParentId == sector.SectorId && s.SectorName != sector.SectorName)
                                            .ToList();
            if (sector.ChildSectors.Count > 0)
                foreach (var child in sector.ChildSectors)
                    FillSectorChildren(child, sectorList);

        }

        public async Task<List<SectorsTable>> GetSectorsAsync() => await _dbContext.Sectors.ToListAsync();

        public async Task<bool> InsertSectorsAsync(List<SectorsTable> sectorList)
        {
            using (var insertSectorDbTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var sector in sectorList)
                    {
                        sector.SectorName = sector.SectorName.Trim();
                        if (_dbContext.Sectors.Any(x => x.SectorId == sector.SectorId) == false) _dbContext.Entry(sector).State = EntityState.Added;
                    }
                    await _dbContext.SaveChangesAsync();
                    insertSectorDbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    insertSectorDbTransaction.Rollback();
                    insertSectorDbTransaction.Dispose();
                    throw ex;
                }
            }
        }
    }
}
