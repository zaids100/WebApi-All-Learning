using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Playlist_Manager.Models
{
    public class Playlist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        public int UserId { get; set; }
        public User? User { get; set; }

        
        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
    }
}
