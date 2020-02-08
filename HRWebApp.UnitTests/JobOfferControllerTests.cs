using Microsoft.VisualStudio.TestTools.UnitTesting;
using HRWebApp.Controllers;
using HRWebApp.Models;
using Moq;
using HRWebApp.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace HRWebApp.UnitTests
{
    [TestClass]
    public class JobOfferControllerTests
    {

        private DataContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            DataContext context = new DataContext(options);
            Company microsoft = new Company { Id = 1, Name = "Microsoft" };
            Company januszex = new Company { Id = 2, Name = "Januszex" };
            context.Companies.Add(microsoft);
            context.Companies.Add(januszex);
            context.JobOffers.Add(new JobOffer { Id = 1, Created = DateTime.Now, CompanyId = 1, Description = "asd", JobTitle = "Back-end Developer", Location = "Warsaw", SalaryFrom = 3000, SalaryTo = 5000, ValidUntil = new DateTime(2020, 4, 3, 12, 0, 0) });
            context.JobOffers.Add(new JobOffer { Id = 2, Created = DateTime.Now, CompanyId = 2, Description = "test1", JobTitle = "Front-end Developer", Location = "Cracow", SalaryFrom = 8000, SalaryTo = 10500, ValidUntil = new DateTime(2021, 2, 23, 6, 30, 0) });
            context.JobOffers.Add(new JobOffer { Id = 3, Created = DateTime.Now, CompanyId = 2, Description = "lalala", JobTitle = "Data analyst", Location = "Montreal", SalaryFrom = 5000, SalaryTo = 6000, ValidUntil = new DateTime(2020, 1, 30, 22, 0, 0) });
            context.JobOffers.Add(new JobOffer { Id = 4, Created = DateTime.Now, CompanyId = 1, Description = "despriptioooon", JobTitle = "Graphic designer", Location = "Vienna", SalaryFrom = 2000, SalaryTo = 3000, ValidUntil = new DateTime(2020, 3, 12, 0, 0, 0) });



            context.JobApplications.Add(new JobApplication { Id = 1, ContactAgreement = true, CvUrl = "www.example.com", EmailAddress = "xxx@gmail.com", FirstName = "Janusz", LastName = "Nowak", OfferId = 1, PhoneNumber = "505234432" });
            context.JobApplications.Add(new JobApplication { Id = 2, ContactAgreement = true, CvUrl = "www.example.pl", EmailAddress = "xyz@gmail.com", FirstName = "Martyna", LastName = "Szczecina", OfferId = 1, PhoneNumber = "50312339" });

            context.SaveChanges();

            return context;
        }
        [TestMethod]
        public async Task Index_GivenNoSearchString_ReturnsCorrectViewModel()
        {
            //Arrange
            var context = GetContextWithData();
            var controller = new JobOfferController(context);

            //Act
            var result = await controller.Index(null) as ViewResult;
            var model = result.Model as IndexViewModel;

            //Assert
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.JobOffers[0].JobApplications);
            Assert.AreEqual(2, model.Companies.Count);
            Assert.AreEqual(4, model.JobOffers.Count);
        }

        [TestMethod]
        public async Task Index_GivenSearchString_ReturnsCorrectViewModel()
        {
            //Arrange
            var context = GetContextWithData();
            var controller = new JobOfferController(context);

            //Act
            var result = await controller.Index("Develo") as ViewResult;
            var model = result.Model as IndexViewModel;
            //Assert
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.JobOffers[0].JobApplications);
            Assert.AreEqual(2, model.Companies.Count);
            Assert.AreEqual(2, model.JobOffers.Count);
        }

        [TestMethod]
        public void Create_GivenInvalidOffer_DoesntAddItToDB()
        {
            //Arrange
            var context = GetContextWithData();
            var controller = new JobOfferController(context);
            int jobOffersAtStart = context.JobOffers.Count();
            List<Company> companies = context.Companies.ToList();
            //SaralyFrom > SalaryTo
            CreateViewModel cvm1 = new CreateViewModel { Companies = companies, Id = 5, SalaryFrom = 3000, SalaryTo = 100, JobTitle = "ExampleJobTitle", CompanyId = 1, Created = DateTime.Now, Description = "blahblah", Location = "London", ValidUntil = new DateTime(2020, 3, 12, 0, 0, 0) };
            //MissingValues
            CreateViewModel cvm2 = new CreateViewModel { Companies = companies, Id = 6, SalaryTo = 100, JobTitle = "ExampleJobTitle", Created = DateTime.Now, Description = "blahblah", Location = "London", ValidUntil = new DateTime(2020, 3, 12, 0, 0, 0) };
            //ValidUntil is past
            CreateViewModel cvm3 = new CreateViewModel { Companies = companies, Id = 7, ValidUntil = new DateTime(1999, 3, 12, 0, 0, 0), SalaryFrom = 3000, SalaryTo = 100, JobTitle = "ExampleJobTitle,", CompanyId = 1, Created = DateTime.Now, Description = "blahblah", Location = "London" };

            //Act
            _ = controller.Create(cvm1);
            _ = controller.Create(cvm2);
            _ = controller.Create(cvm3);
            int jobOffersAfter = context.JobOffers.Count();

            //Assert
            Assert.AreEqual(jobOffersAtStart, jobOffersAfter);
        }

        [TestMethod]
        public void Apply_ToNonExistingOffer_ReturnsBadRequest()
        {
            //Arrange
            var context = GetContextWithData();
            var controller = new JobOfferController(context);


            //Act
            var result = controller.Apply(-1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));

        }
    }
}
