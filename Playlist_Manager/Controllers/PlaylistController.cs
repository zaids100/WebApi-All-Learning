using Microsoft.AspNetCore.Mvc;
using Playlist_Manager.DTOs.Playlist;
using Playlist_Manager.Models;
using Playlist_Manager.Services;
using System;
using System.Threading.Tasks;

namespace Playlist_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly PlaylistService _playlistService;

        public PlaylistController(PlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        // Create playlist
        [HttpPost]
        public async Task<IActionResult> CreatePlaylist([FromBody] PlaylistCreateDto dto)
        {
            try
            {
                var created = await _playlistService.CreatePlaylistAsync(dto);
                return CreatedAtAction(nameof(GetPlaylistById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Get playlist by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylistById(int id)
        {
            var playlist = await _playlistService.GetPlaylistByIdAsync(id);
            if (playlist == null) return NotFound(new { message = "Playlist not found" });
            return Ok(playlist);
        }

        // Get all playlists
        [HttpGet]
        public async Task<IActionResult> GetAllPlaylists()
        {
            var playlists = await _playlistService.GetAllPlaylistsAsync();
            return Ok(playlists);
        }

        // Update playlist
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, [FromBody] PlaylistUpdateDto dto)
        {
            var updated = await _playlistService.UpdatePlaylistAsync(id, dto);
            if (!updated) return NotFound(new { message = "Playlist not found" });
            return Ok(new { message = "Playlist updated successfully" });
        }

        // Delete playlist
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var deleted = await _playlistService.DeletePlaylistAsync(id);
            if (!deleted) return NotFound(new { message = "Playlist not found" });
            return Ok(new { message = "Playlist deleted successfully" });
        }

        // Get all playlists of a user
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPlaylistsByUser(int userId)
        {
            var playlists = await _playlistService.GetPlaylistsByUserIdAsync(userId);
            return Ok(playlists);
        }
    }
}
