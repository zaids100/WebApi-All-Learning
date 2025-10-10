using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playlist_Manager.Services;

namespace Playlist_Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="admin")]
    public class EngagementAnalyticsController : ControllerBase
    {
        private readonly EngagementAnalyticsService _service;

        public EngagementAnalyticsController(EngagementAnalyticsService service)
        {
            _service = service;
        }

        [HttpGet("popular-playlists")]
        public async Task<IActionResult> GetMostPopularPlaylists()
        {
            var result = await _service.GetMostPopularPlaylistsAsync();
            return Ok(result);
        }

        [HttpGet("active-users")]
        public async Task<IActionResult> GetMostActiveUsers()
        {
            var result = await _service.GetMostActiveUsersAsync();
            return Ok(result);
        }

        [HttpGet("collaborative-songs")]
        public async Task<IActionResult> GetMostCollaborativeSongs()
        {
            var result = await _service.GetMostCollaborativeSongsAsync();
            return Ok(result);
        }

        [HttpGet("artist-playlist-frequency")]
        public async Task<IActionResult> GetMostFrequentArtistsInPlaylists()
        {
            var result = await _service.GetMostFrequentArtistsInPlaylistsAsync();
            return Ok(result);
        }
    }
}
