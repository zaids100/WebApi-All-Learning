using System.ComponentModel.DataAnnotations;

namespace Playlist_Manager.DTOs.User
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        public byte[]? ProfileImage { get; set; }
    }
}
