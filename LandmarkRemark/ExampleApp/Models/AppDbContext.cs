using System;
using ExampleApp.Services;
using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        /// <summary>
        /// Just seed some test data
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Remark>().HasData(
                new Remark
                {
                    Id = Guid.NewGuid(),
                    Username = "test1@example.com",
                    Note = "This is a test note",
                    Latitude = 123.456,
                    Longitude = 456.789,
                    DateCreated = DateTime.Now
                }
            );
            
            AuthService authService = new AuthService("doesntmatter");

            var pass1Hashed = authService.GenerateHash("testing123");
            var pass2Hashed = authService.GenerateHash("321testing");
            
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Test User 1",
                    Username = "test1@example.com",
                    Password = pass1Hashed
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Test User 2",
                    Username = "test2@example.com",
                    Password = pass2Hashed
                }
            );
        }

        public DbSet<Remark> Remarks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}