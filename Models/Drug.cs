using System.ComponentModel.DataAnnotations;

namespace SPCManagementSystemAPI.Models
{
    public class Drug
    {
        [Key] 
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }
    }
}

