using WebApplication1.Models;

public interface IAppointmentService
{
    Task<List<Appointment>> GetAppointmentsByUserAsync(string userId);
    Task<Appointment?> GetByIdAsync(int id);
    Task CreateAppointmentAsync(Appointment appointment);
    Task UpdateAppointmentAsync(Appointment appointment);
    Task DeleteAppointmentAsync(Appointment appointment);
}