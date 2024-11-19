using LoadDWH.Data.Entities.Northwind;
using Microsoft.EntityFrameworkCore;


namespace LoadDWH.Data.Context
{
    public partial class NorwindContext : DbContext
    {
        public NorwindContext(DbContextOptions<NorwindContext> options) : base(options)
        { 

        }

        #region"Db Sets"
        
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        
        #endregion

    }
}
