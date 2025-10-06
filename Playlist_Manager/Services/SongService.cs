using Playlist_Manager.DTOs;
using Playlist_Manager.DTOs.Song;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using Playlist_Manager.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist_Manager.Services
{
    public class SongService
    {
        private readonly ISong _songRepository;

        public SongService(ISong songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<SongResponseDto> CreateSongAsync(SongCreateDto dto)
        {
            var song = new Song
            {
                Title = dto.Title,
                Artist = dto.Artist,
                Album = dto.Album,
                Genre = dto.Genre,
                Duration = dto.Duration,
                ReleaseDate = dto.ReleaseDate,
                SongLink = dto.SongLink
            };

            var created = await _songRepository.CreateSongAsync(song);
            return MapToDto(created);
        }

        public async Task<SongResponseDto?> GetSongByIdAsync(int id)
        {
            var song = await _songRepository.GetSongByIdAsync(id);
            return song == null ? null : MapToDto(song);
        }

        public async Task<IEnumerable<SongResponseDto>> GetAllSongsAsync()
        {
            var songs = await _songRepository.GetAllSongsAsync();
            return songs.Select(MapToDto);
        }

        public async Task<bool> UpdateSongAsync(int id, SongUpdateDto dto)
        {
            var song = await _songRepository.GetSongByIdAsync(id);
            if (song == null) return false;

            song.Title = dto.Title ?? song.Title;
            song.Artist = dto.Artist ?? song.Artist;
            song.Album = dto.Album ?? song.Album;
            song.Genre = dto.Genre ?? song.Genre;
            song.Duration = dto.Duration ?? song.Duration;
            song.ReleaseDate = dto.ReleaseDate ?? song.ReleaseDate;
            song.SongLink = dto.SongLink ?? song.SongLink;

            return await _songRepository.UpdateSongAsync(song);
        }

        public async Task<bool> DeleteSongAsync(int id)
        {
            return await _songRepository.DeleteSongAsync(id);
        }

        public async Task<IEnumerable<SongResponseDto>> GetSongsByArtistAsync(string artist)
        {
            var songs = await _songRepository.GetSongsByArtistAsync(artist);
            return songs.Select(MapToDto);
        }

        private SongResponseDto MapToDto(Song song)
        {
            return new SongResponseDto
            {
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
                Album = song.Album,
                Genre = song.Genre,
                Duration = song.Duration,
                ReleaseDate = song.ReleaseDate,
                SongLink = song.SongLink
            };
        }
    }
}
