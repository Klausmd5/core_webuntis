using Microsoft.EntityFrameworkCore;

namespace CorePlugin.DbLib;

public class PlannerContext : DbContext
{
    public PlannerContext(DbContextOptions<PlannerContext> options) : base(options) { }

    public DbSet<Meeting> Meetings { get; set; } = null!;
    public DbSet<MeetingTeacher> MeetingTeachers { get; set; } = null!;
    public DbSet<MeetingStudent> MeetingStudents { get; set; } = null!;
}
