using System.ComponentModel.DataAnnotations.Schema;

namespace CorePlugin.DbLib;

public class Meeting
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string Subject { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string Location { get; set; } = null!;

    public virtual List<MeetingTeacher> Teachers { get; } = new();
    public virtual List<MeetingStudent> Students { get; } = new();
}
