using CorePlugin.DbLib;
using CorePlugin.Plugin.Dtos;
using CorePlugin.Plugin.Exceptions;
using Microsoft.EntityFrameworkCore;
using DateTime = System.DateTime;

namespace CorePlugin.Plugin.Services;

public class PlannerService
{
    private readonly PlannerContext _plannerContext;

    public PlannerService(
        PlannerContext plannerContext
    )
    {
        _plannerContext = plannerContext;
    }

    public IEnumerable<GapDto> FindGaps(FindGapsModel findGapsModel)
    {
        var gaps = new List<GapDto>();

        var teacherTimetableEntries = findGapsModel.TeacherParticipants
            .Select(x => findGapsModel.Teachers.Single(y => x.Id == y.Id))
            .ToDictionary(x => x.Id, x => x.TimetableEntries);
        var studentTimetableEntries = findGapsModel.StudentParticipants
            .Select(x => findGapsModel.Students.Single(y => x.Id == y.Id))
            .ToDictionary(x => x.Id, x => x.TimetableEntries);
        var allParticipants = teacherTimetableEntries.ToDictionary(x => $"t{x.Key}", x => x.Value)
            .Concat(studentTimetableEntries.ToDictionary(x => $"s{x.Key}", x => x.Value))
            .ToDictionary(x => x.Key, x => x.Value);

        var edgesByDay = GetEdgesByDay(allParticipants, findGapsModel.StartToleranceMinutes, findGapsModel.EndToleranceMinutes)
            .ToList();

        foreach (var edges in edgesByDay.Select(entry => entry.Value))
        {
            for (var i = 0; i < edges.Count - 1; i++)
            {
                var startEdge = edges[i];

                var j = i + 1;
                DateTime endEdge;

                do
                {
                    endEdge = edges[j];
                    j++;
                } while (IsSame(startEdge, endEdge, findGapsModel.MinDurationMinutes, false));

                var freeTeacherIds = teacherTimetableEntries
                    .Where(x => !x.Value.Any(y => startEdge >= y.Start && startEdge < y.End && endEdge <= y.End && endEdge > y.Start))
                    .Select(x => x.Key)
                    .ToList();
                var freeStudentIds = studentTimetableEntries
                    .Where(x => !x.Value.Any(y => startEdge >= y.Start && startEdge < y.End && endEdge <= y.End && endEdge > y.Start))
                    .Select(x => x.Key)
                    .ToList();

                gaps.Add(new GapDto
                {
                    Start = startEdge,
                    End = endEdge,
                    FreeTeacherIds = freeTeacherIds,
                    FreeStudentIds = freeStudentIds,
                });
            }
        }

        var forcedTeacherIds = findGapsModel.TeacherParticipants.Where(x => x.Forced)
            .Select(x => x.Id);
        var forcedStudentIds = findGapsModel.StudentParticipants.Where(x => x.Forced)
            .Select(x => x.Id);

        return gaps.Where(x => forcedTeacherIds.All(y => x.FreeTeacherIds.Contains(y)) && forcedStudentIds.All(y => x.FreeStudentIds.Contains(y)))
            .OrderByDescending(x => x.FreeTeacherIds.Count + x.FreeStudentIds.Count)
            .ThenBy(x => x.Start);
    }

    private static Dictionary<DateTime, List<DateTime>> GetEdgesByDay(Dictionary<string, List<FindGapsModelTimetableEntry>> timetableEntries,
        int? startToleranceMinutes, int? endToleranceMinutes)
    {
        var edgesByDay = new Dictionary<DateTime, List<DateTime>>();

        var days = new Dictionary<DateTime, Dictionary<string, List<FindGapsModelTimetableEntry>>>();
        foreach (var entries in timetableEntries)
        {
            foreach (var entry in entries.Value)
            {
                var day = entry.Start.Date;
                if (!days.ContainsKey(day)) days[day] = new Dictionary<string, List<FindGapsModelTimetableEntry>>();
                if (!days[day].ContainsKey(entries.Key)) days[day][entries.Key] = new List<FindGapsModelTimetableEntry>();
                days[day][entries.Key].Add(entry);
            }
        }

        foreach (var day in days)
        {
            var edges = new List<DateTime>();

            var start = day.Value.Max(x => x.Value.Min(y => y.Start));
            var end = day.Value.Min(x => x.Value.Max(y => y.End));

            var earliestStart = start.AddMinutes((startToleranceMinutes ?? 0) * -1);
            var latestEnd = end.AddMinutes(endToleranceMinutes ?? 0);

            edges.Add(start);
            if (startToleranceMinutes > 0)
                edges.Add(earliestStart);

            edges.Add(end);
            if (endToleranceMinutes > 0)
                edges.Add(latestEnd);

            foreach (var entry in day.Value.SelectMany(timetableEntriesByDay => timetableEntriesByDay.Value))
            {
                if (!edges.Any(x => IsSame(x, entry.Start))
                    && entry.Start > earliestStart && entry.Start < latestEnd)
                    edges.Add(entry.Start);

                if (!edges.Any(x => IsSame(x, entry.End))
                    && entry.End > earliestStart && entry.End < latestEnd)
                    edges.Add(entry.End);
            }

            edgesByDay[day.Key] = edges;
        }

        return edgesByDay.OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value.OrderBy(y => y).ToList());
    }

    private static bool IsSame(DateTime dateTime1, DateTime dateTime2, int toleranceMinutes = 0, bool inclusive = true)
    {
        if (inclusive)
            return Math.Abs((int)(dateTime1 - dateTime2).TotalMinutes) <= toleranceMinutes;
        else
            return Math.Abs((int)(dateTime1 - dateTime2).TotalMinutes) < toleranceMinutes;
    }

    public IEnumerable<MeetingDto> GetMeetings()
    {
        return _plannerContext.Meetings
            .Include(x => x.Teachers)
            .Include(x => x.Students)
            .Select(x => new MeetingDto
            {
                Id = x.Id,
                TeacherIds = x.Teachers
                    .Select(y => y.TeacherId)
                    .ToList(),
                StudentIds = x.Students
                    .Select(y => y.StudentId)
                    .ToList(),
                Subject = x.Subject,
                Description = x.Description,
                From = x.From,
                To = x.To,
                Location = x.Location,
            })
            .ToList();
    }

    public void PlanMeeting(MeetingModel meetingModel)
    {
        if (meetingModel.To <= meetingModel.From)
            throw new BadDateException();

        if (meetingModel.TeacherIds.Count() + meetingModel.StudentIds.Count() < 2)
            throw new NotEnoughParticipants();

        var meeting = _plannerContext.Meetings.Add(
            new Meeting
            {
                Subject = meetingModel.Subject.Trim(),
                Description = meetingModel.Description.Trim(),
                From = meetingModel.From,
                To = meetingModel.To,
                Location = meetingModel.Location.Trim(),
            }
        ).Entity;

        _plannerContext.SaveChanges();

        _plannerContext.MeetingTeachers.AddRange(
            meetingModel.TeacherIds
                .Select(x => new MeetingTeacher
                {
                    MeetingId = meeting.Id,
                    TeacherId = x,
                })
        );

        _plannerContext.MeetingStudents.AddRange(
            meetingModel.StudentIds
                .Select(x => new MeetingStudent
                {
                    MeetingId = meeting.Id,
                    StudentId = x,
                })
        );

        _plannerContext.SaveChanges();
    }
}
