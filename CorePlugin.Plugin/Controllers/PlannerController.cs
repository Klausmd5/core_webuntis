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

    [HttpGet]
    public void Test(int? startToleranceMinutes, int? endToleranceMinutes, int? minDuration)
    {
        _plannerService.GetGaps(new List<int> { 1 }, new List<int> { 1, 2 }, DateTime.Now,
            DateTime.Now.AddDays(2), startToleranceMinutes, endToleranceMinutes, minDuration);
    }
}
