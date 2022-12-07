using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MovieStoreApp.Models.Domain
{
    public class MovieDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public MovieDatabaseContext(DbContextOptions<MovieDatabaseContext> options) : base(options)
        {
        }


        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }
    }
}

