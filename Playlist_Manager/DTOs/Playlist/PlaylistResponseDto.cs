using Playlist_Manager.Models;
using System;
using System.Collections.Generic;

namespace Playlist_Manager.DTOs.Playlist
{
    public class PlaylistResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public IEnumerable<Playlist_Manager.Models.Song>? Songs{ get; set; }
    }
}
