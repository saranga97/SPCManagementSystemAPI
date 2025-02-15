using System;
using System.ComponentModel.DataAnnotations;

namespace SPCManagementSystemAPI.Models
{
    public class Tender
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public string DrugName { get; set; }

        [Required]
        public decimal ProposedPrice { get; set; }

        public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;
    }
}
