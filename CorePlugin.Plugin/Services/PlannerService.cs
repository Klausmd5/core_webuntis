﻿using CorePlugin.Plugin.Dtos;
using DateTime = System.DateTime;

namespace CorePlugin.Plugin.Services;

public class PlannerService
{
    private readonly WebuntisService _webuntisService;

    public PlannerService(
        WebuntisService webuntisService
    )
    {
        _webuntisService = webuntisService;
    }

    public IEnumerable<GapDto> FindGaps(FindGapsModel findGapsModel)
    {
        var gaps = new List<GapDto>();

        var teacherTimetableEntries = findGapsModel.TeacherIds.Select(x => x.Id)
            .ToDictionary(x => x, x => _webuntisService.GetTeacherTimetable(x, findGapsModel.From, findGapsModel.To));
        var studentTimetableEntries = findGapsModel.StudentIds.Select(x => x.Id)
            .ToDictionary(x => x, x => _webuntisService.GetStudentTimetable(x, findGapsModel.From, findGapsModel.To));
        var allTimetableEntries = teacherTimetableEntries.ToDictionary(x => $"T{x.Key}", x => x.Value)
            .Concat(studentTimetableEntries.ToDictionary(x => $"S{x.Key}", x => x.Value));

        var edges = GetEdges(allTimetableEntries, findGapsModel.StartToleranceMinutes, findGapsModel.EndToleranceMinutes, findGapsModel.MinDurationMinutes)
            .ToList();

        for (var i = 0; i < edges.Count - 1;)
        {
            var startEdge = edges[i];
            var endEdge = edges[i + 1];
            edges.RemoveAt(i);
            edges.RemoveAt(i);

            var freeTeacherIds = teacherTimetableEntries
                .Where(x => !x.Value.Any(y => IsBetween(startEdge, y.Start, y.End)))
                .Select(x => x.Key)
                .ToList();
            var freeStudentIds = studentTimetableEntries
                .Where(x => !x.Value.Any(y => IsBetween(startEdge, y.Start, y.End)))
                .Select(x => x.Key)
                .ToList();

            gaps.Add(new GapDto
            {
                Start = startEdge,
                End = endEdge,
                FreeTeacherIds = freeTeacherIds,
                FreeStudentIds = freeStudentIds
            });
        }

        var forcedTeacherIds = findGapsModel.TeacherIds.Where(x => x.Forced)
            .Select(x => x.Id);
        var forcedStudentIds = findGapsModel.StudentIds.Where(x => x.Forced)
            .Select(x => x.Id);

        return gaps.Where(x => forcedTeacherIds.All(y => x.FreeTeacherIds.Contains(y)) && forcedStudentIds.All(y => x.FreeStudentIds.Contains(y)))
            .OrderByDescending(x => x.FreeTeacherIds.Count + x.FreeStudentIds.Count);
    }

    private static IEnumerable<DateTime> GetEdges(IEnumerable<KeyValuePair<string, IEnumerable<WebUntisTimetableEntry>>> timetableEntries,
        int? startToleranceMinutes, int? endToleranceMinutes, int? minDurationMinutes)
    {
        var edges = new List<DateTime>();

        var days = new Dictionary<DateTime, Dictionary<string, List<WebUntisTimetableEntry>>>();
        foreach (var entries in timetableEntries)
        {
            foreach (var entry in entries.Value)
            {
                var day = entry.Start.Date;
                if (!days.ContainsKey(day)) days[day] = new Dictionary<string, List<WebUntisTimetableEntry>>();
                if (!days[day].ContainsKey(entries.Key)) days[day][entries.Key] = new List<WebUntisTimetableEntry>();
                days[day][entries.Key].Add(entry);
            }
        }

        foreach (var day in days)
        {
            var start = day.Value.Max(x => x.Value.Min(y => y.Start));
            var end = day.Value.Min(x => x.Value.Max(y => y.End));

            if (startToleranceMinutes != 0 && startToleranceMinutes >= (minDurationMinutes ?? 0))
            {
                edges.Add(start);
                edges.Add(start.AddMinutes(startToleranceMinutes.Value * -1));
            }

            if (endToleranceMinutes != 0 && endToleranceMinutes >= (minDurationMinutes ?? 0))
            {
                edges.Add(end);
                edges.Add(end.AddMinutes(endToleranceMinutes.Value));
            }

            var earliestStart = start.AddMinutes((startToleranceMinutes ?? 0) * -1);
            var latestEnd = end.AddMinutes(endToleranceMinutes ?? 0);

            foreach (var timetableEntriesByDay in day.Value)
            {
                foreach (var entry in timetableEntriesByDay.Value)
                {
                    if (!edges.Any(x => IsSame(x, entry.Start))
                        && earliestStart.CompareTo(entry.Start) < 0 && latestEnd.CompareTo(entry.Start) > 0
                        && !IsSame(earliestStart, entry.Start, minDurationMinutes ?? 0)
                        && timetableEntriesByDay.Value.All(x => !IsSame(x.End, entry.Start, minDurationMinutes ?? 0)))
                    {
                        edges.Add(entry.Start);
                    }

                    if (!edges.Any(x => IsSame(x, entry.End))
                        && earliestStart.CompareTo(entry.End) < 0 && latestEnd.CompareTo(entry.End) > 0 && !IsSame(latestEnd, entry.End, minDurationMinutes ?? 0)
                        && timetableEntriesByDay.Value.All(x => !IsSame(x.Start, entry.End, minDurationMinutes ?? 0)))
                    {
                        edges.Add(entry.End);
                    }
                }
            }
        }

        return edges.OrderBy(x => x);
    }

    private static bool IsSame(DateTime dateTime1, DateTime dateTime2, int toleranceMinutes = 0)
    {
        return Math.Abs((int)(dateTime1 - dateTime2).TotalMinutes) <= toleranceMinutes;
    }

    private static bool IsBetween(DateTime value, DateTime start, DateTime end)
    {
        return (int)(value - start).TotalMinutes > 0 && (int)(end - value).TotalMinutes > 0;
    }
}