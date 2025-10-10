using Playlist_Manager.DTOs.SongArtist;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist_Manager.Services
{
    public class SongArtistService
    {
        private readonly ISongArtist _songArtistRepository;
        private readonly ISong _songRepository;
        private readonly IArtist _artistRepository;

        public SongArtistService(ISongArtist songArtistRepository, ISong songRepository, IArtist artistRepository)
        {
            _songArtistRepository = songArtistRepository;
            _songRepository = songRepository;
            _artistRepository = artistRepository;
        }

        public async Task<IEnumerable<SongArtistResponseDto>> GetAllAsync()
        {
            var list = await _songArtistRepository.GetAllAsync();
            return list.Select(sa => new SongArtistResponseDto
            {
                SongId = sa.SongId,
                SongTitle = sa.Song.Title,
                ArtistId = sa.ArtistId,
                ArtistName = sa.Artist.Name
            });
        }

        public async Task<IEnumerable<SongArtistResponseDto>> GetBySongIdAsync(int songId)
        {
            if (songId <= 0)
                throw new ArgumentException("Invalid Song ID.");

            var song = await _songRepository.GetSongByIdAsync(songId);
            if (song == null)
                throw new KeyNotFoundException($"Song with ID {songId} not found.");

            var list = await _songArtistRepository.GetBySongIdAsync(songId);
            return list.Select(sa => new SongArtistResponseDto
            {
                SongId = sa.SongId,
                SongTitle = sa.Song.Title,
                ArtistId = sa.ArtistId,
                ArtistName = sa.Artist.Name
            });
        }

        public async Task<IEnumerable<SongArtistResponseDto>> GetByArtistIdAsync(int artistId)
        {
            if (artistId <= 0)
                throw new ArgumentException("Invalid Artist ID.");

            var artist = await _artistRepository.GetArtistByIdAsync(artistId);
            if (artist == null)
                throw new KeyNotFoundException($"Artist with ID {artistId} not found.");

            var list = await _songArtistRepository.GetByArtistIdAsync(artistId);
            return list.Select(sa => new SongArtistResponseDto
            {
                SongId = sa.SongId,
                SongTitle = sa.Song.Title,
                ArtistId = sa.ArtistId,
                ArtistName = sa.Artist.Name
            });
        }

        public async Task<bool> AddAsync(SongArtistCreateDto dto)
        {
            if (dto.SongId <= 0)
                throw new ArgumentException("Invalid Song ID.");
            if (dto.ArtistId <= 0)
                throw new ArgumentException("Invalid Artist ID.");

            // ✅ Check existence
            var song = await _songRepository.GetSongByIdAsync(dto.SongId);
            if (song == null)
                throw new KeyNotFoundException($"Song with ID {dto.SongId} not found.");

            var artist = await _artistRepository.GetArtistByIdAsync(dto.ArtistId);
            if (artist == null)
                throw new KeyNotFoundException($"Artist with ID {dto.ArtistId} not found.");

            // ✅ Prevent duplicate mapping
            var existing = await _songArtistRepository.GetBySongIdAsync(dto.SongId);
            if (existing.Any(sa => sa.ArtistId == dto.ArtistId))
                throw new InvalidOperationException("This song is already associated with the artist.");

            var entity = new SongArtist
            {
                SongId = dto.SongId,
                ArtistId = dto.ArtistId
            };

            var success = await _songArtistRepository.AddAsync(entity);
            if (!success)
                throw new InvalidOperationException("Failed to add Song–Artist mapping.");

            return true;
        }

        public async Task<bool> DeleteAsync(int songId, int artistId)
        {
            if (songId <= 0 || artistId <= 0)
                throw new ArgumentException("Invalid Song or Artist ID.");

            var song = await _songRepository.GetSongByIdAsync(songId);
            if (song == null)
                throw new KeyNotFoundException($"Song with ID {songId} not found.");

            var artist = await _artistRepository.GetArtistByIdAsync(artistId);
            if (artist == null)
                throw new KeyNotFoundException($"Artist with ID {artistId} not found.");

            var deleted = await _songArtistRepository.DeleteAsync(songId, artistId);
            if (!deleted)
                throw new InvalidOperationException("Mapping not found or could not be deleted.");

            return true;
        }
    }
}
