using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FrankProject.Models;

namespace FrankProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<FrankProject.Models.Post> Post { get; set; }

        public DbSet<FrankProject.Models.Comment> Comment { get; set; }

        public DbSet<FrankProject.Models.Rating> Rating { get; set; }

        public DbSet<FrankProject.Models.Category> Category { get; set; }
    }
}
