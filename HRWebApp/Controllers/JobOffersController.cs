using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRWebApp.EntityFramework;
using HRWebApp.Models;

namespace HRWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOffersController : ControllerBase
    {
        private readonly DataContext _context;

        public JobOffersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/JobOffers
        /// <summary>
        /// Retrieve all available Job Offers.
        /// </summary>
        /// <returns>All available Job Offers in JSON format</returns>
        [HttpGet]
        public IEnumerable<JobOffer> GetJobOffers()
        {
            return _context.JobOffers;
        }


        // GET: api/JobOffers/5
        /// <summary>
        /// Retrieve a Job Offer by its ID.
        /// </summary>
        /// <param name="id">The ID of the desired Job Offer</param>
        /// <returns>Job Offer data in JSON format</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobOffer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobOffer = await _context.JobOffers.FindAsync(id);

            if (jobOffer == null)
            {
                return NotFound();
            }

            return Ok(jobOffer);
        }

        // PUT: api/JobOffers/5
        /// <summary>
        /// Updates or creates Job Offer
        /// </summary>
        /// <remarks>
        /// Indepotent method, putting the same object multiple times has the same effet as putting one.
        /// </remarks>
        /// <param name="id">ID of existing or new Job Offer</param>
        /// <param name="jobOffer">Job Offer instance to add or update</param>
        /// <returns>204 No Content response if updated Job Offer is valid, 400 Bad Request otherwise</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobOffer([FromRoute] int id, [FromBody] JobOffer jobOffer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobOffer.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobOffer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobOfferExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/JobOffers
        /// <summary>
        /// Creates new Job Offer
        /// </summary>
        /// <param name="jobOffer">new Job Offer instance</param>
        /// <returns>new Job Offer with 201 Created response if Job Offer is valid, 400 Bad Request otherwise</returns>
        [HttpPost]
        public async Task<IActionResult> PostJobOffer([FromBody] JobOffer jobOffer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.JobOffers.Add(jobOffer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobOffer", new { id = jobOffer.Id }, jobOffer);
        }

        // DELETE: api/JobOffers/5
        /// <summary>
        /// Deletes a Job Offer with given ID.
        /// </summary>
        /// <param name="id">ID of Job Offer to delete</param>
        /// <returns>Deleted Job Offer data in JSON format</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobOffer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer == null)
            {
                return NotFound();
            }

            _context.JobOffers.Remove(jobOffer);
            await _context.SaveChangesAsync();

            return Ok(jobOffer);
        }

        private bool JobOfferExists(int id)
        {
            return _context.JobOffers.Any(e => e.Id == id);
        }
    }
}