using Playlist_Manager.DTOs;
using Playlist_Manager.DTOs.Playlist;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using Playlist_Manager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist_Manager.Services
{
    public class PlaylistService
    {
        private readonly IPlaylist _playlistRepository;

        public PlaylistService(IPlaylist playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        // Create playlist
        public async Task<PlaylistResponseDto> CreatePlaylistAsync(PlaylistCreateDto dto)
        {
            var playlist = new Playlist
            {
                Name = dto.Name,
                Description = dto.Description,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _playlistRepository.CreatePlaylistAsync(playlist);

            return new PlaylistResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                CreatedAt = created.CreatedAt,
                UserId = created.UserId,
                Songs = created.PlaylistSongs.Select(ps => ps.Song).ToList()
            };
        }

        // Get playlist by ID
        public async Task<PlaylistResponseDto?> GetPlaylistByIdAsync(int id)
        {
            var playlist = await _playlistRepository.GetPlaylistByIdAsync(id);
            if (playlist == null) return null;

            return new PlaylistResponseDto
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description,
                CreatedAt = playlist.CreatedAt,
                UserId = playlist.UserId,
                Songs = playlist.PlaylistSongs.Select(ps => ps.Song).ToList()
            };
        }

        // Get all playlists
        public async Task<IEnumerable<PlaylistResponseDto>> GetAllPlaylistsAsync()
        {
            var playlists = await _playlistRepository.GetAllPlaylistsAsync();
            return playlists.Select(p => new PlaylistResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                UserId = p.UserId,
                Songs = p.PlaylistSongs.Select(ps => ps.Song).ToList()
            });
        }

        // Update playlist
        public async Task<bool> UpdatePlaylistAsync(int id, PlaylistUpdateDto dto)
        {
            var playlist = await _playlistRepository.GetPlaylistByIdAsync(id);
            if (playlist == null) return false;

            playlist.Name = dto.Name ?? playlist.Name;
            playlist.Description = dto.Description ?? playlist.Description;

            return await _playlistRepository.UpdatePlaylistAsync(playlist);
        }

        // Delete playlist
        public async Task<bool> DeletePlaylistAsync(int id)
        {
            return await _playlistRepository.DeletePlaylistAsync(id);
        }

        // Get all playlists of a user
        public async Task<IEnumerable<PlaylistResponseDto>> GetPlaylistsByUserIdAsync(int userId)
        {
            var playlists = await _playlistRepository.GetPlaylistsByUserIdAsync(userId);
            return playlists.Select(p => new PlaylistResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                UserId = p.UserId,
                Songs = p.PlaylistSongs.Select(ps => ps.Song).ToList()
            });
        }
    }
}
