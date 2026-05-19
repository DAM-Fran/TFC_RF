namespace TFC_RelevosFamiliares.Models.Appointment;

public class AppointmentCreateRequest
{
    public string ClientId { get; set; }
    public string CaregiverId { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string Description { get; set; }
}
