using Microsoft.AspNetCore.Mvc;
using PlaygroundSolutionOS.Application.DTOs;
using PlaygroundSolutionOS.Application.Interfaces;

namespace PlaygroundSolutionOS.Api.Controllers;


[Route("api/projects/{projectName}/commits")]
[ApiController]
public class CommitsController : ControllerBase
{
    private readonly ICommitActivityService _commitService;

    public CommitsController(ICommitActivityService commitActivityService)
    {
        _commitService = commitActivityService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommitActivityDTO>>> Get([FromRoute] string projectName)
    {
        var commits = await _commitService.GetCommits(projectName);
        if (commits == null)
        {
            return Ok(new {});
        }

        return Ok(commits);
    }
    
    [HttpGet("{id}", Name = "GetCommit")]
    public async Task<ActionResult<CommitActivityDTO>> GetById(string id)
    {
        var commit = await _commitService.GetById(id);
        if (commit == null) return NotFound("Commit not found");
        return Ok(commit);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromRoute] string projectName, [FromBody] CommitActivityDTO? commitDto)
    {
        if (commitDto == null)
        {
            return BadRequest("Invalid Data");
        }

        await _commitService.Add(projectName, commitDto);

        return new CreatedAtRouteResult(
            "GetCommit", new { projectName = projectName, id = commitDto.Id }, commitDto);
    }
}