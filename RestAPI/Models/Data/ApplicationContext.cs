using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RestAPI.Models.Data;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<VendingMachine> VendingMachines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry);

            entity.ToTable("Country");

            entity.Property(e => e.IdCountry).HasColumnName("idCountry");
            entity.Property(e => e.Country1)
                .HasMaxLength(50)
                .HasColumnName("Country");
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => e.IdProduct);

            entity.ToTable("goods");

            entity.HasIndex(e => e.ProductName, "IX_goods").IsUnique();

            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.MinimumStock).HasColumnName("minimumStock");
            entity.Property(e => e.Price)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("productName");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Sales)
                .HasMaxLength(50)
                .HasColumnName("sales");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.IdManufacturer);

            entity.ToTable("manufacturer");

            entity.HasIndex(e => e.ManufacturersName, "IX_manufacturer").IsUnique();

            entity.Property(e => e.IdManufacturer).HasColumnName("idManufacturer");
            entity.Property(e => e.ManufacturersName)
                .HasMaxLength(50)
                .HasColumnName("manufacturersName");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.IdPerson);

            entity.HasIndex(e => e.Email, "IX_Persons").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "IX_Persons_1").IsUnique();

            entity.Property(e => e.IdPerson).HasColumnName("idPerson");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.IdSales);

            entity.ToTable("sales");

            entity.Property(e => e.IdSales).HasColumnName("idSales");
            entity.Property(e => e.DateTimeSale)
                .HasColumnType("datetime")
                .HasColumnName("dateTimeSale");
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.IdVm).HasColumnName("idVM");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("paymentMethod");
            entity.Property(e => e.SoldProduct).HasColumnName("soldProduct");
            entity.Property(e => e.SummaSale)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("summaSale");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.SalesNavigation)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sales_goods");

            entity.HasOne(d => d.IdVmNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdVm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sales_VendingMachine");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdService);

            entity.ToTable("service");

            entity.Property(e => e.IdService).HasColumnName("idService");
            entity.Property(e => e.DateService).HasColumnName("dateService");
            entity.Property(e => e.DescriptionService)
                .HasMaxLength(50)
                .HasColumnName("descriptionService");
            entity.Property(e => e.IdPerson).HasColumnName("idPerson");
            entity.Property(e => e.IdVm).HasColumnName("idVM");
            entity.Property(e => e.Problem)
                .HasMaxLength(50)
                .HasColumnName("problem");

            entity.HasOne(d => d.IdPersonNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.IdPerson)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_service_Persons");

            entity.HasOne(d => d.IdVmNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.IdVm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_service_VendingMachine");
        });

        modelBuilder.Entity<VendingMachine>(entity =>
        {
            entity.HasKey(e => e.IdVm);

            entity.ToTable("VendingMachine");

            entity.HasIndex(e => e.Number, "IX_VendingMachine").IsUnique();

            entity.HasIndex(e => e.InventoryNumber, "IX_VendingMachine_1").IsUnique();

            entity.Property(e => e.IdVm).HasColumnName("idVM");
            entity.Property(e => e.Adres)
                .HasMaxLength(100)
                .HasColumnName("adres");
            entity.Property(e => e.DataLastExamination).HasColumnName("dataLastExamination");
            entity.Property(e => e.DateInventory).HasColumnName("dateInventory");
            entity.Property(e => e.DateNextExamination).HasColumnName("dateNextExamination");
            entity.Property(e => e.DateOfManufacture).HasColumnName("dateOfManufacture");
            entity.Property(e => e.DateOfOperation).HasColumnName("dateOfOperation");
            entity.Property(e => e.IdCountry).HasColumnName("idCountry");
            entity.Property(e => e.IdManufacturer).HasColumnName("idManufacturer");
            entity.Property(e => e.IdPerson).HasColumnName("idPerson");
            entity.Property(e => e.IntertestInterval).HasColumnName("intertestInterval");
            entity.Property(e => e.InventoryNumber)
                .HasMaxLength(50)
                .HasColumnName("inventoryNumber");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .HasColumnName("number");
            entity.Property(e => e.ResoursTa).HasColumnName("resoursTA");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Summa)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("summa");
            entity.Property(e => e.TimeExamination).HasColumnName("timeExamination");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.IdCountry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachine_Country");

            entity.HasOne(d => d.IdManufacturerNavigation).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.IdManufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachine_manufacturer");

            entity.HasOne(d => d.IdPersonNavigation).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.IdPerson)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachine_Persons");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
