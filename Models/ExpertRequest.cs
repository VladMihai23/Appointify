using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ExpertRequest
    {
        [Key]
        public int ExpertId { get; set; }
        
        public string UserId { get; set; }
        
        [Required]
        public string Description {get; set;}

        public bool IsApproved { get; set; } = false; 
    }
}