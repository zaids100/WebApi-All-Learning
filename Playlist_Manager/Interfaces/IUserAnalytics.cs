using Playlist_Manager.DTOs.Analytics;

namespace Playlist_Manager.Interfaces
{
    public interface IUserAnalytics
    {
        Task<int> GetTotalUsersAsync();
        Task<double> GetAvgPlaylistsPerUserAsync();
        Task<IEnumerable<UserMonthlyCountDto>> GetNewUsersByMonthAsync();
        Task<IEnumerable<TopPlaylistUserDto>> GetTopPlaylistCreatorsAsync(int topN);
    }
}
