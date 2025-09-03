using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class AppUser : IdentityUser
    {
       [Column(TypeName = "timestamp with time zone")]
       public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? Address { get; set; }
        
        public string? ProfilePicturePath { get; set; }
        
    }
}