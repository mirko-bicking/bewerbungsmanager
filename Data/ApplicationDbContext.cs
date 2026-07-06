using System.IO;
using JobApplicationTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<JobApplication> JobApplications { get; set; }

        private static string GetDatabasePath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string applicationFolder = Path.Combine(appDataPath, "Bewerbungsmanager");

            Directory.CreateDirectory(applicationFolder);

            return Path.Combine(applicationFolder, "jobapplications.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath = GetDatabasePath();

            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }
    }
}