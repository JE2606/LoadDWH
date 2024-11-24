 #nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoadDWH.Models.DwVentas.ModelsVentas;

public partial class DwVentasContext : DbContext
{
    public DwVentasContext(DbContextOptions<DwVentasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FactServedCustomer> FactServedCustomers { get; set; }

    public virtual DbSet<FactVenta> FactVentas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FactServedCustomer>(entity =>
        {
            entity.HasKey(e => e.ServedCustomerId).HasName("PK__FactServ__1B2A595C6D2A76F3");

            entity.Property(e => e.EmployeeName).HasMaxLength(255);
        });

        modelBuilder.Entity<FactVenta>(entity =>
        {
            entity.HasKey(e => e.VentaId).HasName("PK__FactVent__5B4150ACDC8C274E");

            entity.Property(e => e.CompanyName).HasMaxLength(255);
            entity.Property(e => e.EmployeeName).HasMaxLength(255);
            entity.Property(e => e.ShipName).HasMaxLength(255);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}