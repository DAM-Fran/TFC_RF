namespace TFC_RelevosFamiliares.Models.Caregiver;

public class Availability
{
    public List<TimeRange> Monday { get; set; }
    public List<TimeRange> Tuesday { get; set; }
    public List<TimeRange> Wednesday { get; set; }
    public List<TimeRange> Thursday { get; set; }
    public List<TimeRange> Friday { get; set; }
    public List<TimeRange> Saturday { get; set; }
    public List<TimeRange> Sunday { get; set; }
}
