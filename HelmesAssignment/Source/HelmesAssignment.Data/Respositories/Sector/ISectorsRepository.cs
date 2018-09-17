using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmesAssignment.Data.Respositories.Sector
{
    public interface ISectorsRepository<T,U> : IDisposable
        where T : class
        where U : class
    {
        Task<bool> InsertSectorsAsync(List<T> sectorList);
        Task<List<T>> GetSectorsAsync();
        Task<List<U>> FillSectorList();
    }
}
