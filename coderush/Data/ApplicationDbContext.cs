using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using coderush.Models;

namespace coderush.Data
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
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<coderush.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<coderush.Models.NumberSequence> NumberSequence { get; set; }

        public DbSet<coderush.Models.UserProfile> UserProfile { get; set; }

        public DbSet<coderush.Models.BookGeneral> BookGeneral { get; set; }

        public DbSet<coderush.Models.BookMeetingRoom> BookMeetingRoom { get; set; }

        public DbSet<coderush.Models.BookCar> BookCar { get; set; }

        public DbSet<coderush.Models.MeetingRoom> MeetingRoom { get; set; }

        public DbSet<coderush.Models.Car> Car { get; set; }

        public DbSet<coderush.Models.General> General { get; set; }

        public DbSet<coderush.Models.MyAgenda> MyAgenda { get; set; }
    }
}
