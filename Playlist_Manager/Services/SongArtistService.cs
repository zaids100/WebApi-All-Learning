using Playlist_Manager.DTOs.SongArtist;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist_Manager.Services
{
    public class SongArtistService
    {
        private readonly ISongArtist _repo;

        public SongArtistService(ISongArtist repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SongArtistResponseDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(sa => new SongArtistResponseDto
            {
                SongId = sa.SongId,
                SongTitle = sa.Song.Title,
                ArtistId = sa.ArtistId,
                ArtistName = sa.Artist.Name,
                //AddedAt = sa.AddedAt
            });
        }

        public async Task<bool> AddAsync(SongArtistCreateDto dto)
        {
            var entity = new SongArtist
            {
                SongId = dto.SongId,
                ArtistId = dto.ArtistId
            };
            return await _repo.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(int songId, int artistId)
        {
            return await _repo.DeleteAsync(songId, artistId);
        }

        public async Task<IEnumerable<SongArtistResponseDto>> GetBySongIdAsync(int songId)
        {
            var list = await _repo.GetBySongIdAsync(songId);
            return list.Select(sa => new SongArtistResponseDto
            {
                SongId = sa.SongId,
                SongTitle = sa.Song.Title,
                ArtistId = sa.ArtistId,
                ArtistName = sa.Artist.Name,
                //AddedAt = sa.AddedAt
            });
        }

        public async Task<IEnumerable<SongArtistResponseDto>> GetByArtistIdAsync(int artistId)
        {
            var list = await _repo.GetByArtistIdAsync(artistId);
            return list.Select(sa => new SongArtistResponseDto
            {
                SongId = sa.SongId,
                SongTitle = sa.Song.Title,
                ArtistId = sa.ArtistId,
                ArtistName = sa.Artist.Name,
                //AddedAt = sa.AddedAt
            });
        }
    }
}
