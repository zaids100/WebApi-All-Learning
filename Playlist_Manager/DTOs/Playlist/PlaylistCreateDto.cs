namespace Playlist_Manager.DTOs.Playlist
{
    public class PlaylistCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int UserId { get; set; }
    }
}
