namespace Playlist_Manager.DTOs.Analytics
{
    public class UserAnalyticsDto
    {
        public int TotalUsers { get; set; }
        public double AvgPlaylistsPerUser { get; set; }
        public IEnumerable<UserMonthlyCountDto>? NewUsersByMonth { get; set; } 
        public IEnumerable<TopPlaylistUserDto>? TopPlaylistCreators { get; set; }
    }
}
