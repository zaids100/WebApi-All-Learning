using Microsoft.EntityFrameworkCore;
using System;

namespace Playlist_Manager.Models
{
    public class PlaylistManagerContext : DbContext
    {
        public PlaylistManagerContext(DbContextOptions<PlaylistManagerContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Song> Songs { get; set; } = null!;
        public DbSet<Playlist> Playlists { get; set; } = null!;
        public DbSet<PlaylistSong> PlaylistSongs { get; set; } = null!;
        public DbSet<SongArtist> SongArtists { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many: Song ↔ Artist
            modelBuilder.Entity<SongArtist>()
                .HasKey(sa => new { sa.SongId, sa.ArtistId });

            modelBuilder.Entity<SongArtist>()
                .HasOne(sa => sa.Song)
                .WithMany(s => s.SongArtists)
                .HasForeignKey(sa => sa.SongId);

            modelBuilder.Entity<SongArtist>()
                .HasOne(sa => sa.Artist)
                .WithMany(a => a.SongArtists)
                .HasForeignKey(sa => sa.ArtistId);

            // Many-to-Many: Playlist ↔ Song
            modelBuilder.Entity<PlaylistSong>()
                .HasKey(ps => new { ps.PlaylistId, ps.SongId });

            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Playlist)
                .WithMany(p => p.PlaylistSongs)
                .HasForeignKey(ps => ps.PlaylistId);

            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Song)
                .WithMany(s => s.PlaylistSongs)
                .HasForeignKey(ps => ps.SongId);

            // One-to-Many: User → Playlist
            modelBuilder.Entity<User>()
                .HasMany(u => u.Playlists)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            // SEED DATA -----------------------------------

            var seedDate = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc);

            // Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "zaid123",
                    Email = "zaid@example.com",
                    PasswordHash = "hashedpassword123",
                    Role = "user",
                    CreatedAt = seedDate
                },
                new User
                {
                    Id = 2,
                    Username = "admin",
                    Email = "admin@example.com",
                    PasswordHash = "adminhashedpassword",
                    Role = "admin",
                    CreatedAt = seedDate
                }
            );

            // Artists
            modelBuilder.Entity<Artist>().HasData(
                new Artist { Id = 1, Name = "Arijit Singh", Bio = "Indian playback singer", CreatedAt = seedDate },
                new Artist { Id = 2, Name = "Taylor Swift", Bio = "American singer-songwriter", CreatedAt = seedDate },
                new Artist { Id = 3, Name = "The Weeknd", Bio = "Canadian singer and producer", CreatedAt = seedDate }
            );

            // Songs
            modelBuilder.Entity<Song>().HasData(
                new Song
                {
                    Id = 1,
                    Title = "Tum Hi Ho",
                    Artist = "Arijit Singh",
                    Album = "Aashiqui 2",
                    Genre = "Romantic",
                    Duration = 250,
                    ReleaseDate = new DateTime(2013, 4, 10),
                    SongLink = "https://example.com/tumhiho"
                },
                new Song
                {
                    Id = 2,
                    Title = "Blinding Lights",
                    Artist = "The Weeknd",
                    Album = "After Hours",
                    Genre = "Pop",
                    Duration = 200,
                    ReleaseDate = new DateTime(2019, 11, 29),
                    SongLink = "https://example.com/blindinglights"
                },
                new Song
                {
                    Id = 3,
                    Title = "Love Story",
                    Artist = "Taylor Swift",
                    Album = "Fearless",
                    Genre = "Country",
                    Duration = 230,
                    ReleaseDate = new DateTime(2008, 9, 15),
                    SongLink = "https://example.com/lovestory"
                }
            );

            // SongArtist relationships
            modelBuilder.Entity<SongArtist>().HasData(
                new SongArtist { SongId = 1, ArtistId = 1 },
                new SongArtist { SongId = 2, ArtistId = 3 },
                new SongArtist { SongId = 3, ArtistId = 2 }
            );

            // Playlists
            modelBuilder.Entity<Playlist>().HasData(
                new Playlist
                {
                    Id = 1,
                    Name = "Zaid's Favorites",
                    Description = "My all-time favorite songs",
                    CreatedAt = seedDate,
                    UserId = 1
                },
                new Playlist
                {
                    Id = 2,
                    Name = "Pop Hits",
                    Description = "Popular pop songs",
                    CreatedAt = seedDate,
                    UserId = 2
                }
            );

            // PlaylistSong relationships
            modelBuilder.Entity<PlaylistSong>().HasData(
                new PlaylistSong { PlaylistId = 1, SongId = 1, AddedAt = seedDate },
                new PlaylistSong { PlaylistId = 1, SongId = 2, AddedAt = seedDate },
                new PlaylistSong { PlaylistId = 2, SongId = 3, AddedAt = seedDate }
            );
        }
    }
}
