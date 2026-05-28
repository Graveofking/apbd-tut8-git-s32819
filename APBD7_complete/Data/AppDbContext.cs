using Microsoft.EntityFrameworkCore;
using APBD7.Models;

namespace APBD7.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PC> PCs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PC>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.Property(e => e.Description)
                .HasColumnType("TEXT");

            entity.HasOne(e => e.ComponentManufacturer)
                .WithMany(m => m.Components)
                .HasForeignKey(e => e.ComponentManufacturersId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ComponentType)
                .WithMany(t => t.Components)
                .HasForeignKey(e => e.ComponentTypesId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.Property(e => e.FoundationDate)
                .HasColumnType("date");
        });

        modelBuilder.Entity<PCComponent>(entity =>
        {
            entity.HasKey(e => new { e.PCId, e.ComponentCode });

            entity.HasOne(e => e.PC)
                .WithMany(p => p.PCComponents)
                .HasForeignKey(e => e.PCId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Component)
                .WithMany(c => c.PCComponents)
                .HasForeignKey(e => e.ComponentCode)
                .OnDelete(DeleteBehavior.Restrict);
        });
        

        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer
            {
                Id = 1,
                Abbreviation = "INT",
                FullName = "Intel Corporation",
                FoundationDate = new DateTime(1968, 7, 18)
            },
            new ComponentManufacturer
            {
                Id = 2,
                Abbreviation = "AMD",
                FullName = "Advanced Micro Devices, Inc.",
                FoundationDate = new DateTime(1969, 5, 1)
            },
            new ComponentManufacturer
            {
                Id = 3,
                Abbreviation = "NVD",
                FullName = "NVIDIA Corporation",
                FoundationDate = new DateTime(1993, 4, 4)
            }
        );

        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Central Processing Unit" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Processing Unit" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Random Access Memory" }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Code = "I9-13900K",
                Name = "Intel Core i9-13900K",
                Description = "24-core desktop processor",
                ComponentManufacturersId = 1,
                ComponentTypesId = 1
            },
            new Component
            {
                Code = "RX-7900XT",
                Name = "AMD Radeon RX 7900 XT",
                Description = "20GB GDDR6 graphics card",
                ComponentManufacturersId = 2,
                ComponentTypesId = 2
            },
            new Component
            {
                Code = "RTX-4090",
                Name = "NVIDIA GeForce RTX 4090",
                Description = "24GB GDDR6X graphics card",
                ComponentManufacturersId = 3,
                ComponentTypesId = 2
            }
        );

        modelBuilder.Entity<PC>().HasData(
            new PC
            {
                Id = 1,
                Name = "Gaming Beast X",
                Weight = 12.5,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
                Stock = 5
            },
            new PC
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
                Stock = 12
            },
            new PC
            {
                Id = 3,
                Name = "Workstation Ultra",
                Weight = 18.0,
                Warranty = 48,
                CreatedAt = new DateTime(2026, 3, 1, 8, 0, 0),
                Stock = 3
            }
        );

        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent { PCId = 1, ComponentCode = "I9-13900K", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "RTX-4090",  Amount = 1 },
            new PCComponent { PCId = 2, ComponentCode = "RX-7900XT", Amount = 1 },
            new PCComponent { PCId = 3, ComponentCode = "I9-13900K", Amount = 2 },
            new PCComponent { PCId = 3, ComponentCode = "RTX-4090",  Amount = 2 }
        );
    }
}
