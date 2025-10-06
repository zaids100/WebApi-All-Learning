namespace Playlist_Manager.DTOs.Song
{
    public class SongUpdateDto
    {
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public string? Genre { get; set; }
        public int? Duration { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? SongLink { get; set; }
    }
}
