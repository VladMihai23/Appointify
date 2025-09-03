using WebApplication1.Models;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Appointment>> GetAppointmentsByUserAsync(string userId)
    {
        return await _repository.GetUserAppointmentsAsync(userId);
    }

    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAppointmentAsync(Appointment appointment)
    {
        await _repository.AddAsync(appointment);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAppointmentAsync(Appointment appointment)
    {
        await _repository.UpdateAsync(appointment);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAppointmentAsync(Appointment appointment)
    {
        await _repository.DeleteAsync(appointment);
        await _repository.SaveChangesAsync();
    }
}