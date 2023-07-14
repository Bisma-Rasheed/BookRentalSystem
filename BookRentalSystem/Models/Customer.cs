using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookRentalSystem.Models
{
    [NotMapped]
    public class Customer : IdentityUser
    {
    }
}
