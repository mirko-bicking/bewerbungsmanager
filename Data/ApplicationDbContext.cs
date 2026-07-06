using Microsoft.EntityFrameworkCore;
using JobApplicationTracker.Models;

namespace JobApplicationTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=jobapplications.db");
        }
    }
}