using Playlist_Manager.DTOs.Analytics;
using Playlist_Manager.Interfaces;

namespace Playlist_Manager.Services
{
    public class EngagementAnalyticsService
    {
        private readonly IEngagementAnalytics _repo;

        public EngagementAnalyticsService(IEngagementAnalytics repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PlaylistEngagementDto>> GetMostPopularPlaylistsAsync()
            => await _repo.GetMostPopularPlaylistsAsync();

        public async Task<IEnumerable<UserEngagementDto>> GetMostActiveUsersAsync()
            => await _repo.GetMostActiveUsersAsync();

        public async Task<IEnumerable<SongCollaborationDto>> GetMostCollaborativeSongsAsync()
            => await _repo.GetMostCollaborativeSongsAsync();

        public async Task<IEnumerable<ArtistPlaylistPresenceDto>> GetMostFrequentArtistsInPlaylistsAsync()
            => await _repo.GetMostFrequentArtistsInPlaylistsAsync();
    }
}
