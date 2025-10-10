using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Playlist_Manager.Application.Interfaces;
using Playlist_Manager.Application.Services;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using Playlist_Manager.Repositories;
using Playlist_Manager.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PlaylistManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));
builder.Services.AddControllers()
.AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler =
System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
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
builder.Services.AddScoped<IToken, TokenService>();
builder.Services.AddScoped<IEngagementAnalytics, EngagementAnalyticsRepository>();
builder.Services.AddScoped<EngagementAnalyticsService>();



// JWT setup
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSection["Secret"] ?? "supersecretkey123");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSection["Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true
    };
});

builder.Services.AddSwaggerGen(c =>
{
    // JWT Authentication for Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token as: Bearer <your_token>"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


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
