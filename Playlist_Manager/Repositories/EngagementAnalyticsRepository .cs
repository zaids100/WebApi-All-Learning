using Microsoft.EntityFrameworkCore;
using Playlist_Manager.DTOs.Analytics;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;

namespace Playlist_Manager.Repositories
{
    public class EngagementAnalyticsRepository : IEngagementAnalytics
    {
        private readonly PlaylistManagerContext _context;

        public EngagementAnalyticsRepository(PlaylistManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlaylistEngagementDto>> GetMostPopularPlaylistsAsync()
        {
            return await _context.Playlists
                .Select(p => new PlaylistEngagementDto
                {
                    PlaylistName = p.Name,
                    SongCount = p.PlaylistSongs.Count
                })
                .OrderByDescending(p => p.SongCount)
                .Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserEngagementDto>> GetMostActiveUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserEngagementDto
                {
                    Username = u.Username,
                    PlaylistCount = u.Playlists.Count
                })
                .OrderByDescending(u => u.PlaylistCount)
                .Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<SongCollaborationDto>> GetMostCollaborativeSongsAsync()
        {
            return await _context.Songs
                .Select(s => new SongCollaborationDto
                {
                    SongTitle = s.Title,
                    ArtistCount = s.SongArtists.Count
                })
                .OrderByDescending(s => s.ArtistCount)
                .Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<ArtistPlaylistPresenceDto>> GetMostFrequentArtistsInPlaylistsAsync()
        {
            return await _context.Artists
                .Select(a => new ArtistPlaylistPresenceDto
                {
                    ArtistName = a.Name,
                    PlaylistAppearances = a.SongArtists
                        .SelectMany(sa => sa.Song.PlaylistSongs)
                        .Select(ps => ps.PlaylistId)
                        .Distinct()
                        .Count()
                })
                .OrderByDescending(a => a.PlaylistAppearances)
                .Take(10)
                .ToListAsync();
        }
    }
}
