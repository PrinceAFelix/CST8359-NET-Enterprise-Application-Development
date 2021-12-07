using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment2.Models;

namespace Assignment2.Data
{
    public class SchoolCommunityContext : DbContext
    {
        public SchoolCommunityContext(DbContextOptions<SchoolCommunityContext> options) : base(options) { }


        public DbSet<Student> Students
        {
            get;
            set;
        }

        public DbSet<Community> Communities
        {
            get;
            set;
        }

        public DbSet<CommunityMembership> CommunityMemberships
        {
            get;
            set;
        }


        public DbSet<Advertisement> Advertisements 
        { 
            get; 
            set; 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Community>().ToTable("Community");
            modelBuilder.Entity<CommunityMembership>().ToTable("CommunityMembership");
            modelBuilder.Entity<Advertisement>().ToTable("Advertisement");
            modelBuilder.Entity<CommunityMembership>().HasKey(c => new { c.StudentId, c.CommunityId });

        }
    }
}
