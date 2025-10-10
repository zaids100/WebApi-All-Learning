using System;
using System.ComponentModel.DataAnnotations;

namespace Playlist_Manager.DTOs.PlaylistSong
{
    public class PlaylistSongCreateDto
    {
        [Required(ErrorMessage = "Playlist ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Playlist ID must be a positive integer.")]
        public int PlaylistId { get; set; }

        [Required(ErrorMessage = "Song ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Song ID must be a positive integer.")]
        public int SongId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? AddedAt { get; set; }
    }
}
