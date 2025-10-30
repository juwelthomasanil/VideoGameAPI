using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameAPI.Data;

namespace VideoGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly VideoGameDbContext _dbContext;

        public VideoGameController(VideoGameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
        {
            return Ok(await _dbContext.VideoGames.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGamebyId(int id)
        {
            var game = await _dbContext.VideoGames.FindAsync(id);
            if (game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<VideoGame>> AddVideoGame(VideoGame newgame)
        {
            if (newgame is null)
                return BadRequest();

            _dbContext.VideoGames.Add(newgame);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVideoGamebyId), new { id = newgame.Id }, newgame);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideoGame(int id, VideoGame Updatedgame)
        {
            var game = await _dbContext.VideoGames.FindAsync(id);
            if (game is null)
                return NotFound();

            game.Title = Updatedgame.Title;
            game.Platform = Updatedgame.Platform;
            game.Developer = Updatedgame.Developer;
            game.Publisher = Updatedgame.Publisher;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdateVideoGame(int id)
        {
            var game = await _dbContext.VideoGames.FindAsync(id);
            if (game is null)
                return NotFound();

            _dbContext.Remove(game);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
