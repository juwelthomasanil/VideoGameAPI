using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using VideoGameAPI.Controllers;
using VideoGameAPI.Data;

namespace VideoGameAPI.Tests
{
    public class VideoGameDbContextTests
    {
        [Fact]
        public void DbContext_Should_Have_VideoGames_DbSet()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VideoGameDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new VideoGameDbContext(options);

            // Act
            var videoGames = context.VideoGames;

            // Assert
            videoGames.Should().NotBeNull();
        }

        [Fact]
        public void DbContext_Should_Seed_Data_OnModelCreating()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VideoGameDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new VideoGameDbContext(options);

            // Act
            context.Database.EnsureCreated(); // This triggers OnModelCreating and seeding

            // Assert
            var videoGames = context.VideoGames.ToList();
            videoGames.Should().NotBeEmpty();
            videoGames.Should().HaveCount(3);
            videoGames.Should().Contain(v => v.Title == "Spiderman" && v.Platform == "PS5");
            videoGames.Should().Contain(v => v.Title == "Knight War" && v.Platform == "Xbox");
            videoGames.Should().Contain(v => v.Title == "Clash of Clans" && v.Platform == "Mobile");
        }
    }
}