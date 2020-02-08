using HRWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRWebApp.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Microsoft" },
                new Company { Id = 2, Name = "Januszex" },
                new Company { Id = 3, Name = "Google" },
                new Company { Id = 4, Name = "Comarch" }
                );
            modelBuilder.Entity<JobOffer>().HasData(
                new JobOffer { Id = 1, Created = DateTime.Now, CompanyId = 1, Description = "Praca w młodym dynamicznym zespole", JobTitle = "Back-end Developer", Location = "Warsaw", SalaryFrom = 3000, SalaryTo = 5000, ValidUntil = new DateTime(2020, 4, 3,22,0,0) },
                new JobOffer { Id = 2, Created = DateTime.Now, CompanyId = 2, Description = "Pracuj u nas, dajemy pizzę za darmo!", JobTitle = "Front-end Developer", Location = "Cracow", SalaryFrom = 8000, SalaryTo = 10500, ValidUntil = new DateTime(2021, 2, 23,0,0,0) },
                new JobOffer { Id = 3, Created = DateTime.Now, CompanyId = 4, Description = "Poszukujemy kandydata który posiada minimum 30 lat doświadczenia", JobTitle = "Data analyst", Location = "Madrid", SalaryFrom = 5000, SalaryTo = 6000, ValidUntil = new DateTime(2020, 1, 30,12,30,0) },
                new JobOffer { Id = 4, Created = DateTime.Now, CompanyId = 1, Description = "Oferujemy nieograniczone możliwości rozwoju", JobTitle = "Graphic designer", Location = "Berlin", SalaryFrom = 2000, SalaryTo = 3000, ValidUntil = new DateTime(2020, 3, 12,0,0,0) }
                );
            modelBuilder.Entity<JobApplication>().HasData(
                new JobApplication { Id = 1, ContactAgreement = true, CvUrl = "www.example.com", EmailAddress = "xxx@interia.pl", FirstName = "Janusz", LastName = "Nowak", OfferId = 1, PhoneNumber = "505234432" },
                new JobApplication { Id = 2, ContactAgreement = true, CvUrl = "www.example.pl", EmailAddress = "xyz@gmail.com", FirstName = "Martyna", LastName = "Szczecina", OfferId = 1, PhoneNumber = "503125339" },
                new JobApplication { Id = 3, ContactAgreement = true, CvUrl = "www.foteczka.pl", EmailAddress = "asd@o2.com", FirstName = "Katarzyna", LastName = "Jarzyna", OfferId = 3, PhoneNumber = "700124689" },
                new JobApplication { Id = 4, ContactAgreement = false, CvUrl = "www.stockphoto.com/lalala", EmailAddress = "abc@buziaczek.com", FirstName = "Karol", LastName = "Janik", OfferId = 2, PhoneNumber = "669312339" }

                );

        }
    }
}
