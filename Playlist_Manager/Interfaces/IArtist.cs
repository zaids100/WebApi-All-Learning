using Playlist_Manager.Models;

namespace Playlist_Manager.Interfaces
{
    public interface IArtist
    {
        Task<Artist> CreateArtistAsync(Artist artist);
        Task<Artist?> GetArtistByIdAsync(int id);
        Task<IEnumerable<Artist>> GetAllArtistsAsync();
        Task<bool> UpdateArtistAsync(Artist artist);
        Task<bool> DeleteArtistAsync(int id);
    }
}
