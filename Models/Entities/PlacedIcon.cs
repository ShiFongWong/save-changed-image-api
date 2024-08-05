using Microsoft.EntityFrameworkCore;

namespace save_changed_image_api.Models.Entities
{
    public class PlacedIcon
    {
        public int Id { get; set; }  // Auto-incrementing primary key
        public string? SessionId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Size { get; set; }
        public string ImgSrc { get; set; }
    }

    public class IconsDbContext : DbContext
    {
        public DbSet<PlacedIcon> PlacedIcons { get; set; }

        public IconsDbContext(DbContextOptions<IconsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlacedIcon>().ToTable("PlacedIcons");
        }
    }
}