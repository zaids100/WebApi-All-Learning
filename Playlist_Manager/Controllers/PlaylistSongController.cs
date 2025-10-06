using Microsoft.AspNetCore.Mvc;
using Playlist_Manager.DTOs;
using Playlist_Manager.DTOs.PlaylistSong;
using Playlist_Manager.Services;
using System.Threading.Tasks;

namespace Playlist_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistSongController : ControllerBase
    {
        private readonly PlaylistSongService _playlistSongService;

        public PlaylistSongController(PlaylistSongService playlistSongService)
        {
            _playlistSongService = playlistSongService;
        }

        // ✅ Add Song to Playlist
        [HttpPost]
        public async Task<IActionResult> AddSongToPlaylist([FromBody] PlaylistSongCreateDto dto)
        {
            try
            {
                var result = await _playlistSongService.AddSongToPlaylistAsync(dto);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ✅ Get All Songs in a Playlist
        [HttpGet("{playlistId}")]
        public async Task<IActionResult> GetSongsByPlaylistId(int playlistId)
        {
            var result = await _playlistSongService.GetSongsByPlaylistIdAsync(playlistId);
            return Ok(result);
        }

        // ✅ Remove Song from Playlist
        [HttpDelete("{playlistId}/{songId}")]
        public async Task<IActionResult> RemoveSongFromPlaylist(int playlistId, int songId)
        {
            var deleted = await _playlistSongService.RemoveSongFromPlaylistAsync(playlistId, songId);
            if (!deleted)
                return NotFound(new { message = "Song not found in playlist." });

            return Ok(new { message = "Song removed successfully." });
        }
    }
}
