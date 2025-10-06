using Microsoft.EntityFrameworkCore;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playlist_Manager.Repositories
{
    public class SongArtistRepository : ISongArtist
    {
        private readonly PlaylistManagerContext _context;

        public SongArtistRepository(PlaylistManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SongArtist>> GetAllAsync()
        {
            return await _context.SongArtists
                .Include(sa => sa.Song)
                .Include(sa => sa.Artist)
                .ToListAsync();
        }

        public async Task<IEnumerable<SongArtist>> GetBySongIdAsync(int songId)
        {
            return await _context.SongArtists
                .Include(sa => sa.Artist)
                .Where(sa => sa.SongId == songId)
                .ToListAsync();
        }

        public async Task<IEnumerable<SongArtist>> GetByArtistIdAsync(int artistId)
        {
            return await _context.SongArtists
                .Include(sa => sa.Song)
                .Where(sa => sa.ArtistId == artistId)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(SongArtist songArtist)
        {
            await _context.SongArtists.AddAsync(songArtist);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int songId, int artistId)
        {
            var entry = await _context.SongArtists
                .FirstOrDefaultAsync(sa => sa.SongId == songId && sa.ArtistId == artistId);

            if (entry == null) return false;

            _context.SongArtists.Remove(entry);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
