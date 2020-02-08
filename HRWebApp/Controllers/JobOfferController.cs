using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRWebApp.EntityFramework;
using HRWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRWebApp.Controllers
{
    public class JobOfferController : Controller
    {
        private readonly DataContext _context;

        public JobOfferController(DataContext context)
        {
            _context = context;
            _context.JobOffers.Include("JobApplications").ToList();
        }


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery(Name = "search")] string searchString)
        {
            IndexViewModel model = new IndexViewModel();
            model.Companies = await _context.Companies.ToListAsync();
            if (string.IsNullOrEmpty(searchString))
            {

                model.JobOffers = await _context.JobOffers.ToListAsync();
                return View(model);
            }

            var searchResult = from s in _context.JobOffers
                               where s.JobTitle.Contains(searchString)
                               select s;
            model.JobOffers = searchResult.ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                JobOffer jo = await _context.JobOffers.FirstOrDefaultAsync(x => x.Id == id);
                jo.JobApplications = await _context.JobApplications.Where(x => x.OfferId == jo.Id).ToListAsync();
                Company c = await _context.Companies.FirstOrDefaultAsync(x => x.Id == jo.CompanyId);
                DetailsViewModel model = new DetailsViewModel
                {
                    Jo = jo,
                    Company = c.Name
                };
                return View(model);
            }
            catch (Exception e)
            {
                return BadRequest("Offer with this id doesn't exist!");
            }


        }

        public async Task<ActionResult> Apply(int id)
        {
            try
            {
                JobOffer jo = await _context.JobOffers.FirstOrDefaultAsync(x => x.Id == id);

                Company c = await _context.Companies.FirstOrDefaultAsync(x => x.Id == jo.CompanyId);

                ApplyViewModel model = new ApplyViewModel
                {
                    Jo = jo,
                    Company = c.Name
                };
                return View(model);
            }
            catch (Exception e)
            {
                return BadRequest("Offer with this id doesn't exist!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Apply(int id, ApplyViewModel model)
        {
            model.Jo = await _context.JobOffers.FirstOrDefaultAsync(x => x.Id == id);
            Company c = await _context.Companies.FirstOrDefaultAsync(x => x.Id == model.Jo.CompanyId);
            model.Company = c.Name;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            JobApplication ja = new JobApplication
            {
                OfferId = model.Jo.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                ContactAgreement = model.ContactAgreement,
                CvUrl = model.CvUrl,
            };
            await _context.JobApplications.AddAsync(ja);
            _context.Update(model.Jo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = model.Jo.Id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                JobOffer jo = await _context.JobOffers.FirstOrDefaultAsync(x => x.Id == id);
                return View(jo);
            }
            catch (Exception e)
            {
                return BadRequest("Offer with this id doesn't exist!");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(JobOffer model)
        {
            JobOffer jo = await _context.JobOffers.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (jo == null)
            {
                return NotFound("Offer not found");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.SalaryTo < model.SalaryFrom)
            {
                ModelState.AddModelError("", "Salary to must be greater or equal to Salary from.");
                return View(model);
            }

            jo.Description = model.Description;
            jo.ValidUntil = model.ValidUntil;
            jo.SalaryFrom = model.SalaryFrom;
            jo.SalaryTo = model.SalaryTo;
            jo.Location = model.Location;
            _context.Update(jo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = model.Id });
        }

        public async Task<ActionResult> Delete(int id)
        {
            JobOffer jo = await _context.JobOffers.FirstOrDefaultAsync(x => x.Id == id);
            if (jo == null)
            {
                return NotFound("Offer not found");
            }
            _context.JobApplications.RemoveRange(await _context.JobApplications.Where(x => x.OfferId == jo.Id).ToListAsync());
            await _context.SaveChangesAsync();
            _context.JobOffers.Remove(jo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var model = new CreateViewModel
            {
                Companies = await _context.Companies.ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Companies = await _context.Companies.ToListAsync();
                return View(model);
            }
            if (model.SalaryTo < model.SalaryFrom)
            {
                ModelState.AddModelError("", "Salary to must be greater or equal to Salary from.");
                return View(model);
            }
            JobOffer jo = new JobOffer
            {
                CompanyId = model.CompanyId,
                Description = model.Description,
                JobTitle = model.JobTitle,
                Location = model.Location,
                SalaryFrom = model.SalaryFrom,
                SalaryTo = model.SalaryTo,
                ValidUntil = model.ValidUntil,
                Created = DateTime.Now
            };

            _context.JobOffers.Add(jo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

    public class IndexViewModel
    {
        public List<JobOffer> JobOffers { get; set; }
        public List<Company> Companies { get; set; }
    }
    public class CreateViewModel : JobOffer
    {
        public List<Company> Companies { get; set; }
    }

    public class ApplyViewModel : JobApplication
    {
        public JobOffer Jo { get; set; }
        public string Company { get; set; }
    }

    public class DetailsViewModel
    {
        public JobOffer Jo { get; set; }
        public string Company { get; set; }

    }
}