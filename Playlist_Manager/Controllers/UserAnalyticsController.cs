using Microsoft.AspNetCore.Mvc;
using Playlist_Manager.Application.Interfaces;
using System.Threading.Tasks;

namespace Playlist_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAnalyticsController : ControllerBase
    {
        private readonly IUserAnalyticsService _service;

        public UserAnalyticsController(IUserAnalyticsService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAnalytics()
        {
            try
            {
                var analytics = await _service.GetAnalyticsAsync();
                return Ok(analytics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
