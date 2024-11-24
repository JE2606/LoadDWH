 #nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoadDWH.Models.Models;

public partial class NorthwindContext : DbContext
{
    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<VwservedCustomer> VwservedCustomers { get; set; }

    public virtual DbSet<Vwventa> Vwventas { get; set; }

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}