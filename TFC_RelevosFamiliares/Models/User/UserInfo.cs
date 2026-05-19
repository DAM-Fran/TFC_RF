using TFC_RelevosFamiliares.Models.Geo;

namespace TFC_RelevosFamiliares.Models.User;

public class UserInfo
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Description { get; set; }
    public string ProfilePictureUri { get; set; }
    public GeoPoint Location { get; set; }
}
