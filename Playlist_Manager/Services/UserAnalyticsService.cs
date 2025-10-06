using Playlist_Manager.Application.Interfaces;
using Playlist_Manager.DTOs.Analytics;
using Playlist_Manager.Interfaces;

namespace Playlist_Manager.Application.Services
{
    public class UserAnalyticsService : IUserAnalyticsService
    {
        private readonly IUserAnalytics _repository;

        public UserAnalyticsService(IUserAnalytics repository)
        {
            _repository = repository;
        }

        public async Task<UserAnalyticsDto> GetAnalyticsAsync()
        {
            var totalUsers = await _repository.GetTotalUsersAsync();
            var avgPlaylists = await _repository.GetAvgPlaylistsPerUserAsync();
            var newUsersByMonth = await _repository.GetNewUsersByMonthAsync();
            var topCreators = await _repository.GetTopPlaylistCreatorsAsync(5);

            return new UserAnalyticsDto
            {
                TotalUsers = totalUsers,
                AvgPlaylistsPerUser = avgPlaylists,
                NewUsersByMonth = newUsersByMonth,
                TopPlaylistCreators = topCreators
            };
        }
    }
}
