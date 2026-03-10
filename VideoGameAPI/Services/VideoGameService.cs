using VideoGameAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace VideoGameAPI.Services
{
    public class VideoGameService : IVideoGameService
    {
        // We receive the DbContext through Dependency Injection
        // (the same pattern you already use in your controller)
        private readonly VideoGameDbContext _context;

        public VideoGameService(VideoGameDbContext context)
        {
            _context = context;
        }

        public async Task<List<VideoGame>> GetAllAsync()
        {
            return await _context.VideoGames.ToListAsync();
        }

        public async Task<VideoGame?> GetByIdAsync(int id)
        {
            // FindAsync returns null if not found — the ? on VideoGame? means "nullable"
            return await _context.VideoGames.FindAsync(id);
        }

        public async Task<VideoGame> CreateAsync(VideoGame game)
        {
            _context.VideoGames.Add(game);
            await _context.SaveChangesAsync();
            return game; // EF Core fills in the Id after saving
        }

        public async Task<VideoGame> AddAsync(VideoGame newGame)
        {
            _context.VideoGames.Add(newGame);
            await _context.SaveChangesAsync();
            return newGame;
        }

        public async Task<VideoGame?> UpdateAsync(int id, VideoGame updatedGame)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null) return null; // Return null = "not found"

            // Copy the new values onto the existing tracked entity
            game.Title = updatedGame.Title;
            game.Platform = updatedGame.Platform;
            game.Developer = updatedGame.Developer;
            game.Publisher = updatedGame.Publisher;

            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null) return false; // Return false = "nothing was deleted"

            _context.VideoGames.Remove(game);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}