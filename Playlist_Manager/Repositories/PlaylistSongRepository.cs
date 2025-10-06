using Microsoft.EntityFrameworkCore;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist_Manager.Repositories
{
    public class PlaylistSongRepository : IPlaylistSong
    {
        private readonly PlaylistManagerContext _context;

        public PlaylistSongRepository(PlaylistManagerContext context)
        {
            _context = context;
        }

        public async Task<PlaylistSong> AddSongToPlaylistAsync(PlaylistSong playlistSong)
        {
            var existing = await _context.PlaylistSongs
                .FirstOrDefaultAsync(ps => ps.PlaylistId == playlistSong.PlaylistId && ps.SongId == playlistSong.SongId);

            if (existing != null)
                throw new System.Exception("Song already exists in the playlist.");

            await _context.PlaylistSongs.AddAsync(playlistSong);
            await _context.SaveChangesAsync();
            return playlistSong;
        }

        public async Task<IEnumerable<PlaylistSong>> GetSongsByPlaylistIdAsync(int playlistId)
        {
            return await _context.PlaylistSongs
                .Include(ps => ps.Song)
                .Include(ps => ps.Playlist)
                .Where(ps => ps.PlaylistId == playlistId)
                .ToListAsync();
        }

        public async Task<bool> RemoveSongFromPlaylistAsync(int playlistId, int songId)
        {
            var record = await _context.PlaylistSongs
                .FirstOrDefaultAsync(ps => ps.PlaylistId == playlistId && ps.SongId == songId);

            if (record == null)
                return false;

            _context.PlaylistSongs.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
