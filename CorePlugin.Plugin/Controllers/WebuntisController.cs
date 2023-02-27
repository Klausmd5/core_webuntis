using CorePlugin.Plugin.Dtos;
using CorePlugin.Plugin.Services;
using Microsoft.AspNetCore.Mvc;

namespace CorePlugin.Plugin.Controllers;

[ApiController]
[Route("[controller]")]
public class WebuntisController
{
    private readonly WebuntisService _webuntisService;

    public WebuntisController(
        WebuntisService webuntisService
    )
    {
        _webuntisService = webuntisService;
    }

    [HttpGet]
    public List<TeacherDto> GetTeachers()
    {
        return _webuntisService.GetTeachers()
            .Select(x => new TeacherDto
            {
                Id = x.Id,
                FirstName = x.FirstName ?? "",
                LastName = x.LastName ?? ""
            })
            .ToList();
    }

    [HttpGet]
    public List<StudentDto> GetStudents()
    {
        return _webuntisService.GetStudents()
            .Select(x => new StudentDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            })
            .ToList();
    }
}
