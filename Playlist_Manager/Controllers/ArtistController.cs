using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playlist_Manager.DTOs;
using Playlist_Manager.DTOs.Artist;
using Playlist_Manager.Services;
using System.Threading.Tasks;

namespace Playlist_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="admin")]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistService _artistService;

        public ArtistController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateArtist([FromBody] ArtistCreateDto dto)
        {
            var created = await _artistService.CreateArtistAsync(dto);
            return CreatedAtAction(nameof(GetArtistById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            var artist = await _artistService.GetArtistByIdAsync(id);
            if (artist == null) return NotFound(new { message = "Artist not found" });
            return Ok(artist);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            var artists = await _artistService.GetAllArtistsAsync();
            return Ok(artists);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, [FromBody] ArtistUpdateDto dto)
        {
            var updated = await _artistService.UpdateArtistAsync(id, dto);
            if (!updated) return NotFound(new { message = "Artist not found" });
            return Ok(new { message = "Artist updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var deleted = await _artistService.DeleteArtistAsync(id);
            if (!deleted) return NotFound(new { message = "Artist not found" });
            return Ok(new { message = "Artist deleted successfully" });
        }
    }
}
