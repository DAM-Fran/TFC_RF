using TFC_RelevosFamiliares.Models.Caregiver;

namespace TFC_RelevosFamiliares.Models.User;

public class User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserInfo UserInfo { get; set; }
    public CaregiverInfo CaregiverInfo { get; set; }
}
