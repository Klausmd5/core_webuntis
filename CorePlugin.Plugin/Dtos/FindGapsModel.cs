namespace CorePlugin.Plugin.Dtos;

public class FindGapsModel
{
    public IEnumerable<FindGapsModelPerson> Teachers { get; set; } = null!;
    public IEnumerable<FindGapsModelPerson> Students { get; set; } = null!;
    public IEnumerable<FindGapsModelParticipant> TeacherParticipants { get; set; } = null!;
    public IEnumerable<FindGapsModelParticipant> StudentParticipants { get; set; } = null!;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int StartToleranceMinutes { get; set; }
    public int EndToleranceMinutes { get; set; }
    public int MinDurationMinutes { get; set; }
}

public class FindGapsModelPerson
{
    public int Id { get; set; }
    public List<FindGapsModelTimetableEntry> TimetableEntries { get; set; } = null!;
}

public class FindGapsModelTimetableEntry
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

public class FindGapsModelParticipant
{
    public int Id { get; set; }
    public bool Forced { get; set; }
}
