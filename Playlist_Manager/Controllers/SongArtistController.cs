using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playlist_Manager.DTOs.SongArtist;
using Playlist_Manager.Services;
using System.Threading.Tasks;

namespace Playlist_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="user")]
    public class SongArtistController : ControllerBase
    {
        private readonly SongArtistService _service;

        public SongArtistController(SongArtistService service)
        {
            _service = service;
        }

        // ✅ GET: api/SongArtist
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // ✅ GET: api/SongArtist/song/5
        [HttpGet("song/{songId:int}")]
        public async Task<IActionResult> GetBySongId(int songId)
        {
            if (songId <= 0)
                return BadRequest(new { message = "Invalid Song ID." });

            var result = await _service.GetBySongIdAsync(songId);
            return Ok(result);
        }

        // ✅ GET: api/SongArtist/artist/10
        [HttpGet("artist/{artistId:int}")]
        public async Task<IActionResult> GetByArtistId(int artistId)
        {
            if (artistId <= 0)
                return BadRequest(new { message = "Invalid Artist ID." });

            var result = await _service.GetByArtistIdAsync(artistId);
            return Ok(result);
        }

        // ✅ POST: api/SongArtist
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SongArtistCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.SongId <= 0 || dto.ArtistId <= 0)
                return BadRequest(new { message = "Invalid Song or Artist ID." });

            try
            {
                var created = await _service.AddAsync(dto);
                if (!created)
                    return Conflict(new { message = "This Song-Artist mapping already exists or could not be created." });

                return CreatedAtAction(nameof(GetBySongId), new { songId = dto.SongId }, new { message = "Mapping created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal Server Error: {ex.Message}" });
            }
        }

        // ✅ DELETE: api/SongArtist/5/10
        [HttpDelete("{songId:int}/{artistId:int}")]
        public async Task<IActionResult> Delete(int songId, int artistId)
        {
            if (songId <= 0 || artistId <= 0)
                return BadRequest(new { message = "Invalid Song or Artist ID." });

            try
            {
                var deleted = await _service.DeleteAsync(songId, artistId);
                if (!deleted)
                    return NotFound(new { message = "Mapping not found." });

                return Ok(new { message = "Mapping deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal Server Error: {ex.Message}" });
            }
        }
    }
}
