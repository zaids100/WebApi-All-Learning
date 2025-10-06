using Playlist_Manager.DTOs.Analytics;

namespace Playlist_Manager.Application.Interfaces
{
    public interface IUserAnalyticsService
    {
        Task<UserAnalyticsDto> GetAnalyticsAsync();
    }
}
