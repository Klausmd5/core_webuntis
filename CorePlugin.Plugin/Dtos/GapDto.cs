namespace CorePlugin.Plugin.Dtos;

public class GapDto
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public List<int> FreeTeacherIds { get; set; } = null!;
    public List<int> FreeStudentIds { get; set; } = null!;
}
