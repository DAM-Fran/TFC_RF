using TFC_RelevosFamiliares.Models.Appointment;

namespace TFC_RelevosFamiliares.Interfaces;

public interface IReservationService
{
    Task<List<Appointment>> GetMyReservationsAsync(string clientId);
    Task<bool> CreateReservationAsync(Appointment reserva);
    Task<bool> CancelReservationAsync(string reservationId);
}
