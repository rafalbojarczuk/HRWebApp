using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using HRWebApp.CustomValidation;

namespace HRWebApp.Models
{
    public class JobOffer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        [Display(Name = "Job title")]
        public string JobTitle { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [Display(Name = "Salary from")]
        public int SalaryFrom { get; set; }

        [Required]
        [Display(Name = "Salary to")]
        public int SalaryTo { get; set; }
        public DateTime Created { get; set; }

        [Required]
        [DateNotPastValidation(ErrorMessage ="Please select future date")]
        [Display(Name = "Valid until")]
        public DateTime ValidUntil { get; set; }
        public List<JobApplication> JobApplications { get; set; } = new List<JobApplication>();


    }
}
