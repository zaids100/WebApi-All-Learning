namespace Playlist_Manager.DTOs.Analytics
{
    public class ArtistPlaylistPresenceDto
    {
        public string ArtistName { get; set; } = string.Empty;
        public int PlaylistAppearances { get; set; }
    }
}
