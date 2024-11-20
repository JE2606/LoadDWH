using LoadDWH.Data.Entities.Northwind;
using Microsoft.EntityFrameworkCore;

namespace LoadDWH.Data.Context
{
    public partial class NorwindContext : DbContext
    {
        public NorwindContext(DbContextOptions<NorwindContext> options) : base(options)
        {

        }

        #region "Db Sets"
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        public DbSet<Vwventa> Vwventa { get; set; }
        public DbSet<VwservedCustomer> VwservedCustomer { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VwservedCustomer>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VWServedCustomers");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(31);
            });

            modelBuilder.Entity<Vwventa>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VWVentas");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.CustomerKey)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsFixedLength();
                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(31);
                entity.Property(e => e.OrderDate).HasColumnType("datetime");
                entity.Property(e => e.ShipName).HasMaxLength(40);
                entity.Property(e => e.Total).HasColumnType("money");
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
