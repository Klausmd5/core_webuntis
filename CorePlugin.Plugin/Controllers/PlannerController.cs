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
            var gaps = _plannerService.FindGaps(findGapsModel);
            return Ok(gaps);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
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
        try
        {
            _plannerService.PlanMeeting(meetingModel);
            return Ok();
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }
}
