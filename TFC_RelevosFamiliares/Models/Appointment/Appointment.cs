namespace TFC_RelevosFamiliares.Models.Appointment;

public class Appointment
{
    public string Id { get; set; }
    public string ClientId { get; set; }
    public string CaregiverId { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
    public double? ReviewRating { get; set; }
    public string Review { get; set; }
}
