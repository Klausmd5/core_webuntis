using CorePlugin.Plugin.Dtos;
using CorePlugin.Plugin.Dtos.Webuntis;

namespace CorePlugin.Plugin.Services;

public class WebuntisService
{
    public IEnumerable<Teacher> GetTeachers()
    {
        return new[]
        {
            new Teacher
            {
                Id = 1,
                FirstName = "Klaus",
                LastName = "Aigner"
            },
            new Teacher
            {
                Id = 2,
                FirstName = "Karin",
                LastName = "Allerstorfer"
            },
            new Teacher
            {
                Id = 3,
                FirstName = "Peter",
                LastName = "Anzenberger"
            },
            new Teacher
            {
                Id = 4,
                FirstName = "Andreas",
                LastName = "Baumgartner"
            }
        };
    }

    public IEnumerable<Student> GetStudents()
    {
        return new[]
        {
            new Student
            {
                Id = 1,
                FirstName = "Tim",
                LastName = "Sturm"
            },
            new Student
            {
                Id = 2,
                FirstName = "Tamara",
                LastName = "Wimmer"
            },
            new Student
            {
                Id = 3,
                FirstName = "Simon",
                LastName = "Feichtlbauer"
            },
            new Student
            {
                Id = 4,
                FirstName = "Andreas",
                LastName = "Aigner"
            }
        };
    }

    public IEnumerable<TimetableEntry> GetTeacherTimetable(int id, DateTime from, DateTime to)
    {
        return new[]
        {
            new TimetableEntry
            {
                Start = DateTime.Now,
                End = DateTime.Now.Add(TimeSpan.FromHours(2))
            },
            new TimetableEntry
            {
                Start = DateTime.Now.Add(TimeSpan.FromHours(3)),
                End = DateTime.Now.Add(TimeSpan.FromHours(5))
            },
            new TimetableEntry
            {
                Start = DateTime.Now.AddDays(1),
                End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(2))
            },
            new TimetableEntry
            {
                Start = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(3)),
                End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(5))
            }
        };
    }

    public IEnumerable<TimetableEntry> GetStudentTimetable(int id, DateTime from, DateTime to)
    {
        return id switch
        {
            1 => new[]
            {
                new TimetableEntry { Start = DateTime.Now, End = DateTime.Now.Add(TimeSpan.FromHours(1)) },
                new TimetableEntry
                {
                    Start = DateTime.Now.Add(TimeSpan.FromHours(1)),
                    End = DateTime.Now.Add(TimeSpan.FromHours(2))
                },
                new TimetableEntry
                {
                    Start = DateTime.Now.Add(TimeSpan.FromHours(3)),
                    End = DateTime.Now.Add(TimeSpan.FromHours(6))
                },
                new TimetableEntry
                {
                    Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(1))
                },
                new TimetableEntry
                {
                    Start = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(1)),
                    End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(2))
                },
                new TimetableEntry
                {
                    Start = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(3)),
                    End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(6))
                }
            },
            2 => new[]
            {
                new TimetableEntry
                {
                    Start = DateTime.Now.Add(TimeSpan.FromHours(3)),
                    End = DateTime.Now.Add(TimeSpan.FromHours(5))
                },
                new TimetableEntry
                {
                    Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(3))
                },
                new TimetableEntry
                {
                    Start = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(3)),
                    End = DateTime.Now.AddDays(1).Add(TimeSpan.FromHours(5))
                }
            },
            _ => new TimetableEntry[] { }
        };
    }
}
