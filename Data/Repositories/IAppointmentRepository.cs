using WebApplication1.Models;

public interface IAppointmentRepository
{
    Task<List<Appointment>> GetUserAppointmentsAsync(string userId);
    Task<Appointment?> GetByIdAsync(int id);
    Task AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(Appointment appointment);
    Task SaveChangesAsync();
}