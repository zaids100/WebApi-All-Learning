namespace Playlist_Manager.DTOs.PlaylistSong
{
    public class PlaylistSongCreateDto
    {
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
        public DateTime? AddedAt { get; set; }
    }
}
