using System;

namespace Playlist_Manager.DTOs.SongArtist
{
    public class SongArtistResponseDto
    {
        public int SongId { get; set; }
        public string SongTitle { get; set; } = string.Empty;
        public int ArtistId { get; set; }
        public string ArtistName { get; set; } = string.Empty;
        //public DateTime AddedAt { get; set; }
    }
}
