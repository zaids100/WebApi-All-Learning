using Playlist_Manager.Models;

namespace Playlist_Manager.Interfaces
{
    public interface ISongArtist
    {
        Task<IEnumerable<SongArtist>> GetAllAsync();
        Task<IEnumerable<SongArtist>> GetBySongIdAsync(int songId);
        Task<IEnumerable<SongArtist>> GetByArtistIdAsync(int artistId);
        Task<bool> AddAsync(SongArtist songArtist);
        Task<bool> DeleteAsync(int songId, int artistId);
    }
}
