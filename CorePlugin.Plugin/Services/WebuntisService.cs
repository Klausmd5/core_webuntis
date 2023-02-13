using CorePlugin.Plugin.Dtos;

namespace CorePlugin.Plugin.Services;

public class WebuntisService
{
    public IEnumerable<WebUntisTimetableEntry> GetTeacherTimetable(int id, DateTime from, DateTime to)
    {
        return new[]
        {
            new WebUntisTimetableEntry
            {
                Start = DateTime.Now,
                End = DateTime.Now.Add(TimeSpan.FromHours(2))
            },
            new WebUntisTimetableEntry
            {
                Start = DateTime.Now.Add(TimeSpan.FromHours(3)),
                End = DateTime.Now.Add(TimeSpan.FromHours(5))
            },
            new WebUntisTimetableEntry
            {
                Start = DateTime.Now.AddDays(1),
                End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(2))
            },
            new WebUntisTimetableEntry
            {
                Start = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(3)),
                End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(5))
            }
        };
    }

    public IEnumerable<WebUntisTimetableEntry> GetStudentTimetable(int id, DateTime from, DateTime to)
    {
        return id switch
        {
            1 => new[]
            {
                new WebUntisTimetableEntry { Start = DateTime.Now, End = DateTime.Now.Add(TimeSpan.FromHours(1)) },
                new WebUntisTimetableEntry
                {
                    Start = DateTime.Now.Add(TimeSpan.FromHours(1)),
                    End = DateTime.Now.Add(TimeSpan.FromHours(2))
                },
                new WebUntisTimetableEntry
                {
                    Start = DateTime.Now.Add(TimeSpan.FromHours(3)),
                    End = DateTime.Now.Add(TimeSpan.FromHours(6))
                },
                new WebUntisTimetableEntry
                {
                    Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(1))
                },
                new WebUntisTimetableEntry
                {
                    Start = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(1)),
                    End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(2))
                },
                new WebUntisTimetableEntry
                {
                    Start = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(3)),
                    End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(6))
                }
            },
            2 => new[]
            {
                new WebUntisTimetableEntry
                {
                    Start = DateTime.Now.Add(TimeSpan.FromHours(3)),
                    End = DateTime.Now.Add(TimeSpan.FromHours(5))
                },
                new WebUntisTimetableEntry
                {
                    Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(3))
                },
                new WebUntisTimetableEntry
                {
                    Start = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(3)),
                    End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(5))
                }
            },
            _ => new WebUntisTimetableEntry[] { }
        };
    }
}
