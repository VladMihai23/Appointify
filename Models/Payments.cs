using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }  
        
        [Required]
        public decimal Amount { get; set; }  
        public string CardNumber { get; set; }  
        
        public string CardHolderName { get; set; }  
        
        public string ExpiryDate { get; set; }  
        
        public string CVV { get; set; }  
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}