using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<ExpertRequest> ExpertRequests { get; set; }
        
        public DbSet<Contact> Contacts { get; set; }
        
        public DbSet<Appointment> Appointments { get; set; } 
        
        public DbSet<Review> Reviews { get; set; }
        
        public DbSet<Payment> Payments { get; set; }
        
        
    }
    
}