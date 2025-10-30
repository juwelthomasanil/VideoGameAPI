using Microsoft.EntityFrameworkCore;
using VideoGameAPI.Controllers;

namespace VideoGameAPI.Data
{
    public class VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : DbContext(options)
    {        
        public DbSet<VideoGame> VideoGames => Set<VideoGame>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGame>().HasData(
                new VideoGame
                {
                    Id = 1,
                    Title = "Spiderman",
                    Platform = "PS5",
                    Developer = "Insomania",
                    Publisher = "Sony"
                },
                new VideoGame
                {
                    Id = 2,
                    Title = "Knight War",
                    Platform = "Xbox",
                    Developer = "T2T",
                    Publisher = "Microsoft"
                },
                new VideoGame
                {
                    Id = 3,
                    Title = "Clash of Clans",
                    Platform = "Mobile",
                    Developer = "RedToken",
                    Publisher = "Bonarat"
                }
            );
        }
    }
}
