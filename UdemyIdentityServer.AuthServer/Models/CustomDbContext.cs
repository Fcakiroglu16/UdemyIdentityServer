using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyIdentityServer.AuthServer.Models
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions opts) : base(opts)
        {
        }

        public DbSet<CustomUser> customUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // md5 sha256 sha512

            modelBuilder.Entity<CustomUser>().HasData(
                new CustomUser() { Id = 1, Email = "fcakiroglu@outlook.com", Password = "password", City = "istanbul", UserName = "facakiroglu16" },
                         new CustomUser() { Id = 2, Email = "ahmet@outlook.com", Password = "password", City = "Ankara", UserName = "ahmet16" },
                                new CustomUser() { Id = 3, Email = "mehmet@outlook.com", Password = "password", City = "Konya", UserName = "mehmet16" }

            );

            base.OnModelCreating(modelBuilder);
        }
    }
}