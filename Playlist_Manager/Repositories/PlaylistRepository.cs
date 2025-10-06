using Microsoft.EntityFrameworkCore;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;

namespace Playlist_Manager.Repositories
{
    public class PlaylistRepository : IPlaylist
    {
        private readonly PlaylistManagerContext _context;

        public PlaylistRepository(PlaylistManagerContext context)
        {
            _context = context;
        }

        // Create new playlist
        public async Task<Playlist> CreatePlaylistAsync(Playlist playlist)
        {
            await _context.Playlists.AddAsync(playlist);
            await _context.SaveChangesAsync();
            return playlist;
        }

        // Get playlist by ID
        public async Task<Playlist?> GetPlaylistByIdAsync(int id)
        {
            return await _context.Playlists
                .Include(p => p.PlaylistSongs)
                .ThenInclude(ps => ps.Song)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Get all playlists
        public async Task<IEnumerable<Playlist>> GetAllPlaylistsAsync()
        {
            return await _context.Playlists
                .Include(p => p.PlaylistSongs)
                .ThenInclude(ps => ps.Song)
                .ToListAsync();
        }

        // Update playlist
        public async Task<bool> UpdatePlaylistAsync(Playlist playlist)
        {
            var existing = await _context.Playlists.FindAsync(playlist.Id);
            if (existing == null) return false;

            existing.Name = playlist.Name ?? existing.Name;
            existing.Description = playlist.Description ?? existing.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete playlist
        public async Task<bool> DeletePlaylistAsync(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null) return false;

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get all playlists by user
        public async Task<IEnumerable<Playlist>> GetPlaylistsByUserIdAsync(int userId)
        {
            return await _context.Playlists
                .Where(p => p.UserId == userId)
                .Include(p => p.PlaylistSongs)
                .ThenInclude(ps => ps.Song)
                .ToListAsync();
        }
    }
}
