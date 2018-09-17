using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmesAssignment.Data.Respositories.UserInput
{
    public interface IUserInputRepository<T> : IDisposable
        where T : class
    {
        Task<bool> InsertUserInputAsync(T input);
    }
}
