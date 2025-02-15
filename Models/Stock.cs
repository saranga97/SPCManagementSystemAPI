using System;
using System.ComponentModel.DataAnnotations;

namespace SPCManagementSystemAPI.Models
{
    public class Stock
    {
        [Key]
        public string Id { get; set; } 

        [Required]
        public string DrugId { get; set; } 

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string UpdatedBy { get; set; } 

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
