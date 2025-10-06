using System;
using System.ComponentModel.DataAnnotations;

namespace Playlist_Manager.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Artist { get; set; } = string.Empty;

        [StringLength(100)]
        public string Album { get; set; } = string.Empty;

        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Range(1, 3600)]
        public int Duration { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Url]
        public string? SongLink { get; set; }
        public ICollection<SongArtist> SongArtists { get; set; } = new List<SongArtist>();
        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
    }
}
