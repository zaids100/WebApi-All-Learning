using System;
using System.ComponentModel.DataAnnotations;

namespace Playlist_Manager.Models
{
    public class PlaylistSong
    {
        [Required(ErrorMessage = "Playlist ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Playlist ID must be a positive integer.")]
        public int PlaylistId { get; set; }

        [Required(ErrorMessage = "Song ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Song ID must be a positive integer.")]
        public int SongId { get; set; }

        [Required]
        public Playlist Playlist { get; set; } = null!;

        [Required]
        public Song Song { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
