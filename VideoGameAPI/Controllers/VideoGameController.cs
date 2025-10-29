using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames = new List<VideoGame>
        {
            new VideoGame
            {
                Id=1,
                Title="Spiderman",
                Platform="PS5",
                Developer="Insomania",
                Publisher="Sony"
            },
            new VideoGame
            {
                Id=2,
                Title="Knight War",
                Platform="Xbox",
                Developer="T2T",
                Publisher="Microsoft"
            },
            new VideoGame
            {
                Id=3,
                Title="Clash of Clans",
                Platform="Mobile",
                Developer="RedToken",
                Publisher="Bonarat"
            }
        };
        [HttpGet]
        public ActionResult<List<VideoGame>> GetVideoGames()
        {
            return Ok(videoGames);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<VideoGame> GetVideoGamebyId(int id)
        {
            var game = videoGames.FirstOrDefault(x => x.Id == id);
            if (game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpPost]
        public ActionResult<VideoGame> AddVideoGame(VideoGame newgame)
        {
            if (newgame is null)
                return BadRequest();

            newgame.Id = videoGames.Max(x => x.Id) + 1;
            videoGames.Add(newgame);
            return CreatedAtAction(nameof(GetVideoGamebyId), new { id = newgame.Id }, newgame);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideoGame(int id, VideoGame Updatedgame)
        {
            var game = videoGames.FirstOrDefault(x => x.Id == id);
            if (game is null)
                return NotFound();

            game.Title = Updatedgame.Title;
            game.Platform = Updatedgame.Platform;
            game.Developer = Updatedgame.Developer;
            game.Publisher = Updatedgame.Publisher;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult UpdateVideoGame(int id)
        {
            var game = videoGames.FirstOrDefault(x => x.Id == id);
            if (game is null)
                return NotFound();

            videoGames.Remove(game);

            return NoContent();
        }
    }
}
