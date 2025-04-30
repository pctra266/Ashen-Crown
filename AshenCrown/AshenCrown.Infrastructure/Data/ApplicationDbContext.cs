using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AshenCrown.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AshenCrown.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Mission> Missions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mission>().HasData(
                new Mission
                {
                    Id = 1,
                    Content = "The darkness is going, We need to ....",
                    IsComplete = false
                });
        }
    }
}
