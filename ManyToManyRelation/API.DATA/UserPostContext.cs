using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain;
using Microsoft.EntityFrameworkCore;
namespace API.DATA
{
    public class UserPostContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }

        public UserPostContext(DbContextOptions<UserPostContext> options)
            : base(options) { }
        //protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=LAPTOP-K80BMT9V;initial catalog=practiceDb;integrated security=true;trustservercertificate=true");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(
              u =>
              {
                  u.HasKey(u => u.UserId);
                  u.Property(u => u.Username).IsRequired().HasMaxLength(50);
                  u.Property(u => u.Email).IsRequired().HasMaxLength(100);
              }
            );

            modelBuilder.Entity<Post>(
              p =>
              {
                  p.HasKey(p => p.PostId);
                  p.Property(p => p.Title).IsRequired().HasMaxLength(100);
                  p.Property(p => p.Content).IsRequired();
              }
            );

            modelBuilder.Entity<UserPost>(
              up =>
              {
                  up.HasKey(up => new { up.UserId, up.PostId });
                  up.HasOne(up => up.User)
                    .WithMany(u => u.UserPosts)
                    .HasForeignKey(up => up.UserId);
                  up.HasOne(up => up.Post)
                    .WithMany(p => p.UserPosts)
                    .HasForeignKey(up => up.PostId);
              }
            );








        }
    }
}
