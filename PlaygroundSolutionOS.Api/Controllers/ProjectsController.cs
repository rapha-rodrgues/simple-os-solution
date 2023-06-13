using Microsoft.AspNetCore.Mvc;
using PlaygroundSolutionOS.Application.DTOs;
using PlaygroundSolutionOS.Application.Interfaces;

namespace PlaygroundSolutionOS.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> Get()
    {
        var categories = await _projectService.GetProjects();
        if (categories == null)
        {
            return Ok(new {});
        }

        return Ok(categories);
    }
    
    [HttpGet("{name}", Name = "GetProject")]
    public async Task<ActionResult<ProjectDTO>> Get(string name)
    {
        var project = await _projectService.GetById(name);
        if (project == null) return NotFound("Project not found");
        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProjectDTO? projectDto)
    {
        if (projectDto == null)
        {
            return BadRequest("Invalid Data");
        }

        await _projectService.Add(projectDto);

        return new CreatedAtRouteResult(
            "GetProject", new { name = projectDto.Name }, projectDto);
    }
}