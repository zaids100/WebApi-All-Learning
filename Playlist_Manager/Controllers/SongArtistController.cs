using Microsoft.AspNetCore.Mvc;
using Playlist_Manager.DTOs.SongArtist;
using Playlist_Manager.Services;
using System.Threading.Tasks;

namespace Playlist_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongArtistController : ControllerBase
    {
        private readonly SongArtistService _service;

        public SongArtistController(SongArtistService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("song/{songId}")]
        public async Task<IActionResult> GetBySongId(int songId)
        {
            var result = await _service.GetBySongIdAsync(songId);
            return Ok(result);
        }

        [HttpGet("artist/{artistId}")]
        public async Task<IActionResult> GetByArtistId(int artistId)
        {
            var result = await _service.GetByArtistIdAsync(artistId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SongArtistCreateDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (!result)
                return BadRequest(new { message = "Unable to add Song-Artist mapping." });

            return Ok(new { message = "Mapping created successfully." });
        }

        [HttpDelete("{songId}/{artistId}")]
        public async Task<IActionResult> Delete(int songId, int artistId)
        {
            var result = await _service.DeleteAsync(songId, artistId);
            if (!result)
                return NotFound(new { message = "Mapping not found." });

            return Ok(new { message = "Mapping deleted successfully." });
        }
    }
}
