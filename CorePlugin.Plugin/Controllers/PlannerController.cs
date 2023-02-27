using CorePlugin.Plugin.Dtos;
using CorePlugin.Plugin.Services;
using Microsoft.AspNetCore.Mvc;

namespace CorePlugin.Plugin.Controllers;

[ApiController]
[Route("[controller]")]
public class PlannerController : ControllerBase
{
    private readonly PlannerService _plannerService;

    public PlannerController(
        PlannerService plannerService
    )
    {
        _plannerService = plannerService;
    }

    [HttpPost]
    public IEnumerable<GapDto> FindGaps([FromBody] FindGapsModel findGapsModel)
    {
        return _plannerService.FindGaps(findGapsModel);
    }

    [HttpGet]
    public IEnumerable<MeetingDto> GetMeetings()
    {
        return _plannerService.GetMeetings();
    }

    [HttpPost]
    public void CreateMeeting([FromBody] MeetingModel meetingModel)
    {
        _plannerService.PlanMeeting(meetingModel);
    }
}
