using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelmesAssignment.Data.Context;
using HelmesAssignment.Entites;
using HelmesAssignment.Entites.Input;

namespace HelmesAssignment.Data.Respositories.UserInput
{
    public class UserInputRepository : IUserInputRepository<UserInputTable>
    {
        private readonly HelmesDbContext _dbContext = null;

        public UserInputRepository(HelmesDbContext context)
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

        public async Task<bool> InsertUserInputAsync(UserInputTable input)
        {
            using (var insertUserInputDbTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (_dbContext.UserInput.Any(x => x.UserName == input.UserName) == false)
                        _dbContext.Entry(input).State = EntityState.Added;
                    else
                        _dbContext.Entry(input).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    insertUserInputDbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    insertUserInputDbTransaction.Rollback();
                    insertUserInputDbTransaction.Dispose();
                    throw ex;
                }
            }
        }
    }
}
