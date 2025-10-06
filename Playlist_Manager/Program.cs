using Microsoft.EntityFrameworkCore;
using Playlist_Manager.Models;
using Playlist_Manager.Repositories;
using Playlist_Manager.Services;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Application.Interfaces;
using Playlist_Manager.Application.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PlaylistManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IPlaylist, PlaylistRepository>();
builder.Services.AddScoped<PlaylistService>();
builder.Services.AddScoped<ISong, SongRepository>();
builder.Services.AddScoped<SongService>();
builder.Services.AddScoped<IArtist, ArtistRepository>();
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<IPlaylistSong, PlaylistSongRepository>();
builder.Services.AddScoped<PlaylistSongService>();
builder.Services.AddScoped<ISongArtist, SongArtistRepository>();
builder.Services.AddScoped<SongArtistService>();
builder.Services.AddScoped<IUserAnalytics, UserAnalyticsRepository>();
builder.Services.AddScoped<IUserAnalyticsService, UserAnalyticsService>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
