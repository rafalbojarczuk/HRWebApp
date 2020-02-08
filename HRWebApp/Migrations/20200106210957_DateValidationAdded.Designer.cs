﻿// <auto-generated />
using System;
using HRWebApp.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HRWebApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200106210957_DateValidationAdded")]
    partial class DateValidationAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HRWebApp.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new { Id = 1, Name = "Microsoft" },
                        new { Id = 2, Name = "Januszex" },
                        new { Id = 3, Name = "Google" },
                        new { Id = 4, Name = "Comarch" }
                    );
                });

            modelBuilder.Entity("HRWebApp.Models.JobApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ContactAgreement");

                    b.Property<string>("CvUrl")
                        .IsRequired();

                    b.Property<string>("EmailAddress")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int?>("JobOfferId");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int>("OfferId");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("JobOfferId");

                    b.ToTable("JobApplications");

                    b.HasData(
                        new { Id = 1, ContactAgreement = true, CvUrl = "www.example.com", EmailAddress = "xxx@interia.pl", FirstName = "Janusz", LastName = "Nowak", OfferId = 1, PhoneNumber = "505234432" },
                        new { Id = 2, ContactAgreement = true, CvUrl = "www.example.pl", EmailAddress = "xyz@gmail.com", FirstName = "Martyna", LastName = "Szczecina", OfferId = 1, PhoneNumber = "503125339" },
                        new { Id = 3, ContactAgreement = true, CvUrl = "www.foteczka.pl", EmailAddress = "asd@o2.com", FirstName = "Katarzyna", LastName = "Jarzyna", OfferId = 3, PhoneNumber = "700124689" },
                        new { Id = 4, ContactAgreement = false, CvUrl = "www.stockphoto.com/lalala", EmailAddress = "abc@buziaczek.com", FirstName = "Karol", LastName = "Janik", OfferId = 2, PhoneNumber = "669312339" }
                    );
                });

            modelBuilder.Entity("HRWebApp.Models.JobOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("JobTitle")
                        .IsRequired();

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<int>("SalaryFrom");

                    b.Property<int>("SalaryTo");

                    b.Property<DateTime>("ValidUntil");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("JobOffers");

                    b.HasData(
                        new { Id = 1, CompanyId = 1, Created = new DateTime(2020, 1, 6, 22, 9, 57, 253, DateTimeKind.Local), Description = "Praca w młodym dynamicznym zespole", JobTitle = "Back-end Developer", Location = "Warsaw", SalaryFrom = 3000, SalaryTo = 5000, ValidUntil = new DateTime(2020, 4, 3, 22, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 2, CompanyId = 2, Created = new DateTime(2020, 1, 6, 22, 9, 57, 255, DateTimeKind.Local), Description = "Pracuj u nas, dajemy pizzę za darmo!", JobTitle = "Front-end Developer", Location = "Cracow", SalaryFrom = 8000, SalaryTo = 10500, ValidUntil = new DateTime(2021, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 3, CompanyId = 4, Created = new DateTime(2020, 1, 6, 22, 9, 57, 255, DateTimeKind.Local), Description = "Poszukujemy kandydata który posiada minimum 30 lat doświadczenia", JobTitle = "Data analyst", Location = "Madrid", SalaryFrom = 5000, SalaryTo = 6000, ValidUntil = new DateTime(2020, 1, 30, 12, 30, 0, 0, DateTimeKind.Unspecified) },
                        new { Id = 4, CompanyId = 1, Created = new DateTime(2020, 1, 6, 22, 9, 57, 255, DateTimeKind.Local), Description = "Oferujemy nieograniczone możliwości rozwoju", JobTitle = "Graphic designer", Location = "Berlin", SalaryFrom = 2000, SalaryTo = 3000, ValidUntil = new DateTime(2020, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                    );
                });

            modelBuilder.Entity("HRWebApp.Models.JobApplication", b =>
                {
                    b.HasOne("HRWebApp.Models.JobOffer")
                        .WithMany("JobApplications")
                        .HasForeignKey("JobOfferId");
                });

            modelBuilder.Entity("HRWebApp.Models.JobOffer", b =>
                {
                    b.HasOne("HRWebApp.Models.Company")
                        .WithMany("JobOffers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
