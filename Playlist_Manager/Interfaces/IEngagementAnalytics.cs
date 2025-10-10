using Playlist_Manager.DTOs.Analytics;

namespace Playlist_Manager.Interfaces
{
    public interface IEngagementAnalytics
    {
        Task<IEnumerable<PlaylistEngagementDto>> GetMostPopularPlaylistsAsync();
        Task<IEnumerable<UserEngagementDto>> GetMostActiveUsersAsync();
        Task<IEnumerable<SongCollaborationDto>> GetMostCollaborativeSongsAsync();
        Task<IEnumerable<ArtistPlaylistPresenceDto>> GetMostFrequentArtistsInPlaylistsAsync();
    }
}
