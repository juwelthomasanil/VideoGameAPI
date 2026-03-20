using Microsoft.EntityFrameworkCore;
using VideoGameAPI.Data;
using VideoGameAPI.Services;
using Xunit;

namespace VideoGameAPI.Tests
{
    public class VideoGameServiceTests
    {
        // This helper method creates a fresh in-memory database for each test.
        // Each test gets its own isolated DB so they don't interfere with each other.
        private VideoGameDbContext CreateFreshDatabase(string dbName)
        {
            var options = new DbContextOptionsBuilder<VideoGameDbContext>()
                .UseInMemoryDatabase(databaseName: dbName) // Not SQL Server — lives in RAM only
                .Options;
            return new VideoGameDbContext(options);
        }

        // ─────────────────────────────────────────────────────────────────────
        // TEST: GetAllAsync returns all games
        // ─────────────────────────────────────────────────────────────────────
        [Fact] // [Fact] marks this method as a test that xUnit will discover and run
        public async Task GetAllAsync_WhenGamesExist_ReturnsAllGames()
        {
            // ARRANGE — set up the world before the test runs
            using var context = CreateFreshDatabase("GetAll_Test");
            context.VideoGames.AddRange(
                new VideoGame { Id = 1, Title = "Spiderman", Platform = "PS5", Developer = "Insomniac", Publisher = "Sony" },
                new VideoGame { Id = 2, Title = "Halo", Platform = "Xbox", Developer = "Bungie", Publisher = "Microsoft" }
            );
            await context.SaveChangesAsync();
            var service = new VideoGameService(context);

            // ACT — call the thing we're testing
            var result = await service.GetAllAsync();

            // ASSERT — verify the outcome
            Assert.Equal(2, result.Count); // We added 2 games, we should get 2 back
        }

        // ─────────────────────────────────────────────────────────────────────
        // TEST: GetAllAsync on an empty DB returns an empty list (not null, not an error)
        // ─────────────────────────────────────────────────────────────────────
        [Fact]
        public async Task GetAllAsync_WhenNoGamesExist_ReturnsEmptyList()
        {
            using var context = CreateFreshDatabase("GetAll_Empty_Test");
            var service = new VideoGameService(context);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);     // Should return a list object, not null
            Assert.Empty(result);       // The list should have zero items
        }

        // ─────────────────────────────────────────────────────────────────────
        // TEST: GetByIdAsync finds the correct game
        // ─────────────────────────────────────────────────────────────────────
        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsCorrectGame()
        {
            using var context = CreateFreshDatabase("GetById_Valid_Test");
            context.VideoGames.Add(new VideoGame { Id = 1, Title = "Spiderman", Platform = "PS5" });
            await context.SaveChangesAsync();
            var service = new VideoGameService(context);

            var result = await service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Spiderman", result.Title); // Make sure it's the RIGHT game
        }

        // ─────────────────────────────────────────────────────────────────────
        // TEST: GetByIdAsync returns null for a non-existent ID (not an exception)
        // ─────────────────────────────────────────────────────────────────────
        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
        {
            using var context = CreateFreshDatabase("GetById_Invalid_Test");
            var service = new VideoGameService(context);

            var result = await service.GetByIdAsync(999); // ID 999 doesn't exist

            Assert.Null(result); // We expect null, not an exception
        }

        // ─────────────────────────────────────────────────────────────────────
        // TEST: CreateAsync saves the game and returns it with an assigned ID
        // ─────────────────────────────────────────────────────────────────────
        [Fact]
        public async Task CreateAsync_ValidGame_SavesAndReturnsGame()
        {
            using var context = CreateFreshDatabase("Create_Test");
            var service = new VideoGameService(context);
            var newGame = new VideoGame { Title = "God of War", Platform = "PS5", Developer = "Santa Monica", Publisher = "Sony" };

            var result = await service.CreateAsync(newGame);

            Assert.NotNull(result);
            Assert.True(result.Id > 0);              // EF Core should have assigned an ID
            Assert.Equal("God of War", result.Title); // The data should be preserved
            Assert.Equal(1, context.VideoGames.Count()); // Should be exactly 1 game in DB
        }

        // ─────────────────────────────────────────────────────────────────────
        // TEST: DeleteAsync removes the game and returns true
        // ─────────────────────────────────────────────────────────────────────
        [Fact]
        public async Task DeleteAsync_WithValidId_DeletesGameAndReturnsTrue()
        {
            using var context = CreateFreshDatabase("Delete_Valid_Test");
            context.VideoGames.Add(new VideoGame { Id = 1, Title = "Spiderman", Platform = "PS5" });
            await context.SaveChangesAsync();
            var service = new VideoGameService(context);

            var result = await service.DeleteAsync(1);

            Assert.True(result);                         // Method should return true (success)
            Assert.Equal(0, context.VideoGames.Count()); // Game should actually be gone from DB
        }

        // ─────────────────────────────────────────────────────────────────────
        // TEST: DeleteAsync returns false when the ID doesn't exist
        // ─────────────────────────────────────────────────────────────────────
        [Fact]
        public async Task DeleteAsync_WithInvalidId_ReturnsFalse()
        {
            using var context = CreateFreshDatabase("Delete_Invalid_Test");
            var service = new VideoGameService(context);

            var result = await service.DeleteAsync(999); // ID doesn't exist

            Assert.False(result); // Method should return false — nothing to delete
        }

        // ─────────────────────────────────────────────────────────────────────
        // TEST: UpdateAsync updates the correct fields
        // ─────────────────────────────────────────────────────────────────────
        [Fact]
        public async Task UpdateAsync_WithValidId_UpdatesAndReturnsGame()
        {
            using var context = CreateFreshDatabase("Update_Valid_Test");
            context.VideoGames.Add(new VideoGame { Id = 1, Title = "Old Title", Platform = "PS4" });
            await context.SaveChangesAsync();
            var service = new VideoGameService(context);

            var updatedData = new VideoGame { Title = "New Title", Platform = "PS5", Developer = "Dev", Publisher = "Pub" };
            var result = await service.UpdateAsync(1, updatedData);

            Assert.NotNull(result);
            Assert.Equal("New Title", result.Title);  // Title should be updated
            Assert.Equal("PS5", result.Platform);     // Platform should be updated
        }
    }
}s