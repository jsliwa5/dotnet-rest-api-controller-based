using System.Data.Common;
using dotnet_rest_api_controller_based.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rest_api_controller_based.DataDb
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<Event> Events { get; set; } 
        //public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Organizer> Organizers {  get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Events)
                .WithMany(e => e.Attendees);

            modelBuilder.Entity<Organizer>()
                .HasMany(o => o.Events)
                .WithOne(e => e.Organizer)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
