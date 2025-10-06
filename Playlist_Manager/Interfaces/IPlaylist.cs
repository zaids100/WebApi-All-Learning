using Playlist_Manager.Models;

namespace Playlist_Manager.Interfaces
{
    public interface IPlaylist
    {
        Task<Playlist> CreatePlaylistAsync(Playlist playlist);
        Task<Playlist?> GetPlaylistByIdAsync(int id);
        Task<IEnumerable<Playlist>> GetAllPlaylistsAsync();
        Task<bool> UpdatePlaylistAsync(Playlist playlist);
        Task<bool> DeletePlaylistAsync(int id);
        Task<IEnumerable<Playlist>> GetPlaylistsByUserIdAsync(int userId);
    }
}
