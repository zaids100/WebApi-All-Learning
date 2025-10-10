using Playlist_Manager.DTOs.PlaylistSong;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist_Manager.Services
{
    public class PlaylistSongService
    {
        private readonly IPlaylistSong _playlistSongRepository;
        private readonly IPlaylist _playlistRepository;
        private readonly ISong _songRepository;

        public PlaylistSongService(
            IPlaylistSong playlistSongRepository,
            IPlaylist playlistRepository,
            ISong songRepository)
        {
            _playlistSongRepository = playlistSongRepository;
            _playlistRepository = playlistRepository;
            _songRepository = songRepository;
        }

        public async Task<PlaylistSongResponseDto> AddSongToPlaylistAsync(PlaylistSongCreateDto dto)
        {
            if (dto.PlaylistId <= 0)
                throw new ArgumentException("Invalid Playlist ID.");

            if (dto.SongId <= 0)
                throw new ArgumentException("Invalid Song ID.");

            // ✅ Check if playlist exists
            var playlist = await _playlistRepository.GetPlaylistByIdAsync(dto.PlaylistId);
            if (playlist == null)
                throw new KeyNotFoundException($"Playlist with ID {dto.PlaylistId} not found.");

            // ✅ Check if song exists
            var song = await _songRepository.GetSongByIdAsync(dto.SongId);
            if (song == null)
                throw new KeyNotFoundException($"Song with ID {dto.SongId} not found.");

            // ✅ Prevent adding duplicate song to same playlist
            var existingSongs = await _playlistSongRepository.GetSongsByPlaylistIdAsync(dto.PlaylistId);
            if (existingSongs.Any(ps => ps.SongId == dto.SongId))
                throw new InvalidOperationException("This song already exists in the playlist.");

            var entity = new PlaylistSong
            {
                PlaylistId = dto.PlaylistId,
                SongId = dto.SongId,
                AddedAt = dto.AddedAt ?? DateTime.UtcNow
            };

            try
            {
                var created = await _playlistSongRepository.AddSongToPlaylistAsync(entity);

                return new PlaylistSongResponseDto
                {
                    PlaylistId = created.PlaylistId,
                    SongId = created.SongId,
                    AddedAt = created.AddedAt,
                    PlaylistName = playlist.Name,
                    SongTitle = song.Title
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to add song to playlist: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PlaylistSongResponseDto>> GetSongsByPlaylistIdAsync(int playlistId)
        {
            if (playlistId <= 0)
                throw new ArgumentException("Invalid Playlist ID.");

            var playlist = await _playlistRepository.GetPlaylistByIdAsync(playlistId);
            if (playlist == null)
                throw new KeyNotFoundException($"Playlist with ID {playlistId} not found.");

            var list = await _playlistSongRepository.GetSongsByPlaylistIdAsync(playlistId);

            return list.Select(ps => new PlaylistSongResponseDto
            {
                PlaylistId = ps.PlaylistId,
                SongId = ps.SongId,
                PlaylistName = ps.Playlist?.Name ?? playlist.Name,
                SongTitle = ps.Song?.Title ?? string.Empty,
                AddedAt = ps.AddedAt
            });
        }

        public async Task<bool> RemoveSongFromPlaylistAsync(int playlistId, int songId)
        {
            if (playlistId <= 0 || songId <= 0)
                throw new ArgumentException("Invalid Playlist or Song ID.");

            var playlist = await _playlistRepository.GetPlaylistByIdAsync(playlistId);
            if (playlist == null)
                throw new KeyNotFoundException($"Playlist with ID {playlistId} not found.");

            var song = await _songRepository.GetSongByIdAsync(songId);
            if (song == null)
                throw new KeyNotFoundException($"Song with ID {songId} not found.");

            var removed = await _playlistSongRepository.RemoveSongFromPlaylistAsync(playlistId, songId);
            if (!removed)
                throw new InvalidOperationException("Song does not exist in the playlist.");

            return true;
        }
    }
}
