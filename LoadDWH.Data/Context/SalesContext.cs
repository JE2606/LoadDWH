
using LoadDWH.Data.Entities.DwNorthwind;
using LoadDWH.Data.Entities.DwVentas;
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
        public DbSet<DimShippers> DimShippers { get; set; }
        public DbSet<FactVentas> FactVentas { get; set; }
        public DbSet<FactServedCustomer> FactServedCustomer { get; set; }
        #endregion

    }
}
