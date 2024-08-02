using Microsoft.EntityFrameworkCore;
using System.Xml;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public DbSet<MyEntity> MyEntities { get; set; } // Example DbSet

    // Configure entity mappings here (optional)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MyEntity>().HasData(
            new MyEntity { Id = 1, Name = "Sample Data" }
        );
    }


}
