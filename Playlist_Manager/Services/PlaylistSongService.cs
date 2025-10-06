using Playlist_Manager.DTOs.PlaylistSong;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using Playlist_Manager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist_Manager.Services
{
    public class PlaylistSongService
    {
        private readonly IPlaylistSong _playlistSongRepository;

        public PlaylistSongService(IPlaylistSong playlistSongRepository)
        {
            _playlistSongRepository = playlistSongRepository;
        }

        public async Task<PlaylistSongResponseDto> AddSongToPlaylistAsync(PlaylistSongCreateDto dto)
        {
            var entity = new PlaylistSong
            {
                PlaylistId = dto.PlaylistId,
                SongId = dto.SongId,
                AddedAt = dto.AddedAt ?? DateTime.UtcNow
            };

            var created = await _playlistSongRepository.AddSongToPlaylistAsync(entity);

            return new PlaylistSongResponseDto
            {
                PlaylistId = created.PlaylistId,
                SongId = created.SongId,
                AddedAt = created.AddedAt,
                PlaylistName = created.Playlist?.Name ?? string.Empty,
                SongTitle = created.Song?.Title ?? string.Empty
            };
        }
        public async Task<IEnumerable<PlaylistSongResponseDto>> GetSongsByPlaylistIdAsync(int playlistId)
        {
            var list = await _playlistSongRepository.GetSongsByPlaylistIdAsync(playlistId);

            return list.Select(ps => new PlaylistSongResponseDto
            {
                PlaylistId = ps.PlaylistId,
                SongId = ps.SongId,
                PlaylistName = ps.Playlist?.Name ?? string.Empty,
                SongTitle = ps.Song?.Title ?? string.Empty,
                AddedAt = ps.AddedAt
            });
        }
        public async Task<bool> RemoveSongFromPlaylistAsync(int playlistId, int songId)
        {
            return await _playlistSongRepository.RemoveSongFromPlaylistAsync(playlistId, songId);
        }
    }
}
