using JobApplicationTracker.Data;
using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services
{
    public class JobApplicationService
    {
        public List<JobApplication> GetApplications(string searchText, string selectedStatus)
        {
            using var db = new ApplicationDbContext();
            db.Database.EnsureCreated();

            var query = db.JobApplications.AsQueryable();

            searchText = searchText.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(application =>
                    application.CompanyName.ToLower().Contains(searchText) ||
                    application.PositionTitle.ToLower().Contains(searchText) ||
                    application.ContactPerson.ToLower().Contains(searchText) ||
                    application.ContactEmail.ToLower().Contains(searchText) ||
                    application.Notes.ToLower().Contains(searchText));
            }

            if (selectedStatus != "Alle")
            {
                query = query.Where(application => application.Status == selectedStatus);
            }

            return query
                .OrderByDescending(application => application.ApplicationDate)
                .ToList();
        }

        public void AddApplication(JobApplication application)
        {
            using var db = new ApplicationDbContext();
            db.Database.EnsureCreated();

            db.JobApplications.Add(application);
            db.SaveChanges();
        }

        public void UpdateApplication(JobApplication updatedApplication)
        {
            using var db = new ApplicationDbContext();
            db.Database.EnsureCreated();

            var applicationFromDatabase = db.JobApplications
                .FirstOrDefault(application => application.Id == updatedApplication.Id);

            if (applicationFromDatabase is null)
            {
                throw new InvalidOperationException("Die Bewerbung wurde in der Datenbank nicht gefunden.");
            }

            applicationFromDatabase.CompanyName = updatedApplication.CompanyName;
            applicationFromDatabase.PositionTitle = updatedApplication.PositionTitle;
            applicationFromDatabase.ContactPerson = updatedApplication.ContactPerson;
            applicationFromDatabase.ContactEmail = updatedApplication.ContactEmail;
            applicationFromDatabase.ApplicationDate = updatedApplication.ApplicationDate;
            applicationFromDatabase.Status = updatedApplication.Status;
            applicationFromDatabase.Notes = updatedApplication.Notes;

            db.SaveChanges();
        }

        public void DeleteApplication(int applicationId)
        {
            using var db = new ApplicationDbContext();
            db.Database.EnsureCreated();

            var applicationFromDatabase = db.JobApplications
                .FirstOrDefault(application => application.Id == applicationId);

            if (applicationFromDatabase is null)
            {
                throw new InvalidOperationException("Die Bewerbung wurde in der Datenbank nicht gefunden.");
            }

            db.JobApplications.Remove(applicationFromDatabase);
            db.SaveChanges();
        }
        public List<JobApplication> GetAllApplications()
        {
            using var db = new ApplicationDbContext();
            db.Database.EnsureCreated();

            return db.JobApplications.ToList();
        }
    }
}