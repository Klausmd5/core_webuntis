using System.ComponentModel.DataAnnotations.Schema;

namespace CorePlugin.DbLib;

public class MeetingTeacher
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public long MeetingId { get; set; }
    public Meeting Meeting { get; set; } = null!;

    public int TeacherId { get; set; }
}
