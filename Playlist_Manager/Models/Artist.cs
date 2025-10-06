using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Playlist_Manager.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Bio { get; set; }

        [Url]
        public byte[]? ProfileImage { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<SongArtist> SongArtists { get; set; } = new List<SongArtist>();
    }
}
