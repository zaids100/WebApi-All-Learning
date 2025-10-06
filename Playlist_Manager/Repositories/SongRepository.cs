using Microsoft.EntityFrameworkCore;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist_Manager.Repositories
{
    public class SongRepository : ISong
    {
        private readonly PlaylistManagerContext _context;

        public SongRepository(PlaylistManagerContext context)
        {
            _context = context;
        }

        public async Task<Song> CreateSongAsync(Song song)
        {
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();
            return song;
        }

        public async Task<Song?> GetSongByIdAsync(int id)
        {
            return await _context.Songs.FindAsync(id);
        }

        public async Task<IEnumerable<Song>> GetAllSongsAsync()
        {
            return await _context.Songs.ToListAsync();
        }

        public async Task<bool> UpdateSongAsync(Song song)
        {
            var existing = await _context.Songs.FindAsync(song.Id);
            if (existing == null) return false;

            existing.Title = song.Title ?? existing.Title;
            existing.Artist = song.Artist ?? existing.Artist;
            existing.Album = song.Album ?? existing.Album;
            existing.Genre = song.Genre ?? existing.Genre;
            existing.Duration = song.Duration != 0 ? song.Duration : existing.Duration;
            existing.ReleaseDate = song.ReleaseDate ?? existing.ReleaseDate;
            existing.SongLink = song.SongLink ?? existing.SongLink;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSongAsync(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null) return false;

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Song>> GetSongsByArtistAsync(string artist)
        {
            return await _context.Songs
                .Where(s => s.Artist.ToLower() == artist.ToLower())
                .ToListAsync();
        }
    }
}
