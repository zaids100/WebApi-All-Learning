using Playlist_Manager.Models;

namespace Playlist_Manager.Interfaces
{
    public interface IToken
    {
        Task<(string Token, DateTime ExpiresAt)> GenerateAccessTokenAsync(User user);
    }
}
