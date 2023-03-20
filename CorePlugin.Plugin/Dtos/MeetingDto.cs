namespace CorePlugin.Plugin.Dtos;

public class MeetingDto
{
    public long Id { get; set; }

    public string Subject { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string Location { get; set; } = null!;

    public List<int> TeacherIds { get; set; } = null!;
    public List<int> StudentIds { get; set; } = null!;
}
