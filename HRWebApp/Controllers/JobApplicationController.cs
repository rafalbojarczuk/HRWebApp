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
    public class JobApplicationController : Controller
    {
        private readonly DataContext _context;

        public JobApplicationController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobApplications.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            JobApplication ja = await _context.JobApplications.FirstOrDefaultAsync(x => x.Id == id);
            return View(ja);
        }

        public async Task<ActionResult> Delete(int id)
        {
            JobApplication ja = await _context.JobApplications.FirstOrDefaultAsync(x => x.Id == id);
            if (ja == null)
            {
                return NotFound("Offer not found");
            }
            _context.JobApplications.Remove(ja);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                JobApplication ja = await _context.JobApplications.FirstOrDefaultAsync(x => x.Id == id);
                return View(ja);
            }
            catch (Exception e)
            {
                return BadRequest("Offer with this id doesn't exist!");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(JobApplication model)
        {
            JobApplication ja = await _context.JobApplications.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (ja == null)
            {
                return NotFound("Offer not found");
            }
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            ja.EmailAddress = model.EmailAddress;
            ja.PhoneNumber = model.PhoneNumber;
            ja.ContactAgreement = model.ContactAgreement;
            ja.CvUrl = model.CvUrl;
            _context.Update(ja);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = model.Id });
        }
    }
}