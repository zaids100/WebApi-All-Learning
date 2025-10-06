using Microsoft.EntityFrameworkCore;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playlist_Manager.Repositories
{
    public class ArtistRepository : IArtist
    {
        private readonly PlaylistManagerContext _context;

        public ArtistRepository(PlaylistManagerContext context)
        {
            _context = context;
        }

        public async Task<Artist> CreateArtistAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
            return artist;
        }

        public async Task<Artist?> GetArtistByIdAsync(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public async Task<IEnumerable<Artist>> GetAllArtistsAsync()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<bool> UpdateArtistAsync(Artist artist)
        {
            var existing = await _context.Artists.FindAsync(artist.Id);
            if (existing == null) return false;

            existing.Name = artist.Name ?? existing.Name;
            existing.Bio = artist.Bio ?? existing.Bio;
            existing.ProfileImage = artist.ProfileImage ?? existing.ProfileImage;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteArtistAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return false;

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
