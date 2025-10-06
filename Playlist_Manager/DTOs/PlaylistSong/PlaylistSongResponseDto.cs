namespace Playlist_Manager.DTOs.PlaylistSong
{
    public class PlaylistSongResponseDto
    {
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; } = string.Empty;
        public int SongId { get; set; }
        public string SongTitle { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; }
    }
}
