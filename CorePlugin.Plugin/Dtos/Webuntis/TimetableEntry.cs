namespace CorePlugin.Plugin.Dtos.Webuntis;

public class TimetableEntry
{

    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public List<int>? ClassIds { get; set; }
    public List<int>? RoomIds { get; set; }
    public List<int>? SubjectIds { get; set; }
    public List<int>? TeacherIds { get; set; }
    public string? LessonType { get; set; }
    public string? Code { get; set; }
    public string? SubstitutionText { get; set; }
}
