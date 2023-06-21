using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Required, StringLength(50)]
        public string CustomerName { get; set; }
        [Required, StringLength(15)]
        public string Contact { get; set; }
    }
}
