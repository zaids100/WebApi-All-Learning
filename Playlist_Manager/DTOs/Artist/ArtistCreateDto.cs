namespace Playlist_Manager.DTOs.Artist
{
    public class ArtistCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public byte[]? ProfileImage { get; set; }
    }
}
