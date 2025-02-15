using System;
using System.ComponentModel.DataAnnotations;

namespace SPCManagementSystemAPI.Models
{
    public class Order
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int PharmacyId { get; set; } 

        [Required]
        public string DrugId { get; set; } 

        [Required]
        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    }
}


