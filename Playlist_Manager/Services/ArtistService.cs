using Playlist_Manager.DTOs;
using Playlist_Manager.DTOs.Artist;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using Playlist_Manager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist_Manager.Services
{
    public class ArtistService
    {
        private readonly IArtist _artistRepository;

        public ArtistService(IArtist artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<ArtistResponseDto> CreateArtistAsync(ArtistCreateDto dto)
        {
            var artist = new Artist
            {
                Name = dto.Name,
                Bio = dto.Bio,
                ProfileImage = dto.ProfileImage,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _artistRepository.CreateArtistAsync(artist);
            return MapToDto(created);
        }

        public async Task<ArtistResponseDto?> GetArtistByIdAsync(int id)
        {
            var artist = await _artistRepository.GetArtistByIdAsync(id);
            return artist == null ? null : MapToDto(artist);
        }

        public async Task<IEnumerable<ArtistResponseDto>> GetAllArtistsAsync()
        {
            var artists = await _artistRepository.GetAllArtistsAsync();
            return artists.Select(MapToDto);
        }

        public async Task<bool> UpdateArtistAsync(int id, ArtistUpdateDto dto)
        {
            var artist = await _artistRepository.GetArtistByIdAsync(id);
            if (artist == null) return false;

            artist.Name = dto.Name ?? artist.Name;
            artist.Bio = dto.Bio ?? artist.Bio;
            artist.ProfileImage = dto.ProfileImage ?? artist.ProfileImage;

            return await _artistRepository.UpdateArtistAsync(artist);
        }

        public async Task<bool> DeleteArtistAsync(int id)
        {
            return await _artistRepository.DeleteArtistAsync(id);
        }

        private ArtistResponseDto MapToDto(Artist artist)
        {
            return new ArtistResponseDto
            {
                Id = artist.Id,
                Name = artist.Name,
                Bio = artist.Bio,
                ProfileImage = artist.ProfileImage,
                CreatedAt = artist.CreatedAt
            };
        }
    }
}
