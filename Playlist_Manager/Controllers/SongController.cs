using Microsoft.AspNetCore.Mvc;
using Playlist_Manager.DTOs;
using Playlist_Manager.DTOs.Song;
using Playlist_Manager.Services;
using System.Threading.Tasks;

namespace Playlist_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly SongService _songService;

        public SongController(SongService songService)
        {
            _songService = songService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSong([FromBody] SongCreateDto dto)
        {
            var created = await _songService.CreateSongAsync(dto);
            return CreatedAtAction(nameof(GetSongById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSongById(int id)
        {
            var song = await _songService.GetSongByIdAsync(id);
            if (song == null) return NotFound(new { message = "Song not found" });
            return Ok(song);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _songService.GetAllSongsAsync();
            return Ok(songs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSong(int id, [FromBody] SongUpdateDto dto)
        {
            var updated = await _songService.UpdateSongAsync(id, dto);
            if (!updated) return NotFound(new { message = "Song not found" });
            return Ok(new { message = "Song updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var deleted = await _songService.DeleteSongAsync(id);
            if (!deleted) return NotFound(new { message = "Song not found" });
            return Ok(new { message = "Song deleted successfully" });
        }

        [HttpGet("artist/{artistName}")]
        public async Task<IActionResult> GetSongsByArtist(string artistName)
        {
            var songs = await _songService.GetSongsByArtistAsync(artistName);
            return Ok(songs);
        }
    }
}
