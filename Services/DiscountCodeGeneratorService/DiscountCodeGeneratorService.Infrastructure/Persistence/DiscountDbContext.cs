using DiscountCodeGeneratorService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscountCodeGeneratorService.Infrastructure.Persistence;

public class DiscountDbContext : DbContext
{
    public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
    {
    }

    public DbSet<DiscountCode> DiscountCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DiscountCode>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);
            entity.OwnsOne(d => d.UsageInfo);
        });
    }
}
