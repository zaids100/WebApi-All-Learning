using Playlist_Manager.Models;

namespace Playlist_Manager.Interfaces
{
        public interface IPlaylistSong
        {
            Task<PlaylistSong> AddSongToPlaylistAsync(PlaylistSong playlistSong);
            Task<IEnumerable<PlaylistSong>> GetSongsByPlaylistIdAsync(int playlistId);
            Task<bool> RemoveSongFromPlaylistAsync(int playlistId, int songId);
        }
}
