using AshenCrown.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AshenCrown.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Job> Jobs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    Id = 1,
                    Title = "C# Backend Development Intern (ASP.NET)",
                    Company = "Example Tech Corp",
                    Location = "Remote",
                    Url = "https://example.com/jobs/123",
                    ExperienceRequirement = 0 ,
                    Position ="Intern"
                });
        }
    }
}
