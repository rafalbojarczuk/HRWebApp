using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRWebApp.Migrations
{
    public partial class AddRestOfTheData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JobApplications",
                columns: new[] { "Id", "ContactAgreement", "CvUrl", "EmailAddress", "FirstName", "JobOfferId", "LastName", "OfferId", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, true, "www.example.com", "xxx@interia.pl", "Janusz", null, "Nowak", 1, "505234432" },
                    { 2, true, "www.example.pl", "xyz@gmail.com", "Martyna", null, "Szczecina", 1, "503125339" },
                    { 3, true, "www.foteczka.pl", "asd@o2.com", "Katarzyna", null, "Jarzyna", 3, "700124689" },
                    { 4, false, "www.stockphoto.com/lalala", "abc@buziaczek.com", "Karol", null, "Janik", 2, "669312339" }
                });

            migrationBuilder.InsertData(
                table: "JobOffers",
                columns: new[] { "Id", "CompanyId", "Created", "Description", "JobTitle", "Location", "SalaryFrom", "SalaryTo", "ValidUntil" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 1, 5, 19, 37, 11, 523, DateTimeKind.Local), "Praca w młodym dynamicznym zespole", "Back-end Developer", "Warsaw", 3000, 5000, new DateTime(2020, 4, 3, 22, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2020, 1, 5, 19, 37, 11, 525, DateTimeKind.Local), "Pracuj u nas, dajemy pizzę za darmo!", "Front-end Developer", "Cracow", 8000, 10500, new DateTime(2021, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 4, new DateTime(2020, 1, 5, 19, 37, 11, 525, DateTimeKind.Local), "Poszukujemy kandydata który posiada minimum 30 lat doświadczenia", "Data analyst", "Madrid", 5000, 6000, new DateTime(2020, 1, 30, 12, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, new DateTime(2020, 1, 5, 19, 37, 11, 525, DateTimeKind.Local), "Oferujemy nieograniczone możliwości rozwoju", "Graphic designer", "Berlin", 2000, 3000, new DateTime(2020, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
