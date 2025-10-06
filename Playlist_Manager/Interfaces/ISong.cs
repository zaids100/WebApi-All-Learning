using Playlist_Manager.Models;

namespace Playlist_Manager.Interfaces
{
    public interface ISong
    {
        Task<Song> CreateSongAsync(Song song);
        Task<Song?> GetSongByIdAsync(int id);
        Task<IEnumerable<Song>> GetAllSongsAsync();
        Task<bool> UpdateSongAsync(Song song);
        Task<bool> DeleteSongAsync(int id);
        Task<IEnumerable<Song>> GetSongsByArtistAsync(string artist);
    }
}
