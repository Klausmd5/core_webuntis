namespace CorePlugin.Plugin.Dtos;

public class FindGapsModel
{
    public IEnumerable<FindGapsModelId> TeacherIds { get; set; } = null!;
    public IEnumerable<FindGapsModelId> StudentIds { get; set; } = null!;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int StartToleranceMinutes { get; set; }
    public int EndToleranceMinutes { get; set; }
    public int MinDurationMinutes { get; set; }
}

public class FindGapsModelId
{
    public int Id { get; set; }
    public bool Forced { get; set; }
}
