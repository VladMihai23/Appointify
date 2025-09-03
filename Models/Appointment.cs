using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public AppUser? User { get; set; }
        
        [Required]
        public string ServiceName { get; set; }
        
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";
    }
}