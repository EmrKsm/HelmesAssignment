using HelmesAssignment.Entites.Input;
using HelmesAssignment.Entites.Sectors;
using System;
using System.Data.Entity;

namespace HelmesAssignment.Data.Context
{
    public class HelmesDbContext : DbContext
    {
        public HelmesDbContext (string connectionString) : base(connectionString)
        {
            if (connectionString == string.Empty || connectionString == null) throw new ArgumentNullException("connectionString");
        }

        public virtual DbSet<SectorsTable> Sectors { get; set; }
        public virtual DbSet<UserInputTable> UserInput { get; set; }
    }
}
