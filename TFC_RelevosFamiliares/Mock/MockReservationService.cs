using TFC_RelevosFamiliares.Interfaces;
using TFC_RelevosFamiliares.Models.Appointment;

namespace TFC_RelevosFamiliares.Mock;

public class MockReservationService : IReservationService
{
    private readonly List<Appointment> _reservas = new();

    public Task<List<Appointment>> GetMyReservationsAsync(string clientId)
    {
        var result = _reservas
            .Where(r => r.ClientId == clientId)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<bool> CreateReservationAsync(Appointment reserva)
    {
        // Simular ID autogenerado
        reserva.Id ??= Guid.NewGuid().ToString();

        // Simular fecha de creación
        reserva.CreatedAt = DateTime.Now;

        _reservas.Add(reserva);
        return Task.FromResult(true);
    }

    public Task<bool> CancelReservationAsync(string reservationId)
    {
        var reserva = _reservas.FirstOrDefault(r => r.Id == reservationId);
        if (reserva is null)
            return Task.FromResult(false);

        _reservas.Remove(reserva);
        return Task.FromResult(true);
    }
}
