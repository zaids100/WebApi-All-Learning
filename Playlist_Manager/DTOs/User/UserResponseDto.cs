﻿namespace Playlist_Manager.DTOs.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = "user";

        public DateTime CreatedAt { get; set; }

        public byte[]? ProfileImage { get; set; }
    }
}
