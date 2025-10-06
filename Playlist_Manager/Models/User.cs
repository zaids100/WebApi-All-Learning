using System;
using System.ComponentModel.DataAnnotations;

namespace Playlist_Manager.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "user";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public byte[]? ProfileImage { get; set; }
        public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
