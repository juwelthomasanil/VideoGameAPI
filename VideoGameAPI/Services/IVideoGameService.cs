namespace VideoGameAPI.Services
{
    public interface IVideoGameService
    {
        Task<List<VideoGame>> GetAllAsync();
        Task<VideoGame?> GetByIdAsync(int id);
        Task<VideoGame> AddAsync(VideoGame newGame);
        Task<VideoGame?> UpdateAsync(int id, VideoGame updatedGame);
        Task<bool> DeleteAsync(int id);
    }
}
