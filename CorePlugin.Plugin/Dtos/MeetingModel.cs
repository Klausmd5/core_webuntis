namespace CorePlugin.Plugin.Dtos;

public class MeetingModel
{
    public IEnumerable<int> TeacherIds { get; set; } = null!;
    public IEnumerable<int> StudentIds { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string Location { get; set; } = null!;
}
