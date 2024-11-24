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
        public DbSet<VwVenta> Vwventa { get; set; }
        public DbSet<VwservedCustomer> VwservedCustomer { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VwservedCustomer>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VWServedCustomers");

                                 entity.Property(e => e.EmployeeKey) 
                    .HasColumnName("EmployeeKey");

                entity.Property(e => e.EmployeeName)
                    .IsRequired()  
                    .HasMaxLength(31);

                entity.Property(e => e.TotalCustomersServed)
                    .HasColumnName("TotalCustomersServed");

                entity.Property(e => e.DataKey)
                    .HasColumnName("DataKey"); 
            });


            modelBuilder.Entity<VwVenta>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VWVentas");

                                 entity.Property(e => e.CustomerKey)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsFixedLength();
                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(40);

                                 entity.Property(e => e.EmployeeID)
                    .IsRequired();
                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(31);

                                 entity.Property(e => e.ShipperID)
                    .IsRequired();
                entity.Property(e => e.ShipperName)
                    .IsRequired()
                    .HasMaxLength(40);

                                 entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(15);

                                 entity.Property(e => e.ProductID)
                    .IsRequired();
                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                                 entity.Property(e => e.DateKey).IsRequired();
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.Month).IsRequired();

                                 entity.Property(e => e.TotalVentas).HasColumnType("money");
                entity.Property(e => e.Cantidad).HasColumnType("int");
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
