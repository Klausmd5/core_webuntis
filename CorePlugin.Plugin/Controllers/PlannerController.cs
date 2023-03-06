using CorePlugin.Plugin.Dtos;
using CorePlugin.Plugin.Exceptions;
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

    [HttpPost("Gaps")]
    public ActionResult<IEnumerable<GapDto>> FindGaps([FromBody] FindGapsModel findGapsModel)
    {
        try
        {
            return Ok(_plannerService.FindGaps(findGapsModel));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("Meetings")]
    public ActionResult<IEnumerable<MeetingDto>> GetMeetings()
    {
        return Ok(_plannerService.GetMeetings());
    }

    [HttpPost("Meeting")]
    public ActionResult CreateMeeting([FromBody] MeetingModel meetingModel)
    {
        _plannerService.PlanMeeting(meetingModel);
        return Ok();
    }
}
