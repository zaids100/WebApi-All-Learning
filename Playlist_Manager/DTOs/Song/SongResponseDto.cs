namespace Playlist_Manager.DTOs.Song
{
    public class SongResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Duration { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? SongLink { get; set; }
    }
}
