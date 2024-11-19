
using LoadDWH.Data.Entities.DwNorthwind;
using Microsoft.EntityFrameworkCore;

namespace LoadDWH.Data.Context
{
    public class SalesContext : DbContext
    {
        
        public SalesContext(DbContextOptions<SalesContext> options) : base(options) {}

        #region "Db Sets"
        public DbSet<DimCustomers> DimCustomers { get; set; }
        public DbSet<DimEmployees> DimEmployees { get; set; }
        public DbSet<DimProducts> DimProducts { get; set; }
        public DbSet<DimShippers> DimShippers { get; set; } //Creacion
        #endregion

    }
}
