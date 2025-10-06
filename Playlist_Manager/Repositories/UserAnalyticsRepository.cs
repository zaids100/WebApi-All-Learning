using Microsoft.EntityFrameworkCore;
using Playlist_Manager.DTOs.Analytics;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using System;

namespace Playlist_Manager.Repositories
{
    public class UserAnalyticsRepository : IUserAnalytics
    {
        private readonly PlaylistManagerContext _context;

        public UserAnalyticsRepository(PlaylistManagerContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<double> GetAvgPlaylistsPerUserAsync()
        {
            var userCount = await _context.Users.CountAsync();
            if (userCount == 0) return 0;

            var totalPlaylists = await _context.Playlists.CountAsync();
            return Math.Round((double)totalPlaylists / userCount, 2);
        }

        public async Task<IEnumerable<UserMonthlyCountDto>> GetNewUsersByMonthAsync()
        {
            return (await _context.Users
                .GroupBy(u => new { u.CreatedAt.Year, u.CreatedAt.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .ToListAsync()) // Fetch grouped data first
                .Select(g => new UserMonthlyCountDto
                {
                    Month = $"{g.Month:D2}-{g.Year}", // Now safe to format on client
                    Count = g.Count
                })
                .OrderBy(g => g.Month);
        }


        public async Task<IEnumerable<TopPlaylistUserDto>> GetTopPlaylistCreatorsAsync(int topN)
        {
            return await _context.Users
                .Select(u => new TopPlaylistUserDto
                {
                    Username = u.Username,
                    PlaylistCount = u.Playlists.Count
                })
                .OrderByDescending(u => u.PlaylistCount)
                .Take(topN)
                .ToListAsync();
        }
    }
}
