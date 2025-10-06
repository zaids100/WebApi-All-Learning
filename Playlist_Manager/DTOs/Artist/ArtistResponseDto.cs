namespace Playlist_Manager.DTOs.Artist
{
    public class ArtistResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public byte[]? ProfileImage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
